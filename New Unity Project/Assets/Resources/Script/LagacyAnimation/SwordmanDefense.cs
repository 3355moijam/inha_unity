using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordmanDefense : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.MaxHP = 100;
        GameManager.Instance.CurrentHP = GameManager.Instance.MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
