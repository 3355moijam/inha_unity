using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode : MonoBehaviour
{
    bool a = false;
    bool b = false;
    bool test1;
    bool test2 => a || b;
    // Start is called before the first frame update
    void Start()
    {
        test1 = a || b;
        a = true;

        Debug.Log("test1 : " + test1.ToString());
        Debug.Log("test2 : " + test2.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
