using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendAttack : MonoBehaviour, IAction
{
	[SerializeField, Range(1.0f, 3.0f)]
	private float range = 2.0f;
	private BlendMove move = null;
	private ActionManager actionManager = null;
	private Transform target = null;
	private Animator animator = null;
	private Damage damage = null;
	// Start is called before the first frame update
	private void Awake()
	{
		move = GetComponent<BlendMove>();
		actionManager = GetComponent<ActionManager>();
		animator = GetComponent<Animator>();
	}
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (target == null)
			return;
		if(IsInRange() == true)
		{
			Debug.Log("Attack");
			move.End();
		}
		else
		{
			Debug.Log("None");
			move.MoveTo(target.position);
		}
    }

	private bool IsInRange()
	{
		Vector2 other = new Vector2(target.position.x, target.position.z);
		Vector2 my = new Vector2(this.transform.position.x, this.transform.position.z);
		
		return Vector2.Distance(other, my) < range;
	}

	public void Begin(object obj)
	{
		Damage _target = obj as Damage;
		Debug.Assert(_target != null, "Damage 형 필요");
		//Debug.Log(animator.GetCurrentAnimatorStateInfo(0).)
		if (animator.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash("Base Layer.Kick"))
		{
			Debug.Log("Cannot Now");
			return; 
		}
		actionManager.StartAction(this);
		this.target = _target.transform;

		animator.SetTrigger("Kick");
		damage = target.GetComponent<Damage>();
	}

	public void End()
	{
		target = null;
	}

	public void Hit()
	{
		Debug.Log("Hit");
		if (damage != null)
		{
			damage.Damaged();
			damage = null;
		}
		End();
	}
}
