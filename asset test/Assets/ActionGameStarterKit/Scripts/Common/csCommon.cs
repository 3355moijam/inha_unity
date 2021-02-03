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

[AddComponentMenu("Insaneoops/csCommon")]
public class csCommon : MonoBehaviour 
{	
	public static void FirstGetIDFromPlayerPref () // 세팅에서 초기값을 불러와서 캐릭터를 세팅함.
	{
		SkinnedMeshRenderer[] playerBody = GameObject.Find ("Player").GetComponentsInChildren<SkinnedMeshRenderer>();
		GameObject temp;
		SkinnedMeshRenderer playerPart;		
		
		temp = Instantiate (Resources.Load (csSetting.playerHeadList[PlayerPrefs.GetInt ("PlayerHeadID")], typeof(GameObject))) as GameObject;
		playerPart = temp.GetComponentInChildren<SkinnedMeshRenderer>();
		playerBody[0].sharedMesh = playerPart.sharedMesh;
		playerBody[0].tag = PlayerPrefs.GetInt ("PlayerHeadID").ToString ();
		Destroy (temp);
		
		temp = Instantiate (Resources.Load (csSetting.playerUpperBodyList[PlayerPrefs.GetInt ("PlayerUpperBodyID")], typeof(GameObject))) as GameObject;
		playerPart = temp.GetComponentInChildren<SkinnedMeshRenderer>();
		playerBody[1].sharedMesh = playerPart.sharedMesh;
		playerBody[1].tag = PlayerPrefs.GetInt ("PlayerUpperBodyID").ToString ();
		Destroy (temp);
		
		temp = Instantiate (Resources.Load (csSetting.playerLowerBodyList[PlayerPrefs.GetInt ("PlayerLowerBodyID")], typeof(GameObject))) as GameObject;
		playerPart = temp.GetComponentInChildren<SkinnedMeshRenderer>();
		playerBody[2].sharedMesh = playerPart.sharedMesh;
		playerBody[2].tag = PlayerPrefs.GetInt ("PlayerLowerBodyID").ToString ();
		Destroy (temp);
		
		temp = Instantiate (Resources.Load (csSetting.playerHandList[PlayerPrefs.GetInt ("PlayerHandID")], typeof(GameObject))) as GameObject;
		playerPart = temp.GetComponentInChildren<SkinnedMeshRenderer>();
		playerBody[3].sharedMesh = playerPart.sharedMesh;
		playerBody[3].tag = PlayerPrefs.GetInt ("PlayerHandID").ToString ();
		Destroy (temp);
		
		temp = Instantiate (Resources.Load (csSetting.playerFootList[PlayerPrefs.GetInt ("PlayerFootID")], typeof(GameObject))) as GameObject;
		playerPart = temp.GetComponentInChildren<SkinnedMeshRenderer>();
		playerBody[4].sharedMesh = playerPart.sharedMesh;
		playerBody[4].tag = PlayerPrefs.GetInt ("PlayerFootID").ToString ();
		Destroy (temp);		
		
		foreach( SkinnedMeshRenderer body in playerBody)
			body.GetComponent<Renderer>().material.color = new Color (PlayerPrefs.GetFloat ("PlayerColorRID"), PlayerPrefs.GetFloat ("PlayerColorGID"), PlayerPrefs.GetFloat ("PlayerColorBID"), 1f);					
	}	
	
	public static void SkinnedMeshBodyColorChange (SkinnedMeshRenderer[] allSkinMesh) // 데미지를 받게 되면 붉게.
	{
		foreach( SkinnedMeshRenderer body in allSkinMesh)
		    body.GetComponent<Renderer>().material.color = Color.red;	
	}
	
	public static void SkinnedMeshBodyColorOrigin (SkinnedMeshRenderer[] allSkinMesh, Color myColor) // 데미지후 원래 생상으로.
	{
		foreach( SkinnedMeshRenderer body in allSkinMesh)
		    body.GetComponent<Renderer>().material.color = myColor;
	}	
	
