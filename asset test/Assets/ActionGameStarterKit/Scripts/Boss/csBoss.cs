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

[AddComponentMenu("Insaneoops/csBoss")]
public class csBoss : MonoBehaviour 
{
	public int bossLife = 100; // Boss's life.

    private SkinnedMeshRenderer[] bossBody; // Boss's skinnedMeshRenderer.
	private Color bossMaterialColor; // 피격시 칼라가 바뀌는데 원본색상을 이후에 유지하기 위해서
	private bool bPlaySound = true; // 업데이트 함수등에서 사운드가 플레이될때 한 프레임에서만 플레이될 수 있도록 바로 bPlaySound변수의 값을 거짓으로 함.

	void Start () 
	{
		bossBody = this.GetComponentsInChildren<SkinnedMeshRenderer>();
		bossMaterialColor = bossBody[0].GetComponent<Renderer>().material.color;			
		csGameManager.bIsBossAlive = true;	
		BossSetting ();
	}
	
	private void BossSetting ()
	{
		csSetting mySetting = (csSetting)FindObjectOfType (typeof (csSetting));
		gameObject.AddComponent <csBossAI>();
		this.GetComponent<BoxCollider>().isTrigger = true;
		GetComponent<Animation>().wrapMode = WrapMode.Default;
		GetComponent<Animation>().AddClip (mySetting.bossStand01, csSetting.bossStand01Animation);
		GetComponent<Animation>()[csSetting.bossStand01Animation].wrapMode = WrapMode.Loop;
		GetComponent<Animation>().AddClip (mySetting.bossRun01, csSetting.bossRun01Animation);
		GetComponent<Animation>()[csSetting.bossRun01Animation].wrapMode = WrapMode.Loop;
		GetComponent<Animation>().AddClip (mySetting.bossAttack01, csSetting.bossAttack01Animation);
		GetComponent<Animation>()[csSetting.bossAttack01Animation].wrapMode = WrapMode.Once;
		GetComponent<Animation>().AddClip (mySetting.bossAttack02, csSetting.bossAttack02Animation);
		GetComponent<Animation>()[csSetting.bossAttack02Animation].wrapMode = WrapMode.Once;		
		GetComponent<Animation>().AddClip (mySetting.bossDefeat, csSetting.bossDamageAnimation);
		GetComponent<Animation>()[csSetting.bossDamageAnimation].wrapMode = WrapMode.Once;
		GetComponent<Animation>().playAutomatically = false;			
	}	
	
	public void BossLife () // If receive damage from player, this function will be called // 데미지를 받게 되면?. 
	{		
		bossLife -= 60;
		
		if (bossLife <= 0) // If boss's life is less then or equal to 0, boss will be dead // 만일 몬스터의 라이프가 0보다 작다면?.
		{			
			bossLife = 0;
			gameObject.tag = "DeadBody";
			this.GetComponent<BoxCollider>().enabled = false; // 보스는 체력이 없으므로 이제 총알과의 충돌체크를 하지 않음. 
         this.GetComponent<Animation>().enabled = false;
			StartCoroutine (DestroyByPlayer ());
		}else // 몬스터의 라이프가 아직 남아있다면?.
			StartCoroutine (DefeatByPlayer ()); // Defeat 코루틴을 실행한다. // 보스가 데미지를 받게 되면?.
	}	
	
	private IEnumerator DefeatByPlayer () // 보스가 데미지를 받게 되면?.
	{	
		csCommon.SkinnedMeshBodyColorChange (bossBody);
		GetComponent<Animation>().Play(csSetting.bossDamageAnimation); // 피격 애니메이션도 보여준다.
		GetComponent<Animation>().CrossFadeQueued(csSetting.bossStand01Animation, 0.3F, QueueMode.CompleteOthers);
		yield return new WaitForSeconds (0.1f);
		csCommon.SkinnedMeshBodyColorOrigin (bossBody, bossMaterialColor);
	}	
	
	private IEnumerator DestroyByPlayer ()
	{
		csGameManager myGameManager = GameObject.Find ("GameManager").GetComponent<csGameManager>();
		csItemManager myItemManager = myGameManager.GetComponent<csItemManager>();
		csSoundManager mySoundManager = Camera.main.GetComponent<csSoundManager>();
		
		if (bPlaySound)
		{
			bPlaySound = false;
			mySoundManager.PlayGameSound (EGameSound.BossDeath);	
		}

		csGameManager.bIsBossAlive = false;
		csGameManager.bBossSpawnEnabled = true; // 게임상에 보스가 하나라도 있으면 추가로 스폰되지 않도록 하기 위해서.
		SpawnBossDeathBody ();
		this.GetComponent<csBossAI>().bRotateStart = false;
		yield return new WaitForSeconds (0.01f);
		myGameManager.DisplayScore ();
		myItemManager.SpawnItemWeapon (this.transform);
		myItemManager.SpawnItemGold (this.transform, 0);	
		myItemManager.SpawnItemPlayerTransform (this.transform);
		DestroyImmediate (gameObject);
	}	
	
	private void SpawnBossDeathBody ()
	{
		Instantiate (Resources.Load ("SoulBossDeadBody", typeof(Transform)), this.transform.position, this.transform.rotation);
	}	
}