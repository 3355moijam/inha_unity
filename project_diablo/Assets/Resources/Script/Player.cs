using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using DG.Tweening;
public class Player : MonoBehaviour
{
	NavMeshAgent sAgent = null;
    [HideInInspector]
    public NavMeshAgent agent { get => sAgent; }
	Animator sAnimator = null;
    [HideInInspector]
    public Animator animator { get => sAnimator; }


    Vector3 nextNode;

    //skill
    
    //ISkill[] mainSkills;
    //ISkill SkillManager.Instance.MainSkill;
    
    delegate void PlayerAction();
    List<PlayerAction> Actions;
    PlayerAction setDest;
    PlayerAction useMainSkill;
    // Start is called before the first frame update
    void Start()
    {
	    sAgent = GetComponent<NavMeshAgent>();
        sAgent.updateRotation = false;
	    sAnimator = GetComponent<Animator>();
        
        

        // 스킬 액션 순서처리
        Actions = new List<PlayerAction>();
        setDest = new PlayerAction(SetDestination);
		//ISkill test = SkillManager.Instance.MainSkill;

		useMainSkill = new PlayerAction(SkillManager.Instance.MainSkill.OnButton);

		GameManager.Instance.player = this;
    }

    // Update is called once per frame
    void Update()
    {
		ClickLeftMouseButton();
        UseMainSkill();

        if (Actions.Count > 0)
            Actions[Actions.Count - 1]();

		if (nextNode != sAgent.steeringTarget)
		{
            RotateToMoveDirection();
        }

    }
    
    void LateUpdate()
    {
	    sAnimator.SetFloat("Speed", sAgent.velocity.magnitude);
    }
	void SetDestination()
	{
		
        sAgent.destination = GameManager.Instance.mouseHit.point;
        sAgent.stoppingDistance = 1.0f;
        sAgent.isStopped = false;
		RotateToMoveDirection();
        //sAnimator.SetTrigger("StartRun");
        
        
	}



    void RotateToMoveDirection()
	{
        
        nextNode = sAgent.steeringTarget;
        Vector3 lookRotation = nextNode - transform.position;
        transform.DORotateQuaternion(Quaternion.LookRotation(lookRotation), 0.3f);
        
    }

    public void MoveStop()
	{
        sAgent.isStopped = true;
        sAgent.ResetPath();
        nextNode = sAgent.steeringTarget;
    }

    void UseMainSkill()
	{
        if(Input.GetMouseButtonDown(1))
		{
            if (SkillManager.Instance.MainSkill.HasAnimation() && !Actions.Contains(useMainSkill))
                Actions.Add(useMainSkill);

			SkillManager.Instance.MainSkill.OnButtonDown();
		}
        //if (Input.GetMouseButton(1))
        //{
            //SkillManager.Instance.MainSkill.OnButton(hit);
        //}
        if (Input.GetMouseButtonUp(1))
		{
            if (SkillManager.Instance.MainSkill.HasAnimation())
                Actions.Remove(useMainSkill);

			SkillManager.Instance.MainSkill.OnButtonUp();
		}
    }


    void ClickLeftMouseButton()
	{
        if (Input.GetMouseButtonDown(0))
		{
            if (GameManager.Instance.mouseHit.transform != null && !EventSystem.current.IsPointerOverGameObject())
            {
                if (GameManager.Instance.mouseHit.transform.tag == "Enemy")
                {

                }
                else
                {
                    if (!Actions.Contains(setDest)) 
                        Actions.Add(setDest);
                    animator.SetTrigger("StartRun");
                    //SetDestination(hit);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
		{
            Actions.Remove(setDest);
            // 마지막에 눌린 액션에 따라 해당 액션이 재개되게 하는 함수 제작을 고려할 필요 있음.
            if (animator.GetBool("BeamsOn") && !animator.GetCurrentAnimatorStateInfo(0).IsName("BeamsOn")) // 하드코딩
            {
                animator.SetTrigger("StartBeams");
                MoveStop();
            }
		}
    }

    private void OnDrawGizmos()
    {
        //if (agent.path.corners.Length == 0)
        //	return;
        if (sAgent == null)
            return;
        for (int i = 0; i < sAgent.path.corners.Length; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(sAgent.path.corners[i], 0.3f);

            if (i > 0)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(sAgent.path.corners[i - 1], sAgent.path.corners[i]);
            }
        }
    }
}
