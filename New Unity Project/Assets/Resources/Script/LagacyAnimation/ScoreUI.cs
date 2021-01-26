using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreUI : MonoBehaviour
{
    private Text text = null;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        SetScore();
    }

    void SetScore()
	{
        text.text = GameManager.Instance.Score.ToString();
	}
}
