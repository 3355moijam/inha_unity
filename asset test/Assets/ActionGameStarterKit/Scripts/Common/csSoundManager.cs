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

public enum EGameSound {Win, Failed, BossAttack, BossDeath, MonsterDeath, PlayerSwordAttack, PlayerGunAttack, ItemPickup, ThunderSound}

[AddComponentMenu("Insaneoops/csSoundManager")]
public class csSoundManager : MonoBehaviour 
{
	public void PlayGameSound ( EGameSound soundIndex )
	{
		switch (soundIndex)
		{
		case EGameSound.Win:
			GetComponent<AudioSource>().PlayOneShot (csSetting.winSound, 2f);
			break;
		case EGameSound.Failed:
			GetComponent<AudioSource>().PlayOneShot (csSetting.failedSound, 3f);
			break;
		case EGameSound.BossAttack:
			GetComponent<AudioSource>().PlayOneShot (csSetting.bossAttackSound, 3f);
			break;
		case EGameSound.BossDeath:
			GetComponent<AudioSource>().PlayOneShot (csSetting.bossDeathSound, 5f);
			break;
		case EGameSound.MonsterDeath:
			GetComponent<AudioSource>().PlayOneShot (csSetting.monsterDeathSound, 2f);
			break;
		case EGameSound.PlayerSwordAttack:
			GetComponent<AudioSource>().PlayOneShot (csSetting.swordswingSound, 3f);
			break;
		case EGameSound.PlayerGunAttack:
			GetComponent<AudioSource>().PlayOneShot (csSetting.gunShotSound, 3f);
			break;
		case EGameSound.ItemPickup:
			GetComponent<AudioSource>().PlayOneShot (csSetting.itemPickupSound, 3f);
			break;
		case EGameSound.ThunderSound:
         GetComponent<AudioSource>().PlayOneShot(csSetting.thunderSound, 3f);	
			break;
		}
	}	
}