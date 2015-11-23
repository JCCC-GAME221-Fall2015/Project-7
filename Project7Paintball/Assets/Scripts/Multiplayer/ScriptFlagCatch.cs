using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/// <summary>
/// @Author: Mike
/// </summary>
public class ScriptFlagCatch : NetworkBehaviour
{

    [SyncVar]
    public bool isCarryFlag = false;
    public GameObject carryFlag;
    public Material redFlag;
    public Material blueFlag;
    public GameObject flagPrefab;

    public GameObject blueBase;
    public GameObject redBase;

    [SyncVar]
    public bool carryBlueFlag = false;

    string colorKey;
    bool currentCarryFlag;
    // Use this for initialization
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("BlueBase"))
        {
            blueBase = GameObject.FindGameObjectWithTag("BlueBase");
        }
        else
        {
            Debug.LogError("No Blue Base in game scene: Add a game object to the scene and tag it BlueBase");
        }

        if (GameObject.FindGameObjectWithTag("RedBase"))
        {
            redBase = GameObject.FindGameObjectWithTag("RedBase");
        }
        else
        {
            Debug.LogError("No Red Base in game scene: Add a game object to the scene and tag it RedBase");
        }
        currentCarryFlag = isCarryFlag;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentCarryFlag != isCarryFlag)
        {
            carryFlag.SetActive(isCarryFlag);
            currentCarryFlag = isCarryFlag;
        }


        if (colorKey == "RedFlag")
        {
            carryFlag.transform.GetChild(0).GetComponent<Renderer>().material = redFlag;
        }
        else
        {
            carryFlag.transform.GetChild(0).GetComponent<Renderer>().material = blueFlag;
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (!isCarryFlag)
        {
            if (other.transform.parent != null)
            {
                if (other.transform.parent.tag == "BlueFlag" || other.transform.parent.tag == "RedFlag")
                {
                    Debug.Log("Hit the " + other.transform.parent.tag);
                    other.transform.parent.transform.parent.GetComponent<ScriptTeamBase>().DisableFlag();
                    CmdSendIpickedupMessege(other.transform.parent.transform.parent.gameObject);
                    SetFlagCarry(true);
                    SetFlagColor(other.transform.parent.tag);
                    CmdSetFlagColor(other.transform.parent.tag);

                    if (other.transform.parent.tag == "BlueFlag")
                    {
                        carryBlueFlag = true;
                    }
                    else
                    {
                        carryBlueFlag = false;
                    }
                    CmdSetCarryBlueFlagBool(carryBlueFlag);

                }
            }
        }
    }

    [Command]
    void CmdSetFlagColor(string pTag)
    {
        SetFlagColor(pTag);
        colorKey = pTag;
    }

    [Command]
    void CmdSendIpickedupMessege(GameObject obj)
    {
        obj.GetComponent<ScriptTeamBase>().DisableFlag();
    }

    [Command]
    void CmdSetCarryBlueFlagBool(bool pValue)
    {
        carryBlueFlag = pValue;
    }

    public void SetFlagCarry(bool state)
    {
        Debug.Log("Setting the player flag to " + state);
        isCarryFlag = state;
        CmdSetCaryFlag(isCarryFlag);

        if (carryFlag != null)
        {
            //Maybe just change to carryFlag.SetActive(state); -Seba
            if (state)
            {
                carryFlag.SetActive(true);
            }
            else
            {
                carryFlag.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("Flag object not set on player");
        }
    }

    [Command]
    void CmdSetCaryFlag(bool pValue)
    {
        isCarryFlag = pValue;
    }

    public void SetFlagColor(string colorKey)
    {
        if (colorKey == "RedFlag")
        {
            carryFlag.transform.GetChild(0).GetComponent<Renderer>().material = redFlag;
        }
        else
        {
            carryFlag.transform.GetChild(0).GetComponent<Renderer>().material = blueFlag;
        }

    }

    public void DropFlag()
    {
        //Instantiate(flagPrefab, transform.position, Quaternion.identity);
    }
}
