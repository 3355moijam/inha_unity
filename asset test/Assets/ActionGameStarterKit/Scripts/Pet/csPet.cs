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

[AddComponentMenu("Insaneoops/csPet")]
public class csPet : MonoBehaviour 
{
	private void Awake ()
	{
		petSetting ();		
	}
	
	private void petSetting ()
	{
		csSetting mySetting = (csSetting)FindObjectOfType (typeof (csSetting));
		gameObject.AddComponent <csPetAI>();
		this.GetComponent<BoxCollider>().isTrigger = true;
		GetComponent<Animation>().wrapMode = WrapMode.Default;
		GetComponent<Animation>().AddClip (mySetting.petStand01, csSetting.petStand01Animation);
		GetComponent<Animation>()[csSetting.petStand01Animation].wrapMode = WrapMode.Loop;
		GetComponent<Animation>().AddClip (mySetting.petAttack01, csSetting.petAttack01Animation);
		GetComponent<Animation>()[csSetting.petAttack01Animation].wrapMode = WrapMode.Once;
		GetComponent<Animation>().playAutomatically = false;		
	}
}
