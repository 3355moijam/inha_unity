using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    private int defaultInt = 0;
    private float defaultFloat = 0;
    private string defaultString = "XXXX";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
		{
            if(PlayerPrefs.HasKey("ID"))
			{
                string getID = PlayerPrefs.GetString("ID");
                Debug.Log(string.Format("ID : {0}", getID));
			}
			else
			{
                Debug.Log("ID 없음");
			}
		}
        if(Input.GetKey(KeyCode.B))
		{
            string setID = "PlayerID";
            PlayerPrefs.SetString("ID", setID);
            Debug.Log("Saved : " + setID);
		}
        if (Input.GetKey(KeyCode.C))
        {
            PlayerPrefs.SetInt("Int", 33);
            PlayerPrefs.SetFloat("Float", 44.4f);

            int getInt = PlayerPrefs.GetInt("Int");
            float getFloat = PlayerPrefs.GetFloat("Float");

            Debug.Log(getInt.ToString());
            Debug.Log(getFloat.ToString());


        }
        if (Input.GetKey(KeyCode.D))
        {
            int getInt = PlayerPrefs.GetInt("Int2", defaultInt);
            float getFloat = PlayerPrefs.GetFloat("Float2", defaultFloat);
            string getString = PlayerPrefs.GetString("ID2", defaultString);


            Debug.Log(getInt.ToString());
            Debug.Log(getFloat.ToString());
            Debug.Log(getString);
        }

        if (Input.GetKey(KeyCode.F))
        {
            PlayerPrefs.DeleteKey("ID");
            PlayerPrefs.DeleteAll();

        }
        if (Input.GetKey(KeyCode.Z))
        {
            PlayerPrefs.Save();
            //player prefs는 1메가가 넘으면 안된다.
        }
    }
}
