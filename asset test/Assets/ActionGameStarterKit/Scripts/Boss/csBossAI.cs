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

[AddComponentMenu("Insaneoops/csBossAI")]
public class csBossAI : MonoBehaviour // 총알이 맞을 때를 체크하기 위해서 Box Collider를 추가함. 
{
	private Transform playerTransform;
	private Transform myTransform;
	private CharacterController boss;
    private csSoundManager mySoundManager;
	private string bossAttackName;	
	private Vector3 movement = Vector3.zero;
	private bool bAttack = true; // if true, boss can attacks Player.
    private bool bPlaySound = false;
	private Quaternion myQuaternion; 
	private float distance; // Distance between Player and boss.
	private float moveSpeed = 3.0f; // Boss's movement speed.
	private float chasingDistance = 115f;
	private float atackDistance = 3.5f;	
	
	[System.NonSerialized]
	public bool bRotateStart = false; // if true, boss will look at Player.

	private void Start () 
	{
		playerTransform = GameObject.Find ("Player").transform;
		myTransform = this.transform;
      	mySoundManager = (csSoundManager)FindObjectOfType(typeof(csSoundManager));
		GetComponent<Animation>().Play (csSetting.bossStand01Animation); 
		boss = GetComponent<CharacterController>(); // Reference for Boss's CharacterController // Character Controller 컴포넌트를 찾음.			 
		bossAttackName = csSetting.bossAttack01Animation; // Default boss's attack name is "bossAttack01"	
	}	
	
	private void Update () 
	{
		if (bRotateStart) // If bRotateStart is true, Boss will be rotated to player automatically // bRotateStart변수의 값이 true임으로 플레이어를 향해서 회전함.
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, myQuaternion, Time.time / 50.0f);			
	}	
	
	private void FixedUpdate() 
	{
		if (csGameManager.bIsBossAlive) // If bCanMove is true, boss can move or attack the player // bMonsterGo변수의 값이 true이므로 움직이거나 플레이어를 추적하거나 공격할 수 있음.
		{
			movement = myTransform.forward * moveSpeed * Time.deltaTime; // Boss movement // 이동방향과 속도를 구함.
			movement = new Vector3 (movement.x, -10 * Time.deltaTime, movement.z); // Apply gravity // 중력을 적용.
			distance = (playerTransform.position - myTransform.position).magnitude; // Calculate distance between player and boss // 플레이어와의 거리를 구함.
			
			if (distance < chasingDistance) // If the distance between player and boss is less than 115 // 만일 플레이어와의 거리차가 115안쪽이라면?.
			{	
				myQuaternion = Quaternion.LookRotation(playerTransform.position - myTransform.position); // Calculate directoion to player // 플레이어를 향하는 방향을 구하고.
				bRotateStart = true; // Rotate to player // bRotateStart변수의 값을 true로 해서 플레이어를 바라봄.
				
				if (distance < atackDistance) // If the distance between player and boss is less then 3.5, boss will attack the player // 만일 플레이어와의 거리차가 3.5보다 작다면?.
					AttackThePlayer (); // 플레이어를 공격함.
				else // If the distance between boss and player is greater than 3.5 // 플레이어와의 거리차가 115안쪽이지만 아주 가깝지 않다면?.
					ChasingThePlayer (); // 플레이어를 추격.
			}else // 만일 플레이어와의 거리차가 15이상이라면?.
				bRotateStart = false; // bRotateStart변수의 값을 false로 해서 더 이상 플레이어를 바라보지 않고.
		}
    }

	private string RandomBossAttack () // Boss has two attack animation, Random choice // 보스의 2가지 공격중에서 임의로 선택함.
	{
		string bossAttackAnimation = "";
		int tempInt = Random.Range (0, 2);
		bossAttackAnimation = csSetting.bossAttackList[tempInt];
		return bossAttackAnimation;
	}
	
	private void AttackThePlayer () // 플레이어를 공격함.
	{
		if (bAttack && !GetComponent<Animation>().IsPlaying (csSetting.bossDeathAnimation)) // if bAttack is true, and boss is alove // bAttack변수의 값이 true이어서 공격할 수 있고 아직 몬스터가 살아있다면?.
		{
			bossAttackName = RandomBossAttack (); // 보스의 2가지 공격중에서 임의로 선택함.	
			GetComponent<Animation>().CrossFade(bossAttackName, 0.2F); // Play boss's attack animation // 공격 애니메이션을 보여주고.				
			movement += Physics.gravity; // When boss attacks the player, movement is limited // 공격 중에는 이동할 수 없도록 함.
			movement *= Time.deltaTime; // When boss attacks the player, movement is limited // 공격 중에는 이동할 수 없도록 함.	 
			GetComponent<Animation>().CrossFadeQueued(csSetting.bossStand01Animation, 0.3F, QueueMode.CompleteOthers);
         playerTransform.gameObject.GetComponent<csPlayerHealth>().bIsBossAttacked = true;
         playerTransform.gameObject.GetComponent<csPlayerHealth>().PlayerHealth();
         bPlaySound = true;
         Invoke("PlayAttackSound", 0.7f);
			StartCoroutine (TimeForAttack()); // Boss can attack the player after 4.5 seconds // 다음 공격가능 시간을 위한 타이머 설정. // 보스는 한번 공격후 4.5후에 다시 공격할 수 있음.
		}		
	}

	private void ChasingThePlayer () // 플레이어를 추격.
	{
		if (csCommon.IfMonsterIsAheadOfMonster (myTransform, "Monster"))
			GetComponent<Animation>().CrossFade (csSetting.bossStand01Animation);
		else
		{
			GetComponent<Animation>().CrossFade (csSetting.bossRun01Animation); // Change boss's animaiton to Running to chase the player // 바로 이동동작으로 바뀌어서 플레이어를 향해서 이동한다.
			boss.Move( movement ); // Move to player // 실제로 플레이어를 향해서 이동.
			myTransform.rotation = Quaternion.LookRotation(playerTransform.position - myTransform.position); // Rotate to the player // 플레이어를 향해서 회전.					
		}
	}

   private void PlayAttackSound()
   {
      if (bPlaySound)
      {
         bPlaySound = false;
         mySoundManager.PlayGameSound(EGameSound.BossAttack);
      }
   }

	private IEnumerator TimeForAttack () // 보스는 한번 공격후 4.5후에 다시 공격할 수 있음.
	{
		bAttack = false; 
		
		if (csGameManager.bIsBossAlive) // 만일 몬스터가 살아있다면?.
		{			
			yield return new WaitForSeconds (4.5f); // 4.5초 뒤에. 
			bAttack = true; // 몬스터가 다시 플레이어를 공격할 수 있도록 bAttack변수의 값을 true로 해준다.
		}
	}	
}