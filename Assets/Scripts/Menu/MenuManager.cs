using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour, IPointerEnterHandler {
    public AudioClip shoot;
    public AudioClip damage;

    public Canvas quitMenu;
    public Button startText;
    public Button exitText;
    public Button settingsText;

    public Transform enemy;
    public Transform player;

    void Start () {
        quitMenu = quitMenu.GetComponent<Canvas>();
        startText = startText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();
        settingsText = settingsText.GetComponent<Button>();
        quitMenu.enabled = false;
    }
    public void OnPointerEnter ( PointerEventData eventData ) {
        if (eventData.pointerEnter == startText.gameObject) {
            Debug.Log("start");
            enemy.position = new Vector3(enemy.position.x, -0.11f, enemy.position.z);
            player.position = new Vector3(player.position.x, 1.5f, player.position.z);
        }
        if (eventData.pointerEnter == settingsText.gameObject) {
            enemy.position = new Vector3(enemy.position.x, -1.05f, enemy.position.z);
            player.position = new Vector3(player.position.x,-0.05f, player.position.z);
            Debug.Log("settings");
        }
        if(eventData.pointerEnter == exitText.gameObject) {
            enemy.position = new Vector3(enemy.position.x, -2.0f, enemy.position.z);
            player.position = new Vector3(player.position.x, -2.0f, player.position.z);
            Debug.Log("exit");
        }
        if (Input.GetButtonDown("Fire1")) {
            //shoot and make enemy explode
        }
    }

    public void ExitPress () {
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;
    }

    public void NoPress () {
        quitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;
    }

    public void StartNewLevel () {
        SceneManager.LoadScene("Main");
    }

    public void Settings () {
        SceneManager.LoadScene("Settings");
    }

    public void ExitGame () {
        Application.Quit();
    }
}