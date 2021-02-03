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

[AddComponentMenu("Insaneoops/csPlayerMovement")]
public class csPlayerMovement : MonoBehaviour 
{
	private csPlayerAttack myPlayerAttack; // Reference for player's csPlayerAttack script
	private Transform playerTransform;
	private Transform spriteJoystick;
	private CharacterController character; // Reference for CharacterController // 캐릭터 컨트롤러.
	private Vector3 movement = Vector3.zero; // Player's movement
	private Vector3 tempVector;
	private Quaternion rotateToDirection;	 
	
	public float playerSpeed  = 5f; // Player's default speed
	[System.NonSerialized]
	public bool bRotateDirection = false; // 조이스틱이나 키보드의 방향키를 눌렀을때 그 방향으로 회전하기 위해서.

	private void Start () 
	{
		character = GetComponent<CharacterController>(); // Reference for CharacterController // Character Controller 컴포넌트를 찾음.	
		playerTransform = GameObject.Find ("Player").transform;
		myPlayerAttack = GameObject.Find ("Player").GetComponent<csPlayerAttack>(); // Reference for player's csPlayerAttack script
		spriteJoystick = GameObject.Find ("SpriteJoystick").transform;
	}
	
	private void Update ()
	{
		if (bRotateDirection)
			playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, rotateToDirection,11 * Time.deltaTime);
	}

	void FixedUpdate () // Update함수를 쓰게 되면 캐릭터가 흔들림. 
	{
		Vector3 playerVelocity = character.velocity; // Save player's volocity
		playerVelocity = new Vector3 (playerVelocity.x, 0f, playerVelocity.z); // Ignore any vertical movement
		float speed = playerVelocity.magnitude; // Define player's speed
		
		if(speed > 0) // If player is moving // 만일 플레이어가 움직이고 있다면?.
		{	
			if (GetComponent<Animation>().IsPlaying(csSetting.playerAttack01Animation) || GetComponent<Animation>().IsPlaying(csSetting.playerAttack02Animation) || GetComponent<Animation>().IsPlaying(csSetting.playerAttack03Animation) || GetComponent<Animation>().IsPlaying(csSetting.playerAttack04Animation)) // If player's animation is playerAttack01Animation or playerAttack02Animation or playerDamageAnimation
			{
				GetComponent<Animation>().CrossFadeQueued(csSetting.playerRun01Animation, 0.3F, QueueMode.CompleteOthers); // player's animation will be changed to playerRunAnimation
				
				if (csGameManager.bIsPlayerTransformation)
					if (GameObject.Find ("PlayerTransform").GetComponent<Animation>().IsPlaying(csSetting.bossAttack01Animation) || GameObject.Find ("PlayerTransform").GetComponent<Animation>().IsPlaying(csSetting.bossAttack02Animation))				
						GameObject.Find ("PlayerTransform").GetComponent<Animation>().CrossFadeQueued(csSetting.bossRun01Animation, 0.3F, QueueMode.CompleteOthers);
			}else	
			{
				GetComponent<Animation>().CrossFade( csSetting.playerRun01Animation ); // player's animation will be changed to playerRunAnimation

            if (csGameManager.bIsPlayerTransformation && GameObject.Find("PlayerTransform"))
				{
					GameObject.Find ("PlayerTransform").GetComponent<Animation>().CrossFade(csSetting.bossRun01Animation);				
				}
			}
		}else // If player isn't moving // 만일 플레이어가 움직이고 있지 않다면?.
		{
			if (GetComponent<Animation>().IsPlaying(csSetting.playerJumpAnimation) || GetComponent<Animation>().IsPlaying(csSetting.playerAttack01Animation) || GetComponent<Animation>().IsPlaying(csSetting.playerAttack02Animation) || GetComponent<Animation>().IsPlaying(csSetting.playerAttack03Animation) || GetComponent<Animation>().IsPlaying(csSetting.playerAttack04Animation)) // If player's animation is playerJumpAnimation or playerAttack01Animation or playerAttack02Animation or playerDamageAnimation
			{
				GetComponent<Animation>().CrossFadeQueued(csSetting.playerIdle01Animation, 0.3F, QueueMode.CompleteOthers); // player's animation will be changed to playerIdleAnimation
				
				if (csGameManager.bIsPlayerTransformation)
				{
					if (GameObject.Find ("PlayerTransform").GetComponent<Animation>().IsPlaying(csSetting.bossAttack01Animation) || GameObject.Find ("PlayerTransform").GetComponent<Animation>().IsPlaying(csSetting.bossAttack02Animation))				
						GameObject.Find ("PlayerTransform").GetComponent<Animation>().CrossFadeQueued(csSetting.bossStand01Animation, 0.3F, QueueMode.CompleteOthers);
				}
			}else	
			{
				GetComponent<Animation>().CrossFade( csSetting.playerIdle01Animation );	// player's animation will be changed to playerIdleAnimation

            if (csGameManager.bIsPlayerTransformation && GameObject.Find("PlayerTransform"))
					GameObject.Find ("PlayerTransform").GetComponent<Animation>().CrossFade(csSetting.bossStand01Animation);				
			}
		}		

		movement = new Vector3 (Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		movement = playerTransform.TransformDirection (movement);
		
		if (spriteJoystick.localPosition.x != 0f || spriteJoystick.localPosition.y != 0f)
		{
			myPlayerAttack.AllRotateFalse ();
			tempVector = new Vector3(spriteJoystick.localPosition.x,0f,spriteJoystick.localPosition.y);
			tempVector.Normalize();
			
			if(tempVector.sqrMagnitude > 0.0001)
			{
				rotateToDirection = Quaternion.LookRotation(tempVector);
				bRotateDirection = true;
			}
		
			movement = tempVector * playerSpeed * Time.deltaTime;
		}
		
		if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
		{
			Vector2 tempVector2 = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // Debug Purpose.
			spriteJoystick.localPosition = tempVector2 * 70f; // Debug Purpose.
			myPlayerAttack.AllRotateFalse ();
			tempVector = new Vector3 (Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			tempVector.Normalize();
			
			if(tempVector.sqrMagnitude > 0.0001)
			{
				rotateToDirection = Quaternion.LookRotation(tempVector);
				bRotateDirection = true;
			}
		
			movement = tempVector * playerSpeed * Time.deltaTime;
		}else if (Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f) // Debug Purpose.		
			spriteJoystick.localPosition = Vector2.zero; // Debug Purpose.

		movement = new Vector3 (movement.x, -10 * Time.deltaTime, movement.z); // Apply gravity // 중력을 적용.	

		if (GetComponent<Animation>().IsPlaying(csSetting.playerAttack01Animation) || GetComponent<Animation>().IsPlaying(csSetting.playerAttack02Animation)) // If player's animation is playerAttack01Animation or playerAttack02Animation
	    {  	
			movement += Physics.gravity; // Reduce player speed, In attack, player can't move
			movement *= Time.deltaTime;   
			myPlayerAttack.AllRotateFalse ();

			if (myPlayerAttack.selectedTarget != null) // If player find target
			{
				if (Vector3.Distance (playerTransform.position, myPlayerAttack.selectedTarget.transform.position) < 4) // If the distance between player and target is less than 4
					myPlayerAttack.bRotateStart = true; // Player will rotate to the target // 이동보다 공격이 우선되게 함.
				else // If the distance between player and target is greater than 4
					myPlayerAttack.bRotateStart = false; // No need to rotate to target
			}else // If player didn't find the target to attack
				myPlayerAttack.bRotateStart = false; // No need to rotate to target	
	    }
		
		character.Move( movement ); // 실제로 플레이어를 향해서 이동.		
	}	
}