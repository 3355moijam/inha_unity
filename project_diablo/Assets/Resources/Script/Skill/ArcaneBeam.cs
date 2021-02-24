using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FunctionLibrary;
public class ArcaneBeam : MonoBehaviour, IRune
{
	public GameObject[] effects;
	private GameObject[] instances;
	Transform spawnTransform;
	// Start is called before the first frame update
	void Start()
	{
		instances = new GameObject[3];
		spawnTransform = GameManager.Instance.player.gameObject.transform.Find("Magic Spawn Left");
	}

	// Update is called once per frame
	void Update()
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

	public void Init()
	{

		instances[0] = Instantiate(effects[0], spawnTransform);
		instances[1] = Instantiate(effects[1], spawnTransform);
		instances[2] = Instantiate(effects[2], spawnTransform);
	}

	public void Clear()
	{

	}
}
