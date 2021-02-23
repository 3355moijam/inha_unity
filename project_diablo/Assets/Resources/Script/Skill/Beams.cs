using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beams : MonoBehaviour, ISkill
{
	private IRune[] runes;
	private IRune selectedRune;
	// Start is called before the first frame update
	void Start()
    {
		runes = new IRune[3];
		runes[0] = GetComponentInChildren<ArcaneBeam>();
		SetRune(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OnButtonDown()
	{
		selectedRune.OnButtonDown();
		//Debug.Log("");
	}

	public void OnButton()
	{
		selectedRune.OnButton();
	}

	public void OnButtonUp()
	{
		selectedRune.OnButtonUp();
	}

	public void SetRune(int num)
	{
		selectedRune = runes[num % runes.Length];
	}

	public bool HasAnimation()
	{
		return selectedRune.HasAnimation();
	}
}
