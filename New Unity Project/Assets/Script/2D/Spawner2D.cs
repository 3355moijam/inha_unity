using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2D : MonoBehaviour
{
    public GameObject spawnTarget = null;

    Spawner2D(string s)
    {
	    spawnTarget = Resources.Load<GameObject>(s);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn(float x, float y)
	{

	}

    IEnumerable Spawn(Vector2 MinDot, Vector2 MaxDot, float timer)
    {
	    while (true)
	    {
		    Spawn(Random.Range(MinDot.x, MaxDot.x), Random.Range(MinDot.y, MaxDot.y));
		    yield return new WaitForSeconds(timer);
	    }
    }
}
