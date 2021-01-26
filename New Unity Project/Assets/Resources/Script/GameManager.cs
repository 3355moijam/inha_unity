using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager sInstance;
    public static GameManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject newGameObject = new GameObject("_GameManager");
                sInstance = newGameObject.AddComponent<GameManager>();
            }
            return sInstance;
        }
    }

    public string nextSceneName;
    public int Score { get; set; } = 0;
    public int MaxHP { get; set; } = 100;
    public int CurrentHP { get; set; } = 100;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

	// =====================================

	public int nSceneID = -1;
    private void OnGUI()
    {
        //GUI.Box(new Rect(110, 100, 150, 30), "시작 버튼을 누르시오");
        //if (nSceneID == 0)
        //{
        //    if (GUI.Button(new Rect(200, 140, 50, 30), "시작"))
        //    {
        //        ChangeScene();
        //    }
        //}
    }

    private void ChangeScene()
    {
        Debug.Log("씬전환");
        SceneManager.LoadScene("AutoCarAI");


        //List<GameObject> objectList = new List<GameObject>();
        //SceneManager.GetActiveScene().GetRootGameObjects(objectList);
    }
}
