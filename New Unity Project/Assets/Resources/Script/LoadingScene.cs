using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    AsyncOperation async;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadingNextScene(GameManager.Instance.nextSceneName));
    }

	private IEnumerator LoadingNextScene(string sceneName)
	{
        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;
		while (async.progress < 0.9f && delayTime < 2.0f) // async.isDone 도 있는데 가끔 버그가 있었다고 함.
        //while (!async.isDone)
		{
            // : to do something
            delayTime += Time.deltaTime;
            yield return null;
		}
		async.allowSceneActivation = true;
	}

	// Update is called once per frame
	void Update()
    {
        //DelayTime();
    }

    float delayTime = 0.0f;
	private void DelayTime()
	{
		if (async.progress >= 0.9f)
		{
            delayTime += Time.deltaTime;
            if (delayTime > 2.0f)
                async.allowSceneActivation = true;
		}
	}
}
