using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Resources.Script;

public class PlayerBullet2D : MonoBehaviour
{
    private float maxSpeed = 2000;
    private new Rigidbody2D rigidbody = null;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveRight();

        if (transform.position.x > Screen.width * 0.8f)
        {
            Destroy(this.gameObject);
        }
    }

    void MoveRight()
    {
        Vector2 position = rigidbody.transform.position;
        position.x += maxSpeed * Time.deltaTime;

        rigidbody.MovePosition(position);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        //return;
		if(collision.tag == "Enemy")
		{
            // 점수 ++
            // 스코어 인터페이스를 상속시키기

            IEnemy target = collision.gameObject.GetComponent<IEnemy>();
            GameManager.Instance.Score += target.Score;
            Debug.Log("Current Score: " + GameManager.Instance.Score);
			// 이펙트 생성
			// 적 파괴
			Destroy(collision.gameObject);
            // 자신 파괴
            Destroy(this.gameObject);

		}
	}
}
