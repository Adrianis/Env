using UnityEngine;
using System.Collections;

public class DisplayFade : MonoBehaviour {

    public float fadeTime;
    private Color col;
	
	public Material Intro;
	public Material Outro;
	
	/// <summary>
	/// Fade In or Out of the display texture
	/// </summary>
	/// <param name='type'>
	/// Can be "IN" to fade the tex in, or "OUT" to fade it out
	/// </param>
    public void StartFade(string type)
    {
        switch (type)
        {
            case "IN": 
                StartCoroutine("FadeIn");
                break;
            case "OUT":
                StartCoroutine("FadeOut");
                break;
        }
    }
	
	/// <summary>
	/// Switches material based on certain names
	/// </summary>
	/// <param name='mat'>
	/// Either "INTRO" or "OUTRO"
	/// </param>
	public void SwitchMaterial(string mat)
	{
		switch (mat)
		{
			case "INTRO":
				renderer.material = Intro;
				break;
			case "OUTRO":
				renderer.material = Outro;
				break;
		}
	}
	
    IEnumerator FadeOut()
    {
        float rate = 1.0f / fadeTime;
        float i = 1.0f;
        col = new Color(1, 1, 1, 1);

        while (i > 0.0f)
        {
            i -= Time.deltaTime * rate;
            col.a = Mathf.Lerp(0, 1, i);
            renderer.material.color = col;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator FadeIn()
    {
        float rate = 1.0f / fadeTime;
        float i = 0.0f;
        col = new Color(1, 1, 1, 0);

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            col.a = Mathf.Lerp(0, 1, i);
            renderer.material.color = col;
            yield return new WaitForEndOfFrame();
        }
    }

}
