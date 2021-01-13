using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScript : MonoBehaviour
{
    public int SceneID;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.nSceneID = SceneID;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
