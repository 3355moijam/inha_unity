using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FunctionLibrary;

public class BeamsOnState : StateMachineBehaviour
{
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Debug.Log("Beams Enter");
		// beam instantiate
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		// mouse tracking
		Functions.LookAtHitPoint(animator.transform);
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Debug.Log("Beams Exit");
		// beam destroy
		
	}



}
