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

[AddComponentMenu("Insaneoops/csMinimap")]
public class csMinimap : MonoBehaviour 
{
	private GameObject player;
	private Transform spritePlayerIcon;
	private Transform myTransform;
	private GameObject[] spriteMonsterIcon;
	private string EnemyTag = "Monster";
	
	private float Scale = 0.05f;	

	private void Start ()
	{
		player = GameObject.Find ("Player");
		
		if (player)
			spritePlayerIcon = GameObject.Find ("SpritePlayerIcon").transform;
		
		spriteMonsterIcon = GameObject.FindGameObjectsWithTag ("SpriteMonster");
		myTransform = this.transform;
	}

	private void FixedUpdate () 
	{
		Quaternion tempQuaternion = Quaternion.identity;;
		tempQuaternion.eulerAngles  = new Vector3 (0f, 0f, -myTransform.eulerAngles.y);
		
		if (player)
			spritePlayerIcon.rotation = tempQuaternion;
		
		GameObject[] enemylists = GameObject.FindGameObjectsWithTag(EnemyTag);
		Vector2 pos;
		
		if (enemylists.Length > 0)
		{
			if (spriteMonsterIcon.Length > enemylists.Length )
			{
				for (int i = enemylists.Length; i < spriteMonsterIcon.Length; i++)
					spriteMonsterIcon[i].transform.position = new Vector3 (1000f, 0f, 0f);
			}
			
			for(int i=0;i<enemylists.Length;i++)
			{
				pos.x = spritePlayerIcon.position.x + ((enemylists[i].transform.position.x - myTransform.position.x) * Scale);
				pos.y = spritePlayerIcon.position.y + ((enemylists[i].transform.position.z - myTransform.position.z) * Scale);
				spriteMonsterIcon[i].transform.position = new Vector3 (pos.x, pos.y, 0f);
				Quaternion tempQuaternionA = Quaternion.identity;;
				tempQuaternionA.eulerAngles  = new Vector3 (0f, 0f, -enemylists[i].transform.eulerAngles.y);
				spriteMonsterIcon[i].transform.rotation = tempQuaternionA;
			}	
		}else
		{
			for (int i = 0; i < spriteMonsterIcon.Length; i++)
				spriteMonsterIcon[i].transform.position = new Vector3 (1000f, 0f, 0f);			
		}
	}
}