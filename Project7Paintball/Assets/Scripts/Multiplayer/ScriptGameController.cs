using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptGameController : MonoBehaviour {

    public List<ScriptFlagCatch> playerFlagCaptures;

    public GameObject redFlag;
    public GameObject blueFlag;

	// Use this for initialization
	void Start () {
        //get the red flag
        if(GameObject.FindGameObjectWithTag("RedFlag"))
        {
            redFlag = GameObject.FindGameObjectWithTag("RedFlag");
        }
        else
        {
            Debug.LogError("No Red Flag in game scene: Add a flag game object to the scene and tag it RedFlag");
        }

        //get the blue flag
        if (GameObject.FindGameObjectWithTag("BlueFlag"))
        {
            blueFlag = GameObject.FindGameObjectWithTag("BlueFlag");
        }
        else
        {
            Debug.LogError("No Blue Flag in game scene: Add a flag game object to the scene and tag it BlueFlag");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CollectPlayers()
    {

    }
}
