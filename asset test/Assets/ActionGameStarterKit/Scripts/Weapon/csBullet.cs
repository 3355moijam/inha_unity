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
using System.Collections; // 총알이 잘 맞게 하기 위해서 컬라이더의 크기를 키움.

[AddComponentMenu("Insaneoops/csBullet")]
public class csBullet : MonoBehaviour 
{
	private Vector3 direction;	
	
	public float bulletSpeed = 15.0f;

	private void Start () 
	{
		Transform playerTransform = GameObject.Find ("Player").transform;
		direction = playerTransform.forward;
		Destroy (gameObject, 2.0f);
	}

	private void Update () 
	{
		transform.Translate (direction * bulletSpeed * Time.deltaTime);
	}
	
    void OnTriggerEnter(Collider other) 
	{
        if (other.gameObject.tag == "Monster")
		{
			if (other.gameObject.name == "SoulBoss")
				other.gameObject.GetComponent<csBoss>().BossLife ();
			else if (other.gameObject.name == "SoulMonster")
				other.gameObject.GetComponent<csMonster>().MonsterLife ();
		}
	}
}
