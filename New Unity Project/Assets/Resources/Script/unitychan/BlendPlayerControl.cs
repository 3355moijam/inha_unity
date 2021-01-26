using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlendPlayerControl : MonoBehaviour//, IAction
{
    private BlendMove move = null;
    private BlendAttack attack = null;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<BlendMove>();
        attack = GetComponent<BlendAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Attacking() == true) 
            return;
        if (Moving() == true)
            return;
    }

    private bool Moving()
	{
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                move.Begin(hit.point);
                return true;
            }
        }
        return false;
    }

    private bool Attacking()
	{
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
		foreach (RaycastHit hit in hits)
		{
            Damage damage = hit.transform.GetComponent<Damage>();
            if (damage == null)
                continue;

            if(Input.GetMouseButtonDown(1))
			{
                //Debug.Log("Attack");
                attack.Begin(damage);
                return true;
			}
		}
        return false;
	}

	private Ray GetMouseRay()
	{
        return Camera.main.ScreenPointToRay(Input.mousePosition);
	}


}
