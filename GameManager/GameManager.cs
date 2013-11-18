using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public float timeSlowFactor = 1;
    private float newTimeScale;

    public int secondsSinceLastEaten;

    public float delayLevelReloadOnDeath = 1;

	void Start () 
    {
        ChangeTimeScales();
        this.GetComponent<GUIScripts>().enabled = false;
	}

    void OnGUI()
    {
        GUI.Label(new Rect(10, 65, 1000, 20), "Time to survive: " + (360 - (int)Time.timeSinceLevelLoad));
        GUI.Label(new Rect(10, 85, 1000, 20), "Time till starvation: " + secondsSinceLastEaten);
    }

    public void PlayerDeath(string deathType)
    {
        this.GetComponent<GUIScripts>().enabled = true;
        this.GetComponent<GUIScripts>().bDeath = true;
        this.GetComponent<GUIScripts>().deathType = deathType;
        StartCoroutine("DelayLevelReload");
    }

    public void DisplayLoadingText(bool bIsLoading)
    {
        if (bIsLoading)
        {
            this.GetComponent<GUIScripts>().enabled = true;
            this.GetComponent<GUIScripts>().bLoading = true;
        }
        else
        {            
            this.GetComponent<GUIScripts>().bLoading = false;
            this.GetComponent<GUIScripts>().enabled = false;
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
