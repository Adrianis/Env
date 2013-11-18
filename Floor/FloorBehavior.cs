using UnityEngine;
using System.Collections;

public class FloorBehavior : MonoBehaviour {

    private GameObject GameManager;

    void Awake()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
    }
}
