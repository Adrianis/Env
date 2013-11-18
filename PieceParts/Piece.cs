using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {

    public float sleepDelayTimer = 4f;


    private bool beingPulled; // to control when the piece needs to be Kinematic or not
    // property exposed so CircularGravity can simply set this value, while this class logic defines Kinematic settings
    public bool BeingPulled
    {
        get { return beingPulled; }
        set
        {
            // if the piece is now being pulled, and it wasn't being pulled before
            // set it to not be kinematic so it can move
            // this ensures that the Coroutine is not constantly being called and therefore will do its job
			if ((value == true) && (beingPulled == false))
            {
                rigidbody.isKinematic = false;
                StartCoroutine("DelayPhysSleep");
            }
            beingPulled = value;
           
        }
    }


    IEnumerator DelayPhysSleep()
    {
        // delay execution for X time, then set this piece to Kinematic
        // reset Pulled to false to make sure it can be re-pulled again if still in area of effect
        yield return new WaitForSeconds(sleepDelayTimer);
        rigidbody.isKinematic = true;
        beingPulled = false;
    }
}
