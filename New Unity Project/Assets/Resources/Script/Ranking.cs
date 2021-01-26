using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    private string defaultID = "XXXX";
    private int defaultScore = 0;
    private List<ScoreData> rankingList = new List<ScoreData>();
    private struct ScoreData
	{
        public string ID;
        public int Score;

        public ScoreData(string id, int score)
		{
            ID = id; Score = score;
		}
        public static bool operator >(ScoreData a, ScoreData b) => a.Score > b.Score;
        public static bool operator <(ScoreData a, ScoreData b) => a.Score < b.Score;
        public static bool operator >=(ScoreData a, ScoreData b) => a.Score >= b.Score;
        public static bool operator <=(ScoreData a, ScoreData b) => a.Score <= b.Score;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("Top1ID"))
		{
            rankingList.Add(new ScoreData(PlayerPrefs.GetString("Top1ID"), PlayerPrefs.GetInt("Top1Score")));
		}
        if (PlayerPrefs.HasKey("Top2ID"))
        {
            rankingList.Add(new ScoreData(PlayerPrefs.GetString("Top2ID"), PlayerPrefs.GetInt("Top2Score")));
        }
        if (PlayerPrefs.HasKey("Top3ID"))
        {
            rankingList.Add(new ScoreData(PlayerPrefs.GetString("Top3ID"), PlayerPrefs.GetInt("Top3Score")));
        }
        if (PlayerPrefs.HasKey("NewID"))
        {
            rankingList.Add(new ScoreData(PlayerPrefs.GetString("NewID"), PlayerPrefs.GetInt("NewScore")));
        }
        //if(rankingList.Orderby)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
