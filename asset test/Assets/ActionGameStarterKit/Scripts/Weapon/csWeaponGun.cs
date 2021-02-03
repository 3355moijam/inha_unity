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

[AddComponentMenu("Insaneoops/csWeaponGun")]
public class csWeaponGun : MonoBehaviour 
{
	void Start () 
	{
		Transform playerWeaponPivot = GameObject.Find ("Player").transform.Find (csSetting.weaponPivotName).transform;
		this.transform.parent = playerWeaponPivot;
		this.transform.position = playerWeaponPivot.position;
		this.transform.rotation = playerWeaponPivot.rotation;
		this.transform.localScale = new Vector3 (1.1f, 1.1f, 1.1f);
		this.name = "WeaponGun";
	}	
}
