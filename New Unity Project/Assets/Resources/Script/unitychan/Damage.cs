using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
	private List<Material> materials = new List<Material>();
	private List<Color> colors = new List<Color>();
	private void Awake()
	{
		Renderer[] renderers = GetComponentsInChildren<Renderer>();
		foreach (Renderer renderer in renderers)
		{
			materials.AddRange(renderer.materials);
		}
		foreach (var item in materials)
		{
			colors.Add(item.color);
		}
	}
	public void Damaged()
	{
		Debug.Log("아파");
		foreach (Material material in materials)
		{
			material.color = Color.red;
		}
		Invoke("RestoreMaterial", 0.0f);
	}

	private void RestoreMaterial()
	{
		for (int i = 0; i < materials.Count; i++)
		{
			materials[i].color = colors[i];
		}
	}
}
