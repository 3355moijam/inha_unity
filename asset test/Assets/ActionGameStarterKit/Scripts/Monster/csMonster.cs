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

[AddComponentMenu("Insaneoops/csMonster")]
public class csMonster : MonoBehaviour 
{
	private SkinnedMeshRenderer[] monsterBody;
	private Color monsterMaterialColor;
	private bool bPlaySound = true; // 업데이트 함수등에서 사운드가 플레이될때 한 프레임에서만 플레이될 수 있도록 바로 bPlaySound변수의 값을 거짓으로 함.

	public int monsterLife = 100; // Monster's default health // 몬스터의 라이프.
	
	private void Awake ()
	{
		MonsterSetting ();		
	}
	
	private void Start ()
	{
	   monsterBody = this.GetComponentsInChildren<SkinnedMeshRenderer>();
       monsterMaterialColor = monsterBody[0].GetComponent<Renderer>().material.color;
	}

	private void MonsterSetting ()
	{
		csSetting mySetting = (csSetting)FindObjectOfType (typeof (csSetting));
		gameObject.AddComponent <csMonsterAI>();
		this.GetComponent<BoxCollider>().isTrigger = true;
		GetComponent<Animation>().wrapMode = WrapMode.Default;
		GetComponent<Animation>().AddClip (mySetting.monsterStand01, csSetting.monsterStand01Animation);
		GetComponent<Animation>()[csSetting.monsterStand01Animation].wrapMode = WrapMode.Loop;
		GetComponent<Animation>().AddClip (mySetting.monsterRun01, csSetting.monsterRun01Animation);
		GetComponent<Animation>()[csSetting.monsterRun01Animation].wrapMode = WrapMode.Loop;
		GetComponent<Animation>().AddClip (mySetting.monsterJump, csSetting.monsterJumpAnimation);
		GetComponent<Animation>()[csSetting.monsterJumpAnimation].wrapMode = WrapMode.Once;
		GetComponent<Animation>().AddClip (mySetting.monsterAttack01, csSetting.monsterAttack01Animation);
		GetComponent<Animation>()[csSetting.monsterAttack01Animation].wrapMode = WrapMode.Once;
		GetComponent<Animation>().AddClip (mySetting.monsterDefeat, csSetting.monsterDamageAnimation);
		GetComponent<Animation>()[csSetting.monsterDamageAnimation].wrapMode = WrapMode.Once;
		GetComponent<Animation>().playAutomatically = false;
	}

	public void MonsterLife () // When monster receive damage, this funnction will be called // 데미지를 받게 되면?.
	{
		monsterLife -= 60;
		
		if (monsterLife <= 0) 
		{			
			monsterLife = 0;
			gameObject.tag = "DeadBody";
			this.GetComponent<BoxCollider>().enabled = false; // 몬스터는 체력이 없으므로 이제 총알과의 충돌체크를 하지 않음.
			this.GetComponent<csMonsterAI>().bIsAlive = false;
			this.GetComponent<csMonsterAI>().bAttack = false;
         this.GetComponent<Animation>().enabled = false;
			StartCoroutine (DestroyByPlayer());
		}else // If monster is alive // 몬스터의 라이프가 아직 남아있다면?.
			StartCoroutine (DefeatByPlayer ());
	}	
	
	private IEnumerator DefeatByPlayer ()
	{
		yield return new WaitForSeconds (0.01f); // 애니메이션 이벤트로 처리.
		csCommon.SkinnedMeshBodyColorChange (monsterBody);
		GetComponent<Animation>().Play(csSetting.monsterDamageAnimation); // 피격 애니메이션도 보여준다.
		yield return new WaitForSeconds (0.3f);
		csCommon.SkinnedMeshBodyColorOrigin (monsterBody, monsterMaterialColor);			
	}	
	
	private IEnumerator DestroyByPlayer ()
	{
		csGameManager myGameManager = GameObject.Find ("GameManager").GetComponent<csGameManager>();
		csItemManager myItemManager = myGameManager.GetComponent<csItemManager>();
		csSoundManager mySoundManager = Camera.main.GetComponent<csSoundManager>();
		
		if (bPlaySound)
		{
			bPlaySound = false;
			mySoundManager.PlayGameSound (EGameSound.MonsterDeath);	
		}

		yield return new WaitForSeconds (0.01f);
		SpawnMonsterDeathBody ();
		this.GetComponent<csMonsterAI>().bRotateStart = false;
		myGameManager.DisplayScore ();
		myItemManager.SpawnItemWeapon (this.transform);	
		myItemManager.SpawnItemGold (this.transform, 1);
		myItemManager.SpawnItemPlayerTransform (this.transform);
		DestroyImmediate (gameObject);
	}	
	
	private void SpawnMonsterDeathBody ()
	{
		Instantiate (Resources.Load ("SoulMonsterDeadBody", typeof(Transform)), this.transform.position, this.transform.rotation);
	}
}