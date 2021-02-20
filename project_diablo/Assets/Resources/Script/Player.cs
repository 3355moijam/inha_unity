using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using DG.Tweening;
public class Player : MonoBehaviour
{
	NavMeshAgent agent = null;
	Animator animator = null;
    Vector3 nextNode;
    ISkill[] mainSkills;
    ISkill selectedMainSkill;
    
    delegate void PlayerAction(RaycastHit hit);
    List<PlayerAction> Actions;
    PlayerAction setDest;
    PlayerAction useMainSkill;
    // Start is called before the first frame update
    void Start()
    {
	    agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
	    animator = GetComponent<Animator>();
        
        mainSkills = new ISkill[3];
        mainSkills[0] = transform.Find("SkillManager/MainSkill").GetComponent<Beams>();
        SetMainSkill(0);
        Actions = new List<PlayerAction>();
        setDest = new PlayerAction(SetDestination);
        //useMainSkill = new PlayerAction();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        CheckRaycast(out hit);
        if (Input.GetMouseButton(0))
		{
            if (!EventSystem.current.IsPointerOverGameObject())
            { 
                Debug.Log(hit.transform.tag);
                if(hit.transform.tag == "Enemy")
				{

				}
				else
				{
                    Actions.Add(setDest);
				}
			}
		}
        
        UseMainSkill();

        if(!(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Run")))
		{
            agent.isStopped = true;
            agent.ResetPath();
		}

        if (nextNode != agent.steeringTarget)
		{
            RotateToMoveDirection();
        }

    }
    
    void LateUpdate()
    {
	    animator.SetFloat("Speed", agent.velocity.magnitude);
    }
	void SetDestination(RaycastHit hit)
	{
		
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
		{
            agent.destination = hit.point;
            agent.stoppingDistance = 1.0f;
            agent.isStopped = false;
			RotateToMoveDirection();
		}

	}

    bool CheckRaycast(out RaycastHit hit)
	{
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        return Physics.Raycast(ray, out hit);
    }

    void RotateToMoveDirection()
	{
        nextNode = agent.steeringTarget;
        Vector3 lookRotation = nextNode - transform.position;
        transform.DORotateQuaternion(Quaternion.LookRotation(lookRotation), 0.3f);
    }

    private void OnDrawGizmos()
    {
        //if (agent.path.corners.Length == 0)
        //	return;
        if (agent == null)
            return;
        for (int i = 0; i < agent.path.corners.Length; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(agent.path.corners[i], 0.3f);

            if (i > 0)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(agent.path.corners[i - 1], agent.path.corners[i]);
            }
        }
    }

    public void SetMainSkill(int num)
	{
        selectedMainSkill = mainSkills[num % mainSkills.Length];
	}

    void UseMainSkill()
	{
        if(Input.GetMouseButtonDown(1))
		{
            selectedMainSkill.OnButtonDown(animator, Vector3.zero, Vector3.zero);
		}
        if (Input.GetMouseButton(1))
        {
            selectedMainSkill.OnButton(animator);
        }
        if (Input.GetMouseButtonUp(1))
		{
            selectedMainSkill.OnButtonUp(animator);
        }
    }

}
