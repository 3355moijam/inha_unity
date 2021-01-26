using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Animation spartanKing;
    public AnimationClip DIE;
    CharacterController pcControl;
    private float runSpeed = 10.0f;
    private static float rotSpeed = 360f;
    Vector3 velocity;
    public GameObject objSword = null;
    // Start is called before the first frame update
    void Start()
    {
        spartanKing = gameObject.GetComponentInChildren<Animation>();
        spartanKing.wrapMode = WrapMode.Loop;
        pcControl = gameObject.GetComponent<CharacterController>();

        objSword.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		Animation_Play_1();
		//CharacterControl();
		CharacterControl_Slerp();
    }

	private void CharacterControl_Slerp()
	{
        if (spartanKing.IsPlaying("attack"))
            return;

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if(direction.sqrMagnitude > 0.01f)
		{
            spartanKing.CrossFade("run", 0.3f);
            Vector3 forward = Vector3.Slerp(transform.forward, direction, rotSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction));
            transform.LookAt(transform.position + forward);
		}
        else
		{
            spartanKing.CrossFade("idle", 0.3f);
		}

        pcControl.Move(direction * runSpeed * Time.deltaTime + Physics.gravity);
	}

	private void CharacterControl()
	{
        velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        velocity *= runSpeed;
        if(velocity.magnitude > 0.5f)
		{
            spartanKing.CrossFade("run", 0.3f);
            spartanKing.wrapMode = WrapMode.Loop;
            transform.LookAt(transform.position + velocity);
		}
		else
		{
            spartanKing.CrossFade("idle", 0.3f);

		}

        pcControl.Move(velocity * Time.deltaTime);
	}

	private void Animation_Play_1()
	{
        if (Input.GetKeyDown(KeyCode.F))
		{
            StartCoroutine(AttackToIdle());
		}
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            spartanKing.wrapMode = WrapMode.Loop;
            spartanKing.CrossFade("idle", 0.3f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            spartanKing.wrapMode = WrapMode.Loop;
            spartanKing.CrossFade("walk", 0.3f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            spartanKing.wrapMode = WrapMode.Loop;
            spartanKing.CrossFade("run", 0.3f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            spartanKing.wrapMode = WrapMode.Loop;
            spartanKing.CrossFade("charge", 0.3f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            spartanKing.wrapMode = WrapMode.Loop;
            spartanKing.CrossFade("idlebattle", 0.3f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            spartanKing.wrapMode = WrapMode.Once;
            spartanKing.CrossFade("resist", 0.3f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            spartanKing.wrapMode = WrapMode.Loop;
            spartanKing.CrossFade("victory", 0.3f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            spartanKing.wrapMode = WrapMode.Loop;
            spartanKing.CrossFade("salute", 0.3f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            spartanKing.wrapMode = WrapMode.Once;
            spartanKing.CrossFade(DIE.name, 0.3f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            spartanKing.wrapMode = WrapMode.Once;
            spartanKing.CrossFade("diehard", 0.3f);
        }
    }

    IEnumerator AttackToIdle()
	{
        if (spartanKing.IsPlaying("attack"))
            yield break;

        Invoke("SwordActivate", 0.3f);
        spartanKing.wrapMode = WrapMode.Once;
        spartanKing.CrossFade("attack", 0.3f);
        float dTime = spartanKing.GetClip("attack").length - 0.3f;
        Invoke("SwordDeactivate", dTime - 0.2f);
        yield return new WaitForSeconds(dTime);

        //objSword.SetActive(false);
        spartanKing.wrapMode = WrapMode.Loop;
        spartanKing.CrossFade("idle", 0.3f);
    }

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
        Debug.Log(hit.gameObject.name);
	}

    private void SwordActivate()
    {
        objSword.SetActive(true);
    }
    private void SwordDeactivate()
	{
        objSword.SetActive(false);
    }
}
