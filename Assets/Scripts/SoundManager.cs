using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
    public static AudioClip laserSound;
    public static AudioClip damageSound;
    public static AudioClip explodeSound;
    new AudioSource audio;

    void Start () {
        audio = GetComponent<AudioSource>();
    }

    void playLaser () {
        audio.PlayOneShot(laserSound, 0.1f);
    }

    void playExplode () {

    }

    void playDamage () {
        audio.PlayOneShot(laserSound);
    }
}
