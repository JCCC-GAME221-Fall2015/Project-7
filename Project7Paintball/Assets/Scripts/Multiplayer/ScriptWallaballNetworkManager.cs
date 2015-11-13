﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;


/// <summary>
/// @Author: Andrew Seba
/// @Description: Creates spectator entities, and using their connection
/// Sends what team they want to be. Then when everybody is ready it will
/// assign your team per connection id.
/// </summary>
public class ScriptWallaballNetworkManager : NetworkManager {

    
    private GameObject[] blueSpawnPoints;
    public GameObject spectatorPrefab;

    private bool inProgress = false;
    private List<ScriptConnectedPlayer> connectedPlayers = new List<ScriptConnectedPlayer>();
    private List<GameObject> spectators = new List<GameObject>();

    public override void OnServerSceneChanged(string sceneName)
    {
        base.OnServerSceneChanged(sceneName);
        blueSpawnPoints = GameObject.FindGameObjectsWithTag("BlueSpawn");
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if (inProgress)
        {
            return;
        }
        else
        {
            ScriptConnectedPlayer tempPlayer = new ScriptConnectedPlayer(conn, playerControllerId);
            connectedPlayers.Add(tempPlayer);
        }

        var player = (GameObject)GameObject.Instantiate(spectatorPrefab, Vector3.zero, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        spectators.Add(player);
    }

    /// <summary>
    /// Using the temporary connectedPlayers list, goes though and spawns your player.
    /// </summary>
    public void SpawnAllPlayers()
    {

        //Delete temp spectator connections
        foreach(ScriptConnectedPlayer aboutToDestroy in connectedPlayers)
        {
            NetworkServer.DestroyPlayersForConnection(aboutToDestroy.networkConn);
        }
        //Destory temp spectator objects
        foreach(GameObject spec in spectators)
        {
            NetworkServer.Destroy(spec);
        }
        //Spawn All the players
        foreach(ScriptConnectedPlayer aboutToSpawn in connectedPlayers)
        {
            var player = (GameObject)GameObject.Instantiate(playerPrefab, blueSpawnPoints[Random.Range(0, blueSpawnPoints.Length)].transform.position, Quaternion.identity);
            //Set his team
            player.GetComponent<ScriptPlayerTeam>().myTeam = aboutToSpawn.tempTeam;
            NetworkServer.AddPlayerForConnection(aboutToSpawn.networkConn, player, aboutToSpawn.playerControllerId);
        }
        inProgress = true;
    }

    public void SetMyTeam(NetworkConnection pId, Team pTeam)
    {
        foreach(ScriptConnectedPlayer peep in connectedPlayers)
        {
            if(peep.networkConn == pId)
            {
                peep.tempTeam = pTeam;
                break;
            }
        }
    }


}
