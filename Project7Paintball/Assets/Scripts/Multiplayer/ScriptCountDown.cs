using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScriptCountDown : MonoBehaviour {

    float totalTime;
    float timeLeft;
    ScriptGameManager_References gameManager;
    Text timeLeftText;


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<ScriptGameManager_References>();
        timeLeftText = gameManager.respawnTimeText.GetComponent<Text>();
    }

	public void StartCountDown()
    {
        totalTime = gameManager.respawnTime;
        timeLeft = totalTime;

        timeLeftText.text = string.Format("{0}", timeLeft);
        InvokeRepeating("CountDown", 0, 1);
    }

    void CountDown()
    {
        if(timeLeft > 0)
        {
            timeLeft--;
            timeLeftText.text = string.Format("{0}", timeLeft);
        }
        else
        {
            gameManager.respawnButton.SetActive(true);
            gameManager.respawnPanel.SetActive(false);
            CancelInvoke();
        }
    }

    
}
