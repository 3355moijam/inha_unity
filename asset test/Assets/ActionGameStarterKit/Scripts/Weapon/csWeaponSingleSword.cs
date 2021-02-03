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

[AddComponentMenu("Insaneoops/csWeaponSingleSword")]
public class csWeaponSingleSword : MonoBehaviour 
{
	private WeaponTrail swordTrail;
	
	private float t = 0.033f;
	private float tempT = 0;
	private float animationIncrement = 0.003f;

	private void Start () 
	{
		swordTrail = this.transform.Find ("Trail").GetComponent<WeaponTrail>();
		swordTrail.StartTrail(0.5f, 0.4f);
		Transform playerWeaponPivot = GameObject.Find ("Player").transform.Find (csSetting.weaponPivotName).transform;
		this.transform.parent = playerWeaponPivot;
		this.transform.position = playerWeaponPivot.position;
		this.transform.rotation = playerWeaponPivot.rotation;
		this.transform.localScale = new Vector3 (1.1f, 1.1f, 1.1f);
		this.name = "WeaponSingleSword";
	}

	private void LateUpdate ()
	{
		t = Mathf.Clamp (Time.deltaTime, 0, 0.066f);
		RunAnimations ();	
	}	

	private void RunAnimations ()
	{
		if (t > 0) 
		{
			while (tempT < t) 
			{				
				tempT += animationIncrement;

				if (swordTrail.time > 0) 
					swordTrail.Itterate (Time.time - t + tempT);
				else 
					swordTrail.ClearTrail ();
			}

			tempT -= t;
			
			if (swordTrail.time > 0) 
				swordTrail.UpdateTrail (Time.time, t);
		}
	}	
}