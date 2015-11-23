using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ScriptConnectedPlayer
{
    public NetworkConnection networkConn;
    public short playerControllerId;
    public Team tempTeam = Team.SPECTATOR;

    public ScriptConnectedPlayer(NetworkConnection pConn, short id)
    {
        networkConn = pConn;
        playerControllerId = id;
    }
}