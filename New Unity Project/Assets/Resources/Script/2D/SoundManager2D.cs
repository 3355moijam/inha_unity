using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager2D: MonoBehaviour
{
    private AudioClip[] audioClips;
    private AudioSource audioSource;

    private static SoundManager2D instance = null;
    public static SoundManager2D Instance
	{
		get
		{
            if (instance == null)
            {
                GameObject newGameObject = new GameObject("_SoundManager");
                instance = newGameObject.AddComponent<SoundManager2D>();
            }
            return instance;
        }
	}

	private void Awake()
	{
        DontDestroyOnLoad(this.gameObject);
    }
	// Start is called before the first frame update
	void Start()
    {

        audioClips[0] = Resources.Load(string.Format("Sound/{0}", "get.wav")) as AudioClip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
