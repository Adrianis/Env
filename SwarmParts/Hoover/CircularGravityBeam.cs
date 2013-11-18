using UnityEngine;
using System.Collections;
using System.Linq;

public class CircularGravityBeam : CircularGravity {

    public float beamRadius = 5;

    private GameObject beamEndPos;

    void Start()
    {
        beamEndPos = this.transform.FindChild("HooverAreaEnd").gameObject;
    }

    protected override void CalculateAndEstimateForce()
    {
        Vector3 forceLocation = this.transform.position;
		
		// SC EDIT 01-11-13
        RaycastHit[] colliders = Physics.CapsuleCastAll(forceLocation, beamEndPos.transform.position, beamRadius, (beamEndPos.transform.position - forceLocation));

        var hits =
        from h in colliders
        select h;

        hits = hits.Where(h => h.rigidbody != this.rigidbody);
        hits = hits.Where(h => h.rigidbody == true);


        foreach (var hit in hits)
        {
            // SC EDIT 28-10-13
            if (hit.transform.tag == "Piece")
            {
                hit.transform.gameObject.GetComponent<Piece>().BeingPulled = true;
            }

            hit.rigidbody.AddExplosionForce(forcePower, forceLocation, CalculateRadius());
        }
    }

}
