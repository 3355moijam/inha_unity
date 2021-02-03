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

[AddComponentMenu("Insaneoops/csItemPlayerTransform")]
public class csItemPlayerTransform : MonoBehaviour  // 이 아이템을 먹게 되면 플레이는 변신하게 됨.
{
	private bool bReadyInterect = false; // bReadyInterect가 참이 되어야지 트리거 체크를 함. 아이템이 바닥에 떨어지면. 아이템이 생성되자 마자 바로 트리거 되는 것을 방지.
	
	private void Start () 
	{		
		Destroy (gameObject, 10.0f);
	}

 	private void OnCollisionEnter(Collision collision) // 0.1초 후에 플레이어는 이 아이템을 먹을 준비를 할 수 있음.
	{
		if (collision.gameObject.tag == "Ground")
			Invoke ("IsTriggerOnActivated", 0.1f); // 이제 플레어어가 아이템을 먹을 수 있도록 트리거 속성을 켬.
    }	
	
	private void IsTriggerOnActivated () // 이제 플레어어가 아이템을 먹을 수 있도록 트리거 속성을 켬.
	{
		this.GetComponent<Collider>().isTrigger = true;
		this.GetComponent<Rigidbody>().useGravity = false;
		this.GetComponent<Rigidbody>().isKinematic = true;
		bReadyInterect = true;
	}

    private void OnTriggerEnter(Collider other) 
	{
		if (bReadyInterect )
		{
			if (other.gameObject.tag == "Player" || other.gameObject.tag == "PlayerTransform")
			{
				Camera.main.GetComponent<csSoundManager>().PlayGameSound (EGameSound.ItemPickup);
				csGameManager myGameManager = (csGameManager)FindObjectOfType (typeof (csGameManager));
				myGameManager.goldCount += 20;
				myGameManager.GetComponent<csGUI>().DisplayGoldCount (myGameManager.goldCount);
				
				if (!csGameManager.bIsPlayerTransformation) // bIsPlayerTransformation의 값이 참이라면 이미 게임안에 변신캐릭터가 있음.
					myGameManager.PlayerTransformation (); // 아직 변신을 하지 않았다면 변신을 함.
				
				Destroy (gameObject);			
			}
		}
	}
}