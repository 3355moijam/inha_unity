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

[AddComponentMenu("Insaneoops/csGameManager")]
public class csGameManager : MonoBehaviour 
{
	public static bool bIsBossAlive = false; // if bIsBossAlive is true, boss is alive.
	public static bool bIsPlayerAlive = true; // 카메라는 플레이어를 따라 다니는데, 플레이어가 사망해서 카메라가 플레이어를 찾지 못하는 것을 방지 하기 위해서.
	public static bool bBossSpawnEnabled = true; //if bBossSpawnEnabled is true, boss can be spawned.  // 게임상에 보스가 하나라도 있으면 추가로 스폰되지 않도록 하기 위해서.
	public static bool bIsPlayerTransformation = false;
    public static bool bGameOver = false;
	
	private GameObject player; // Reference for player // 플레이어를 가리킬 변수.
	private Transform playerTransform;
	private Transform myCameraPivot; // Reference for camera pivot point	
	private csPlayerAttack myPlayerAttack; // Reference for player's csPlayerAttack script	
	private csGUI myGUI;
	private csSoundManager mySoundManager;
	private csPlayerWeapon myPlayerWeapon;
	private ArrayList realSpawnPosition = new ArrayList();
	private int myScore = 0; // 몬스터를 물리쳤을 때마다 스코어가 1씩 증가.	
	private bool bPlayerWin = false; // bPlayerWin변수의 값이 참이라면? 플레이어는 승리함.	
	private bool bStartGame = true; // bStartGame변수의 값이 참이라면? 게임을 하기 위해서 카메라가 애니메이션 됨.
    private bool bPlaySound = true; // 게임 플레이동안 한번만 나오므로 초기값으로 true를 줌.
	private float gameStartTime;
	private float gamePresentTime;
	private float elapsedGameTime; // 게임 진행 시간을 실수값으로 표시.

	public int playerLives; // Player's tatal life // 플레이어의 목숨수.
	[System.NonSerialized]
	public Material playerTransformMaterial; // 원래 보스와 다른 머티리얼을 적용해주기 위해서.임시로 씬에 박스를 변신캐릭터의 머티리얼을 적용.
	[System.NonSerialized]
	public string displayGameTime; // 게임 진행 시간을 분, 초로 표시.
	[System.NonSerialized]
	public int summonTime = 10;
	[System.NonSerialized]
	public int goldCount;
	[System.NonSerialized]
	public int maxEnemyCount; //If the number of enemies is greater than maxEnemyCount in game, No more enemy (monster and boss) is allowed. // 게임에서는 maxEnemyCount보다 적은 수만큼의 몬스터만 있을 수 있음.	

	private void Awake ()
	{				
		csCommon.DeleteTempTag (); // 유니티에는 태그버그가 있는 것 같은데 버그를 방지하기 위해서 미리 씬에 모든 태그를 가진 오브젝트를 만들어 놓았다가 게임이 시작되면 초기화 성격으로 없애줌.
		Transform myPlayer = Instantiate (Resources.Load ("Player", typeof(Transform)), new Vector3 (0f, 1.5f, 0f), Quaternion.identity) as Transform;
		myPlayer.name = "Player";
      	GameObject.Find("MonsterSpawnPointCenter").transform.parent = GameObject.Find("Player").transform;
      	GameObject.Find("MonsterSpawnPointCenter").transform.position = GameObject.Find("Player").transform.position + new Vector3(0f, 0.5f, 0f);
		Transform myPetSpawn =  Instantiate (Resources.Load ("Pet", typeof(Transform)), GameObject.Find ("PetSpawnPoint").transform.position, Quaternion.identity) as Transform;
		myPetSpawn.name	= "Pet";
		csCommon.FirstGetIDFromPlayerPref (); // 세팅에서 초기값을 불러와서 캐릭터를 세팅함.
	}

