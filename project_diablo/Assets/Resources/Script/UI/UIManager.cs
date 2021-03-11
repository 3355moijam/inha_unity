using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameObject inventory;
    Image ManaBall;
    Image HPBall;
    // Start is called before the first frame update
    void Start()
    {
        inventory = transform.Find("Inventory").gameObject;
        ManaBall = GameObject.Find("ManaBall").GetComponent<Image>();
        HPBall = GameObject.Find("HPBall").GetComponent<Image>();
    }

	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            OpenInventory();
        SetHPAndMP();
    }

    void OpenInventory()
	{
        inventory.SetActive(!inventory.activeSelf);
	}
    
    void SetHPAndMP()
	{
        HPBall.fillAmount = GameManager.Instance.player.CurrentHP / GameManager.Instance.player.MaxHP;
        ManaBall.fillAmount = GameManager.Instance.player.CurrentMP / GameManager.Instance.player.MaxMP;
    }
    
}
