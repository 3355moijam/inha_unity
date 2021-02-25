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
		((ISkill)this).SetRune(0);
    }

    // Update is called once per frame
    void Update()
    {
    }

	void ISkill.OnButtonDown()
	{
		selectedRune.OnButtonDown();
		//Debug.Log("");
	}

	void ISkill.OnButton()
	{
		selectedRune.OnButton();
	}

	void ISkill.OnButtonUp()
	{
		selectedRune.OnButtonUp();
	}

	void ISkill.SetRune(int num)
	{
		selectedRune = runes[num % runes.Length];
	}

	bool ISkill.HasAnimation() => selectedRune.HasAnimation();

	void ISkill.Init() => selectedRune.Init();

	void ISkill.Clear() => selectedRune.Clear();
}