	private void Start () 
	{                              
		InvokeRepeating ("CallPlayThunderSound", 0.1f, 14.5f);
		playerTransformMaterial = GameObject.Find ("BossMaterial").GetComponent<Renderer>().material;
		DestroyImmediate (GameObject.Find ("BossMaterial"));
		player = GameObject.Find ("Player"); // Reference for player
		playerTransform	= player.transform;			
		playerLives	= 3;
		myGUI = this.GetComponent<csGUI>();
		mySoundManager = Camera.main.GetComponent<csSoundManager>();				
		myPlayerAttack = player.GetComponent<csPlayerAttack>();	 // Reference for player's csPlayerAttack script	
		myPlayerWeapon = player.GetComponent<csPlayerWeapon>();
		myCameraPivot = GameObject.Find ("CameraPivot").transform; // Reference for camera pivot point		
		myGUI.labelHighestGameTimer.text = "Highest Gametime : " + CalculateDisplayGameTime (PlayerPrefs.GetFloat ("PlayerHighScore"), displayGameTime) ;
		Invoke ("ReadyToStart", 2.5f); // ReadyToStart 함수를 호출.
	}
	
	private void CallPlayThunderSound ()
	{  
		mySoundManager.PlayGameSound (EGameSound.ThunderSound);
	}
	
	private void ReadyToStart () // ReadyToStart 함수를 호출.
	{
		myGUI.CallDisplayStartAnimation ();
		bStartGame = false;
		Invoke ("GameStart", 5f); // GameStart 함수를 호출.
	}
	
	private void GameStart () // GameStart 함수를 호출.
	{
		InvokeRepeating ("SpawnEnemy", 0.2f, 10.0f); // Every 10 seconds, monster will be spawned // 일정시간마다 몬스터를 스폰함.
		InvokeRepeating ("BossSpawn", 0.2f, 15.0f); // Every 15 seconds, boss will be spawned // 일정시간마다 보스를스폰함.
		gameStartTime = Time.time;
		InvokeRepeating ("GameTime", 0.1f, 0.1f); // 호출되면 현재의 게임시간을 계산해서 표시함.	
	}

	private void Update ()
	{
		if (bStartGame)
		{
			Transform cameraStart = GameObject.Find ("CameraStart").transform;
			Transform cameraOrigin = GameObject.Find ("CameraOrigin").transform;
			Camera.main.transform.position = Vector3.Lerp(cameraStart.position, cameraOrigin.position, Time.time/2.0f);
			Camera.main.transform.rotation = Quaternion.Lerp(cameraStart.rotation, cameraOrigin.rotation, Time.time/2.0f);				
		}	

		if (playerLives <= 0) // If player's life is 0 // 만일 플레이어의 목숨수가 다했다면?.
		{
			CancelInvoke ("GameTime"); // 호출되면 현재의 게임시간을 계산해서 표시함.
			bPlayerWin = false;
         bGameOver = true;
         PlayGameFailedSound();
			ReturnToGameResult ();
		}		

		if (myScore == 10 || myScore == 20 || myScore == 30) 	
			StartCoroutine (LevelUpAnimation ()); 
		
		if (myScore >= 40) // If score is greater than or equal to 40 // 만일 스코어가 100보다 크다면?.
		{
			myScore = 40; // score is 40 // 스코어는 40으로 고정하고.
			
			if (elapsedGameTime < PlayerPrefs.GetFloat("PlayerHighScore") || PlayerPrefs.GetFloat("PlayerHighScore") == 0) // 득점을 갱신했다면 최고 점수를 등록.
				PlayerPrefs.SetFloat("PlayerHighScore", elapsedGameTime);		
			
			bPlayerWin = true;
         bGameOver = true;
         PlayGameWinSound();
			ReturnToGameResult ();
		}
		
		if (Input.GetKeyUp(KeyCode.K )) // for Debug.				
			myScore = 40;	
		else if (Input.GetKeyUp(KeyCode.L )) // for Debug.				
			playerLives = 0;	
	}

   private void PlayGameWinSound ()
   {
      if (bPlaySound)
      {
         bPlaySound = false;
         mySoundManager.PlayGameSound(EGameSound.Win);
      }
   }

   private void PlayGameFailedSound ()
   {
      if (bPlaySound)
      {
         bPlaySound = false;
         mySoundManager.PlayGameSound(EGameSound.Failed);
      }
   }
	
