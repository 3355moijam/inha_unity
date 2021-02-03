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
using System.Collections.Generic;

[AddComponentMenu("Insaneoops/csPlayerAttack")]
public class csPlayerAttack : MonoBehaviour 
{
	private csPlayerWeapon myPlayerWeapon;
	private csPlayerMovement myPlayerMovement;
	private csGameManager myGameManager;	
	private csSoundManager mySoundManager;
	private Transform playerTransform;
	private Quaternion myQuaternion; // If the distance between player and monster is less than 4, player will rotate to enemy automatically. // 몬스터로의 목표 회전값.
	private bool bAttackByPlayerTransformation = true;

	[System.NonSerialized]
	public GameObject buttonRangeAttack;
	[System.NonSerialized]
	public GameObject buttonAttack;
	[System.NonSerialized]
	public UISprite backgroundButtonRangeAttack;
	[System.NonSerialized]
	public bool bRangeAttackEnabled = true; // 범위 공격에서 일정시간 딜레이 후에 다시 공격이 가능하게 하기 위해서.
	[System.NonSerialized]
	public bool bRotateStart = false; // If true, player will be turn to enemy automatically. // 참이면 몬스터 방향으로 플레이어 회전. // csPlayerAnimationControl에서 간편하게 호출하기 위해서 Public Static으로 선언.
	[System.NonSerialized]
	public string attackName; // Each attack animation name,  // 각 공격애니메이션 이름.
	[System.NonSerialized]
	public List<Transform> allTargetsInGame; // List for all of enemies in game, their Tag is "Monster"; // 현재 게임상에 있는 몬스터들.
	[System.NonSerialized]
	public Transform selectedTarget; // One monster in the game in the shortest distance between player and monsters. // He is the player's target automatically. // 게임상에 있는 몬스터들 중에서 플레이어와의 거리가 가장 짧은 몬스터.
	
	private void Start () 
	{	
		playerTransform = this.transform;
		myPlayerWeapon = this.GetComponent<csPlayerWeapon>();
		myPlayerMovement = this.GetComponent<csPlayerMovement>();
		myGameManager = (csGameManager)FindObjectOfType (typeof (csGameManager));
		mySoundManager = (csSoundManager)FindObjectOfType (typeof (csSoundManager));
		GetComponent<Animation>().Play (csSetting.playerJumpAnimation); // When the game starts, the player's default animation is playerJumpAnimation // 게임이 시작되었을 때 플레이어의 애니메이션은 점프로 시작.				
		allTargetsInGame = new List<Transform>(); // List for all of enemies in game, their Tag is "Monster"; // 게임상에 있는 몬스터들을 담을 리스트 초기화.
		selectedTarget = null; // One monster in the game in the shortest distance between player and monsters. // He is the player's target automatically.	// 게임상에 있는 몬스터들 중에서 플레이어와의 거리가 가장 짧은 몬스터를 담을 변수 초기화.
		buttonRangeAttack = GameObject.Find ("ButtonRangeAttack");
		buttonAttack = GameObject.Find ("ButtonAttack");
		backgroundButtonRangeAttack = GameObject.Find ("BackgroundButtonRangeAttack").GetComponent<UISprite>();
	}	

	private void Update () 
	{
		if (bRotateStart) // If true, player will be turn to enemy automatically. // bRotateStart변수의 값이 true라고 하면 몬스터쪽을 향해 회전함.	 
			playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, myQuaternion, Time.deltaTime * 10f); // Player will be rotated to enemy.		
		
