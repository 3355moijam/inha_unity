using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		Invoke("Dead", 10.0f);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    void Dead()
	{
        Destroy(this.gameObject);
	}
}
