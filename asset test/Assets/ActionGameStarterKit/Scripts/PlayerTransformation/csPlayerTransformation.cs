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

[AddComponentMenu("Insaneoops/csPlayerTransformation")]
public class csPlayerTransformation : MonoBehaviour 
{

	void Start () 
	{
		playerTransformationSetting ();
		GetComponent<Animation>().Play (csSetting.bossStand01Animation);
	}
	
	private void playerTransformationSetting ()
	{
		csSetting mySetting = (csSetting)FindObjectOfType (typeof (csSetting));
		GetComponent<Animation>().wrapMode = WrapMode.Default;
		GetComponent<Animation>().AddClip (mySetting.bossStand01, csSetting.bossStand01Animation);
		GetComponent<Animation>()[csSetting.bossStand01Animation].wrapMode = WrapMode.Loop;
		GetComponent<Animation>().AddClip (mySetting.bossRun01, csSetting.bossRun01Animation);
		GetComponent<Animation>()[csSetting.bossRun01Animation].wrapMode = WrapMode.Loop;
		GetComponent<Animation>().AddClip (mySetting.bossAttack01, csSetting.bossAttack01Animation);
		GetComponent<Animation>()[csSetting.bossAttack01Animation].wrapMode = WrapMode.Once;
		GetComponent<Animation>().AddClip (mySetting.bossAttack02, csSetting.bossAttack02Animation);
		GetComponent<Animation>()[csSetting.bossAttack02Animation].wrapMode = WrapMode.Once;		
		GetComponent<Animation>().AddClip (mySetting.bossDefeat, csSetting.bossDamageAnimation);
		GetComponent<Animation>()[csSetting.bossDamageAnimation].wrapMode = WrapMode.Once;
		GetComponent<Animation>().AddClip (mySetting.bossDeath, csSetting.bossDeathAnimation);
		GetComponent<Animation>()[csSetting.bossDeathAnimation].wrapMode = WrapMode.Once;
		GetComponent<Animation>().playAutomatically = false;			
	}	
}
