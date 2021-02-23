using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FunctionLibrary;
public class BeamsStartState : StateMachineBehaviour
{
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Functions.LookAtHitPoint(animator.transform);
	}
}
