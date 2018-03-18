using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public AudioClip laserSound;
    new AudioSource audio;

    public float speed = 10;
    public float maxHealth = 100;
    public float maxShield = 50;
    float rocketCD = 0;

    public Canvas gameOver;
    public Canvas timePlayed;

    Rigidbody rig;

    public GameObject bulletPrefab;
    public GameObject rocketPrefab;

    public GameObject expBar;
    public GameObject healthBar;
    public GameObject shieldBar;
    public GameObject levelNumber;
    public GameObject rocketCDNumber;

    public GameObject shieldObject;

    List<Transform> bulletSpawns = new List<Transform>();
    public Transform rocketSpawn;
    public Transform bulletSpawn1;
    public Transform bulletSpawn2;
    public Transform bulletSpawn3;

    private bool shooting = false;
    private bool rocketCDBool = false;

    void Start()
    {
        this.GetComponent<Transform>().position = new Vector3(Random.Range(-35, 35), 1, Random.Range(-35, 35));
        audio = GetComponent<AudioSource>();
        rig = GetComponent<Rigidbody>();
        gameOver.enabled = false;

        if (bulletSpawn1 != null)
        {
            bulletSpawns.Add(bulletSpawn1);
        }
        if (bulletSpawn2 != null)
        {
            bulletSpawns.Add(bulletSpawn2);
        }
        if (bulletSpawn3 != null)
        {
            bulletSpawns.Add(bulletSpawn3);
        }
    }

    void Update()
    {
        //shooting button (leftclick)
        if (Input.GetButton("Fire1"))
        {
            if (shooting == false)
            {
                StartCoroutine(waitBlock());
            }
        }
        //rocket shooting button (rightclick)
        if (Input.GetButton("Fire2"))
        {
            if (rocketCDBool == false)
            {
                CmdRocketFire();
            }
        }
        calculateRocketCD();

        //updating canvas for time played and level
        timePlayed.GetComponentInChildren<Text>().text = Mathf.RoundToInt(GameInformation.PlayedTime).ToString();
        levelNumber.GetComponent<Text>().text = GameInformation.PlayerLevel.ToString();
        rocketCDNumber.GetComponent<Text>().text = System.Math.Round(rocketCD, 1).ToString();
        if (!rocketCDBool)
        {
            rocketCDNumber.GetComponent<Text>().text = "Ready!";
        }
    }

    //fixed update 
    void FixedUpdate()
    {
        //cheats / admin commands
        if (Input.GetKeyDown(KeyCode.I))
        {
            this.GetComponent<Transform>().position = new Vector3(30, 1, 35);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            GameInformation.PlayerLevel += 5;
        }


        //prevent Player spawning into planets
        if (Mathf.RoundToInt(GameInformation.PlayedTime) < 1 && GameInformation.isDead)
        {
            this.GetComponent<Transform>().position = new Vector3(Random.Range(-35, 35), 1, Random.Range(-35, 35));
            Debug.Log("Player spawned in Planet, respawned!");
            YesPress();
        }
        //moving player with wasd
        movePlayer();
        //check if the player is dead
        isPlayerDead();
    }

    //waitblock for rocket shooting
    IEnumerator waitRocketBlock()
    {
        yield return new WaitForSeconds(4f);
        rocketCDBool = false;
    }

    //waitblock for accurate shooting
    IEnumerator waitBlock()
    {
        shooting = true;
        yield return new WaitForSeconds(0.2f);
        CmdFire();
    }

    //shooting rocket block
    void CmdRocketFire()
    {
        rocketCDBool = true;
        rocketCD = 4;
        GameObject rocket = (GameObject) Instantiate(rocketPrefab, rocketSpawn.position, transform.rotation);
        rocket.GetComponent<Rocket>().damage = Mathf.RoundToInt(Random.Range(20, 100));

        rocket.GetComponent<Rigidbody>().velocity = rocket.transform.forward * 15.0f;
        StartCoroutine(waitRocketBlock());
    }

    //shooting block
    void CmdFire()
    {
        //Create Bullet from prefab for each bulletspawn of the player
        foreach (Transform t in bulletSpawns)
        {
            GameObject bullet = (GameObject) Instantiate(bulletPrefab, t.position, t.rotation);

            bullet.GetComponent<Bullet>().damage = Mathf.Round(Random.Range(5f, 15f));

            //add velocity to bullet
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 48.0f;
            //play laser sound
            audio.PlayOneShot(laserSound, 0.1f);
            //destroy bullet after 2 sec
            Destroy(bullet, 0.5f);
            shooting = false;
        }
    }

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.tag == "MedicBox")
        {
            AdjustCurrentHealth(-20);
            Destroy(trigger.gameObject);
            Debug.Log("Healthbox Collected");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        switch (hit.tag)
        {
            case "Asteroid":
                //load gameover screen
                Debug.Log("Player hit Asteroid!");
                break;
            case "Planet":
                //load gameover screen
                Debug.Log("Player hit Planet!");
                GameInformation.isDead = true;
                break;
        }
    }

    void isPlayerDead()
    {
        //TODO destroy GameObject and create explosion + sound
        if (GameInformation.isDead)
        {
            this.speed = 0;
            shooting = true;
            gameOver.enabled = true;
        }
    }

    void movePlayer()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed * 100;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * speed * 100;

        rig.AddForce(new Vector3(x, 0, z));

        float xc = transform.position.x;
        float yc = transform.position.y + 20;
        float zc = transform.position.z - 5;

        Camera.main.transform.position = new Vector3(xc, yc, zc);
    }

    //pressed yes in Menu
    public void YesPress()
    {
        SceneManager.LoadScene("Main");
        shooting = false;
        rocketCDBool = false;
        this.speed = 10;
        GameInformation.PlayerLevel = 1;
        GameInformation.isDead = false;
        GameInformation.HitPoints = maxHealth;
        GameInformation.curExperience = 0;
        GameInformation.MaxExperience = 100;
        GameInformation.enemyAmount = 0;
        GameInformation.asteroidAmount = 0;
        GameInformation.PlayedTime = 0;
        GameInformation.healthBoxDropRate = 0.05f;
        GameInformation.Shield = maxShield;
        GameInformation.MaxHitpoints = maxHealth;
    }

    //pressed no in menu
    public void NoPress()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void AdjustCurrentHealth(float adj)
    {
        float curHealth = GameInformation.HitPoints;
        float curShield = GameInformation.Shield;
        if (curShield > 0 && curShield <= maxShield)
        {
            curShield -= adj;
        }

        if (curShield <= 0)
        {
            curShield = 0;
            shieldObject.GetComponent<Renderer>().enabled = false;
        }
        else
        {
            shieldObject.GetComponent<Renderer>().enabled = true;
        }

        if (curShield > maxShield)
        {
            curShield = maxShield;
        }
        if (curShield == 0)
        {
            curHealth -= adj;
        }

        if (curHealth <= 0)
        {
            curHealth = 0;
        }

        if (curHealth > maxHealth)
        {
            float d = curHealth - maxHealth;
            curShield += d;
            curHealth = maxHealth;
        }
        if (maxHealth < 0)
        {
            maxHealth = 0;
        }

        float calc_Health = curHealth / maxHealth;
        float calc_Shield = curShield / maxShield;

        if (healthBar != null)
        {
            setPlayerBar(healthBar, calc_Health);
        }
        if (shieldBar != null)
        {
            setPlayerBar(shieldBar, calc_Shield);
        }
        GameInformation.Shield = curShield;
        GameInformation.HitPoints = curHealth;
    }

    public void setPlayerBar(GameObject bar, float scale)
    {
        bar.transform.localScale =
            new Vector3(bar.transform.localScale.x, Mathf.Clamp(scale, 0, 1), bar.transform.localScale.z);
    }

    public void setPlayerExpBar(GameObject bar, float scale)
    {
        bar.transform.localScale =
            new Vector3(Mathf.Clamp(scale, 0, 1), bar.transform.localScale.y, bar.transform.localScale.z);
    }

    void calculateRocketCD()
    {
        rocketCD -= Time.deltaTime;
        if (rocketCD < 0)
        {
            rocketCD = 0;
        }
    }
}