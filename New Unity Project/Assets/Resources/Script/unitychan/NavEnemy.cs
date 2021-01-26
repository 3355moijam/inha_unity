using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavEnemy : MonoBehaviour
{
    private NavMeshAgent agent = null;
    private Animator animator = null;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        InvokeRepeating("SetTarget", 0.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
		//DrawGizmos();
		if (agent.remainingDistance < float.Epsilon + agent.speed * Time.deltaTime)
			SetTarget();
	}

    void SetTarget()
	{
        GameObject[] Coins = GameObject.FindGameObjectsWithTag("Coin");
        if (Coins.Length == 0)
        {
            GameObject target = GameObject.Find("Player");
            agent.destination = target.transform.position;
            return;
        }
        SetNearestCoin(Coins);
	}

	private void OnTriggerEnter(Collider other)
	{
        Destroy(other.gameObject);
        Debug.Log("Get Coin");
        Invoke("SetTarget", 0.0f);
	}

	private void OnTriggerExit(Collider other)
	{
	}

	void SetNearestCoin(GameObject[] targets)
	{
        float shortestDist = float.MaxValue;
        NavMeshPath shortestPath = null;
        //Vector3 dest = Vector3.zero;
		foreach (var item in targets)
		{
            NavMeshHit hit;
            NavMesh.SamplePosition(item.transform.position, out hit, 3, -1);
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(hit.position, path);
            float pathDist = GetPathLength(path);
            if (shortestDist > pathDist)
			{
                shortestDist = pathDist;
                shortestPath = path;
                //dest = hit.position;
			}
		}
        //agent.SetDestination(dest);
        agent.SetPath(shortestPath);
        //Debug.Log("Shortest : " + shortestDist.ToString());
	}

    public static float GetPathLength(NavMeshPath path)
    {
        float lng = 0.0f;

        if ((path.status != NavMeshPathStatus.PathInvalid) && (path.corners.Length > 1))
        {
            for (int i = 1; i < path.corners.Length; ++i)
            {
                lng += Vector3.Distance(path.corners[i - 1], path.corners[i]);
            }
            //Debug.Log(lng.ToString());
            return lng;
        }
        else
            return float.MaxValue;

    }

	private void OnDrawGizmos()
	{
		//if (agent.path.corners.Length == 0)
		//	return;
		for (int i = 0; i < agent.path.corners.Length; i++)
		{
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(agent.path.corners[i], 0.3f);

            if(i > 0)
			{
                Gizmos.color = Color.red;
                Gizmos.DrawLine(agent.path.corners[i - 1], agent.path.corners[i]);
            }
		}
	}
}
