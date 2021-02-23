using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    ISkill[] mainSkills;
    ISkill selectedMainSkill;
    static private SkillManager sInstance;
    static public SkillManager Instance
	{
        get
		{
            if(sInstance == null)
                sInstance = GameObject.Find("SkillManager").AddComponent<SkillManager>();
            return sInstance;
		}
	}
    // Start is called before the first frame update
    void Start()
    {
        mainSkills = new ISkill[3];
        mainSkills[0] = GetComponentInChildren<Beams>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMainSkill(int num)
    {
        selectedMainSkill = mainSkills[num % mainSkills.Length];
        //useMainSkill = new PlayerAction(selectedMainSkill.OnButton);
    }
}
