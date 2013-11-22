using UnityEngine;
using System.Collections;

public class PieceManager : MonoBehaviour {

    public GameObject PfbPiece;
    public GameObject InitialPiece;

    public int lengthOfPieceLineX = 181;
    public int lengthOfPieceLineZ = 77;

    private Vector3 vStartPosBR;
    private Vector3 vSizeOfPiece;
    private Quaternion qStartRotBR;

    private Vector3 nextPos;

    private GameObject GameManager;


    void Awake()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
    }
	
	void Start ()
    {
        vStartPosBR = InitialPiece.transform.position;
        qStartRotBR = InitialPiece.transform.rotation;
        vSizeOfPiece = new Vector3(2, 4, 1.3f);

        nextPos = vStartPosBR;

        GameManager.GetComponent<GameManager>().DisplayLoadingText(true);
        StartCoroutine("DelayPiecePopulation");
	}

    


    void PopulatePieces()
    {
        for (int a = 1; a <= lengthOfPieceLineZ; a++)
        {
            for (int i = 1; i <= lengthOfPieceLineX; i++)
            {
                nextPos.x += vSizeOfPiece.x; // add X value to get next pos on line
                Instantiate(PfbPiece, nextPos, qStartRotBR);
            }

            nextPos.x = vStartPosBR.x - vSizeOfPiece.x; // reset X to original - 1 to fill first Z line
            nextPos.z -= vSizeOfPiece.y; // add Y value to move placement up
        }
        GameManager.GetComponent<GameManager>().DisplayLoadingText(false);
    }


    IEnumerator DelayPiecePopulation()
    {
        yield return new WaitForSeconds(0.1f);
        PopulatePieces();
    }
}
