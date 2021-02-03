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

[AddComponentMenu("Insaneoops/csPlayerWeapon")]
public class csPlayerWeapon : MonoBehaviour 
{
	private csGUI myGUI;

	[System.NonSerialized]
	public int bulletCount; // just bullet count, if bulletCount is less than 0, player's weapon will be changed to default weapon.
	public int maxBulletCount = 10; // Maximum bullet count.
	[System.NonSerialized]
	public int weaponID; // Weapon ID, 0 is SwordTypeA, 1 is SwordTypeB, 2 is Gun. // 각 무기의 고유 아이디.	
	
	private void Start ()
	{
		myGUI = GameObject.Find ("GameManager").GetComponent<csGUI>();
		bulletCount = maxBulletCount; // The number of bullet is 10.
		DecideFirstWeapon ();
	}
	
	public void CallSpawnBullet () // 총을 쏘았을 때 약간의 딜레이를 주기 위해서.
	{
		Invoke ("SpawnBullet", 0.3f);
	}
	
	private void SpawnBullet () // Spawn bullet // 호출되면 총알이 총구위치로 이동.	 
	{
		if (GetComponent<Animation>().IsPlaying (csSetting.playerAttack03Animation) && weaponID == 2)
		{
			Transform bulletPosition = GameObject.Find ("BulletSpawnPosition").transform; //  이후에 멀티플레이용으로 수정해야 함.
			Instantiate (Resources.Load ("bullet"), bulletPosition.position, Quaternion.identity);
		}
		
		CunsumeBullet (); // Reuce bullet 1 // 총알이 모두 소모되면 기본 무기로 전환하기 위해서.
	}	
	
	public void CunsumeBullet () // Reuce bullet 1 // 총알이 모두 소모되면 기본 무기로 전환하기 위해서.
	{
		bulletCount--;
		DisplayBulletCount ();
		
		if (bulletCount <= 0) // If player consume all of bullet // 만일 총알을 다 소모했으면?.
		{
			myGUI.labelBulletCount.text = "";
			bulletCount = maxBulletCount; // bullet count is 10
			weaponID = 0; // Player's weapon will be changed to default weapon // 기본 무기로 전환.
			WeaponChange (); // Player's weapon will be changed to default weapon // 기본 무기로 전환.
		}		
	}	
	
	private void DisplayBulletCount ()
	{
		myGUI.labelBulletCount.text = "Bullet : " + bulletCount.ToString ();
	}	
	
	public void WeaponChange () // 무기아이템을 먹으면 무기를 교체함.
	{
		GameObject[] tempPlayerWeapon = GameObject.FindGameObjectsWithTag ("PlayerWeapon");
		
		if (tempPlayerWeapon.Length > 0)
		{
			for (int i = 0; i < tempPlayerWeapon.Length; i++)
				Destroy (tempPlayerWeapon[i]);
		}
		
		csPlayerAttack myPlayerAttack = this.GetComponent<csPlayerAttack>();
		Transform myWeapon =  Instantiate (Resources.Load (csSetting.playerWeaponList[weaponID], typeof(Transform))) as Transform;
		myWeapon.name = "PlayerWeapon";
		myPlayerAttack.attackName = csSetting.playerAttackList[weaponID];
		myGUI.labelBulletCount.text = "";
		myPlayerAttack.bRangeAttackEnabled = true;
		myPlayerAttack.backgroundButtonRangeAttack.spriteName = "RangeAttack";
		myPlayerAttack.buttonRangeAttack.GetComponent<Collider>().enabled = true;	
		
		if (weaponID == 2)
		{
			bulletCount = maxBulletCount;
			myGUI.labelBulletCount.text = "Bullet : " + bulletCount.ToString ();
			myPlayerAttack.bRangeAttackEnabled = false;
			myPlayerAttack.backgroundButtonRangeAttack.spriteName = "RangeAttackGray";
			myPlayerAttack.buttonRangeAttack.GetComponent<Collider>().enabled = false;	
		}
	}	
	
	public void DecideFirstWeapon ()
	{
		weaponID = 0;
		WeaponChange (); // 무기아이템을 먹으면 무기를 교체함.
	}		
}