	public static bool IfMonsterIsAheadOfMonster ( Transform myTransformPos, string monsterName ) // 몬스터 앞에 몬스터가 있게 되면 이동하지 않고 정지하기 위해서.
	{
		RaycastHit hit;
		bool tempBool = false;
		
		if (Physics.Raycast(myTransformPos.position, myTransformPos.forward, out hit, 0.5f)) // If there is an another monster ahead of monster // 바로 앞에 몬스터가 가로막고 있다면?.
			tempBool = (hit.transform.tag == monsterName) ? true : false;
		
		return tempBool;
	}	
	
	public static void DeleteAllEnemiesInGame () // 게임이 종료되면 남아있는 몬스터들을 찾아서 없애줌.
	{
		GameObject[] enemyInGame = GameObject.FindGameObjectsWithTag ("Monster"); // Looking for all of gameObject that have Tag named "Monster" // Monster태그를 가진 게임오브젝트들을 찾음.

		foreach (GameObject enemy in enemyInGame) // Destory all of enemies
			DestroyImmediate (enemy);		
	}
	
	public static void DeleteTempTag () // 유니티에는 태그버그가 있는 것 같은데 버그를 방지하기 위해서 미리 씬에 모든 태그를 가진 오브젝트를 만들어 놓았다가 게임이 시작되면 초기화 성격으로 없애줌.
	{
		DestroyImmediate (GameObject.Find ("tempTagLayer"));	
	}	
	
	public static void FindAllEnemiesInGame (List<Transform> allEnemies, string monsterTag, int maxMonsterCount) // Fine all gameObject that has name of tag "Monster" // 게임상에 있는 모든 몬스터들을 찾음.
	{
		allEnemies.Clear (); // First of all, initializing // 우선 초기화.
		GameObject[] enemyInGame = GameObject.FindGameObjectsWithTag (monsterTag); // Fine all gameObject that has name of tag "Monster" // Monster태그를 가진 게임오브젝트들을 찾음. 
		
		foreach (GameObject enemy in enemyInGame)
		{
			allEnemies.Add (enemy.transform);
			maxMonsterCount = allEnemies.Count; // 찾은 몬스터들의 개수를 저장.
		}
	}
	
	public static void SortTargetsByDistance (List<Transform> allEnemies, Transform myTransform) // One monster in the game in the shortest distance between player and monsters. // He is the player's target automatically. // 찾은 몬스터들 중에서 가장 가까운 거리에 있는 몬스터들 찾음.
	{
		allEnemies.Sort (delegate (Transform t1, Transform t2) 
		{
			return Vector3.Distance (t1.position, myTransform.position).CompareTo (Vector3.Distance (t2.position, myTransform.position));
		});
	}	

	public static void FreeResolution (GameObject[] myGamePanel)
	{
		if (myGamePanel.Length > 0)
		{
			for (int i = 0; i < myGamePanel.Length; i++)
			{
                float x = 1f;
                float y = 1f;

				if ((Screen.width == 2048 && Screen.height == 1536) || (Screen.width == 1920 && Screen.height == 1080))	
				{
                    x = 2.5f;
                    y = 2.5f;
                }else if ((Screen.width == 1280 && Screen.height == 800) || (Screen.width == 1280 && Screen.height == 720))
				{
                    x = 1f;
                    y = 1f; 
				}else if (Screen.width == 1136 && Screen.height == 640) 
				{
                    x = 0.82f;
                    y = 0.82f;
                }else if (Screen.width == 1024 && Screen.height == 768) 
				{
                    x = 0.85f;
                    y = 0.85f;
                }else if ((Screen.width == 854 && Screen.height == 480) || (Screen.width == 800 && Screen.height == 480)) 
				{
                    x = 0.6f;
                    y = 0.6f;
                }else if ((Screen.width == 960 && Screen.height == 640) || (Screen.width == 1024 && Screen.height == 600))
				{
                    x = 0.8f;
                    y = 0.8f;
                }else if (Screen.width == 480 && Screen.height == 320) 
				{
                    x = 0.4f;
                    y = 0.4f;
                }else
				{
                    x = 1f;
                    y = 1f; 					
				}

                myGamePanel[i].transform.localScale = new Vector3 (x, y, 1f);
			}	
		}
	}		
}