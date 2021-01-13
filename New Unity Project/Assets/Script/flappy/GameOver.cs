using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnGUI()
	{
        GUI.Box(new Rect(Screen.width * 0.5f - 300, Screen.height * 0.5f - 200, 600, 400), "Score\n" + GameManager.Instance.Score);
        if (GUI.Button(new Rect(Screen.width * 0.5f - 200, Screen.height * 0.5f - 100, 400, 50), "다시시작"))
		{
            SceneManager.LoadScene("04Prefab");
		}
	}
}
