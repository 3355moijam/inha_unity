using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    ISkill[] mainSkills;
    ISkill mainSkill;
    public ISkill MainSkill { get => mainSkill; }

    static private SkillManager sInstance;
    static public SkillManager Instance
	{
        get
		{
            if (sInstance == null)
            {
                sInstance = GameObject.Find("SkillManager").AddComponent<SkillManager>();
                sInstance.Init();
            }
            return sInstance;
		}
	}

    void Init()
    {
        mainSkills = new ISkill[3];
        mainSkills[0] = GetComponentInChildren<Beams>();
        SetMainSkill(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMainSkill(int num)
    {
        mainSkill = mainSkills[num % mainSkills.Length];
        //useMainSkill = new PlayerAction(selectedMainSkill.OnButton);
    }
}
