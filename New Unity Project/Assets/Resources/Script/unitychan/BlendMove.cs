using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlendMove : MonoBehaviour, IAction
{
    private NavMeshAgent agent = null;
    private Animator animator = null;
    private ActionManager actionManager = null;

    private float Speed = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        actionManager = GetComponent<ActionManager>();
    }
    public void Begin(object obj)
	{
        if(obj is Vector3)
		{
            actionManager.StartAction(this);
            Vector3 dest = (Vector3)obj;
            MoveTo(dest);
		}
	}

	public void MoveTo(Vector3 dest)
    {
        agent.isStopped = false;
        agent.destination = dest;

    }

    public void End()
    {
        agent.isStopped = true;
    }

    private void FixedUpdate()
    {
        Vector3 velocity = agent.velocity;
        Vector3 local = transform.InverseTransformDirection(velocity); // 월드->로컬
        Speed = local.z;
        animator.SetFloat("Speed", Speed);

    }
}
