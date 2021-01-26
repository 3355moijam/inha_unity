using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour
{
    public float speedMove = 10.0f;
    public float speedRot = 100.0f;
    // Start is called before the first frame update

    private void Awake()
    {
        print("Awake Call~!");
    }

    private void OnEnable()
    {
        print("OnEnalbe Call~!");
    }
    void Start()
    {
        print("Start Call~!");
    }

    // Update is called once per frame
    void Update()
    {
        print("Update Call~!");
    }

    private void LateUpdate()
    {
        print("LateUpdate Call~!"); // Update 이후에 실행. Update 후에 처리하고 싶은 게 있을 때 여기에 넣는다.
    }

    private void FixedUpdate()
    {
        print("FixedUpdate Call~!"); // 항상 고정된 시간에 실행. 0.02초마다.
    }

    private void OnDisable()
    {
        print("OnDisable Call~!");
    }

    private void OnDestroy()
    {
        print("OnDestroy Call~!");
    }

    private void OnBecameVisible()
    {
        print("OnBecameVisible Call~!");
    }

    private void OnBecameInvisible()
    {
        print("OnBecameInvisible Call~!");
    }
}
