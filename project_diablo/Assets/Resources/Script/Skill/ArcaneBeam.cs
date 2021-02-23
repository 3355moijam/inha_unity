using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FunctionLibrary;
public class ArcaneBeam : MonoBehaviour, IRune
{
	public GameObject[] effect;
	private GameObject[] instance;

	public void CreateEffect(Vector3 position, Vector3 rotation)
	{
		
	}

	public void OnButtonDown()
	{
		GameManager.Instance.player.MoveStop();
		DG.Tweening.DOTween.Kill(GameManager.Instance.player.transform);
		GameManager.Instance.player.animator.SetTrigger("StartBeams");
		//LookHitPoint(hit);
	}

	public void OnButton()
	{
		GameManager.Instance.player.MoveStop();
		GameManager.Instance.player.animator.SetBool("BeamsOn", true);

		//LookHitPoint(hit);
	}

	public void OnButtonUp()
	{
		GameManager.Instance.player.animator.SetBool("BeamsOn", false);

	}

	public bool HasAnimation()
	{
		return true;
	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
