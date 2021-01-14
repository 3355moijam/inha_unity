using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2D : MonoBehaviour
{
    public GameObject spawnTarget = null;

    public Spawner2D(string s)
    {
	    spawnTarget = Resources.Load<GameObject>(s);
    }

    public Spawner2D(GameObject target)
	{
        spawnTarget = target;
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(float x, float y)
	{
        Vector3 pos = new Vector3(x, y, 0);
        Instantiate(spawnTarget, pos, Quaternion.Euler(0,0,0));
    }

    public IEnumerable Spawn(Vector2 MinDot, Vector2 MaxDot, float timer)
    {
	    while (true)
	    {
		    Spawn(Random.Range(MinDot.x, MaxDot.x), Random.Range(MinDot.y, MaxDot.y));
		    yield return new WaitForSeconds(timer);
	    }
    }
}
