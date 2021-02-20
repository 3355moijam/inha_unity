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

	public void OnButtonDown(Animator animator, Vector3 position, Vector3 rotation)
	{
		
	}

	public void OnButton(Animator animator)
	{
		animator.SetBool("Beams", true);
		
		Transform playerTransform = GameObject.Find("Player").transform;
		RaycastHit hit;
		Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
		Vector3 lookPosition = hit.point;
		lookPosition.y = playerTransform.position.y;
		playerTransform.LookAt(lookPosition);
	}

	public void OnButtonUp(Animator animator)
	{
		animator.SetBool("Beams", false);

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
