using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float speed = 10;
    // Start is called before the first frame update
    void Awake()
    {
        transform.GetChild(0).Translate(0, Random.Range(-1.0f, 1.0f), 0);
        transform.GetChild(1).Translate(0, Random.Range(-1.0f, 1.0f), 0);
        Destroy(this.gameObject, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
    }

	private void OnBecameInvisible()
	{
        Debug.Log("invisible");
        
	}
}
