using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 10)]
    private float MoveSpeed = 3.0f;
    [SerializeField]
    private GameObject Tower = null;

    [SerializeField]
    private GameObject[] Wheel;
    [SerializeField]
    private GameObject[] FrontWheel;

    // 생성됐을 때 한 번
    private void Awake()
	{
		
	}
	// Start is called before the first frame update
    // 첫 프레임 시작 전에 한번
	void Start()
    {
        //this.transform.position = new Vector3(0.0f, 1.0f, 0.0f); // >> 월드좌표 0.0, 1.0, 0.0
        /*this.transform.Translate(new Vector3(0.0f, 5.0f, 0.0f));*/   // >> 로컬좌표 0.0, 5.0, 0.0
    }

    // Update is called once per frame
    void Update()
    {
        //Moving();
        float moveDelta = this.MoveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
		{
            Moving(Vector3.forward * moveDelta);
            foreach (var item in Wheel)
            {
                item.transform.Rotate(Vector3.up, -360 * Time.deltaTime, Space.Self);
            }
            foreach (var item in FrontWheel)
            {
                item.transform.rotation = this.transform.rotation;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            Moving(Vector3.forward * -moveDelta);
            foreach (var item in Wheel)
            {
                item.transform.Rotate(Vector3.up, 360 * Time.deltaTime, Space.Self);
            }
            foreach (var item in FrontWheel)
            {
                item.transform.rotation = this.transform.rotation;
			}
        }
        if (Input.GetKey(KeyCode.D))
        {
			this.transform.Rotate(Quaternion.AngleAxis(90 * Time.deltaTime, Vector3.up).eulerAngles);
            foreach (var item in FrontWheel)
            {
                item.transform.rotation = Quaternion.AngleAxis(30, Vector3.up) * this.transform.rotation;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
			this.transform.Rotate(Quaternion.AngleAxis(-90 * Time.deltaTime, Vector3.up).eulerAngles);
            foreach (var item in FrontWheel)
            {
                item.transform.rotation = Quaternion.AngleAxis(-30, Vector3.up) * this.transform.rotation;
            }
        }
        if (Input.GetKey(KeyCode.E) && Tower != null)
        {
            Tower.transform.Rotate(Quaternion.AngleAxis(90 * Time.deltaTime, Vector3.up).eulerAngles);
        }
        if (Input.GetKey(KeyCode.Q) && Tower != null)
        {
            Tower.transform.Rotate(Quaternion.AngleAxis(-90 * Time.deltaTime, Vector3.up).eulerAngles);
        }
    }

    private void Moving(Vector3 dir)
	{

        this.transform.Translate(dir);
	}
}
