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

[AddComponentMenu("Insaneoops/csItemWeapon")]
public class csItemWeapon : MonoBehaviour 
{
	private int chooseWeaponID = 0;
	private bool bReadyInterect = false; // bReadyInterect가 참이 되어야지 트리거 체크를 함. 아이템이 바닥에 떨어지면. 아이템이 생성되자 마자 바로 트리거 되는 것을 방지.
	
	private void Start () 
	{
		chooseWeaponID = Random.Range (0, 3);
		this.GetComponent<Renderer>().material.color = csSetting.itemWeaponColor[chooseWeaponID];
		Destroy (gameObject, 10.0f);
	}

 	private void OnCollisionEnter(Collision collision) // 만일 바닥과 충돌했다면?
	{
		if (collision.gameObject.tag == "Ground")
			Invoke ("IsTriggerOnActivated", 0.5f); // 충돌체의 트리거 속성을 켜서 이제 플레이어가 아이템을 터치할 수 있도록 함.
    }	
	
	private void IsTriggerOnActivated () // 충돌체의 트리거 속성을 켜서 이제 플레이어가 아이템을 터치할 수 있도록 함.
	{
		this.GetComponent<Collider>().isTrigger = true;
		this.GetComponent<Rigidbody>().useGravity = false;
		this.GetComponent<Rigidbody>().isKinematic = true;
		bReadyInterect = true;
	}	

    private void OnTriggerEnter(Collider other) // 변신했을때는 무기 아이템을 먹어도 무기가 바뀌지 않음.
	{
		if (bReadyInterect)
		{
			if (other.gameObject.tag == "Player" || other.gameObject.tag == "PlayerTransform") // 플레이어가 터치했다면? 무기를 교체함.
			{
				csGameManager myGameManager = (csGameManager)FindObjectOfType (typeof (csGameManager));
				csGUI myGUI = (csGUI)FindObjectOfType (typeof (csGUI));
				csPlayerWeapon myPlayerWeapon = GameObject.Find ("Player").GetComponent<csPlayerWeapon>();
				csSoundManager mySoundManager = (csSoundManager)FindObjectOfType (typeof (csSoundManager));			
				
				if (!csGameManager.bIsPlayerTransformation)
				{
					myPlayerWeapon.weaponID = chooseWeaponID;
					myPlayerWeapon.WeaponChange (); 
				}else
					myPlayerWeapon.weaponID = 0; // 변신했을 때 총을 가지고 있더라도 근접무기 공격을 할 수 있도록 하기 위해서 기본무기로 바뀜.
				
				mySoundManager.PlayGameSound (EGameSound.ItemPickup);
				myGameManager.goldCount += 20;
				myGUI.DisplayGoldCount (myGameManager.goldCount);
				Destroy (gameObject);	
			}
		}
	}
}