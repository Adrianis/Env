using UnityEngine;
using System.Collections;

public class GUIScripts : MonoBehaviour {

    public bool bLoading;
    public bool bDeath;
    public string deathType;

    private GUIStyle guistyle;

    void Start()
    {
        guistyle = new GUIStyle();
        guistyle.fontSize = 100;
    }
    
    void OnGUI()
    {
        if (bLoading)
        {
            GUI.Label(new Rect(400, 130, 10000, 100), "LOADING...", guistyle);
        }
        else if (bDeath)
        {
            GUI.Label(new Rect(400, 130, 10000, 100), "DONT " + deathType, guistyle);
        }
    }
}
