using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastExperience : MonoBehaviour
{
    [Range(0, 50)]
    public float distance = 10.0f;

    private RaycastHit[] rayHits;
    private Ray ray;
    // Start is called before the first frame update
    void Start()
    {
        ray = new Ray(this.transform.position, this.transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        Ray_1_3();
    }

	void Ray_1_1()
	{
        if(Physics.Raycast(ray, out rayHits[0], distance))
		{
            Debug.Log(rayHits[0].collider.gameObject.name);
		}
	}

    void Ray_1_2()
    {
        rayHits = Physics.RaycastAll(ray, distance);
		for (int i = 0; i < rayHits.Length; ++i)
		{
            Debug.Log(rayHits[i].collider.gameObject.name + " Hit");
		}
    }

    void Ray_1_3()
	{
        rayHits = Physics.RaycastAll(ray, distance);
        for (int i = 0; i < rayHits.Length; ++i)
        {
            if(rayHits[i].collider.gameObject.tag == "Enemy")
                Debug.Log(rayHits[i].collider.gameObject.name + " is Enemy");

            if (rayHits[i].collider.gameObject.layer == LayerMask.NameToLayer("Test3"))
                Debug.Log(rayHits[i].collider.gameObject.name + " is on Test3");
        }
    }


    private void OnDrawGizmos()
	{
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
	}
}
