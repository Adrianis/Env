using UnityEngine;
using System.Collections;

public class PieceDestruction : MonoBehaviour {

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Piece")
        {
            //GameObject.Destroy(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
}
