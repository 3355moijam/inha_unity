using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using DG.Tweening;
public class Player : MonoBehaviour
{
	NavMeshAgent sAgent = null;
    [HideInInspector]
    public NavMeshAgent agent { get => sAgent; }
	Animator sAnimator = null;
    [HideInInspector]
    public Animator animator { get => sAnimator; }


    Vector3 nextNode;

    //skill
    ISkill[] mainSkills;
    ISkill selectedMainSkill;
    
    delegate void PlayerAction(RaycastHit hit);
    List<PlayerAction> Actions;
    PlayerAction setDest;
    PlayerAction useMainSkill;
    // Start is called before the first frame update
    void Start()
    {
	    sAgent = GetComponent<NavMeshAgent>();
        sAgent.updateRotation = false;
	    sAnimator = GetComponent<Animator>();
        
        mainSkills = new ISkill[3];
        mainSkills[0] = transform.Find("SkillManager/MainSkill").GetComponent<Beams>();
        SetMainSkill(0);

        // 스킬 액션 순서처리
        Actions = new List<PlayerAction>();
        setDest = new PlayerAction(SetDestination);
		useMainSkill = new PlayerAction(selectedMainSkill.OnButton);

		GameManager.Instance.player = this;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        bool isHit = CheckRaycast(out hit);
        if (Input.GetMouseButton(0))
		{
            if (isHit && !EventSystem.current.IsPointerOverGameObject())
            { 
                Debug.Log(hit.transform.tag);
                if(hit.transform.tag == "Enemy")
				{

				}
				else
				{
                    //Actions.Add(setDest);
                    SetDestination(hit);
				}
			}
		}
        
        UseMainSkill(hit);

        if (nextNode != sAgent.steeringTarget)
		{
            RotateToMoveDirection();
        }

    }
    
    void LateUpdate()
    {
	    sAnimator.SetFloat("Speed", sAgent.velocity.magnitude);
    }
	void SetDestination(RaycastHit hit)
	{
		
        sAgent.destination = hit.point;
        sAgent.stoppingDistance = 1.0f;
        sAgent.isStopped = false;
		RotateToMoveDirection();
        sAnimator.SetTrigger("Run");

	}

    bool CheckRaycast(out RaycastHit hit)
	{
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        return Physics.Raycast(ray, out hit);
    }

    void RotateToMoveDirection()
	{
        nextNode = sAgent.steeringTarget;
        Vector3 lookRotation = nextNode - transform.position;
        transform.DORotateQuaternion(Quaternion.LookRotation(lookRotation), 0.3f);
    }

    public void SetMainSkill(int num)
	{
        selectedMainSkill = mainSkills[num % mainSkills.Length];
	}

    public void MoveStop()
	{
        sAgent.isStopped = true;
        sAgent.ResetPath();
    }

    void UseMainSkill(RaycastHit hit)
	{
        if(Input.GetMouseButtonDown(1))
		{
            selectedMainSkill.OnButtonDown(hit);
		}
        if (Input.GetMouseButton(1))
        {
            selectedMainSkill.OnButton(hit);
        }
        if (Input.GetMouseButtonUp(1))
		{
            selectedMainSkill.OnButtonUp(hit);
        }
    }

    private void OnDrawGizmos()
    {
        //if (agent.path.corners.Length == 0)
        //	return;
        if (sAgent == null)
            return;
        for (int i = 0; i < sAgent.path.corners.Length; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(sAgent.path.corners[i], 0.3f);

            if (i > 0)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(sAgent.path.corners[i - 1], sAgent.path.corners[i]);
            }
        }
    }

}
