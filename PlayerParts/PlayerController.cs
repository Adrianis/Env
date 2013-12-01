using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private GameObject GameManager;
    private CharacterMotor Motor;
    private MovementBounds MoveBound;

    private bool isAlive = true;
    public bool canDie = true;
    public Vector3 centerPoint;

    // food
    public float timeTillStarve = 60;
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

    // tech [deprecated]
    private int techLevel;
    public int TechLevel
    {
        get { return techLevel; }
        set
        {
            techLevel = value;
            UpdateTechLevel();
        }
    }

    // upgrades
    private bool canSprint;
    private float defaultSpeed;
    public float sprintSpeed;

    public float sprintUpgradeIterator;
    public float jumpUpgradeIterator;
    public float gravUpgradeIterator;
    public float starveTimeUpgradeIterator;



    ////////////////////
    // ENGINE METHODS //
    ////////////////////

    void Awake()
    {
        Motor = this.GetComponent<CharacterMotor>();
    }

    void Start()
    {
        StartCoroutine("UpdateSecondsSinceLastEaten");

        // initialise default speed to its value from start game start
        defaultSpeed = Motor.movement.maxForwardSpeed;

        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        MoveBound = GameManager.GetComponent<MovementBounds>();

        transform.position = FindSpawnLocation();
        transform.LookAt(GameManager.transform);

    }

    void Update()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            // alter movement speeds while key is held down
            Motor.movement.maxForwardSpeed = sprintSpeed;
            Motor.movement.maxSidewaysSpeed = sprintSpeed;
            Motor.movement.maxBackwardsSpeed = sprintSpeed;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            // revert movement speeds to default
            Motor.movement.maxForwardSpeed = defaultSpeed;
            Motor.movement.maxSidewaysSpeed = defaultSpeed;
            Motor.movement.maxBackwardsSpeed = defaultSpeed;
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (isAlive)
        {
            switch (hit.gameObject.tag)
            {
                case "Floor": // Kill the player if they hit the water
                    if (canDie)
                    {
                        KillPlayer("DROWN");
                        isAlive = false;
                    }
                    break;
                case "Food": // Remove Food & update stats
                    hit.gameObject.SetActive(false);
                    SecsSinceLastEaten = 1;
                    GetComponents<AudioSource>()[0].Play();
                    break;
                case "Tech": // Remove Tech & upgrade stats
                    hit.gameObject.SetActive(false);
                    TechLevel++; // [deprecated]
                    GetComponents<AudioSource>()[1].Play();
                    break;
            }
        }
    }



    ////////////////////
    // MEMBER METHODS //
    ////////////////////

    private void UpdateTechLevel()
    {
        // upgrade values according to iterators
        Motor.movement.gravity -= gravUpgradeIterator;
        Motor.movement.maxFallSpeed -= gravUpgradeIterator;
        Motor.jumping.extraHeight += jumpUpgradeIterator;
        sprintSpeed += sprintUpgradeIterator;
        timeTillStarve += starveTimeUpgradeIterator;

        // update GameManager with new tech level for display [deprecated]
        GameManager.GetComponent<GameManager>().playerTechLevel = TechLevel;
    }

    private void UpdateGameManagerSeconds()
    {
        // Updates the GameManager with the current value of secsSinceLastEaten
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

    private Vector3 FindSpawnLocation()
    {
        float x = Random.Range(MoveBound.vBoundsBR.x, MoveBound.vBoundsTL.x);
        float z = Random.Range(MoveBound.vBoundsTR.z, MoveBound.vBoundsBL.z);
        Vector3 vRet = new Vector3(x, -17, z);
        return vRet;
    }

    ////////////////
    // COROUTINES //
    ////////////////

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
