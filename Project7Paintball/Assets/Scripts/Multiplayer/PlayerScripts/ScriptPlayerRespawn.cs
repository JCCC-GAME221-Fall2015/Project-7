using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


/// <summary>
/// @Author: Andrew Seba
/// @Description: Enables all the player componenets that were disabled in the
/// ScriptPlayerDeath.
/// </summary>
public class ScriptPlayerRespawn : NetworkBehaviour {

    private Script_PlayerHealth healthScript;
    private Image crossHairImage;
    private GameObject respawnButton;

    private GameObject[] blueSpawnPoints;
    private GameObject[] redSpawnPoints;

    // Use this for initialization
    void Start () {
        blueSpawnPoints = GameObject.FindGameObjectsWithTag("BlueSpawn");
        redSpawnPoints = GameObject.FindGameObjectsWithTag("RedSpawn");

        healthScript = GetComponent<Script_PlayerHealth>();
        healthScript.EventRespawn += EnablePlayer;
        crossHairImage = GameObject.Find("SimpleCrosshairImage").GetComponent<Image>();
        SetRespawnButton();
	}
	
    void SetRespawnButton()
    {
        if (isLocalPlayer)
        {
            respawnButton = GameObject.Find("GameManager").GetComponent<ScriptGameManager_References>().respawnButton;
            respawnButton.GetComponent<Button>().onClick.AddListener(CommenceRespawn);
            respawnButton.SetActive(false);
        }
    }

    void OnDisable()
    {
        healthScript.EventRespawn -= EnablePlayer;
    }

    void EnablePlayer()
    {
        GetComponent<ScriptPlayerShoot>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;

        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in renderers)
        {
            rend.enabled = true;
        }

        if (isLocalPlayer)
        {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = true;
            crossHairImage.enabled = true;
            respawnButton.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        //reset position.
        if (GetComponent<ScriptPlayerTeam>().myTeam == Team.BLUE)
        {

            Transform spawnPosition;
            spawnPosition = blueSpawnPoints[Random.Range(0, blueSpawnPoints.Length)].transform;
            transform.position = spawnPosition.position;
            transform.rotation = spawnPosition.rotation;
        }
        else if (GetComponent<ScriptPlayerTeam>().myTeam == Team.RED)
        {
            Transform spawnPosition;
            spawnPosition = redSpawnPoints[Random.Range(0, redSpawnPoints.Length)].transform;
            transform.position = spawnPosition.position;
            transform.rotation = spawnPosition.rotation;
        }
    }

    void CommenceRespawn()
    {
        CmdRespawnOnServer();
    }

    [Command]
    void CmdRespawnOnServer()
    {
        healthScript.ResetHealth();
    }
}
