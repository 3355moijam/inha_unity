using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerOnNav : MonoBehaviour   
{
    //NavMeshAgent agent = null;
    public GameObject target = null;
    public float XRange = 0;
    public float YRange = 0;
    public float ZRange = 0;
    public float SpawnTime = 10;
    // Start is called before the first frame update
    void Start()
    {
		//agent = GetComponent<NavMeshAgent>();
		StartCoroutine(Spawn());
        Random.InitState((int)Time.realtimeSinceStartup);   

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
	{
        NavMeshPath path = new NavMeshPath();
        NavMeshHit hit;
        while (true)
		{
            Vector3 pos;
			for (int i = 0; i < 5; i++)
			{
                
			    pos = transform.position + new Vector3(Random.Range(-XRange, XRange), Random.Range(-YRange, YRange), Random.Range(-ZRange, ZRange));
                NavMesh.SamplePosition(pos, out hit, 3, -1);
                pos = hit.position;
                pos.y = transform.position.y;
			    Instantiate(target, pos, transform.rotation);
			}

            yield return new WaitForSeconds(SpawnTime);
		}
    }
}
