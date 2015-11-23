using UnityEngine;
using System.Collections;

/// <summary>
/// @Author: Andrew Seba
/// @Description: disables the renderer.
/// </summary>
public class ScriptDisableRenderer : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        GetComponent<Renderer>().enabled = false;
	}
}
