using UnityEngine;
using System.Collections;

/// <summary>
/// @Author Jake Skov
/// @Desc ScriptMatRandomize chooses a random material form an array and copies the data to the default material
/// </summary>
public class ScriptMatRandomize : MonoBehaviour 
{
    public Color[] colors = new Color[3];
    public Material defaultMat;

    private const int RAND_MIN = 0;

	// Use this for initialization
	void Awake () 
    {
        int randNum = Random.Range(RAND_MIN, colors.Length);

        switch (randNum)
        { 
            case 0:
                
                break;

            case 1:
                
                break;

            case 2:
                
                break;
        }

        Debug.Log(this.gameObject.name + " " + colors[randNum]);
	}
}
