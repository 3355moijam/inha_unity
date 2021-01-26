using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Resources.Script;
public class EnemyBullet2D : MonoBehaviour, IEnemy
{
    private float maxSpeed = 2000;
    private new Rigidbody2D rigidbody = null;
    public int Score { get; } = 5;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveLeft();

        if (transform.position.x < -Screen.width * 0.8f)
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
}

