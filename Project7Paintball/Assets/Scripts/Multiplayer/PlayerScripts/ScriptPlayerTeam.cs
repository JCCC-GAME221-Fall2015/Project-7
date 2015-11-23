using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public enum Team
{
    BLUE,
    RED,
    DEATHMATCH,
    SPECTATOR
}

/// <summary>
/// @Author: Andrew Seba
/// @Description: Sends your team with buttons to the network manager using
/// your connection.
/// </summary>
public class ScriptPlayerTeam : NetworkBehaviour {

    [SyncVar]
    public Team myTeam = Team.SPECTATOR;

    private GameObject joinBlueButton;
    private GameObject joinRedButton;

    ScriptGameManager_References gameManager;
    ScriptWallaballNetworkManager networkManager;

    void Start()
    {
        networkManager = GameObject.Find("Network Manager").GetComponent<ScriptWallaballNetworkManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<ScriptGameManager_References>();
        if (isLocalPlayer)
        {
            //setup button functions;
            joinBlueButton = gameManager.joinBlueButton;
            joinBlueButton.GetComponent<Button>().onClick.AddListener(SetTeamBlue);
            joinRedButton = gameManager.joinRedButton;
            joinRedButton.GetComponent<Button>().onClick.AddListener(SetTeamRed);
        }
    }

    public void SetTeamBlue()
    {
        myTeam = Team.BLUE;
        CmdSendServerTeam(myTeam);
        EnableStartGameButton();
    }

    public void SetTeamRed()
    {
        myTeam = Team.RED;
        CmdSendServerTeam(myTeam);
        EnableStartGameButton();
    }

    void EnableStartGameButton()
    {
        gameManager.startGameButton.SetActive(true);
    }

    [Command]
    void CmdSendServerTeam(Team pTeam)
    {
        myTeam = pTeam;
        networkManager.SetMyTeam(this.connectionToClient, pTeam);
    }
}
