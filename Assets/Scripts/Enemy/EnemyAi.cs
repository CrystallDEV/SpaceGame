using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAi : MonoBehaviour {
    public AudioClip laserSound;
    new AudioSource audio;

    public GameObject bulletPrefab;
    public Transform bulletSpawn1;
    public Transform bulletSpawn2;
    public Transform bulletSpawn3;
    List<Transform> bulletSpawns = new List<Transform>();

    Transform Player;
    float MoveSpeed = 4;

    float minMoveCloserDist = 20;
    float maxMoveCloserDist = 4;
    float MinDist = 15;

    bool shooting = false;
    // bool isAtNewPos = true;
    // bool isMoving = false;

    Vector3 newPosition;




    void Start () {
        audio = GetComponent<AudioSource>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        if (bulletSpawn1 != null) {
            bulletSpawns.Add(bulletSpawn1);
        }
        if (bulletSpawn2 != null) {
            bulletSpawns.Add(bulletSpawn2);
        }
        if (bulletSpawn3 != null) {
            bulletSpawns.Add(bulletSpawn3);
        }
    }

    void Update () {
        if (GameInformation.isDead) {
            return;
        }
        float dis = Vector3.Distance(transform.position, Player.position);
        if (dis >= maxMoveCloserDist) {
            transform.LookAt(Player);
        }

        if (dis <= minMoveCloserDist && dis >= maxMoveCloserDist) {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            if (dis <= MinDist) {
                if (shooting == false) {
                    StartCoroutine(waitBlock());
                }
            }


            //fly to a random position if player is not in range
            //get random position (Random.Range();)
            //raycast to position and check if a planet is in the way 
            //if everythings allright move to position
            //wait till position is reached 

            /*
        } else {
            if (isAtNewPos == true && isMoving == false) {
                newPosition = new Vector3(Random.Range(-35, 35), 1, Random.Range(-35, 35));
                isAtNewPos = false;
                isMoving = true;
                RaycastHit hit;
                if (Physics.Raycast(transform.position, newPosition, out hit)) {

                }
            } else {

                if (isMoving) {
                    transform.LookAt(newPosition);
                    transform.position += transform.forward * MoveSpeed * Time.deltaTime;
                }
                if (transform.position == newPosition) {
                    isAtNewPos = true;
                    isMoving = false;
                }
            }
            */
        }

    }

    void CmdFire () {
        //Create Bullet from prefab for each bulletspawn of the player
        foreach (Transform t in bulletSpawns) {
            GameObject bullet = (GameObject) Instantiate(bulletPrefab, t.position, t.rotation);

            bullet.GetComponent<EnemyBullet>().damage = Mathf.Round(Random.Range(5f, 10f));

            //add velocity to bullet
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20.0f;
            //play laser sound
            audio.PlayOneShot(laserSound, 0.1f);


            //destroy bullet after 2 sec
            Destroy(bullet, 0.8f);
            shooting = false;
        }
    }

    //waitblock for accurate shooting
    IEnumerator waitBlock () {
        shooting = true;
        yield return new WaitForSeconds(1f);
        CmdFire();
    }
}