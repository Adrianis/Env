using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private GameObject GameManager;

    private bool isAlive = true;

    public float timeTillStarve = 60;


    // Food stuffs
    private int secsSinceLastEaten;
    public int SecsSinceLastEaten
    {
        get { return secsSinceLastEaten; }
        set
        {
            secsSinceLastEaten = value;
            UpdateGameManagerSeconds();
        }
    }

    void Awake()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    void Start()
    {
        StartCoroutine("UpdateSecondsSinceLastEaten");
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (isAlive)
        {
            switch (hit.gameObject.tag)
            {
                case "Floor": // Kill the player if they hit the water
                    Debug.LogWarning("PlayerController: Player Death");
                    KillPlayer("DROWN");
                    isAlive = false;
                    break;
                case "Food": // Remove Food & update stats
                    hit.gameObject.SetActive(false);
                    SecsSinceLastEaten = 1;
                    break;
                case "Tech":
                    break;
            }        
        }
    }

    private void UpdateGameManagerSeconds()
    {
        GameManager.GetComponent<GameManager>().secondsSinceLastEaten = SecsSinceLastEaten;
    }

    private void KillPlayer(string deathType)
    {
        // disable controls
        this.GetComponent<CharacterMotor>().enabled = false;
        this.GetComponent<FPSInputController>().enabled = false;
        this.GetComponent<MouseLook>().enabled = false;
        this.transform.GetChild(1).GetComponent<MouseLook>().enabled = false;

        // call up Death in game manager, pass type
        GameManager.GetComponent<GameManager>().PlayerDeath(deathType);
    }

    private IEnumerator UpdateSecondsSinceLastEaten()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (SecsSinceLastEaten < timeTillStarve)
			{
                SecsSinceLastEaten += 1;
			}
            else
			{
                KillPlayer("STARVE");
			}
        }
    }
}
