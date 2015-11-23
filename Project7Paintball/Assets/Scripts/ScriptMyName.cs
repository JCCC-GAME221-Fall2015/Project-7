using UnityEngine;
using UnityEngine.UI;
using System.Collections;


/// <summary>
/// @Author: Andrew Seba
/// </summary>
public class ScriptMyName : MonoBehaviour {

    [Tooltip("Default Name")]
    public string myName = "Seba";

    public void _SetName(GameObject inputField)
    {
        myName = inputField.GetComponent<Text>().text;
    }
}
