/*--------------------------------------------------------------------------------
----------------------------------------------------------------------
			Unity3D Action Game StarterKit Ver 1.3
----------------------------------------------------------------------	
		
I hope this package will help you to develop basic action game using Unity3D.
This package has been saved Unity3D ver 4.2.2f1.

This package has everything you need to create a basic top-down fighting game, complete with controls, animations, enemies, fighting, pickups, levelling-up and spawn system. 

Get a head start with this simple, clean and elegant starter kit!

Action Game Starter Kit consists of
- Character parts system.
- Player's basic & range attack.
- Auto targeting system.
- Boss & Monster AI.
- Pet system.
- Item pickup for weapon change.
- Boss & Monster spawn system.
- Level up system.
- PC & Mobile is supported.
- And more.

if you have any questions or suggestions, just mail me.
My Email is insaneoops@naver.com
----------------------------------------------------------------------------------*/

using UnityEngine;
using System.Collections;

[AddComponentMenu("Insaneoops/csMonsterAI")]
public class csMonsterAI : MonoBehaviour // 총알이 맞을 때를 체크하기 위해서 Box Collider를 추가함. 
{
	private Transform playerTransform;
	private Transform myTransform;	
	private CharacterController monster; // Reference for CharacterController // 캐릭터 컨트롤러.	
	public bool bAttack = true; // If bAttack is true, monster can attack // bAttack변수가 값이 true일 때만 공격할 수 있음.
	private bool bCanMove = false; // If bCanMove is true, monster can move // bCanMove 변수의 값이 true여야만 몬스터는 움직일 수 있음.
	private Vector3 movement = Vector3.zero;
	private Quaternion myQuaternion; // Quaternion value direction to player // 플레이어를 향하는 방향값을 담을 변수.
	private float distance; // Value for distance bewtween player and monster // 플레이어와 몬스터와의 거리를 담을 변수.
	private float moveSpeed = 3.0f; // Monster's default movement speed // 몬스터의 이동속도.
	private float chasingDistance = 15f;
	private float atackDistance = 1.5f; 
	
	[System.NonSerialized]
	public bool bIsAlive = true; // If bIsAlive is true, monster is alive // bIsAlive변수의 값이 true라면 몬스터는 살아있음.
	[System.NonSerialized]
	public bool bRotateStart = false; // If bRotateStart is true, monster will be rotated to player automatically // bRotateStart변수의 값이 true일 때 몬스터는 플레이어를 향해서 회전함.

	private void Start () 
	{
		playerTransform = GameObject.Find ("Player").transform;
		myTransform = this.transform;
		monster = GetComponent<CharacterController>(); // Reference for CharacterController // Character Controller 컴포넌트를 찾음.		
		GetComponent<Animation>().Play (csSetting.monsterJumpAnimation); // When the game starts, monster's basic animation is monJumpAnimation // 점프해서 착지동작으로 시작. 
		Invoke ("LookingForPlayer", 0.1f); // 몬스터가 점프할 시간을 주기 위해서.
	}	

