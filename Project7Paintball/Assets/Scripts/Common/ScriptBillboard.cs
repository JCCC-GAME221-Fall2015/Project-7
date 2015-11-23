using UnityEngine;


/// <summary>
/// @Author: Andrew Seba
/// @Description: Makes sure the transform is facing the camera at all times.
/// </summary>
public class ScriptBillboard : MonoBehaviour
{

    public Transform playerCamera;

    void Start()
    {
        playerCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCamera != null)
        {
            Vector3 dir = playerCamera.forward;
            dir.y = 0.0f;
            transform.rotation = Quaternion.LookRotation(dir);
        }
        else
        {
            playerCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }

    }
}
