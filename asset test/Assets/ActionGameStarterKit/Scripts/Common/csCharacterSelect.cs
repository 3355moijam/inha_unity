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

[AddComponentMenu("Insaneoops/csCharacterSelect")]
public class csCharacterSelect : MonoBehaviour 
{
	private int indexForPlayerPartsList = 1; // 각 파츠들 배열에 인덱스하기 위한 , 파츠들의 0번째 인덱스가 기본복장이므로 1부터 시작.;
		
	private void Start () 
	{
		DefinePlayerColorSample (); // 플레이어가 사용할 5가지 색상을 모은다.
		PlayerSpawnInitially (); // 선택창에서 보여질 플레이어와 무기를 생성한다.
		csCommon.FirstGetIDFromPlayerPref (); // 세팅에서 초기값을 불러와서 캐릭터를 세팅함.
		DontDestroyOnLoad (GameObject.Find ("GameSetting"));
	}
	
	public void PlayerSpawnInitially () // 선택창에서 보여질 플레이어와 무기를 생성한다.
	{
		Transform playerSpawnPoint = GameObject.Find ("PlayerSpawnPoint").transform; // 플레이어가 스폰될 위치를 찾고.
		GameObject myPlayer = Instantiate (Resources.Load ("Player", typeof(GameObject)) , playerSpawnPoint.position, playerSpawnPoint.rotation) as GameObject;
		myPlayer.name = "Player";	
		myPlayer.tag = "Player";
		myPlayer.GetComponent<csPlayerAttack>().enabled = false;
		myPlayer.GetComponent<csPlayerHealth>().enabled = false;
		myPlayer.GetComponent<csPlayerMovement>().enabled = false;
		myPlayer.GetComponent<csPlayerWeapon>().enabled = false;
		myPlayer.GetComponent<csMinimap>().enabled =  false;
		myPlayer.GetComponent<Animation>().Play (csSetting.playerIdle01Animation);	
		GameObject player = GameObject.Find ("Player");	
		Transform playerWeaponPivot = GameObject.Find ("Player").transform.Find (csSetting.weaponPivotName).transform;
		Transform myWeapon =  Instantiate (Resources.Load (csSetting.playerWeaponList[0], typeof(Transform))) as Transform;
		myWeapon.GetComponent<csWeaponSingleSword>().enabled = false;
		myWeapon.transform.parent = playerWeaponPivot;
		myWeapon.transform.position = playerWeaponPivot.position;
		myWeapon.transform.rotation = playerWeaponPivot.rotation;
		myWeapon.transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
		player.transform.localScale = playerSpawnPoint.localScale;
		player.transform.parent = playerSpawnPoint;	
		
//		for (int i = 0; i < playerBody.Length; i++) // 플레어어 각 파츠의 SkinnedMeshRenderer의 SharedMesh를 알기 위해서.
//			Debug.Log ("playerBody" + i + " is" + playerBody[i].name);		
	}

	private void DefinePlayerColorSample () // 플레이어가 사용할 5가지 색상을 모은다.
	{
		for (int i = 0; i < 5; i++)
			csSetting.playerColor.Add (GameObject.Find ("PlayerColorSample" + i).GetComponentInChildren<MeshRenderer>().GetComponent<Renderer>().material.color);
	}
	
	public void ChangePlayerColorTypeA () // 플레어어의 칼라값을 0번 타입으로.
	{
		ChangePlayerColor (0); // 각 칼라타입으로 플레이어의 색상값을 적용한다.
	}	
	
	public void ChangePlayerColorTypeB () // 플레어어의 칼라값을 1번 타입으로.
	{
		ChangePlayerColor (1); // 각 칼라타입으로 플레이어의 색상값을 적용한다.
	}	
	
	public void ChangePlayerColorTypeC () // 플레어어의 칼라값을 2번 타입으로.
	{
		ChangePlayerColor (2); // 각 칼라타입으로 플레이어의 색상값을 적용한다.
	}	
	
	public void ChangePlayerColorTypeD () // 플레어어의 칼라값을 3번 타입으로.
	{
		ChangePlayerColor (3); // 각 칼라타입으로 플레이어의 색상값을 적용한다.
	}	
	
	public void ChangePlayerColorTypeE () // 플레어어의 칼라값을 4번 타입으로.
	{
		ChangePlayerColor (4); // 각 칼라타입으로 플레이어의 색상값을 적용한다.
	}		
	
	public void ChangePlayerColor ( int myInt ) // 각 칼라타입으로 플레이어의 색상값을 적용한다.
	{
		SkinnedMeshRenderer[] myPlayerBody = GameObject.Find ("Player").GetComponentsInChildren<SkinnedMeshRenderer>();
		
		foreach( SkinnedMeshRenderer body in myPlayerBody)
			body.GetComponent<Renderer>().material.color = csSetting.playerColor[myInt];	
	}
	
