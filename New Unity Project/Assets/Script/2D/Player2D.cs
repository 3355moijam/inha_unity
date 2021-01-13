using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : MonoBehaviour
{
	[SerializeField]
	private float maxSpeed = 200;

	private new Rigidbody2D rigidbody = null;

	// Start is called before the first frame update
	void Start()
    {
		rigidbody = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Move_2(x, y);
    }
    void Move_0(float x, float y)
	{
        transform.Translate(x * maxSpeed * Time.deltaTime, y * maxSpeed * Time.deltaTime, 0);
	}
    void Move_1(float x, float y)
	{
        rigidbody.AddForce(new Vector2(x * maxSpeed * Time.deltaTime, y * maxSpeed * Time.deltaTime));
	}

    void Move_2(float x, float y)
	{
		Vector2 position = rigidbody.transform.position;
		position.x += x * maxSpeed * Time.deltaTime;
		position.y += y * maxSpeed * Time.deltaTime;
		
		rigidbody.MovePosition(position);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("<color=red>충돌! </color>" + other.name);
		Destroy(other); // 지연시간을 주면 사운드를 플레이하고 사라지는 등의 연출이 가능

	}

}
