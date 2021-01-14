using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2D : MonoBehaviour
{
    private new Rigidbody2D rigidbody = null;
    private float maxSpeed = 1500;
    private Spawner2D shooter = null;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
        // shooter = new Spawner2D("총알위치");
        shooter.Spawn(transform.position, transform.position, 1.6f);
    }

    // Update is called once per frame
    void Update()
    {
        MoveLeft();

        if(transform.position.x < 0)
		{
            Destroy(this.gameObject);
		}            
    }

    void MoveLeft()
	{
        Vector2 position = rigidbody.transform.position;
        position.x += maxSpeed * Time.deltaTime;

        rigidbody.MovePosition(position);
    }
}

