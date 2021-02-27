using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : BaseEnemy
{

	// Start is called before the first frame update
	void Start()
    {
		base.Init();
		HP = 100;
		DefencePoint = 20;
		AttackPoint = 30;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	protected override void DeadProcess()
	{
		
	}

	protected override void AfterDeadProcess()
	{
		
	}

	void EnableCollider()
	{
		GetComponent<Collider>().enabled = true;
		Debug.Log("Collider On");
	}
}
