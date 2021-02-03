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

[AddComponentMenu("Insaneoops/csSetting")]
public class csSetting : MonoBehaviour 
{
	public static List<string> playerHeadList 		= new List<string>(3){"PlayerHead000", "PlayerHead001", "PlayerHead002"};
	public static List<string> playerUpperBodyList 	= new List<string>(3){"PlayerUpperBody000", "PlayerUpperBody001", "PlayerUpperBody002"};
	public static List<string> playerLowerBodyList 	= new List<string>(3){"PlayerLowerBody000", "PlayerLowerBody001", "PlayerLowerBody002"};
	public static List<string> playerHandList 		= new List<string>(3){"PlayerHand000", "PlayerHand001", "PlayerHand002"};
	public static List<string> playerFootList 		= new List<string>(3){"PlayerFoot000", "PlayerFoot001", "PlayerFoot002"};
	public static List<string> playerWeaponList 	= new List<string>(3) {"WeaponSingleSword", "WeaponDualSword", "WeaponGun"};
	
	public static List<string> playerAllPartsList 	= new List<string>(3);
	public static List<Color>  playerColor 			= new List<Color>(5);
	public static List<Color>  itemWeaponColor 		= new List<Color>(3){Color.red, Color.blue, Color.green};
	
	public static string playerIdle01Animation;
	public static string playerRun01Animation;
	public static string playerJumpAnimation;
	public static string playerAttack01Animation;
	public static string playerAttack02Animation;
	public static string playerAttack03Animation;
	public static string playerAttack04Animation;
	public static List<string> playerAttackList = new List<string>(3);
	
	public static string bossStand01Animation;
	public static string bossRun01Animation;
	public static string bossAttack01Animation;
	public static string bossAttack02Animation;
	public static string bossDamageAnimation;
	public static string bossDeathAnimation;
	public static List<string> bossAttackList = new List<string>(2);
	
	public static string monsterStand01Animation;
	public static string monsterRun01Animation;
	public static string monsterJumpAnimation;
	public static string monsterAttack01Animation;
	public static string monsterDamageAnimation;
	public static string monsterDeathAnimation;
	
	public static string petStand01Animation; 
	public static string petAttack01Animation;
	
	public static AudioClip winSound;
	public static AudioClip failedSound;
	public static AudioClip bossAttackSound;
	public static AudioClip bossDeathSound;
	public static AudioClip monsterDeathSound;
	public static AudioClip swordswingSound;
	public static AudioClip gunShotSound;
	public static AudioClip itemPickupSound;
	public static AudioClip thunderSound;	
	
	public static string weaponPivotName = "b_Bip/b_Hips/b_Spine/b_Spine1/b_Spine2/b_Neck/b_RightClav/b_RightArm/b_RightForeArm/b_RightHand/WeaponPivotRight";
	public static string playerSpineForGunAttackName = "b_Bip/b_Hips/b_Spine/b_Spine1";
	
	public string PlayerAnimation = "";
	public AnimationClip playerIdle01;
	public AnimationClip playerRun01;
	public AnimationClip playerJump;
	public AnimationClip playerAttack01;
	public AnimationClip playerAttack02;
	public AnimationClip playerAttack03;
	public AnimationClip playerAttack04;

	public string BossAnimation = "";
	public AnimationClip bossStand01;
	public AnimationClip bossRun01;
	public AnimationClip bossAttack01;
	public AnimationClip bossAttack02;
	public AnimationClip bossDefeat;
	public AnimationClip bossDeath;
	
	public string MonsterAnimation = "";
	public AnimationClip monsterStand01;
	public AnimationClip monsterRun01;
	public AnimationClip monsterJump;
	public AnimationClip monsterAttack01;
	public AnimationClip monsterDefeat;
	public AnimationClip monsterDeath;
	
	public string PetAnimation = "";
	public AnimationClip petStand01;	
	public AnimationClip petAttack01;
	
	public string GameSound = "";
	public AudioClip winSoundClip;
	public AudioClip failedSoundClip;
	public AudioClip bossAttackSoundClip;
	public AudioClip bossDeathSoundClip;
	public AudioClip monsterDeathSoundClip;
	public AudioClip swordswingSoundClip;
	public AudioClip gunShotSoundClip;
	public AudioClip itemPickupSoundClip;
	public AudioClip thunderSoundClip;
	
	private void Start ()
	{
		playerIdle01Animation = playerIdle01.name;
		playerRun01Animation = playerRun01.name;
		playerJumpAnimation = playerJump.name;
		playerAttack01Animation = playerAttack01.name;
		playerAttack02Animation = playerAttack02.name;
		playerAttack03Animation = playerAttack03.name;
		playerAttack04Animation = playerAttack04.name;
		playerAttackList.Add (playerAttack01Animation);
		playerAttackList.Add (playerAttack02Animation);
		playerAttackList.Add (playerAttack03Animation);
		
		bossStand01Animation = bossStand01.name;
		bossRun01Animation = bossRun01.name;
		bossAttack01Animation = bossAttack01.name;
		bossAttack02Animation = bossAttack02.name;
		bossDamageAnimation = bossDefeat.name;
		bossDeathAnimation = bossDeath.name;
		bossAttackList.Add (bossAttack01Animation);
		bossAttackList.Add (bossAttack02Animation);
		
		monsterStand01Animation = monsterStand01.name;
		monsterRun01Animation = monsterRun01.name;
		monsterJumpAnimation = monsterJump.name;
		monsterAttack01Animation = monsterAttack01.name;
		monsterDamageAnimation = monsterDefeat.name;
		monsterDeathAnimation = monsterDeath.name;
		
		petStand01Animation = petStand01.name;
		petAttack01Animation = petAttack01.name;
		
		winSound = winSoundClip;
		failedSound = failedSoundClip;
		bossAttackSound = bossAttackSoundClip;
		bossDeathSound = bossDeathSoundClip;
		monsterDeathSound = monsterDeathSoundClip;
		swordswingSound = swordswingSoundClip;
		gunShotSound = gunShotSoundClip;
		itemPickupSound = itemPickupSoundClip;
		thunderSound = thunderSoundClip;
	}
}