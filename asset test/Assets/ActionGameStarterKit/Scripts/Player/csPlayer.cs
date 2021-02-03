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

[AddComponentMenu("Insaneoops/csPlayer")]
public class csPlayer : MonoBehaviour 
{
	private void Awake ()
	{
		PlayerSetting ();		
	}
	
	private void PlayerSetting ()
	{
		csSetting mySetting = (csSetting)FindObjectOfType (typeof (csSetting));
		gameObject.AddComponent <csPlayerAttack>();
		gameObject.AddComponent <csPlayerHealth>();
		gameObject.AddComponent <csPlayerMovement>();
		gameObject.AddComponent <csPlayerWeapon>();
		gameObject.AddComponent <csMinimap>();
		this.GetComponent<BoxCollider>().isTrigger = true;
		GetComponent<Animation>().wrapMode = WrapMode.Default;
		GetComponent<Animation>().AddClip (mySetting.playerIdle01, csSetting.playerIdle01Animation);
		GetComponent<Animation>()[csSetting.playerIdle01Animation].wrapMode = WrapMode.Loop;
		GetComponent<Animation>().AddClip (mySetting.playerRun01, csSetting.playerRun01Animation);
		GetComponent<Animation>()[csSetting.playerRun01Animation].wrapMode = WrapMode.Loop;
		GetComponent<Animation>().AddClip (mySetting.playerJump, csSetting.playerJumpAnimation);
		GetComponent<Animation>()[csSetting.playerJumpAnimation].wrapMode = WrapMode.Once;
		GetComponent<Animation>().AddClip (mySetting.playerAttack01, csSetting.playerAttack01Animation);
		GetComponent<Animation>()[csSetting.playerAttack01Animation].wrapMode = WrapMode.Once;
		GetComponent<Animation>().AddClip (mySetting.playerAttack02, csSetting.playerAttack02Animation);
		GetComponent<Animation>()[csSetting.playerAttack02Animation].wrapMode = WrapMode.Once;
		GetComponent<Animation>().AddClip (mySetting.playerAttack03, csSetting.playerAttack03Animation);
		GetComponent<Animation>()[csSetting.playerAttack03Animation].wrapMode = WrapMode.Once;
		GetComponent<Animation>().AddClip (mySetting.playerAttack04, csSetting.playerAttack04Animation);
		GetComponent<Animation>()[csSetting.playerAttack04Animation].wrapMode = WrapMode.Once;		
		GetComponent<Animation>().playAutomatically = false;	
		Transform playerSpineForGunAttack = GameObject.Find (csSetting.playerSpineForGunAttackName).transform; // In gun attack, player can atack the enemies while running, at that time attack animation and running animation is mixed.  // spineForGunAttack is the basic bone to mimix attack animation and running animation.	
		GetComponent<Animation>()[csSetting.playerAttack03Animation].layer = 1; // In gun attack, player can atack the enemies while running, at that time attack animation and running animation is mixed. // 상하체 분리 애니메이션을 하기 위해서 레이어 설정.
		GetComponent<Animation>()[csSetting.playerRun01Animation].layer = 0; // In gun attack, player can atack the enemies while running, at that time attack animation and running animation is mixed.  // 상하체 분리 애니메이션을 하기 위해서 레이어 설정.	
		GetComponent<Animation>()[csSetting.playerAttack03Animation].blendMode = AnimationBlendMode.Blend; // In gun attack, player can atack the enemies while running, at that time attack animation and running animation is mixed. // 상하체 분리 애니메이션을 설정하고.
		GetComponent<Animation>()[csSetting.playerAttack03Animation].AddMixingTransform(playerSpineForGunAttack);	// In gun attack, player can atack the enemies while running, at that time attack animation and running animation is mixed. // 상하체 분리 애니메이션을 설정하고.			
		DefinePlayerAnimationEvent();
	}
	
	private void DefinePlayerAnimationEvent()
	{
		AnimationEvent playerAttack01SoundEvent = new AnimationEvent();
	   playerAttack01SoundEvent.functionName = "PlayAttackSound";    
		playerAttack01SoundEvent.time = 0.04f;
	   GetComponent<Animation>()[csSetting.playerAttack01Animation].clip.AddEvent(playerAttack01SoundEvent); 		
	    
		AnimationEvent playerAttack02SoundEvent = new AnimationEvent();
	   playerAttack02SoundEvent.functionName = "PlayAttackSound";    
		playerAttack02SoundEvent.time = 0.5f;
	   GetComponent<Animation>()[csSetting.playerAttack02Animation].clip.AddEvent(playerAttack02SoundEvent); 	
	    
		AnimationEvent playerAttack03SoundEvent = new AnimationEvent();
	   playerAttack03SoundEvent.functionName = "PlayAttackSound";    
		playerAttack03SoundEvent.time = 0.07f;
	   GetComponent<Animation>()[csSetting.playerAttack03Animation].clip.AddEvent(playerAttack03SoundEvent);
	    
		AnimationEvent playerAttack03Event = new AnimationEvent();
	   playerAttack03Event.functionName = "FireBullet";    
		playerAttack03Event.time = 0.07f;
	   GetComponent<Animation>()[csSetting.playerAttack03Animation].clip.AddEvent(playerAttack03Event); 	
		
		AnimationEvent playerAttack04SoundEvent = new AnimationEvent();
	   playerAttack04SoundEvent.functionName = "PlayAttackSound";    
		playerAttack04SoundEvent.time = 0.15f;
	   GetComponent<Animation>()[csSetting.playerAttack04Animation].clip.AddEvent(playerAttack04SoundEvent);		
	}	
}