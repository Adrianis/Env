using UnityEngine;
using System.Collections;

public class Tech : MonoBehaviour {

    private GameObject SwarmPoint;

    void Start()
    {
        //SwarmPoint = GameObject.FindGameObjectWithTag("SwarmPoint");
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            //GameObject.FindGameObjectWithTag("SwarmPoint").GetComponent<TechManager>().actualSpawnCount--;
            this.gameObject.SetActive(false);
        }
    }
}
