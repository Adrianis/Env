using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public float timeSlowFactor = 1;
    private float newTimeScale;

    public int secondsSinceLastEaten;
    public int playerTechLevel;

    public float delayLevelReloadOnDeath = 1;

    private GameObject Display;
    private GameObject Player;
    private GameObject Hoover;

    public int secsTillGameEnd = 360;
    public float secsTillEndGameResume = 6;

	void Start () 
    {
        ChangeTimeScales();
        this.GetComponent<GUIScripts>().enabled = false;

        Display = GameObject.FindGameObjectWithTag("Display");
        Player = GameObject.FindGameObjectWithTag("Player");
        Hoover = GameObject.FindGameObjectWithTag("Hoover");

        StartCoroutine("CheckForGameEnd");
	}

    void OnGUI()
    {
        GUI.Label(new Rect(10, 65, 1000, 20), "Time to survive: " + (secsTillGameEnd - (int)Time.timeSinceLevelLoad));
        GUI.Label(new Rect(10, 85, 1000, 20), "Time till starvation: " + (Player.GetComponent<PlayerController>().timeTillStarve - secondsSinceLastEaten));
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

    public void DisplayEndingText()
    {
        Display.GetComponent<DisplayFade>().SwitchMaterial("OUTRO");
        Display.GetComponent<DisplayFade>().StartFade("IN");
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
        // delay the level reload after death so death message can be read
        yield return new WaitForSeconds(delayLevelReloadOnDeath);
        Application.LoadLevel("EnvTestScene");
    }

    IEnumerator CheckForGameEnd()
    {
        // repeat for duration of game, checking when the survival time has expired, then run the Outro text
        while ((secsTillGameEnd - (int)Time.timeSinceLevelLoad) > 0)
        {
            yield return new WaitForSeconds(1);
        }
        DisplayEndingText();
        StartCoroutine("DelayEndGameResume");
        Player.GetComponent<PlayerController>().canDie = false;
    }

    IEnumerator DelayEndGameResume()
    {
        // fade out the Outro text so the end game can continue
        yield return new WaitForSeconds(secsTillEndGameResume);
        Display.GetComponent<DisplayFade>().StartFade("OUT");
        Hoover.GetComponent<HooverDestruction>().FallApart();
    }
}
