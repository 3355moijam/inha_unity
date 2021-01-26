using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Resources.Script;

public class Enemy2D : MonoBehaviour, IEnemy
{
    private new Rigidbody2D rigidbody = null;
    private float maxSpeed = 500;
    private Spawner2D shooter = null;
    public int Score { get; } = 10;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
		shooter = this.GetComponent<Spawner2D>();
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        MoveLeft();

        if(transform.position.x < -Screen.width * 0.8f)
		{
            Destroy(this.gameObject);
		}            
    }

    void MoveLeft()
	{
        Vector2 position = rigidbody.transform.position;
        position.x -= maxSpeed * Time.deltaTime;

        rigidbody.MovePosition(position);
    }

    IEnumerator Shoot()
	{
		while (true)
		{
            shooter.Spawn(transform.position.x, transform.position.y);
            yield return new WaitForSeconds(1.6f);
		}
	}
}