		if (Input.GetKeyDown(KeyCode.Return )) // If Return key is pushed, attack
			Attack ();	
		else if (Input.GetKey(KeyCode.Return )) // if Return key is pushed continue, attack repeatly		
			AttackRepeat ();
		else if (Input.GetKeyUp(KeyCode.Return )) // if Return key is released, stop attack		
			CancleAttack ();	
		else if (Input.GetKeyUp(KeyCode.X ))				
			RangeAttack ();			
	}	

	public void TargetEnemy (List<Transform> allEnemies) // When Return key is pushed, just attack, this function will be called. // 플레이어가 공격버튼을 누르면 호출되어서 타겟을 찾는 함수.
	{
		csCommon.FindAllEnemiesInGame (allEnemies, "Monster", myGameManager.maxEnemyCount); // Fine all gameObject that has name of tag "Monster" // 게임상의 모든 몬스터들을 찾아서. 
		
		if (allEnemies.Count != 0) // If player find enemy in game. // 하나라도 찾았다면?.
		{
			if (allEnemies.Count > 1)
				csCommon.SortTargetsByDistance (allTargetsInGame, playerTransform); // One monster in the game in the shortest distance between player and monsters. // He is the player's target automatically.	 // 가장 가까운 몬스터를 찾아서.	 	
			
			selectedTarget = allEnemies[0]; // One monster in the game in the shortest distance between player and monsters. // He is the player's target automatically.	// 공격할 대상으로 지정.	
			bRotateStart = (Vector3.Distance (playerTransform.position, selectedTarget.transform.position) < 2) ? true : false;
		}else
			bRotateStart = false;
	}	

	public void CallFindAllEnemiesInGame () // 게임상에 있는 모든 몬스터를 찾음.
	{
		csCommon.FindAllEnemiesInGame (allTargetsInGame, "Monster", myGameManager.maxEnemyCount);
	}
	
	public void Attack ()
	{				
		if (!GetComponent<Animation>().IsPlaying(attackName)) // If player's animation is jump or run or idle // 아직 공격을 하고 있지 않다면?.
		{			
			TargetEnemy (allTargetsInGame); // When Return key is pushed, just attack, this function will be called. // 공격할 타겟을 찾음.
			
			if (selectedTarget != null) // If player find enemy to attack // 만일 공격할 타겟을 찾았다면?.
			{									
				if (Vector3.Distance (selectedTarget.transform.position, playerTransform.position) < 4 ) // If distance between player and enemy is less than 4 // 만일 플레이어와 타겟의 거리차가 4보다 작다면?.				
				{					
					myQuaternion = CalculateQuaternion (selectedTarget.transform.position, playerTransform.position); // Calculate direction to enemy // 타겟 방향을 구하고.
					AllRotateFalse ();
					bRotateStart = true; // 몬스터에게 줄 데미지는 애니메이션 동작의 이벤트로.
					GiveDamageToMonster ();
				}else // If distance between player and enemy is greater that 4 // 만일 플레이어와 공격할 대상과의 거리가 4보다 크다면?.	
					bRotateStart = false;			
			}else // If there is no enemies in game yet // 만일 공격할 대상을 찾지 못했다면?.
				bRotateStart = false;	
		}

		if (!csGameManager.bIsPlayerTransformation)
			GetComponent<Animation>().CrossFade(attackName, 0.1f); // Anyway, play attack animation	
		
		
		if (csGameManager.bIsPlayerTransformation && bAttackByPlayerTransformation)
			RangeAttack ();		
	}
	
	private void GiveDamageToMonster ()
	{
      if (!csGameManager.bIsPlayerTransformation)
      {
         if (selectedTarget.name == "SoulMonster") // If target's name is "SoulMonster" // 만일 타겟이 몬스터라면 몬스터쪽의 Health값을 깍고.
         {
            selectedTarget.GetComponent<csMonster>().MonsterLife(); // Reduce target's health // 칼로 공격할 때는 다음과 같이 몬스터에게 데미지를 주고 총알을 사용했을 경우는 총알의 충돌체로 몬스터에게 데미지를 준다.
            csFXManager.GiveDamageFX(selectedTarget.transform, 1);
         }
         else if (selectedTarget.name == "SoulBoss") // If target's name is "SoulBoss" // 만일 타겟이 보스라면 보스쪽의 Health값을 깍고.
         {
            selectedTarget.GetComponent<csBoss>().BossLife(); // Reduce target's health // 칼로 공격할 때는 다음과 같이 몬스터에게 데미지를 주고 총알을 사용했을 경우는 총알의 충돌체로 몬스터에게 데미지를 준다.	
            csFXManager.GiveDamageFX(selectedTarget.transform, 0);
         }
      }
	}
	
	private void bAttackByPlayerTransformationTrue ()
	{
		bAttackByPlayerTransformation = true;
	}
	
	private void PlayAttackSound () // 공격사운드를 플레이함.// 애니메이션 동작에서 호출.
	{
		mySoundManager.PlayGameSound (EGameSound.PlayerSwordAttack);	
		
		if ((myPlayerWeapon.weaponID == 2))
			mySoundManager.PlayGameSound (EGameSound.PlayerGunAttack);	
	}
	
	private Quaternion CalculateQuaternion (Vector3 targetPosition, Vector3 playerPosition) // 타겟방향으로 플레이어가 회전하기 위해서.
	{
		Quaternion tempQuaternion;
		tempQuaternion = Quaternion.LookRotation(targetPosition - playerPosition); 
		return tempQuaternion;
	}

	private void FireBullet () // 애니메이션 동작에서 호출.
	{
		myPlayerWeapon.CallSpawnBullet ();
	}		

	private string RandomPlayerTransfomationAttack () // Boss has two attack animation, Random choice
	{
		string bossAttackAnimation = "";
		int tempInt = Random.Range (0, 2);
		bossAttackAnimation = csSetting.bossAttackList[tempInt];
		return bossAttackAnimation;
	}	
	
	public void RangeAttack ()
	{
      GetComponent<Animation>().CrossFade(csSetting.playerAttack04Animation);
		
	  if (csGameManager.bIsPlayerTransformation)
      {
         GameObject.Find("PlayerTransform").GetComponent<Animation>().Play(RandomPlayerTransfomationAttack());
         Invoke("RangeAttackStart", 1.2f);
      }
      else
         Invoke("RangeAttackStart", 0.3f);
	}	

   private void RangeAttackStart ()
   {
		TargetEnemy (allTargetsInGame); // When X key is pushed, just attack, this function will be called. // 공격할 타겟을 찾음.

		if (selectedTarget != null) // If player find enemy to attack // 만일 공격할 타겟을 찾았다면?.
		{	
			if (bRangeAttackEnabled && myPlayerWeapon.weaponID != 2)
			{
				myQuaternion = CalculateQuaternion (selectedTarget.transform.position, playerTransform.position); // Calculate direction to enemy
				AllRotateFalse ();	
				bRotateStart = true;	
				RangeAttackDisabled ();
				Collider[] colliders;
				
				if((colliders = Physics.OverlapSphere(playerTransform.position, 3f)).Length > 1) //Presuming the object you are testing also has a collider 0 otherwise
				{							
					foreach (Collider enemy in colliders)
					{
						GetComponent<Animation>().CrossFade (attackName);
						if (enemy.tag == "Monster")
						{
                     if (enemy.name == "SoulMonster") // If target's name is "SoulMonster" // 만일 타겟이 몬스터라면 몬스터쪽의 Health값을 깍고.
                     {
                        enemy.GetComponent<csMonster>().MonsterLife(); // Reduce target's health // 칼로 공격할 때는 다음과 같이 몬스터에게 데미지를 주고 총알을 사용했을 경우는 총알의 충돌체로 몬스터에게 데미지를 준다.
                        csFXManager.GiveDamageFX(enemy.transform, 1);
                     }
                     else if (enemy.name == "SoulBoss") // If target's name is "SoulBoss" // 만일 타겟이 보스라면 보스쪽의 Health값을 깍고.
                     {
                        enemy.GetComponent<csBoss>().BossLife(); // Reduce target's health // 칼로 공격할 때는 다음과 같이 몬스터에게 데미지를 주고 총알을 사용했을 경우는 총알의 충돌체로 몬스터에게 데미지를 준다.		
                        csFXManager.GiveDamageFX(enemy.transform, 0);
                     }
						}
					}			
				}
				RangeAttackDisabled ();
				StartCoroutine ("RangeAttackEnabled");
			}
		}

		RangeAttackDisabled (); // 범위 공격은 강한 공격이므로 공격시 일정시간 공격을 할 수 없도록.
		Invoke ("RangeAttackEnabled", 1f); // 범위 공격은 강한 공격이므로 공격시 일정시간 공격을 할 수 없도록.
   }

	public void AttackRepeat () // If Return key is pushed Continued // 공격 버튼을 계속 눌렀을 때 호출.
	{
      InvokeRepeating("Attack", 0.01f, 0.2f);
	}

	public void CancleAttack ()
	{
		bRotateStart = false;
		CancelInvoke("Attack");
	}
	
	public void AllRotateFalse ()
	{
		myPlayerMovement.bRotateDirection = false;
		bRotateStart = false; 		
	}
	
	private void RangeAttackEnabled () // 범위 공격은 강한 공격이므로 공격시 일정시간 공격을 할 수 없도록.
	{
		bRangeAttackEnabled = true;
		
		if (csGameManager.bIsPlayerTransformation)
			backgroundButtonRangeAttack.spriteName = "AttackButton";
		else
			backgroundButtonRangeAttack.spriteName = "RangeAttack";
		
		buttonRangeAttack.GetComponent<Collider>().enabled = true;		
	}
	
	private void RangeAttackDisabled () // 범위 공격은 강한 공격이므로 공격시 일정시간 공격을 할 수 없도록.
	{
		bRangeAttackEnabled = false;
		
		if (csGameManager.bIsPlayerTransformation)
			backgroundButtonRangeAttack.spriteName = "AttackButtonGray";
		else
			backgroundButtonRangeAttack.spriteName = "RangeAttackGray";
		
		buttonRangeAttack.GetComponent<Collider>().enabled = false;		
	}	
}