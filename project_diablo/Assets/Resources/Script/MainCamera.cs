using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject target = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	void LateUpdate()
	{
        if (target != null) 
		{
            Vector3 position = target.transform.position;
            position.x += 5.7f;
            position.y += 9.0f;
            position.z += -3.0f;
            transform.position = position;
		}
	}
}