	private void FixedUpdate () 
	{
		if (bIsPlayerAlive)
			myCameraPivot.position = playerTransform.position;
	}	

	public void DisplayScore () // 스코어를 표시함.
	{
		myScore++;
		myGUI.labelScore.text = "Score is : " + myScore.ToString ();		
	}
	
	private void GameTime () // 호출되면 현재의 게임시간을 계산해서 표시함.
	{
		gamePresentTime = Time.time;
		elapsedGameTime = gamePresentTime - gameStartTime;
		myGUI.labelPresentGametimer.text = "Present Gametime : " + CalculateDisplayGameTime (elapsedGameTime, displayGameTime);
	}
	
	public string CalculateDisplayGameTime (float myFloat, string gameTime) // 게임시간을 계산함.
	{
		int minutes = (int)(myFloat / 60f) ; 
		int seconds = (int)(myFloat % 60f) ;
		gameTime = string.Format ("{0:00}:{1:00}", Mathf.Max(0, minutes), Mathf.Max(0, seconds)); 
		return gameTime;
	}

	private void BossSpawn () // Function for boss spawn. // 보스를 스폰함. 
	{
		GameObject[] bossSpawnPoint = GameObject.FindGameObjectsWithTag ("BossSpawnPoint");
		
		if (bBossSpawnEnabled == true) //if bBossSpawnEnabled is true, Boss will be spawned. // 스폰될 몬스터는 특정 숫자로 제한하지만 보스는 죽으면 다시 스폰됨.
		{
			bBossSpawnEnabled = false; // 게임상에 보스가 하나라도 있으면 추가로 스폰되지 않도록 하기 위해서.
			int tempInt = Random.Range (0, 4);
			Transform spawnedBoss =  Instantiate (Resources.Load ("SoulBoss", typeof(Transform)), bossSpawnPoint[tempInt].transform.position, Quaternion.identity) as Transform;
			spawnedBoss.name = "SoulBoss";
			Transform bossWeaponPivot = GameObject.Find ("SoulBoss").transform.Find (csSetting.weaponPivotName).transform;
			Transform bossWeapon =  Instantiate (Resources.Load ("WeaponBoss", typeof(Transform))) as Transform;
			bossWeapon.transform.parent = bossWeaponPivot;
			bossWeapon.transform.position = bossWeaponPivot.position;
			bossWeapon.transform.rotation = bossWeaponPivot.rotation;
			bossWeapon.transform.localScale = new Vector3 (1.1f, 1.1f, 1.1f);
			bossWeapon.name = "WeaponBoss";			
		}
		
		myPlayerAttack.CallFindAllEnemiesInGame (); // Search for all Monsters and Boss in game.  // 게임상에 있는 몬스터와 보스들의 수를 카운트 함.
	}
	
	private void CalculateRandomPositionForMonster () // 총 8개의 몬스터 스폰 위치에서 임의의 5군데를 골라냄.
	{				
		ArrayList fakeSpawnPosition = new ArrayList{0,1,2,3,4,5,6,7}; 
		realSpawnPosition.Clear();	
		
		for (int i = 8; i > 3; i-- ) 
		{
			int tempInt = Random.Range(0, i);
			realSpawnPosition.Add (fakeSpawnPosition[tempInt]);
			fakeSpawnPosition.RemoveAt(tempInt);			
		}			
	}
	
