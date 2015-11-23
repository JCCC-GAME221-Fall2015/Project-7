using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

//@author Mike Dobson

public class ScriptBaseCollider : NetworkBehaviour {

    [Command]
    void CmdReturnFlag()
    {
        GetComponentInParent<ScriptTeamBase>().ReturnFlag();
    }

    void OnTriggerEnter(Collider other)
    {
        //if the player collides with me
        if(other.tag == "Player")
        {
            //check to see which team the player is on and switch
            switch(other.GetComponent<ScriptPlayerTeam>().myTeam)
            {
                //if the player is on the red team
                case Team.RED:
                    //and is at the red base 
                    if(tag == "RedBase")
                    {
                        //check if he is carrying the blue flag and switch
                        switch(other.GetComponent<ScriptFlagCatch>().carryBlueFlag)
                        {
                            //if the player is carrying the blue flag at the red base and is on the red team check for win
                            case true:
                                GetComponentInParent<ScriptTeamBase>().CheckWinCondition();
                                break;
                            //if the player is carrying the red flag at the red base and is on the red team return the flag
                            case false:
                                GetComponentInParent<ScriptTeamBase>().ReturnFlag();
                                CmdReturnFlag();
                                Debug.Log("Command Sent");

                                other.GetComponent<ScriptFlagCatch>().SetFlagCarry(false);
                                break;
                        }
                    }
                    break;
                //if the player is on the blue team
                case Team.BLUE:
                    //and is at the blue base
                    if(tag == "BlueBase")
                    {
                        //check if the player is carrying the blue flag and switch
                        switch (other.GetComponent<ScriptFlagCatch>().carryBlueFlag)
                        {
                            //if the player is carrying the blue flag at the blue base and is on the blue team return the flag
                            case true:
                                GetComponentInParent<ScriptTeamBase>().ReturnFlag();
                                other.GetComponent<ScriptFlagCatch>().SetFlagCarry(false);
                                break;
                            //if the player is carrying the red flag at the blue base and is on the blue team check for win
                            case false:
                                GetComponentInParent<ScriptTeamBase>().CheckWinCondition();
                                break;
                        }
                    }
                    break;
            }
        }
    }
}
