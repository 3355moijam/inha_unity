using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamEffect : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject beamStart;
    public GameObject beam;
    public GameObject beamEnd;

    private LineRenderer line;

    [Header("Adjustable Variables")]
    public float beamEndOffset = 1f; //How far from the raycast hit point the end effect is positioned
    public float textureScrollSpeed = 8f; //How fast the texture scrolls along the beam
    public float textureLengthScale = 1f; //Length of the beam texture
    public float beamRange = 10f;
    float beamRadius = 0.1f;
    public bool isPierce = false;
    // Start is called before the first frame update
    delegate void SkillType();
    SkillType BeamType;

    public float DamageMultiplier = 1;

    void Start()
    {
        line = beam.GetComponent<LineRenderer>();
        if (isPierce)
            BeamType = new SkillType(ShootPierceBeamToMouse);
        else
            BeamType = new SkillType(ShootBeamToMouse);
    }

    // Update is called once per frame
    void Update()
    {
        BeamType();
    }

	private void LateUpdate()
	{
        
	}

    void ShootBeamToMouse()
    {
        Vector3 start = transform.position;
        line.positionCount = 2;
        line.SetPosition(0, start);
        Vector3 dir = GameManager.Instance.mouseHit.point - start;
        dir = GameManager.Instance.player.transform.forward;
        dir.y = 0;
        dir.Normalize();
        Vector3 end = Vector3.zero;
        RaycastHit hit;
        
        if (Physics.SphereCast(start, beamRadius, dir, out hit, beamRange))
            end = hit.point - (dir * beamEndOffset);
        else
            end = start + (dir * beamRange);

        beamEnd.transform.position = end;
        line.SetPosition(1, end);

        beamStart.transform.LookAt(end);
        beamEnd.transform.LookAt(start);

        float distance = Vector3.Distance(start, end);
        line.sharedMaterial.mainTextureScale = new Vector2(distance / textureLengthScale, 1);
        line.sharedMaterial.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);

        if(hit.transform != null && hit.transform.tag == "Enemy")
		{
            DamageToTarget(hit.transform.GetComponent<BaseEnemy>(), true);
        }
    }

    void ShootPierceBeamToMouse()
    {
        Vector3 start = transform.position;
        line.positionCount = 2;
        line.SetPosition(0, start);

        Vector3 dir = GameManager.Instance.mouseHit.point - start;
        dir.y = 0;
        dir.Normalize();

        Vector3 end = start + (dir * beamRange);
        
        RaycastHit[] hits = Physics.SphereCastAll(start, beamRadius, dir, beamRange);
        Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));
        
        foreach (RaycastHit target in hits)
        {
            // 플레이어는 무시, 몬스터는 관통, 기타 오브젝트는 break
            if (target.transform.CompareTag("Player"))
                continue;
            else if (target.transform.CompareTag("Enemy"))
            {
                
                DamageToTarget(target.transform.GetComponent<BaseEnemy>(), true);
            }
            else
            {
                end = target.point - (dir * beamEndOffset);
                break;
            }
        }

        //if (Physics.Raycast(start, dir, out hit, beamRange))
        //end = hit.point - (dir * beamEndOffset);
        //else
        //end = start + (dir * beamRange);

        beamEnd.transform.position = end;
        line.SetPosition(1, end);

        beamStart.transform.LookAt(end);
        beamEnd.transform.LookAt(start);

        float distance = Vector3.Distance(start, end);
        line.sharedMaterial.mainTextureScale = new Vector2(distance / textureLengthScale, 1);
        line.sharedMaterial.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);
    }

    void DamageToTarget(BaseEnemy target, bool isTickDamage = false)
	{
        float damage = AttackMinusDef(target) * DamageMultiplier;
        target.Hitted(damage, isTickDamage);
	}

    float AttackMinusDef(BaseEnemy target)
    {
        float ret = (GameManager.Instance.player.AttackPoint - target.DefencePoint);
        ret = ret < 0 ? 0 : ret;
        return ret;
    }

	private void OnDestroy()
	{
		
	}

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.cyan;
        Vector3 target, dir;
        dir = GameManager.Instance.mouseHit.point - transform.position;
        dir.y = transform.position.y;
        dir.Normalize();
        target = transform.position + dir * beamRange;
        //Gizmos.DrawLine(transform.position, target);

        if (line != null)
        {
            Gizmos.DrawLine(line.GetPosition(0), line.GetPosition(1));
            Gizmos.DrawWireSphere((line.GetPosition(0) + line.GetPosition(1)) * 0.5f, beamRadius);
            Gizmos.DrawWireSphere(line.GetPosition(1), beamRadius);
        }
	}
}
