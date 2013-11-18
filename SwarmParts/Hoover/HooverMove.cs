using UnityEngine;
using System.Collections;

public class HooverMove : MonoBehaviour {

    private GameObject FollowPoint;

    void Awake()
    {
        FollowPoint = GameObject.FindGameObjectWithTag("HooverFollowPoint");
    }

    void Update()
    {
        this.transform.LookAt(FollowPoint.transform);
    }
}
