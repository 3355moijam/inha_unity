using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSlide : MonoBehaviour
{
    private float speed = 1.5f;
    Material material = null;
    private float xOffset = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        xOffset -= speed * Time.deltaTime;
        material.mainTextureOffset = new Vector2(xOffset, 0);
        material.SetTextureOffset("_BumpMap", new Vector2(xOffset, 0));
    }
}
