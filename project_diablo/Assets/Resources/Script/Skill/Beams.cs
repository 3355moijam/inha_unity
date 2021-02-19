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

	public void OnButtonDown(Animator animator, Vector3 position, Vector3 rotation)
	{
		selectedRune.OnButtonDown(animator, position, rotation);
	}

	public void OnButton(Animator animator)
	{
		selectedRune.OnButton(animator);
	}

	public void OnButtonUp(Animator animator)
	{
		selectedRune.OnButtonUp(animator);
	}

	public void SetRune(int num)
	{
		selectedRune = runes[num % runes.Length];
	}
}
