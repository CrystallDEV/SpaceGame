using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Logo : MonoBehaviour {

    public Image blurr;
    public bool blurred = false;

    void Start () {
    }

	// Use this for initialization
	void Update () {
        if (blurred == false) {
            fadeIn();
        }
        if(blurred == true) {
            fadeOut();
        }
	}

    IEnumerator fadeBlock () {
        yield return new WaitForSeconds(3f);
        blurred = true;
    }

    void fadeIn () {
        Color c = blurr.color;
        if (c.a > 0.05) {
            c.a -= Time.deltaTime;
        }
        if(c.a< 0.05) {
            StartCoroutine(fadeBlock());
        }
        blurr.color = c;
    }

    void fadeOut () {
        Color c = blurr.color;
        
        if (c.a < 1) {
            c.a += Time.deltaTime;
        }
        if(c.a > 0.95) {
            SceneManager.LoadScene("MainMenu");
        }
        blurr.color = c;
    }
}