	private void SpawnEnemy () // Function for monster spawn. // 몬스터를 스폰함.
	{
		GameObject[] monsterSpawnPoint = GameObject.FindGameObjectsWithTag ("MonsterSpawnPoint");
		
		if (maxEnemyCount < 11 && !bIsBossAlive) // 현재 게임에서 몬스터의 수가 11를 넘지 않는다면?. // 스폰될 몬스터는 특정 숫자로 제한하지만 보스는 죽으면 다시 스폰됨.
		{
			CalculateRandomPositionForMonster (); // 총 8개의 몬스터 스폰 위치에서 임의의 5군데를 골라냄.

			for (int j = 0; j < 5; j++)
			{
				Transform spawnMonster = Instantiate (Resources.Load ("SoulMonster", typeof(Transform)), monsterSpawnPoint[(int)(realSpawnPosition[j])].transform.position, Quaternion.identity) as Transform;
				spawnMonster.name = "SoulMonster1";
				Transform monsterWeaponPivot = GameObject.Find ("SoulMonster1").transform.Find (csSetting.weaponPivotName).transform;
				Transform monsterWeapon =  Instantiate (Resources.Load ("WeaponMonster", typeof(Transform))) as Transform;
				monsterWeapon.transform.parent = monsterWeaponPivot;
				monsterWeapon.transform.position = monsterWeaponPivot.position;
				monsterWeapon.transform.rotation = monsterWeaponPivot.rotation;
				monsterWeapon.transform.localScale = new Vector3 (1.1f, 1.1f, 1.1f);
				monsterWeapon.name = "WeaponMonster";				
				spawnMonster.name = "SoulMonster";
			}				
		}
		
		myPlayerAttack.CallFindAllEnemiesInGame (); // Search for all Monsters and Boss in game.  // 게임상에 있는 몬스터와 보스들의 수를 카운트 함.
	}

	private IEnumerator LevelUpAnimation () // Show lovel up animation // 레벨업 애니메이션을 보여줌. // 레벨업 애니메이션을 한번만 보여주기 위해서 코루틴으로 작성.
	{
		myGUI.spriteLevelUp.SetActive (true);
		myGUI.spriteLevelUp.GetComponent<Animation>().Play ("UILevelUpAni01");
		yield return new WaitForSeconds (0.5f);
		myGUI.spriteLevelUp.SetActive (false);
	}

	private void ReturnToGameResult () // After 5 seconds, return to result scene // 일정 시간 후에 메뉴로 돌아감.
	{
		CancelInvoke ("SpawnEnemy"); // No more spawn monster // 더 이상 몬스터는 스폰하지 않고.
		CancelInvoke ("GameTime"); // No more spawn boss	
		csCommon.DeleteAllEnemiesInGame (); // 게임이 종료되면 남아있는 몬스터들을 찾아서 없애줌.

		if (bPlayerWin)
		{
			myGUI.spriteGameResult[0].SetActive (true);
			myGUI.spriteGameResult[1].SetActive (false);
		}else
		{
			bIsPlayerAlive = false;
			myGUI.spriteGameResult[0].SetActive (false);
			myGUI.spriteGameResult[1].SetActive (true);			
		}
		
		maxEnemyCount = 0; // SpawnEnemy함수를 호출하기 위해서 초기화.		
		bIsBossAlive = false; // SpawnEnemy함수를 호출하기 위해서 초기화.
		myGUI.labelEnemyCount.text = "";
		myGUI.labelPlayerHealth.text = "";
		myGUI.labelPlayerLives.text = "";	
		Invoke ("ResultPopup", 3f); // 결과화면을 표시하고 게임을 다시 할 것인지, 메뉴로 돌아갈 것인지를 결정함.
	}
	
	private void ResultPopup () // 결과화면을 표시하고 게임을 다시 할 것인지, 메뉴로 돌아갈 것인지를 결정함.
	{
		myGUI.spriteGameResult[0].SetActive (false);
		myGUI.spriteGameResult[1].SetActive (false);			
		myGUI.panelPopup.SetActive (true);
	}
	
	public void QuitGame ()
	{
		Application.Quit ();
	}
	
	public void GoToMainMenu ()
	{
		Invoke ("MainMenu", 1f);
	}
	
	private void MainMenu ()
	{
		Application.LoadLevel ("1MainMenu");
	}
	
	public void GoToMainGame ()
	{	
		Invoke ("MainGame", 1f);
	}	
	
	private void MainGame ()
	{
		Application.LoadLevel ("2MainGame");		
	}
	
