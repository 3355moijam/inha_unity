using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Resources.Script;

public class EnemyController : MonoBehaviour, IEnemy
{
    Animation animation = null;
    CharacterController pcControl;
    public int Score { get; } = 10;
    private bool dead = false;
    private float runSpeed = 6.0f;
    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponentInChildren<Animation>();
        pcControl = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
		MoveDown();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Sword")
		{
            StartCoroutine(Dead());
		}
        else if(other.tag == "FinishLine")
		{
            GameManager.Instance.CurrentHP -= Score;
            Destroy(this.gameObject);
        }
	}

    IEnumerator Dead()
	{
        dead = true;
        string animName;
        if (Random.value <= 0.5)
            animName = "die";
        else
            animName = "diehard";
        Debug.Log("Dead");
        animation.CrossFade(animName, 0.3f);
        animation.wrapMode = WrapMode.Once;
        this.GetComponent<Collider>().enabled = false;
        float dTime = animation.GetClip(animName).length - 0.3f;
        GameManager.Instance.Score += Score;

        yield return new WaitForSeconds(dTime);

        Renderer[] childRenderer = GetComponentsInChildren<Renderer>();
        Color[] color = new Color[childRenderer.Length];
		for (int i = 0; i < childRenderer.Length; ++i)
		{
            color[i] = childRenderer[i].material.color;
		}
		while (color[0].a > 0)
		{
            for (int i = 0; i < childRenderer.Length; ++i)
            {
                color[i].a -= 0.01f;
                childRenderer[i].material.SetColor("_Color", color[i]);
            }
            yield return null;
        }

        Destroy(this.gameObject);
    }

    private void MoveDown()
	{
        if (dead)
            return;

        animation.CrossFade("run", 0.3f);
        Vector3 forward = new Vector3(0, 0, -1);
		transform.LookAt(transform.position + forward);

		pcControl.Move(-Vector3.forward * runSpeed * Time.deltaTime + Physics.gravity);
	}
}
