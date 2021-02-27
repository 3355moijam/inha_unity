using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemy : MonoBehaviour//, IEnemy
{
	protected float HP;
	public float DefencePoint { get; protected set; }
	public float AttackPoint { get; protected set; }

	protected Animator animator;
	protected NavMeshAgent agent;

	virtual public void Hitted(float damage)
	{
		animator.SetTrigger("Hit");
		HP -= damage;
		Debug.Log(gameObject + " Hit!");
		if (HP <= 0)
			StartCoroutine(Dead());
	}

	virtual protected IEnumerator Dead()
	{
		GetComponent<Collider>().enabled = false;
		agent.enabled = false;
		agent.isStopped = true;
		animator.SetTrigger("Dead");

		DeadProcess();

		yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

		AfterDeadProcess();

		yield return new WaitForSeconds(10.0f);

		Destroy(gameObject);
	}

	/// <summary>
	///	HP, 방어력, 공격력은 별도 처리 필요
	/// </summary>
	virtual protected void Init()
	{
		animator = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		//agent.isStopped = true;
		gameObject.tag = "Enemy";
	}

	/// <summary>
	/// 사망 직후 경험치 전달, 사망효과 등을 처리하기 위한 함수
	/// </summary>
	abstract protected void DeadProcess();

	/// <summary>
	/// 사망 애니메이션이 끝난 후 처리할 함수
	/// </summary>
	abstract protected void AfterDeadProcess();
}
