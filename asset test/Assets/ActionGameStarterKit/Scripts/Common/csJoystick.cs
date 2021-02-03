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

[AddComponentMenu("Insaneoops/csJoystick")]
public class csJoystick : MonoBehaviour 
{
	private Transform spriteJoystick;
	private UIAnchor joystickAnchor;	
	private Camera uiCamera;
	private Ray ray;
	private RaycastHit hit;		
	private Vector2 joystickOffset;
	private Vector2 joystickCenter;
	private Vector2 mousePos;
	private bool bIsTouched = false;
	private float offsetValue = 70f;

	private void Start () 
	{
		joystickAnchor = GameObject.Find("Joystick").GetComponent<UIAnchor>();
		joystickCenter = new Vector2(joystickAnchor.relativeOffset.x * Screen.width, joystickAnchor.relativeOffset.y * Screen.height);
		spriteJoystick = GameObject.Find ("SpriteJoystick").transform;
		uiCamera = GameObject.Find ("UICamera").GetComponent<Camera>();
	}

	private void Update () 
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			if(Input.touchCount > 0)
			{
				ray = uiCamera.ScreenPointToRay(Input.mousePosition);
	
				if (Physics.Raycast(ray, out hit, Mathf.Infinity))
				{	
					if (hit.transform.tag == "Joystick")
					{
						Touch touch = Input.GetTouch(0);
						
						if(!bIsTouched )
							bIsTouched = true;
						
						joystickOffset  = touch.position - joystickCenter;
						
						if(joystickOffset.magnitude > offsetValue)
						{
							joystickOffset.Normalize();
							joystickOffset = Vector2.Scale(joystickOffset, new Vector2(offsetValue, offsetValue));
						}
			
						spriteJoystick.localPosition = new Vector3(joystickOffset.x, joystickOffset.y,0);
					}else
					{
						if(bIsTouched)
						{
							bIsTouched = false;
							joystickOffset = Vector2.zero;
							spriteJoystick.localPosition = Vector3.zero;
						}
					}
				}
			}
		}else
		{
			if(Input.GetMouseButton(0))
			{
				ray = uiCamera.ScreenPointToRay(Input.mousePosition);
	
				if (Physics.Raycast(ray, out hit, Mathf.Infinity))
				{	
					if (hit.transform.tag == "Joystick")	
					{				
						mousePos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
						
						if(!bIsTouched)
							bIsTouched = true;
						
						joystickOffset = mousePos - joystickCenter;
						
						if(joystickOffset.magnitude > offsetValue)
						{
							joystickOffset.Normalize();
							joystickOffset = Vector2.Scale(joystickOffset, new Vector2(offsetValue, offsetValue));
						}
						
						spriteJoystick.localPosition = new Vector3(joystickOffset.x,joystickOffset.y,0);
					}
				}
			}else
			{
				if(bIsTouched)
				{
					bIsTouched = false;
					joystickOffset = Vector2.zero;
					spriteJoystick.localPosition = Vector3.zero;
				}
			}
		}
	}
}