	private void Update () 
	{
		if (bRotateStart) // If bRotateStart is true, monster will be rotated to player automatically // bRotateStart변수의 값이 true임으로 플레이어를 향해서 회전함.
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, myQuaternion, Time.time / 50.0f);		
	}
	
	private void FixedUpdate() 
	{
		if (bCanMove && bIsAlive) // If bCanMove is true, monster can move // bCanMove 변수의 값이 true이므로 움직이거나 플레이어를 추적하거나 공격할 수 있음.
		{
			movement = myTransform.forward * moveSpeed * Time.smoothDeltaTime; // Calculate direction and speed // 이동방향과 속도를 구함.
			movement = new Vector3 (movement.x, -10 * Time.deltaTime, movement.z); // Apply gravity // 중력을 적용.
			distance = (playerTransform.position - myTransform.position).magnitude; // Calculate distance between player and monster // 플레이어와의 거리를 구함.

			if (distance < chasingDistance) // If the distance between player and monster is less than 15 // 만일 플레이어와의 거리차가 15안쪽이라면?.
			{				
				myQuaternion = Quaternion.LookRotation(playerTransform.position - myTransform.position); // Calculate direction angle to player // 플레이어를 향하는 방향을 구하고.
				bRotateStart = true; // turn to the player // bRotateStart변수의 값을 true로 해서 플레이어를 바라봄.

				if (distance < atackDistance) // If the distance between player and monster is less than 1.5 // 만일 플레이어와의 거리차가 2보다 작다면?.
					AttackThePlayer ();
				else // If the distance between player and monster is greater than 1.5 // 플레이어와의 거리차가 15안쪽이지만 아주 가깝지 않다면?.
					ChasingThePlayer ();	
			}else // If the distance between monster and player is greater than 15 // 만일 플레이어와의 거리차가 15이상이라면?.
			{
				bRotateStart = false; // No need to rotate to player // bRotateStart변수의 값을 false로 해서 더 이상 플레이어를 바라보지 않고.
				GetComponent<Animation>().CrossFade (csSetting.monsterStand01Animation); // Just Idle animation // 대기동작을 한다.
			}	
		}
    }	
	
	private void LookingForPlayer () // 몬스터가 점프할 시간을 주기 위해서.
	{
		bCanMove = true;
	}	
	
	private void AttackThePlayer ()
	{
		if (bAttack && !GetComponent<Animation>().IsPlaying (csSetting.monsterDeathAnimation)) // If bAttack is true and monster is alive // bAttack변수의 값이 true이어서 공격할 수 있고 아직 몬스터가 살아있다면?.
		{
			GetComponent<Animation>().CrossFade (csSetting.monsterAttack01Animation); // Play attack Animation // 공격 애니메이션을 보여주고. 
			GetComponent<Animation>().CrossFadeQueued(csSetting.monsterStand01Animation, 0.3F, QueueMode.CompleteOthers);
         playerTransform.gameObject.GetComponent<csPlayerHealth>().bIsMonsterAttacked = true;
         playerTransform.gameObject.GetComponent<csPlayerHealth>().PlayerHealth();
			StartCoroutine (TimeForAttack ()); // After 1.5 Seconds, monster can attack player again // 다음 공격가능 시간을 위한 타이머 설정.
		}		
	}

	private void ChasingThePlayer ()
	{
		if (GetComponent<Animation>().IsPlaying(csSetting.monsterJumpAnimation)) // If monster's animation is monJumpAnimation // 만일 점프중이라면?.
		{
			GetComponent<Animation>().CrossFadeQueued(csSetting.monsterRun01Animation, 0.3F, QueueMode.CompleteOthers); // Monster's animation is monRunAnimation after monJumpAnimation animation is finished // 점프가 끝난 후에 바로 플레이어를 향해서 이동한다.
			monster.Move( movement ); // 실제로 플레이어를 향해서 이동.
			myTransform.rotation = Quaternion.LookRotation(playerTransform.position - myTransform.position); // 플레이어를 향해서 회전.				
		}else // If monster's animation isn't monJumpAnimation // 점프동작이 아니고 이동이나 공격이나 다른동작중이라면?
		{
			if (csCommon.IfMonsterIsAheadOfMonster (myTransform, "Monster"))	
				GetComponent<Animation>().CrossFade (csSetting.monsterStand01Animation); // Dont's chase player, just idle // 플레이어를 쫒지 않고 대기자세.
			else
			{
				GetComponent<Animation>().CrossFade (csSetting.monsterRun01Animation); // 바로 이동동작으로 바뀌어서 플레이어를 향해서 이동한다.
				monster.Move( movement ); // 실제로 플레이어를 향해서 이동.
				myTransform.rotation = Quaternion.LookRotation(playerTransform.position - myTransform.position); // 플레이어를 향해서 회전.						
			}
		}
	}

	private IEnumerator TimeForAttack () // 몬스터는 한 번 공격후 일정시간 후에 다시 공격할 수 있음.
	{
		bAttack = false; 
		
		if (bIsAlive) // 만일 몬스터가 살아있다면?.
		{
			yield return new WaitForSeconds (1.5f); // 1초 뒤에.
			bAttack = true; // 몬스터가 다시 플레이어를 공격할 수 있도록 bAttack변수의 값을 true로 해준다.
		}
	}
}