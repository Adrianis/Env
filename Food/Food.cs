using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

	void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            this.gameObject.SetActive(false);
        }
    }
}
