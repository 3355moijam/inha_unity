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

[AddComponentMenu("Insaneoops/csPlayerHealth")]
public class csPlayerHealth : MonoBehaviour 
{	
	private csGameManager myGameManager; // Reference for GameManager.
	private csGUI myGUI;
	private SkinnedMeshRenderer[] playerBody;
	private int maxPlayerHealth = 100; // Player's maxHealth value
	
	[System.NonSerialized]
	public bool bIsBossAttacked; // If bIsBossAttacked is true, player is damaged by boss // 보스에게 공격 받았는지?.
	[System.NonSerialized]
	public bool bIsMonsterAttacked; // If bIsMonsterAttacked is true, player is damaged by monster // 몬스터에게 공격 받았는지?.
	public int playerHealth; // Player's health value // 플레이어의 Health.

	private void Start () 
	{
		playerBody = this.GetComponentsInChildren<SkinnedMeshRenderer>();
		playerHealth = maxPlayerHealth; // Player's health value // 플레이어의 Health.
		myGameManager = GameObject.Find ("GameManager").GetComponent<csGameManager>(); // Reference for gameManager's csGameManager script
		myGUI = myGameManager.GetComponent<csGUI>();
		DisplayPlayerHealthAndLife (); // 플레이어의 Health값과  목숨수를 표시함.
		DefinePlayerColor ();
	}
	
	private void DisplayPlayerHealthAndLife () // 플레이어의 Health값과  목숨수를 표시함.
	{
		myGUI.labelPlayerHealth.text = "PlayerHealth : " + playerHealth.ToString ();
		myGUI.labelPlayerLives.text = "PlayerLives : " + myGameManager.playerLives.ToString ();			
	}

	public void PlayerHealth () // 호출되면 플레이어의 Health값을 줄임.
	{
		if (bIsBossAttacked)
			playerHealth -= 5;
		else if (bIsMonsterAttacked)
			playerHealth -= 1;

		if (playerHealth <= 0f) // If player's health is less than or equal to 0 // 만일 플레이어의 라이프가 0보다 작다면?
		{
			playerHealth = maxPlayerHealth; // Player's health is default // 다시 플레이어의 라이프를 1로 채워주고.
			myGameManager.playerLives--; // Reduce player's life // 플레이어의 목숨수를 하나 줄여준다.
		}	
		
		bIsBossAttacked = false;
		bIsMonsterAttacked = false;
		
		DisplayPlayerHealthAndLife (); // 플레이어의 Health값과  목숨수를 표시함.	
		playerBodyMaterialChange (); // Called when the player is attacked by boss or player // 보스나 몬스터로부터 호출.
	}

	public void playerBodyMaterialChange () // Called when the player is attacked by boss or player // 보스나 몬스터로부터 호출.
	{
		StartCoroutine ("BodyMaterialChange");
	}
	
	private IEnumerator BodyMaterialChange () // Called when the player is attacked by boss or player
	{	
		float delayTime = 0f;
		
		if (bIsMonsterAttacked) // If player is attacked by monster // 만일 몬스터에게 공격 받았다면?.
			delayTime = 0.7f;
		else if (bIsBossAttacked) // If player is attacked by boss // 만일 보스에게 공격 받았다면?. 
			delayTime = 1.2f;

		yield return new WaitForSeconds (delayTime);
		csCommon.SkinnedMeshBodyColorChange (playerBody);
		yield return new WaitForSeconds (0.2f); // After 0.2 seconds
		DefinePlayerColor (); // 플레이어가 공격을 받으면 레드로 바뀌고 원래 상태로 돌아옴.
	}
	
	private void DefinePlayerColor () // 플레이어가 공격을 받으면 레드로 바뀌고 원래 상태로 돌아옴.
	{
		Color myColor = new Color();
		myColor = new Color (PlayerPrefs.GetFloat ("PlayerColorRID"), PlayerPrefs.GetFloat ("PlayerColorGID"), PlayerPrefs.GetFloat ("PlayerColorBID"), 1f);					
		
		foreach( SkinnedMeshRenderer body in playerBody)
		    body.GetComponent<Renderer>().material.color = myColor;	
	}	
}