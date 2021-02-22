using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneBeam : MonoBehaviour, IRune
{
	public GameObject[] effect;
	private GameObject[] instance;

	public void CreateEffect(Vector3 position, Vector3 rotation)
	{
		
	}

	public void OnButtonDown(RaycastHit hit)
	{
		
	}

	public void OnButton(RaycastHit hit)
	{
		GameManager.Instance.player.MoveStop();
		GameManager.Instance.player.animator.SetBool("Beams", true);
		
		Transform playerTransform = GameObject.Find("Player").transform;
		Vector3 lookPosition = hit.point;
		lookPosition.y = playerTransform.position.y;
		playerTransform.LookAt(lookPosition);
	}

	public void OnButtonUp(RaycastHit hit)
	{
		GameManager.Instance.player.animator.SetBool("Beams", false);

	}

	public bool HasAnimation()
	{
		return true;
	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
