using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// Author: Andrew Seba
/// Description: Controls health updates
/// </summary>
public class Script_PlayerHealth : NetworkBehaviour {

    [SyncVar(hook = "OnHealthChange")]
    private int health = 100;
    private Text healthText;
    public GameObject healthBar;

    private bool shouldDie = false;
    public bool isDead = false;

    public delegate void DieDelegate();
    public event DieDelegate EventDie;

    public delegate void RespawnDelegate();
    public event RespawnDelegate EventRespawn;

    void Start()
    {
        healthText = GameObject.Find("Health Text").GetComponent<Text>();
        SetHealthText();
    }
	
	// Update is called once per frame
	void Update () {
        CheckCondition();
        SetHealthText();
    }

    void CheckCondition()
    {
        if(health <= 0 && !shouldDie && !isDead)
        {
            shouldDie = true;
        }

        if(health <= 0 && shouldDie)
        {
            if(EventDie != null)
            {
                EventDie();
            }

            shouldDie = false;
        }

        if(health > 0 && isDead)
        {
            if(EventRespawn != null)
            {
                EventRespawn();
            }

            isDead = false;
        }
    }

    void SetHealthText()
    {
        Vector2 tempSize = new Vector2((health / 100f), 1);
        if (isLocalPlayer)
        {
            healthText.text = "Health: " + health.ToString();
            CmdUpdateHealthBar(tempSize);
        }
        
        healthBar.transform.localScale = tempSize;
        
    }

    [Command]
    void CmdUpdateHealthBar(Vector2 tempAnchor)
    {
        healthBar.transform.localScale = tempAnchor;
    }

    public void DeductHealth(int dmg)
    {
        health -= dmg;
    }

    void OnHealthChange(int hlth)
    {
        health = hlth;
        SetHealthText();
    }

    public void ResetHealth()
    {
        health = 100;
    }
}
