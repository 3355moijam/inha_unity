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

[AddComponentMenu("Insaneoops/csPet")]
public class csPetAI : MonoBehaviour 
{
	private Transform playerTransform;
	private Transform myTransform;
	private Transform selectedTargetByPet; // One monster in the game in the shortest distance between player and monsters. // He is the player's target automatically. // 게임상에 있는 몬스터들 중에서 플레이어와의 거리가 가장 짧은 몬스터.	
	private Vector3 distance;

	public float movementSpeed = 5;

	private void Start () 
	{
		playerTransform = GameObject.Find ("Player").transform;
		myTransform = this.transform;
		GetComponent<Animation>().Play (csSetting.petStand01Animation);
	}	

	private void FixedUpdate ()
	{	
		FollowPlayer ();
	}

	private void FollowPlayer ()
	{
		Vector3 direction = Vector3.zero;
		Vector3 targetPositionForPlayer = Vector3.zero;
		Vector3 distanceBetween = Vector3.zero;
		Quaternion myQuaternion;
		targetPositionForPlayer = playerTransform.position + playerTransform.forward + new Vector3 (-1f, 1.5f, -1f);
		distanceBetween = targetPositionForPlayer - myTransform.position;
		direction = (distanceBetween).normalized;
		myQuaternion = Quaternion.LookRotation(direction);
		
		if (distanceBetween.magnitude > 2f)
		{
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, myQuaternion, 0.1f);	
			myTransform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);
		}

		GetComponent<Animation>().Play (csSetting.petStand01Animation);
//		
//		if (animation.IsPlaying (csSetting.petAttackAnimation))
//			animation.CrossFadeQueued(csSetting.petStandAnimation, 0.3F, QueueMode.CompleteOthers);
	}
	
//	private void DelayForLookingForEnemy () // 펫은 3초마다 타겟을 찾음.
//	{
//	
//	
//	[System.NonSerialized]
//	public bool bLookingForEnemy = false; // bLookingForEnemy변수의 값이 참이여야만 펫은 타겟을 찾기 시작함.	
//	
//		if (bLookingForEnemy) //FixedUpdate
//		{
//			LookingForEnemy (); // 펫은 3초마다 타겟을 찾음.
//			bLookingForEnemy = false;
//			Invoke ("DelayForLookingForEnemy", 3f); // 펫은 3초마다 타겟을 찾음.
//		}
//		
//		try
//		{
//			if (selectedTargetByPet)
//			{
//				if (bAttackEnemyWithPlayer)
//					AttackEnemy ();
//				else
//					FollowPlayer ();	
//			}else
//				FollowPlayer ();
//		}catch
//		{
//			FollowPlayer ();
//		}	                          // FixedUpdate
//	
//	
//	
//		bLookingForEnemy = true;
//	}
//	
//	private void LookingForEnemy () // 펫은 3초마다 타겟을 찾음.
//	{
//		TargetEnemy ();
//		
//		try
//		{
//			if (selectedTargetByPet)
//			{
//				Vector3 targetPosition = Vector3.zero;
//				targetPosition = selectedTargetByPet.position + new Vector3 (0f, 1.5f, 0f);
//				distance = targetPosition - myTransform.position;
//				bAttackEnemyWithPlayer = (distance.magnitude) > 3 ? false : true;
//			}
//		}catch
//		{
//		}
//	}	

