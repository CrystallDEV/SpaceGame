using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {
    public AudioClip rockExplode;
    new AudioSource audio;

    public float damage;

    void Start () {
        audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter ( Collider collision ) {
        GameObject hit = collision.gameObject;
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
                break;

                case "LowEnemy":
                break;

                case "Planet":
                Destroy(this.gameObject);
                break;

                case "Asteroid":
                audio.PlayOneShot(rockExplode, 1f);
                Destroy(this.gameObject);
                break;
            }
        }
        Destroy(gameObject);
    }
}