using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject obstacleObj = null;
    public float XRange = 0.0f;
    public float YRange = 5.0f;
    public float ZRange = 0.0f;
    public float SpawnTime = 1.5f;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            if (obstacleObj != null)
            {
                Vector3 pos = transform.position + new Vector3(Random.Range(-XRange, XRange), Random.Range(-YRange, YRange), Random.Range(-ZRange, ZRange));
                Instantiate(obstacleObj, pos, transform.rotation);
            }

            yield return new WaitForSeconds(SpawnTime);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