//	private void AttackEnemy ()
//	{
//		if (selectedTargetByPet.gameObject != null)
//		{
//			if (bDamageToMonster)
//			{
//				animation.Play (mySetting.petAttackAnimation);
//				Vector3 targetPosition = Vector3.zero;
//				float wishSpeed = 7f; // 오브젝트의 이동속도를 결정합니다. 1이면 1초동안 거리 1만큼 이동을 합니다.
//				float lerpSpeed = 1f;
//				targetPosition = selectedTargetByPet.position + new Vector3 (0f, 2f, 0f);
//				lerpSpeed = wishSpeed / distance.magnitude;
//				myTransform.rotation = selectedTargetByPet.localRotation;
//				
//				if (distance.magnitude > 0.2f)	
//					StartCoroutine(MoveToTarget(myTransform,myTransform.position, targetPosition, lerpSpeed));
//
//				bDamageToMonster = false;
//				Invoke ("bDamageToMonsterTrue", 1.5f);
//			}else
//				animation.CrossFadeQueued(mySetting.petStandAnimation, 0.3F, QueueMode.CompleteOthers);
//		}
//	}
//	
//	private void GiveDamageToMonsterByPet ()
//	{
//		if (selectedTargetByPet.name == "SoulMonster") // If target's name is "SoulMonster" // 만일 타겟이 몬스터라면 몬스터쪽의 Health값을 깍고.
//			selectedTargetByPet.GetComponent<csMonsterAI>().MonsterLife (); // Reduce target's health // 칼로 공격할 때는 다음과 같이 몬스터에게 데미지를 주고 총알을 사용했을 경우는 총알의 충돌체로 몬스터에게 데미지를 준다.		
//		else if (selectedTargetByPet.name == "SoulBoss") // If target's name is "SoulBoss" // 만일 타겟이 보스라면 보스쪽의 Health값을 깍고.
//			selectedTargetByPet.GetComponent<csBossAI>().BossLife (); // Reduce target's health // 칼로 공격할 때는 다음과 같이 몬스터에게 데미지를 주고 총알을 사용했을 경우는 총알의 충돌체로 몬스터에게 데미지를 준다.					
//	}
//	
//	private void bDamageToMonsterTrue ()
//	{
//		bDamageToMonster = true;
//	}
//	
//	private IEnumerator MoveToTarget(Transform sourceTransform, Vector3 source , Vector3 target ,float lerpSpeed)
//	{
//		float lerpTime = 0;
//		
//		while (lerpTime * lerpSpeed < 1f)
//		{
//			sourceTransform.position = Vector3.Lerp(source, target, lerpTime * lerpSpeed);
//			lerpTime += Time.deltaTime;
//			yield return null;
//		}
//	}	
//	
//	private void TargetEnemy () 
//	{
//		FindAllEnemiesInGame (); // Fine all gameObject that has name of tag "Monster" // 게임상의 모든 몬스터들을 찾아서.
//
//		if (allTargetsInGame.Count != 0) // If player find enemy in game. // 하나라도 찾았다면?.
//		{
//			if (allTargetsInGame.Count > 1)
//				SortTargetsByDistance (); // One monster in the game in the shortest distance between player and monsters. // He is the player's target automatically.	 // 가장 가까운 몬스터를 찾아서.		
//			
//			selectedTargetByPet = allTargetsInGame[0]; // One monster in the game in the shortest distance between player and monsters. // He is the player's target automatically.	// 공격할 대상으로 지정. 	
//		}
//	}		
//
//	private void FindAllEnemiesInGame () // Fine all gameObject that has name of tag "Monster" // 게임상에 있는 모든 몬스터들을 찾음.
//	{
//		allTargetsInGame.Clear (); // First of all, initializing // 우선 초기화.
//		GameObject[] enemyInGame = GameObject.FindGameObjectsWithTag ("Monster"); // Fine all gameObject that has name of tag "Monster" // Monster태그를 가진 게임오브젝트들을 찾음. 
//		
//		foreach (GameObject enemy in enemyInGame)
//			allTargetsInGame.Add (enemy.transform);
//	}
//
//	private void SortTargetsByDistance () // One monster in the game in the shortest distance between player and monsters. // He is the player's target automatically. // 찾은 몬스터들 중에서 가장 가까운 거리에 있는 몬스터들 찾음.	
//	{
//		allTargetsInGame.Sort (delegate (Transform t1, Transform t2) 
//		{
//			return Vector3.Distance (t1.position, myTransform.position).CompareTo (Vector3.Distance (t2.position, myTransform.position));
//		});
//	}		
}