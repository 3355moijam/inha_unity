using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
	private NavMeshAgent agent = null;
    private float rotationSpeed = 20;
    // Start is called before the first frame update
    void Start()
    {
	    agent = GetComponent<NavMeshAgent>();
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
        if (!agent.isStopped)
        {
            Vector3 lookRotation = agent.steeringTarget - transform.position;
			//Debug.Log(Quaternion.LookRotation(lookrotation));
			if (Quaternion.LookRotation(lookRotation) != Quaternion.identity)
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRotation), rotationSpeed * Time.deltaTime);
        }
    }
	void SetDestination(RaycastHit hit)
	{
		
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        ;
        if (Physics.Raycast(ray, out hit))
		{
            //agent.enabled = false;
            //transform.LookAt(hit.point);
            //agent.enabled = true;
            agent.destination = hit.point;
            agent.stoppingDistance = 0;
            agent.isStopped = false;

		}

	}

    bool CheckRaycast(out RaycastHit hit)
	{
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        return Physics.Raycast(ray, out hit);
    }

}
