using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private GameObject GameManager;

    private bool isAlive = true;


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
                case "Food": // Update food stat
                    hit.gameObject.SetActive(false);
                    SecsSinceLastEaten = 1;
                    break;
                default: break;
            }        
        }
    }

    private void UpdateGameManagerSeconds()
    {
        GameManager.GetComponent<GameManager>().secondsSinceLastEaten = SecsSinceLastEaten;
    }

    private void KillPlayer(string deathType)
    {
        GameManager.GetComponent<GameManager>().PlayerDeath(deathType);
    }

    private IEnumerator UpdateSecondsSinceLastEaten()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (SecsSinceLastEaten < 40)
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
