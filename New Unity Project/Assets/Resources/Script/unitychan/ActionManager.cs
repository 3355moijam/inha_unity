using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    private IAction curAction;
    public void StartAction(IAction newAction)
	{
        if (curAction == newAction) 
            return;

        if (curAction != null)
            curAction.End();
        curAction = newAction;
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
