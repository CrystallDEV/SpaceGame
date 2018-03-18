using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Bullet : MonoBehaviour {
    public AudioClip rockExplode;
    new AudioSource audio;

    public float damage;

    void Start () {
        audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter ( Collider collision ) {
        GameObject hit = collision.gameObject;
        LowEnemy lowEnemy = hit.GetComponent<LowEnemy>();
        MidEnemy midEnemy = hit.GetComponent<MidEnemy>();
        PlayerController player = hit.GetComponent<PlayerController>();

        if (hit != null) {
            switch (hit.gameObject.tag) {

                //Decide what the bullet did hit
                case "Player":
                if (GameInformation.HitPoints != 0) {
                    player.AdjustCurrentHealth(damage);
                }
                break;

                case "MidEnemy":
                Debug.Log("hit midenemy");
                if (midEnemy != null && damage != 0) {
                    hit.GetComponent<EnemyManager>().AdjustCurrentHealth(damage, midEnemy);
                }
                Destroy(this.gameObject);
                break;

                case "LowEnemy":
                Debug.Log("hit lowenemy");
                if (lowEnemy != null && damage != 0) {
                    hit.GetComponent<EnemyManager>().AdjustCurrentHealth(damage, lowEnemy);
                }
                Destroy(this.gameObject);
                break;

                case "Planet":
                break;

                case "Asteroid":

                audio.PlayOneShot(rockExplode, 1f);
                Destroy(this.gameObject);
                break;
                case "Bullet":
                break;

            }
        }
        Destroy(gameObject);
    }
}
