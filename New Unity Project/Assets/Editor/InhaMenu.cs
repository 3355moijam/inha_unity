using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class InhaMenu : MonoBehaviour
{
	[MenuItem("InhaMenu/Clear PlayerPrefs")]
	private static void Clear_PlayerPrefsAll()
	{
		PlayerPrefs.DeleteAll();
		Debug.Log("Clear PlayerPrefs All");
	}

	[MenuItem("InhaMenu/SubMenu/Select")]
	private static void subMenu_Selected()
	{
		Debug.Log("Sub Menu 1 - Selected");
	}

	[MenuItem("InhaMenu/SubMenu/Hotkey Test 1 %#v")]
	private static void subMenu_Hotkey_1()
	{
		Debug.Log("Hotkey Test : Ctrl + Shift + v");
	}

	[MenuItem("Assets/Load Selected Scene")]
	private static void LoadSelectedScene()
	{
		var selected = Selection.activeObject;
		if (EditorApplication.isPlaying)
		{
			EditorSceneManager.LoadScene(AssetDatabase.GetAssetPath(selected));
		}
		else
			EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(selected));
	}
}
