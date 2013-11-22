using UnityEngine;
using System.Collections;

public class HooverDestruction : MonoBehaviour {

    public void FallApart()
    {
        foreach (Transform child in transform)
        {
            if (child.tag != "HooverPart")
            {
                child.gameObject.SetActive(false);
            }
            else child.rigidbody.isKinematic = false;
        }

        this.transform.DetachChildren();
    }
}
