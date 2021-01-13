using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    public float jumpPower = 5.0f;
    public int Score = 0;

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance.Score = Score;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
		{
            GetComponent<Rigidbody>().velocity = new Vector3(0, jumpPower, 0);
		}

        if (transform.position.y < -6.5 || transform.position.y > 6.5)
            GameOver();
    }
    
    private void GameOver()
	{
        GameManager.Instance.Score = Score;
        SceneManager.LoadScene("FlappyOver");
	}

	private void OnCollisionEnter(Collision collision)
	{
        GameOver();
	}

	private void OnTriggerExit(Collider other)
	{
        Score++;
	}

	private void OnGUI()
	{
        GUI.Box(new Rect(Screen.width * 0.5f - 50, 0, 100, 25), "Score: " + Score);
	}


}
