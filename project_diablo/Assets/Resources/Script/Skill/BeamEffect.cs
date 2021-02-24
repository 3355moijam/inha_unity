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

    // Start is called before the first frame update
    void Start()
    {
        line = beam.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ShootBeamToMouse();
    }

    void ShootBeamToMouse()
    {
        Vector3 start = transform.position;
        line.positionCount = 2;
        line.SetPosition(0, start);
        //beamStart.transform.position = start;
        Vector3 dir = GameManager.Instance.mouseHit.point - start;
        dir.y = 0;
        dir.Normalize();

        Vector3 end = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(start, dir, out hit, beamRange))
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
        Gizmos.DrawLine(line.GetPosition(0), line.GetPosition(1));
	}
}
