using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Matt Gipson
/// Contact: Deadwynn@gmail.com
/// Domain: www.livingvalkyrie.com
/// 
/// Description: PickUp 
/// </summary>
public class PickUp : MonoBehaviour {
    #region Fields

    public Transform pickUpTransform;
    bool hasFlag = false;
    GameObject flagHeld;
    int score = 0;
    #endregion

    void OnTriggerEnter(Collider other) {

        switch (other.tag) {
            case "Flag":
                if (!hasFlag) {
                    flagHeld = other.gameObject;
                    flagHeld.transform.parent = this.transform;
                    flagHeld.transform.position = pickUpTransform.position;
                    hasFlag = true;
                }
                break;
            case "PickUpObject":
                print("picked up " + other.name);
                Destroy(other.gameObject);
                break;
            case "FlagDrop":
                if (hasFlag) {
                    hasFlag = false;
                    Destroy(flagHeld);
                    score++;
                    print("score: " + score);
                }
                break;
        }
    }

}