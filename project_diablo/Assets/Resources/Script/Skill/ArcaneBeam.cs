using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FunctionLibrary;
public class ArcaneBeam : MonoBehaviour, IRune
{
	public GameObject effect;
	private GameObject instance;
	Transform spawnTransform;
	// Start is called before the first frame update
	void Start()
	{
		spawnTransform = GameObject.Find("Magic Spawn Pos").transform;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnButtonDown()
	{
		GameManager.Instance.player.MoveStop();
		//DG.Tweening.DOTween.Kill(GameManager.Instance.player.transform);
		GameManager.Instance.player.animator.SetTrigger("StartBeams");
		//LookHitPoint(hit);
	}

	public void OnButton()
	{
		//GameManager.Instance.player.UseMana
		
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

	public void Init()
	{
		if (instance != null)
			return;

		instance = Instantiate(effect, spawnTransform);
		instance.transform.SetParent(spawnTransform);
	}

	public void Clear()
	{
		Destroy(instance);
		instance = null;
	}
}
