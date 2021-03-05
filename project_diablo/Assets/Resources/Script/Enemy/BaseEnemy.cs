using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemy : MonoBehaviour//, IEnemy
{
	public float MaxHP;
	protected float CurrentHP;
	public float DefencePoint { get; protected set; }
	public float AttackPoint { get; protected set; }

	protected Animator animator;
	protected NavMeshAgent agent;

	protected float tickDamageCooltime = 0.5f;
	protected bool tickable = true;

	virtual public void Hitted(float damage, bool isTickDamage = false, float tickCool = 0)
	{
		if(isTickDamage)
		{
			if(tickable)
			{
				tickable = false;
				Invoke("SetTickableTrue", tickCool == 0 ? tickDamageCooltime : tickCool);
			}
			else
			{
				return;
			}
		}
		animator.SetTrigger("Hit");
		CurrentHP -= damage;
		Debug.Log(gameObject + " Hit!");
		if (CurrentHP <= 0)
			StartCoroutine(Dead());
	}

	virtual protected IEnumerator Dead()
	{
		GetComponent<Collider>().enabled = false;
		agent.isStopped = true;
		agent.enabled = false;
		animator.SetTrigger("Dead");

		DeadProcess();

		yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

		AfterDeadProcess();

		//yield return new WaitForSeconds(10.0f);

		Destroy(gameObject, 10.0f);
	}

	void SetTickableTrue() => tickable = true;

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
