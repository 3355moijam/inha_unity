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

[AddComponentMenu("Insaneoops/csGUI")]
public class csGUI : MonoBehaviour 
{
	private GameObject[] allPanel;
	private csPlayerAttack myPlayerAttack;
	private UIButtonMessage[] buttonAttack;
	private UIButtonMessage buttonRangeAttack;	
	private int resolutionWidth;
	private int resolutionHeight;	
	
	public GameObject[] spriteStart;
	public GameObject[] spriteGameResult;
	public GameObject[] labelInfomation;
	public GameObject spriteLevelUp; // 레벨업이 되었을 때 보여줄 스프라이트 게임 오브젝트.
	public GameObject panelPopup;
	public GameObject spriteSummon;
	[System.NonSerialized]
	public UILabel labelBulletCount; // Dubbuging purpose, display bullet count, if player consume all bullets, player's weapon will be changed to default weapon.	
	[System.NonSerialized]
	public UILabel labelHighestGameTimer; 
	[System.NonSerialized]
	public UILabel labelPresentGametimer;	// 몬스터를 물리쳤을 때마다 스코어가 1씩 증가하는 스코어 판.	
	[System.NonSerialized]
	public UILabel labelScore; 
	[System.NonSerialized]
	public UILabel labelGoldCount;
	[System.NonSerialized]
	public UILabel labelPlayerHealth;	
	[System.NonSerialized]
	public UILabel labelPlayerLives;
	[System.NonSerialized]
	public UILabel labelEnemyCount;	

	private void Awake ()
	{
		buttonAttack = GameObject.Find ("ButtonAttack").GetComponentsInChildren<UIButtonMessage>();
		buttonRangeAttack = GameObject.Find ("ButtonRangeAttack").GetComponent<UIButtonMessage>();	
		labelBulletCount = GameObject.Find ("LabelBulletCount").GetComponent<UILabel>(); // Dubbuging purpose, display bullet count, if player consume all bullets, player's weapon will be changed to default weapon.		
		labelBulletCount.text = ""; // Dubbuging purpose, display bullet count, if player consume all bullets, player's weapon will be changed to default weapon.
		labelHighestGameTimer = GameObject.Find ("LabelHighestGameTimer").GetComponent<UILabel>();
		labelPresentGametimer = GameObject.Find ("LabelPresentGameTimer").GetComponent<UILabel>();
		labelScore = GameObject.Find ("LabelScore").GetComponent<UILabel>();
		labelGoldCount = GameObject.Find ("LabelGoldCount").GetComponent<UILabel>();
		labelGoldCount.color = Color.yellow;
		labelPlayerHealth = GameObject.Find ("LabelPlayerHealth").GetComponent<UILabel>();	
		labelPlayerLives = GameObject.Find ("LabelPlayerLives").GetComponent<UILabel>();
		labelEnemyCount = GameObject.Find ("LabelEnemyCount").GetComponent<UILabel>();			
	}
	
	private void Start () // Use this for initialization
	{
		myPlayerAttack = GameObject.Find ("Player").GetComponent<csPlayerAttack>();
		
		for (int i = 0; i < spriteStart.Length; i++)
			spriteStart[i].SetActive (false);
		
		for (int i = 0; i < spriteGameResult.Length; i++)
			spriteGameResult[i].SetActive (false);
		
		for (int i = 0; i < labelInfomation.Length; i++)
			labelInfomation[i].SetActive (false);
		
		panelPopup.SetActive (false);
		spriteLevelUp.gameObject.SetActive (false);
		
		for (int i = 0; i < buttonAttack.Length; i++)
			buttonAttack[i].target = myPlayerAttack.gameObject;
		
		buttonRangeAttack.target = myPlayerAttack.gameObject;	
		allPanel = GameObject.FindGameObjectsWithTag ("Panel");	
		resolutionWidth = Screen.width;
		resolutionHeight = Screen.height;
		AdjustResolution ();		
	}

	private void Update () // Update is called once per frame 
	{
		labelEnemyCount.text = "EnemyCount is : " + myPlayerAttack.allTargetsInGame.Count;
		
		if (Screen.width != resolutionWidth || Screen.height != resolutionHeight)
			AdjustResolution ();				
	}
	
	public void DisplayGoldCount ( int myInt ) // 골드아이템을 획득한 점수를 표시.
	{
		labelGoldCount.text = "" + myInt;
	}	

	public void CallDisplayStartAnimation () // DisplayStartAnimation 코루틴을 호출.
	{
		StartCoroutine (DisplayStartAnimation()); 
	}
	
	private IEnumerator DisplayStartAnimation () // DisplayStartAnimation 코루틴을 호출.
	{
		for (int i = 0; i < spriteStart.Length; i++)
		{
			spriteStart[i].SetActive (true);
			yield return new WaitForSeconds (1.0f);
			spriteStart[i].SetActive (false);
		}
		
		for (int j = 0; j < labelInfomation.Length; j++) 
		{
			yield return new WaitForSeconds (0.1f);
			labelInfomation[j].SetActive (true);
			labelInfomation[j].GetComponent<Animation>().Play ("UIScaleAni01");
		}		
	}
	
	private void AdjustResolution ()
	{
		csCommon.FreeResolution (allPanel);
		resolutionWidth = Screen.width;
		resolutionHeight = Screen.height;			
	}	
}