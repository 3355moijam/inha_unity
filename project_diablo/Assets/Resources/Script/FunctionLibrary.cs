using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FunctionLibrary
{
    public class Functions
    {
		static public void LookAtHitPoint(Transform transform)
		{
			Vector3 lookPosition = GameManager.Instance.mouseHit.point;
			lookPosition.y = transform.position.y;
			transform.LookAt(lookPosition);
		}

		static public bool CheckRaycast(out RaycastHit hit)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			return Physics.Raycast(ray, out hit);
		}
	}
}