	public void ChangePlayerPartsRandom () // 플레이어의 파츠를 랜덤하게 고르고 랜덤색상을 적용한다.
	{
		ChangePlayerParts (0, csSetting.playerHeadList, true);
		ChangePlayerParts (1, csSetting.playerUpperBodyList, true);	
		ChangePlayerParts (2, csSetting.playerLowerBodyList, true);
		ChangePlayerParts (3, csSetting.playerHandList, true);
		ChangePlayerParts (4, csSetting.playerFootList, true);
		ChangePlayerColor (Random.Range (0, 5)); // 각 칼라타입으로 플레이어의 색상값을 적용한다.
	}
	
	public void SavePlayerSetting () // 플레이어의 정보값을 저장한다. // 확인해 봐야 함.
	{
		SkinnedMeshRenderer[] myPlayerBody = GameObject.Find ("Player").GetComponentsInChildren<SkinnedMeshRenderer>();		
		PlayerPrefs.SetInt ("PlayerHeadID", int.Parse(myPlayerBody[0].tag));
		PlayerPrefs.SetInt ("PlayerUpperBodyID", int.Parse(myPlayerBody[1].tag));
		PlayerPrefs.SetInt ("PlayerLowerBodyID", int.Parse(myPlayerBody[2].tag));
		PlayerPrefs.SetInt ("PlayerHandID", int.Parse(myPlayerBody[3].tag));
		PlayerPrefs.SetInt ("PlayerFootID", int.Parse(myPlayerBody[4].tag));		
		PlayerPrefs.SetFloat ("PlayerColorRID", myPlayerBody[0].GetComponent<Renderer>().material.color.r);
		PlayerPrefs.SetFloat ("PlayerColorGID", myPlayerBody[0].GetComponent<Renderer>().material.color.g);
		PlayerPrefs.SetFloat ("PlayerColorBID", myPlayerBody[0].GetComponent<Renderer>().material.color.b);
	}

	public void ChangePlayerHead () // 플레이어의 각각의 파츠를 다양하게 적용해 준다.
	{
		ChangePlayerParts (0, csSetting.playerHeadList, false);  // 플레이어의 각각의 파츠를 다양하게 적용해 준다.
	}
	
	public void ChangePlayerUpperBody () // 플레이어의 각각의 파츠를 다양하게 적용해 준다.
	{
		ChangePlayerParts (1, csSetting.playerUpperBodyList, false);	// 플레이어의 각각의 파츠를 다양하게 적용해 준다.
	}
	
	public void ChangePlayerLowerBody () // 플레이어의 각각의 파츠를 다양하게 적용해 준다.
	{
		ChangePlayerParts (2, csSetting.playerLowerBodyList, false); // 플레이어의 각각의 파츠를 다양하게 적용해 준다.
	}
	
	public void ChangePlayerHand () // 플레이어의 각각의 파츠를 다양하게 적용해 준다.
	{
		ChangePlayerParts (3, csSetting.playerHandList, false); // 플레이어의 각각의 파츠를 다양하게 적용해 준다.
	}
	
	public void ChangePlayerFoot () // 플레이어의 각각의 파츠를 다양하게 적용해 준다.
	{
		ChangePlayerParts (4, csSetting.playerFootList, false); // 플레이어의 각각의 파츠를 다양하게 적용해 준다.
	}	
	
	private void WriteEachPartsID (int myInt, GameObject myGameObject) // 바꿀 파츠의 태그에 따라서 플레이어의 각 파츠의 태그를 적용한다.
	{
		SkinnedMeshRenderer[] playerBody = GameObject.Find ("Player").GetComponentsInChildren<SkinnedMeshRenderer>();
		playerBody[myInt].tag = myGameObject.tag;
	}

	private void ChangePlayerParts ( int partIndex, List<string> playerAllPartsList, bool myBool) // 플레이어의 각각의 파츠를 다양하게 적용해 준다.
	{
		SkinnedMeshRenderer[] playerBody = GameObject.Find ("Player").GetComponentsInChildren<SkinnedMeshRenderer>();		
		bool bIsRandom = myBool;
		GameObject temp;
		SkinnedMeshRenderer playerPart;

		if (indexForPlayerPartsList == 3)
			indexForPlayerPartsList = 0;

		if (bIsRandom)
			indexForPlayerPartsList = Random.Range (0, 3);
			
		temp = Instantiate (Resources.Load (playerAllPartsList[indexForPlayerPartsList], typeof(GameObject))) as GameObject;	
		WriteEachPartsID (partIndex, temp); // 바꿀 파츠의 태그에 따라서 플레이어의 각 파츠의 태그를 적용한다.
		playerPart = temp.GetComponentInChildren<SkinnedMeshRenderer>();		
		playerBody[partIndex].sharedMesh = playerPart.sharedMesh;
		Destroy (temp);	
		indexForPlayerPartsList++;
	}
	
	public void StartGame ()
	{
		Application.LoadLevel ("2MainGame");
	}	
}