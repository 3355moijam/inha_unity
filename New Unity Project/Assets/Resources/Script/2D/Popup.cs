using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Popup : MonoBehaviour
{

    public GameObject textobj = null;
    public InputField inputText = null;
    private Text titleText;
    private Toggle toggleTest;
    // Start is called before the first frame update
    void Start()
    {
        //textobj.GetComponent<Text>().text = "뷁";
        titleText = textobj.GetComponent<Text>();
        titleText.text = "팝업 타이틀";

        toggleTest = transform.Find("BGMToggle").GetComponent<Toggle>();
	}

    // Update is called once per frame
    void Update()
	{

	}



    void OnTextChanged()
	{
        titleText.text = inputText.text;
	}

    void OnTextEditEnd()
	{
        titleText.text = inputText.text;
	}

    void OnToggleTest()
	{
        if(toggleTest.isOn)
		{
            Debug.Log("toggle on");
            GameObject.Find("BGM").GetComponent<AudioSource>().mute = false;
            //toggleTest.isOn = false;
		}
        else
		{
            Debug.Log("toggle off");
            GameObject.Find("BGM").GetComponent<AudioSource>().mute = true;
            //toggleTest.isOn = true;
        }
	}
    
}