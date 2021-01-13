using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    public float speedMove = 0.4f;
    public float speedRot = 30.0f;
    // Start is called before the first frame update

    //Rigidbody rigidbody = null;
    void Start()
    {
        //rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        
    }

    private void FixedUpdate()
    {
        Move_Rotate();
    }

    private void Move_Rotate()
	{
        float rot = Input.GetAxis("Horizontal");
        float move = Input.GetAxis("Vertical");
        
        // fixed update에서 사용하므로 deltatime 필요 없음

        rot = rot * speedRot;
        move = move * speedMove;

        this.gameObject.transform.Rotate(Vector3.up * rot);
        this.gameObject.transform.Translate(Vector3.forward * move);
    }

	private void OnCollisionEnter(Collision collision)
	{
		
	}

	private void OnCollisionStay(Collision collision)
	{
		
	}

	private void OnCollisionExit(Collision collision)
	{
		
	}

	private void OnTriggerEnter(Collider other)
	{
		
	}

	private void OnTriggerStay(Collider other)
	{
		
	}

	private void OnTriggerExit(Collider other)
	{
		
	}
}
