using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FunctionLibrary;

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

    [HideInInspector]
    public Player player { get; set; }
    RaycastHit hit;
    public RaycastHit mouseHit { get => hit; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Functions.CheckRaycast(out hit);
    }
}
