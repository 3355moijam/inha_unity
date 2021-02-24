using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRune
{
	//GameObject[] effect { get; set; }
	//GameObject[] instance { get; set; }
	//void CreateEffect(Vector3 position, Vector3 rotation);
	void OnButtonDown();
	void OnButton();
	void OnButtonUp();
	bool HasAnimation();

	void Init();
	void Clear();
}
