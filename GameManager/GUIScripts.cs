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
        guistyle.normal.textColor = Color.white;
    }
    
    void OnGUI()
    {
        if (bLoading)
        {
            GUI.Label(new Rect(Screen.width / 2 - 400,Screen.height / 2 - 100, 300, 80), "LOADING...", guistyle);
        }
        else if (bDeath)
        {
            GUI.Label(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 100, 300, 80), "DONT " + deathType, guistyle);
        }
    }
}
