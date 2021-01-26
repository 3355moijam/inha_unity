using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AutoCarAI : MonoBehaviour
{
    // Start is called before the first frame update
    private RaycastHit[] rayHits = new RaycastHit[3];
    private Ray[] rays = new Ray[3];
    private float distance = 7;
    private float speed = 0;
    private float baseSpeed = 5;
    private float rotateSpeed = Mathf.PI * 0.5f;
    private bool stop = false;
    private float stopwatch = 0;
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if(stop == false)
		{
            stopwatch += Time.deltaTime;
            if (stopwatch > 1)
			{
                stop = true;
			}
            return;
		}
        float moveDelta = speed * Time.deltaTime;
        CheckRaycast();
        this.transform.Translate(Vector3.forward * moveDelta);

        {//if (Input.GetKey(KeyCode.W))
         //{
         //    Moving(Vector3.forward * moveDelta);
         //    //foreach (var item in Wheel)
         //    //{
         //    //    item.transform.Rotate(Vector3.up, -360 * Time.deltaTime, Space.Self);
         //    //}
         //    //foreach (var item in FrontWheel)
         //    //{
         //    //    item.transform.rotation = this.transform.rotation;
         //    //}
         //}
         //if (Input.GetKey(KeyCode.S))
         //{
         //    Moving(Vector3.forward * -moveDelta);
         //    //foreach (var item in Wheel)
         //    //{
         //    //    item.transform.Rotate(Vector3.up, 360 * Time.deltaTime, Space.Self);
         //    //}
         //    //foreach (var item in FrontWheel)
         //    //{
         //    //    item.transform.rotation = this.transform.rotation;
         //    //}
         //}
         //if (Input.GetKey(KeyCode.D))
         //{
         //    this.transform.Rotate(Quaternion.AngleAxis(90 * Time.deltaTime, Vector3.up).eulerAngles);
         //    //foreach (var item in FrontWheel)
         //    //{
         //    //    item.transform.rotation = Quaternion.AngleAxis(30, Vector3.up) * this.transform.rotation;
         //    //}
         //}
         //if (Input.GetKey(KeyCode.A))
         //{
         //    this.transform.Rotate(Quaternion.AngleAxis(-90 * Time.deltaTime, Vector3.up).eulerAngles);
         //    //foreach (var item in FrontWheel)
         //    //{
         //    //    item.transform.rotation = Quaternion.AngleAxis(-30, Vector3.up) * this.transform.rotation;
         //    //}
         //}
        }
        SetRay();

    }

    private void OnDrawGizmos()
    {
        //foreach (var ray in rays)
        //{
        //          Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
        //}
        Debug.DrawRay(rays[0].origin, rays[0].direction * distance * 0.7f, Color.red);
        Debug.DrawRay(rays[1].origin, rays[1].direction * distance, Color.blue);
        Debug.DrawRay(rays[2].origin, rays[2].direction * distance, Color.green);
    }

    private void Moving(Vector3 dir)
    {

        this.transform.Translate(dir);
    }

    private void SetRay()
	{
        Vector3 pos = transform.position;
        pos -= transform.forward * 0.5f;
        pos.y += 0.3f;
        //pos.z -= 1;
        rays[0].origin = pos;
        rays[0].direction = transform.forward;

        rays[1].origin = pos;
        rays[1].direction = Quaternion.AngleAxis(30, Vector3.up) * transform.forward;

        rays[2].origin = pos;
        rays[2].direction = Quaternion.AngleAxis(-30, Vector3.up) * transform.forward;
    }

    private void CheckRaycast()
	{
        float rotDelta = rotateSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.zero;
        bool _b1 = Physics.Raycast(rays[1], out rayHits[1], distance);
        bool _b2 = Physics.Raycast(rays[2], out rayHits[2], distance);

        if (Physics.Raycast(rays[0], out rayHits[0], distance * 0.7f) && rayHits[0].collider.gameObject.tag == "Wall")
		{
			Debug.Log("ray0 hits");
            Vector3 normal= Vector3.zero;
            if (_b2)
            {
                normal = Quaternion.AngleAxis(-90, Vector3.up) * rayHits[0].normal;
            }
            else if (_b1)
			{
			    normal = Quaternion.AngleAxis(90, Vector3.up) * rayHits[0].normal;
			}
			normal.y = 0;
            normal.Normalize();
			newDir = Vector3.RotateTowards(transform.forward, normal, rotDelta, 0.0f);
			transform.rotation = Quaternion.LookRotation(newDir);
		}
		else
		{

            
            if (_b1 && _b2)
            {
				Debug.Log("ray1&2 hits " + rayHits[1].collider.gameObject.name + "And " + rayHits[2].collider.gameObject.name);

				if (Mathf.Abs(rayHits[1].distance - rayHits[2].distance) < 0.3f) 
				{
                    {
                        Vector3 normal = rayHits[1].normal + transform.forward;
                        normal.y = 0;
                        normal.Normalize();
                        newDir = Vector3.RotateTowards(transform.forward, normal, rotDelta, 0.0f);
                        transform.rotation = Quaternion.LookRotation(newDir);
                    }
                    {
                        Vector3 normal = rayHits[2].normal + transform.forward;
                        normal.y = 0;
                        normal.Normalize();
                        newDir = Vector3.RotateTowards(transform.forward, normal, rotDelta, 0.0f);
                        transform.rotation = Quaternion.LookRotation(newDir);
                    }
                }
                else if(rayHits[1].distance < rayHits[2].distance)
				{
                    Vector3 normal = rayHits[1].normal + transform.forward;
                    normal.y = 0;
                    normal.Normalize();
                    newDir = Vector3.RotateTowards(transform.forward, normal, rotDelta, 0.0f);
                    transform.rotation = Quaternion.LookRotation(newDir);
                }
                else
				{
                    Debug.Log("ray2 hits");
                    Vector3 normal = rayHits[2].normal + transform.forward;
                    normal.y = 0;
                    normal.Normalize();
                    newDir = Vector3.RotateTowards(transform.forward, normal, rotDelta, 0.0f);
                    transform.rotation = Quaternion.LookRotation(newDir);
                }
            }
            else if(_b1)
            {
                Debug.Log("ray1 hits");
                Vector3 normal = rayHits[1].normal + transform.forward;
                normal.y = 0;
                normal.Normalize();
                newDir = Vector3.RotateTowards(transform.forward, normal, rotDelta, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDir);
            }
            else if (_b2)
            {
                Debug.Log("ray2 hits");
                Vector3 normal = rayHits[2].normal + transform.forward;
                normal.y = 0;
                normal.Normalize();
                newDir = Vector3.RotateTowards(transform.forward, normal, rotDelta, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDir);
            }
            else
			{
                Debug.Log("no hits");
            }
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Plane")
        {
            speed = baseSpeed;
            return; 
        }

		
	}

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Plane")
            return;

        speed = baseSpeed * 0.5f;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Plane")
        {
            speed = 0;
            return; 
        }

		speed = baseSpeed;
	}

    private void OnTriggerEnter(Collider other)
    {
        speed = 0;
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

    }

	private void OnGUI()
	{
		//if(GUI.Button(new Rect(10, 240, 50, 30), "시작버튼"))
		//{
  //          ChangeScene();
		//}
	}

	private void ChangeScene()
	{
        SceneManager.LoadScene("01Basic");
	}
}
