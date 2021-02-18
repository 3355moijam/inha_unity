using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using DG.Tweening;
public class Player : MonoBehaviour
{
	private NavMeshAgent agent = null;
	private Animator animator = null;
    private Vector3 nextNode;
    // Start is called before the first frame update
    void Start()
    {
	    agent = GetComponent<NavMeshAgent>();
        //agent.isStopped = true;
        agent.updateRotation = false;
	    animator = GetComponent<Animator>();
	    //agent.angularSpeed = 999;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
		{
            RaycastHit hit;
            if (CheckRaycast(out hit))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                { 
                    Debug.Log(hit.transform.tag);
                    if(hit.transform.tag == "Enemy")
					{

					}
					else
					{
                        SetDestination(hit);
					}
				}
			}
		}
        if(nextNode != agent.steeringTarget)
		{
            RotateToMoveDirection();
        }
        Debug.Log(agent.isStopped);
        Debug.Log(agent.steeringTarget);
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
            //agent.enabled = false;
            //transform.LookAt(hit.point);
            //agent.enabled = true;
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

    public void setZero()
    {
        agent.speed = 0.0f;
        agent.updatePosition = false;
        agent.updateRotation = false;
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
        agent.destination = agent.transform.position;
        agent.ResetPath();
        // agent.acceleration = float.MaxValue;
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

}