	public void PlayerTransformation () // 아직 변신을 하지 않았다면 변신을 함.
	{
		myPlayerAttack.buttonAttack.SetActive (false);
		myPlayerAttack.backgroundButtonRangeAttack.spriteName = "AttackButton";
		myGUI.spriteSummon.SetActive (true);
		myGUI.spriteSummon.GetComponent<Animation>().Play ("UIScaleAni02");
		GameObject[] tempGameObject = GameObject.FindGameObjectsWithTag ("PlayerTransform");
		
		if (tempGameObject.Length > 0)
		{
			for (int i = 0; i < tempGameObject.Length; i++)
			Destroy (tempGameObject[i]);
		}
		
		if (myPlayerWeapon.weaponID == 2)
			myPlayerWeapon.DecideFirstWeapon ();		
		
		Transform spawnedPlayerTransformation =  Instantiate (Resources.Load ("SoulBoss", typeof(Transform)), player.transform.position, player.transform.rotation) as Transform;
		spawnedPlayerTransformation.name = "PlayerTransform";
		spawnedPlayerTransformation.tag = "PlayerTransform";
		spawnedPlayerTransformation.GetComponent<CharacterController>().enabled = false;
		spawnedPlayerTransformation.GetComponent<BoxCollider>().enabled = false;
		spawnedPlayerTransformation.GetComponent<csBoss>().enabled = false;
		spawnedPlayerTransformation.parent = player.transform;
		spawnedPlayerTransformation.position = player.transform.position;
		spawnedPlayerTransformation.gameObject.AddComponent <csPlayerTransformation>();
		SkinnedMeshRenderer[] myPlayerTransfomationBodySkinedMesh = spawnedPlayerTransformation.GetComponentsInChildren<SkinnedMeshRenderer>();

      foreach (SkinnedMeshRenderer body in myPlayerTransfomationBodySkinedMesh)
         body.material = playerTransformMaterial;
		
		bIsPlayerTransformation = true; // bIsPlayerTransformation의 값이 참이라면 이미 게임안에 변신캐릭터가 있음.
		CallGoToOriginalPlayer ();
	}		
	
	public void CallGoToOriginalPlayer () // 변신캐릭터가 변신이 풀려서 원래의 플레이어로 돌아감.
	{
		Invoke ("SpriteSummonSetActiveFalse", 0.5f); // 변신캐릭터가 소환되었다는 이미지를 안보이게 함.
		Invoke ("CheckGoToOriginalPlayer" , summonTime); // 변신캐릭터가 공격도중에 변신이 풀리는 것을 방지하기 위해서. 공격도중이더라도 공격을 받은 몬스터가 데미지를 받게 되면 이상함.
	}	
	
	private void SpriteSummonSetActiveFalse () // 변신캐릭터가 소환되었다는 이미지를 안보이게 함.
	{
		myGUI.spriteSummon.SetActive (false);
	}	
	
	private void CheckGoToOriginalPlayer () // 변신캐릭터가 공격도중에 변신이 풀리는 것을 방지하기 위해서. 공격도중이더라도 공격을 받은 몬스터가 데미지를 받게 되면 이상함.
	{
		if (GameObject.Find ("PlayerTransform").GetComponent<Animation>().IsPlaying (csSetting.bossAttack01Animation) || GameObject.Find ("PlayerTransform").GetComponent<Animation>().IsPlaying (csSetting.bossAttack01Animation))
			Invoke ("GoToOriginalPlayer", 1f);
		else
			GoToOriginalPlayer (); // 변신을 풀고 원래의 플레이어로 돌아감.
	}	
		
	private void GoToOriginalPlayer () // 변신을 풀고 원래의 플레이어로 돌아감.
	{
		myPlayerAttack.buttonAttack.SetActive (true);
		myPlayerAttack.backgroundButtonRangeAttack.spriteName = "RangeAttack";
		GameObject playerTransform = GameObject.Find ("PlayerTransform");
		Transform tempTransform = playerTransform.transform;
		Destroy (playerTransform);
      Transform deathBody = Instantiate(Resources.Load("SoulBossDeadBody", typeof(Transform)), tempTransform.position, tempTransform.rotation) as Transform;
      SkinnedMeshRenderer[] deadBodySkinedMesh = deathBody.GetComponentsInChildren<SkinnedMeshRenderer>();

      foreach (SkinnedMeshRenderer body in deadBodySkinedMesh)
         body.material = playerTransformMaterial;
		
		bIsPlayerTransformation = false;
	}
}