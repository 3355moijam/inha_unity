using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IRune
{
	//GameObject[] effect { get; set; }
	//GameObject[] instance { get; set; }
	void CreateEffect(Vector3 position, Vector3 rotation);
	void OnButtonDown(Animator animator, Vector3 position, Vector3 rotation);
	void OnButton(Animator animator);
	void OnButtonUp(Animator animator);

	
}
