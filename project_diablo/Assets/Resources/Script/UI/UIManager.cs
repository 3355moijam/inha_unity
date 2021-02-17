using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = transform.Find("Inventory").gameObject;
	}

	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            OpenInventory();
    }

    void OpenInventory()
	{
        inventory.SetActive(!inventory.activeSelf);
	}

    
}
