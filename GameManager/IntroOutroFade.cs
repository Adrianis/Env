using UnityEngine;
using System.Collections;

public class IntroOutroFade : MonoBehaviour {

    private GameObject Camera;

    void Awake()
    {
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
    }


    void FadeIn()
    {

    }

}
