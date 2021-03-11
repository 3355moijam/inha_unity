using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FunctionLibrary;
public class BeamsStartState : StateMachineBehaviour
{
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (GameManager.Instance.player.CurrentMP < animator.GetFloat("ManaCost"))
		{
			animator.SetBool("EnoughMana", false);
		}
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Functions.LookAtHitPoint(animator.transform);
	}
}
