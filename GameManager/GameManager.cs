using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public bool hasGameEnded = false;

    public float timeSlowFactor = 1;
    private float newTimeScale;

    public int secondsSinceLastEaten;
    public int playerTechLevel;

    public float delayLevelReloadOnDeath = 1;

    private GameObject Display;
    private GameObject Player;
    private GameObject Hoover;

    public int secsTillGameEnd;
    public float secsTillEndGameResume = 6;

    private Texture2D guiStarveTex;
    private GUIStyle guiStarveStyle;
    private Texture2D guiSurviveTex;
    private GUIStyle guiSurviveStyle;
    private Texture2D guiUpgradeTex;
    private GUIStyle guiUpgradeStyle;


    ////////////////////
    // ENGINE METHODS //
    ////////////////////


	void Start () 
    {
        ChangeTimeScales();
        this.GetComponent<GUIScripts>().enabled = false;

        Display = GameObject.FindGameObjectWithTag("Display");
        Player = GameObject.FindGameObjectWithTag("Player");
        Hoover = GameObject.FindGameObjectWithTag("Hoover");

        StartCoroutine("CheckForGameEnd");

        InitialiseGUIStyles();
	}

    void OnGUI()
    {
     /*   GUI.Label(new Rect(10, 65, 1000, 20), "Time to survive: " + (secsTillGameEnd - (int)Time.timeSinceLevelLoad));
        GUI.Label(new Rect(10, 85, 1000, 20), "Time till starvation: " + (Player.GetComponent<PlayerController>().timeTillStarve - secondsSinceLastEaten));
        GUI.Label(new Rect(10, 105, 1000, 20), "Tech Level: " + playerTechLevel);*/

        if (!hasGameEnded)
        {
            int survive = (secsTillGameEnd - (int)Time.timeSinceLevelLoad);
            int eaten = (int)(Player.GetComponent<PlayerController>().timeTillStarve - secondsSinceLastEaten) * 2;
            int upgrade = playerTechLevel * 30;

            GUI.Label(new Rect(10, 30, survive, 20), "Survive", guiSurviveStyle);
            GUI.Label(new Rect(10, 50, eaten, 20), "Eat", guiStarveStyle);
            GUI.Label(new Rect(10, 70, upgrade, 20), "Tech", guiUpgradeStyle);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            Application.Quit();
        }
    }


    ////////////////////
    // MEMBER METHODS //
    ////////////////////


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

    private void InitialiseGUIStyles()
    {
        Color[] colours = {Color.grey};

        guiStarveTex = new Texture2D(1, 1);
        guiStarveTex.SetPixel(1, 1, Color.green);
        guiStarveTex.wrapMode = TextureWrapMode.Repeat;
        guiStarveTex.Apply();
        guiStarveStyle = new GUIStyle();
        guiStarveStyle.normal.background = guiStarveTex;

        guiSurviveTex = new Texture2D(1, 1);
        guiSurviveTex.SetPixel(1, 1, Color.grey);
        guiSurviveTex.wrapMode = TextureWrapMode.Repeat;
        guiSurviveTex.Apply();
        guiSurviveStyle = new GUIStyle();
        guiSurviveStyle.normal.background = guiSurviveTex;

        guiUpgradeTex = new Texture2D(1, 1);
        guiUpgradeTex.SetPixel(1, 1, Color.yellow);
        guiUpgradeTex.wrapMode = TextureWrapMode.Repeat;
        guiUpgradeTex.Apply();
        guiUpgradeStyle = new GUIStyle();
        guiUpgradeStyle.normal.background = guiUpgradeTex;
    }

    ////////////////
    // COROUTINES //
    ////////////////

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
        hasGameEnded = true;
    }

    IEnumerator DelayEndGameResume()
    {
        // fade out the Outro text so the end game can continue
        yield return new WaitForSeconds(secsTillEndGameResume);
        Display.GetComponent<DisplayFade>().StartFade("OUT");
        Hoover.GetComponent<HooverDestruction>().FallApart();
    }
}
