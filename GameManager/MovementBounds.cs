using UnityEngine;
using System.Collections;

public class MovementBounds : MonoBehaviour {

    public GameObject InitialPiece;
    private GameObject PieceManager;

    public Vector3 vBoundsBL;
    public Vector3 vBoundsTL;
    public Vector3 vBoundsTR;
    public Vector3 vBoundsBR;

    void Awake()
    {
        PieceManager = GameObject.FindGameObjectWithTag("PieceManager");

        float LineX = PieceManager.GetComponent<PieceManager>().lengthOfPieceLineX * 2;
        float LineY = (PieceManager.GetComponent<PieceManager>().lengthOfPieceLineY * 4) - 4;

        vBoundsBR = InitialPiece.transform.position;

        vBoundsBL = new Vector3(vBoundsBR.x + LineX
                               , vBoundsBR.y
                               , vBoundsBR.z);

        vBoundsTL = new Vector3(vBoundsBR.x + LineX
                               , vBoundsBR.y
                               , vBoundsBR.z - LineY);

        vBoundsTR = new Vector3(vBoundsBR.x
                               , vBoundsBR.y
                               , vBoundsBR.z - LineY);


    }
}
