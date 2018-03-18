using UnityEngine;
using System.Collections;

public class AsteroidMovement : MonoBehaviour {
    Transform body;
    Rigidbody asteroid;
    // Use this for initialization
    void Start () {
        body = GetComponent<Transform>();
        asteroid = GetComponent<Rigidbody>();
        body.position = new Vector3(Random.Range(-40,40), 1, Random.Range(-40, 40));
        asteroid.AddForce(new Vector3(Random.Range(-20f, 500.0f), 0, Random.Range(0f, 50.0f)));

    }

    // Update is called once per frame
    void Update () {
        if (asteroid.transform.position.y > 1 | asteroid.transform.position.y < 1) {
            float x = asteroid.transform.position.x;
            float z = asteroid.transform.position.z;
            asteroid.position = new Vector3(x, 1, z);
        }

    }

    void OnCollisionEnter ( Collision collision ) {
        GameObject hit = collision.gameObject;
        PlayerController player = hit.GetComponent<PlayerController>();
        switch (hit.tag) {
            case "Player":
            player.AdjustCurrentHealth(20);
            Destroy(this.gameObject);
            GameInformation.asteroidAmount = GameInformation.asteroidAmount - 1;

            break;
            case "Planet":
            Destroy(this.gameObject);
            GameInformation.asteroidAmount = GameInformation.asteroidAmount - 1;
            break;

            case "Border":
            Destroy(this.gameObject);
            GameInformation.asteroidAmount = GameInformation.asteroidAmount - 1;
            break;
        }
    }
}
