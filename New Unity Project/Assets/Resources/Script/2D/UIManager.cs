using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClickOK()
    {
        Debug.Log("클릭!");
		transform.Find("Popup").gameObject.SetActive(false);
	}

    void PopupActivate()
    {
        Debug.Log("Activate!");
		transform.Find("Popup").gameObject.SetActive(true);
	}
}
