using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePropeller : MonoBehaviour
{
    private float rotSpeed = 180.0f;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate_3();
    }

    private void Rotate_1()
    {
		this.transform.eulerAngles = new Vector3(0.0f, 45.0f, 0);
	}

    private void Rotate_2()
	{
        Quaternion target = Quaternion.Euler(0.0f, 45.0f, 0.0f);
        this.transform.rotation = target;
    }

    private void Rotate_3()
	{
        transform.Rotate(Quaternion.AngleAxis(rotSpeed * Time.deltaTime, Vector3.up).eulerAngles);
	}

    private void Rotate_4()
    {
        this.transform.rotation *= Quaternion.AngleAxis(10, Vector3.forward);
    }

    private void Rotate_5()
	{
        float rot = rotSpeed * Time.deltaTime;
        this.transform.RotateAround(Vector3.zero, Vector3.up, rot);
    }

    public GameObject target = null;
    private void Look_At_1()
	{
        if (target != null)
        {
            Vector3 dirToTarget = target.transform.position - this.transform.position;
            this.transform.forward = dirToTarget.normalized;
        }
	}

    private void Look_At_2()
	{
        if (target != null)
        {
            Vector3 dirToTarget = target.transform.position - this.transform.position;

            this.transform.rotation = Quaternion.LookRotation(dirToTarget, Vector3.up);
        }
    }
    private void Rot_6()
    {
        float y = Input.GetAxis("Horizontal");//a,d 좌우 키 해당하는값 
        y = y * rotSpeed * Time.deltaTime;
        this.gameObject.transform.Rotate(new Vector3(0, y, 0));
    }

}
