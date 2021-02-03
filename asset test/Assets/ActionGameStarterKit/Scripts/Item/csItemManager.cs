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

[AddComponentMenu("Insaneoops/csItemManager")]
public class csItemManager : MonoBehaviour 
{
	public void SpawnItemWeapon (Transform myTransformPos) // 랜덤으로 무기선택 아이템을 생성.
	{
		Vector3 tempVector = new Vector3 (myTransformPos.position.x, myTransformPos.position.y, myTransformPos.position.z);
		GameObject itemWeapon = Instantiate ((Resources.Load ("ItemWeapon", typeof(GameObject))), tempVector + Vector3.up * 2f, Quaternion.identity) as GameObject;
		itemWeapon.GetComponent<Rigidbody>().AddForce((tempVector + Vector3.up) * Random.Range (100f, 300f));
		itemWeapon.GetComponent<Rigidbody>().AddTorque((tempVector + Vector3.up) * Random.Range (100f, 300f));	
		itemWeapon.name = "WeaponItem";
	}	

	public void SpawnItemPlayerTransform (Transform myTransformPos) // 변신아이템을 생성.
	{
		Vector3 tempVector = new Vector3 (myTransformPos.position.x, myTransformPos.position.y, myTransformPos.position.z);
		GameObject itemWeapon = Instantiate ((Resources.Load ("ItemPlayerTransform", typeof(GameObject))), tempVector + Vector3.up * 2f, Quaternion.identity) as GameObject;
		itemWeapon.GetComponent<Rigidbody>().AddForce((tempVector + Vector3.up) * Random.Range (100f, 300f));
		itemWeapon.GetComponent<Rigidbody>().AddTorque((tempVector + Vector3.up) * Random.Range (100f, 300f));	
		itemWeapon.name = "PlayerTransformItem";
	}		
	
	public void SpawnItemGold (Transform myTransformPos, int myInt) // 골드아이템을 생성.
	{
		int randomMaxCountForItemGold = 0;
		
		switch (myInt)
		{
		case 0:
			randomMaxCountForItemGold = Random.Range (25, 50);
			break;
		case 1:
			randomMaxCountForItemGold = Random.Range (1, 4);
			break;
		}
		
		Vector3 tempVector = new Vector3 (myTransformPos.position.x, myTransformPos.position.y, myTransformPos.position.z);
		
		for (int i = 0; i < randomMaxCountForItemGold; i++)
		{
			GameObject itemGold = Instantiate ((Resources.Load ("ItemGold", typeof(GameObject))), tempVector + Vector3.up * Random.Range (0.1f, 1f), Quaternion.identity) as GameObject;
			itemGold.GetComponent<Rigidbody>().AddForce((tempVector + new Vector3 (Random.Range (-550f, 550f), Random.Range (20f, 50f), Random.Range (-550f, 550f))));
			itemGold.GetComponent<Rigidbody>().AddTorque((tempVector + Vector3.up) * Random.Range (300f, 600f));
			itemGold.name = "GoldItem";
		}
	}
}
