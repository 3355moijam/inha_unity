using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IEnemy
{
	protected float HP;
	public float defence { get; set; }
	public float attack { get; set; }

	protected Animator animator;

	virtual public void Hitted(float damage)
	{
		animator.SetTrigger("Hit");
		HP -= damage;
		if (HP < 0)
			StartCoroutine(Dead());
	}

	virtual protected IEnumerator Dead()
	{
		GetComponent<Collider>().enabled = false;
		animator.SetTrigger("Dead");
		DeadProcess();
		yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

		yield return new WaitForSeconds(10.0f);
		Destroy(gameObject);
	}

	virtual protected void Init()
	{
		animator = GetComponent<Animator>();
	}

	// 사망 후 경험치 전달, 사망효과 등을 처리하기 위한 함수
	abstract protected void DeadProcess();
}
