using UnityEngine;
using System.Collections;

public class SwarmManager : MonoBehaviour {

    private GameObject GameManager;

    private Vector3 vBoundsBL;
    private Vector3 vBoundsTL;
    private Vector3 vBoundsTR;
    private Vector3 vBoundsBR;

    private Vector3 MovePosCur; // current postion to move to
    private Vector3 MovePosPrev; // position just at

    public float moveSpeed = 5; // speed of move
    public float hoverHeight = -9f; // height to use in Y of rand vec
    public float targetUpdateTime = 3f; // time to update next move target


    void Awake()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        MovePosCur = new Vector3(); // initialise here so its never null
    }
    
    void Start () 
    {
        StartCoroutine("FindNewMoveLocation");
		
		vBoundsBR = GameManager.GetComponent<MovementBounds>().vBoundsBR;
        vBoundsBL = GameManager.GetComponent<MovementBounds>().vBoundsBL;
        vBoundsTL = GameManager.GetComponent<MovementBounds>().vBoundsTL;
        vBoundsTR = GameManager.GetComponent<MovementBounds>().vBoundsTR;
	}

    void Update()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, MovePosCur, step);
    }

    

    IEnumerator FindNewMoveLocation()
    {
        while (true)
        {
            float x = Random.Range(vBoundsBR.x, vBoundsTL.x);
            float z = Random.Range(vBoundsTR.z, vBoundsBL.z);
            MovePosCur = new Vector3(x, hoverHeight, z);
            yield return new WaitForSeconds(targetUpdateTime);
        }
    }
}
