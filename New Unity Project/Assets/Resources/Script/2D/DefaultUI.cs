using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DefaultUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Image hpbar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowHPbar();
    }

    public void ShowHPbar()
	{
        hpbar.fillAmount = (float) GameManager.Instance.CurrentHP / (float) GameManager.Instance.MaxHP;
	}
}
