using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obstacleObj = null;
    public float range = 5.0f;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            if (obstacleObj != null)
            {
                transform.position = new Vector3(transform.position.x, Random.Range(-range, range), transform.position.z);
                Instantiate(obstacleObj, transform.position, transform.rotation);
            }

            yield return new WaitForSeconds(1.5f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
