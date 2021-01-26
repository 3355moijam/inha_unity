using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecanimControl : MonoBehaviour
{
    public float runSpeed = 6.0f;
    public float rotSpeed = 360f;

    CharacterController pcController;
    //Vector3 direction;

    Animator animator;

    public bool bHurricaneKick { get; private set; } = false;

	// Start is called before the first frame update
	void Start()
    {
        pcController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", pcController.velocity.magnitude);
        if (Input.GetKeyDown(KeyCode.F))
        {
            Kick();
        }
        else if(Input.GetKeyDown(KeyCode.G) && !bHurricaneKick)
		{
			bHurricaneKick = true;
			animator.SetBool("bHurricaneKick", bHurricaneKick);
			animator.SetTrigger("TestHurri");
		}
        else if(Input.GetKeyUp(KeyCode.G) && bHurricaneKick)
		{
			bHurricaneKick = false;
			animator.SetBool("bHurricaneKick", bHurricaneKick);
		}
        else
		{

            CharacterControl_Slerp();
		}

        if(Input.GetMouseButtonDown(1))
		{
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
			{
				//target = hit.point;
			}
		}
    }

    private void CharacterControl_Slerp()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical"));

        if (direction.sqrMagnitude > 0.01f)
        {

            Vector3 foward = Vector3.Slerp(transform.forward,
                direction,
                rotSpeed * Time.deltaTime
                / Vector3.Angle(transform.forward, direction));
            transform.LookAt(transform.position + foward);


        }
        else
        {
        }
        pcController.Move(direction * runSpeed * Time.deltaTime + Physics.gravity);

    }

    void Kick()
    {
		//if (spartanKing.IsPlaying("attack"))
		//    yield break;

		//Invoke("SwordActivate", 0.3f);
		//spartanKing.wrapMode = WrapMode.Once;
		//spartanKing.CrossFade("attack", 0.3f);
		//float dTime = spartanKing.GetClip("attack").length - 0.3f;
		//Invoke("SwordDeactivate", dTime - 0.2f);
		//yield return new WaitForSeconds(dTime);

		//objSword.SetActive(false);
		//spartanKing.wrapMode = WrapMode.Loop;
		//spartanKing.CrossFade("idle", 0.3f);

		animator.SetTrigger("Kick");
		//      if (Input.GetMouseButtonDown(0) && !bHandUp)
		//{
		//          bHandUp = true;
		//          animator.SetBool("bHandUp", bHandUp);
		//          StartCoroutine(HandUp_Routine());
		//}
	}

    IEnumerator HandUp_Routine()
	{
		while (true)
		{
            yield return new WaitForSeconds(0.0f);
            if(bHurricaneKick && animator.GetCurrentAnimatorStateInfo(1).IsName("UpperBody.Attack"))
			{
                if(animator.GetCurrentAnimatorStateInfo(1).normalizedTime >= 1.0f)
				{
                    bHurricaneKick = false;
                    animator.SetBool("bHandUp", bHurricaneKick);
                    break;
				}
			}
		}
	}

}