using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ScriptTeamBase : NetworkBehaviour
{

    public Material redTeamMat;
    public Material blueTeamMat;
    public GameObject flagPrefab;
    public GameObject baseCollider;
    [SyncVar]
    public bool isFlagHere = true;
    bool currentValue;

    // Use this for initialization
    void Start()
    {
        if (tag == "RedBase" || tag == "BlueBase")
        {
            SetBaseColor(tag);
        }
        else
        {
            Debug.LogError("Base is untagged: Tag the base with RedBase or BlueBase to continue");
        }
        currentValue = isFlagHere;
    }

    void Update()
    {
        if(currentValue != isFlagHere)
        {
            flagPrefab.SetActive(isFlagHere);
            currentValue = isFlagHere;
        }
    }

    public void DisableFlag()
    {
        isFlagHere = false;
    }

    public void ReturnFlag()
    {
        Debug.Log("Returned the flag");
        isFlagHere = true;
    }

    void SetBaseColor(string colorKey)
    {
        Material tempMat = (colorKey == "RedBase") ? redTeamMat : blueTeamMat;
        string tempTag = (colorKey == "RedBase") ? "RedFlag" : "BlueFlag";
        this.GetComponent<Renderer>().material = tempMat;
        flagPrefab.transform.GetChild(0).GetComponent<Renderer>().material = tempMat;
        flagPrefab.transform.tag = tempTag;
        baseCollider.transform.tag = colorKey;
    }

    public void CheckWinCondition()
    {
        if (flagPrefab.activeInHierarchy)
        {
            Debug.Log("A winner is found");
        }
        else
        {
            Debug.Log("Flag is missing for win condition");
        }
    }
}
