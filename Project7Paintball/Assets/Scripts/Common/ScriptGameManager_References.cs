using UnityEngine;
using System.Collections;

/// <summary>
/// @Author: Andrew Seba
/// @Description: Holds references for buttons and respawn settings.
/// </summary>
public class ScriptGameManager_References : MonoBehaviour {

    [Header("Respawn Settings")]
    public float respawnTime = 5.0f;

    [Header("Respawn Fields")]
    public GameObject respawnButton;
    public GameObject startGameButton;
    public GameObject respawnPanel;
    public GameObject respawnTimeText;

    [Header("Particle Effect")]
    public GameObject splatParticle;

    [Header("Team Join Buttons")]
    public GameObject joinBlueButton;
    public GameObject joinRedButton;



    [HideInInspector]
    public ScriptWallaballNetworkManager manager;

    void Start()
    {
        if (GameObject.Find("Network Manager") != null)
            manager = GameObject.Find("Network Manager").GetComponent<ScriptWallaballNetworkManager>();
        else
            Debug.Log("You need to start the game in the title scene.");
        
    }

    public void _SpawnAllPlayers()
    {
        manager.SpawnAllPlayers();
    }

}
