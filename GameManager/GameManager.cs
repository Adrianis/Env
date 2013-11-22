using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public float timeSlowFactor = 1;
    private float newTimeScale;

    public int secondsSinceLastEaten;
    public int playerTechLevel;

    public float delayLevelReloadOnDeath = 1;

    private GameObject Display;

	void Start () 
    {
        ChangeTimeScales();
        this.GetComponent<GUIScripts>().enabled = false;
        Display = GameObject.FindGameObjectWithTag("Display");
	}

    void OnGUI()
    {
        GUI.Label(new Rect(10, 65, 1000, 20), "Time to survive: " + (360 - (int)Time.timeSinceLevelLoad));
        GUI.Label(new Rect(10, 85, 1000, 20), "Time till starvation: " + (60 - secondsSinceLastEaten));
        GUI.Label(new Rect(10, 105, 1000, 20), "Tech Level: " + playerTechLevel);
    }

    public void PlayerDeath(string deathType)
    {
        // activate GUI, tell it the player died & give it the type
        this.GetComponent<GUIScripts>().enabled = true;
        this.GetComponent<GUIScripts>().bDeath = true;
        this.GetComponent<GUIScripts>().deathType = deathType;

        // slow time down for dramatic effect?
        //timeSlowFactor = 10;
        //ChangeTimeScales();

        StartCoroutine("DelayLevelReload");
    }

    public void DisplayLoadingText(bool bIsLoading)
    {
        if (!bIsLoading)
        {
			Display.GetComponent<DisplayFade>().SwitchMaterial("INTRO");
            Display.GetComponent<DisplayFade>().StartFade("OUT");
        }
    }

    private void ChangeTimeScales()
    {
        newTimeScale = Time.timeScale / timeSlowFactor;
        Time.timeScale = newTimeScale;
        Time.fixedDeltaTime = Time.fixedDeltaTime / timeSlowFactor;
        Time.maximumDeltaTime = Time.maximumDeltaTime / timeSlowFactor;
    }

    IEnumerator DelayLevelReload()
    {
        yield return new WaitForSeconds(delayLevelReloadOnDeath);
        Application.LoadLevel("EnvTestScene");
    }
}
