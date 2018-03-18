using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInformation : MonoBehaviour {
    public GameObject respawnAsteroidPrefab;
    public GameObject respawnLowEnemyPrefab;
    public GameObject respawnMidEnemyPrefab;
    public GameObject respawnHealthBoxPrefab;
    public static GameObject respawnHealthBox;

    //int to get how many gameobject of a kind are spawned
    public int maxAsteroids;
    public int maxEnemies;
    public int maxExperience;

    //drop rates for items
    public static float healthBoxDropRate;

    //getter and setter
    public static float PlayedTime { get; set; }
    public static string PlayerName { get; set; }
    public static int PlayerLevel { get; set; }
    public static float HitPoints { get; set; }
    public static float AtkDamage { get; set; }
    public static bool isDead { get; set; }
    public static int enemyAmount { get; set; }
    public static int asteroidAmount { get; set; }
    public static int curExperience { get; set; }
    public static int MaxExperience { get; set; }
    public static float MaxHitpoints { get; set; }
    public static float Shield { get; set; }

    void Awake () {
        respawnHealthBox = respawnHealthBoxPrefab;
        if (GameObject.FindGameObjectsWithTag("GameInformation").Length > 1) {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);
    }


    void Update () {
        isPlayerDead();
        adjustStats();
        spawnEnemies();
        calculatePlayedTime();
    }

    //waiting 5 seconds till spawning a new enemy
    void waitSpawnBlock () {

    }

    void spawnEnemies () {
        if (enemyAmount < maxEnemies && PlayedTime > 5) {
            if (PlayerLevel < 5) {
                Instantiate(respawnLowEnemyPrefab, respawnLowEnemyPrefab.transform.position, respawnLowEnemyPrefab.transform.rotation);
                enemyAmount += 1;
                //Debug.Log("Enemy spawned! " + enemyAmount + " / " + maxEnemies + "LowEnemy");
            }
            if (PlayerLevel >= 5 && PlayerLevel < 10) {
                Instantiate(respawnMidEnemyPrefab, respawnMidEnemyPrefab.transform.position, respawnMidEnemyPrefab.transform.rotation);
                enemyAmount += 1;
                //Debug.Log("Enemy spawned! " + enemyAmount + " / " + maxEnemies + " MidEnemy");
            }
            if (PlayerLevel > 10) {
                Instantiate(respawnMidEnemyPrefab, respawnMidEnemyPrefab.transform.position, respawnMidEnemyPrefab.transform.rotation);
                enemyAmount += 1;
                //Debug.Log("Enemy spawned! " + enemyAmount + " / " + maxEnemies + " MidEnemy");
            }
        }
        if (asteroidAmount < maxAsteroids) {
            Instantiate(respawnAsteroidPrefab, respawnAsteroidPrefab.transform.position, respawnAsteroidPrefab.transform.rotation);
            asteroidAmount = asteroidAmount + 1;
            //Debug.Log("Spawned Asteroid");
        }
    }

    void adjustStats () {

        if (PlayerLevel < 5) {
            healthBoxDropRate = 0.10f + (float) PlayerLevel / 100;
            maxExperience = 100 + 20 * PlayerLevel * PlayerLevel;
            maxEnemies = 10 + 2 * PlayerLevel;
            maxAsteroids = 30 + 2 * PlayerLevel;

        }
        if (PlayerLevel < 10 && PlayerLevel >= 5) {
            healthBoxDropRate = 0.10f + (float) PlayerLevel / 80;
            maxExperience = 100 + 20 * PlayerLevel * PlayerLevel;
            maxEnemies = 5 + PlayerLevel;
            maxAsteroids = 30 + 2 * PlayerLevel;
        }
        if (PlayerLevel > 10) {
            healthBoxDropRate = 0.20f + (float) PlayerLevel / 800;
            maxExperience = 100 + 20 * PlayerLevel * PlayerLevel;
            maxEnemies = 5 + 2 * PlayerLevel;
            maxAsteroids = 30 + 2 * PlayerLevel;

        }


        if (curExperience >= maxExperience) { 
            PlayerLevel += 1;
            curExperience -= maxExperience;
        }

        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        float calc_expBar = (float) curExperience / maxExperience;
        player.setPlayerExpBar(player.expBar, calc_expBar);
    }

    void isPlayerDead () {
        if (HitPoints <= 0 && !isDead) {
            HitPoints = 0;
            isDead = true;
            Debug.Log("Player died");
        }
    }
    void calculatePlayedTime () {
        if (!isDead) {
            PlayedTime += Time.deltaTime;
        }
    }

    public static void dropHealthBox ( float dropchanche, GameObject enemy ) {
        if (dropchanche <= healthBoxDropRate) {
            Instantiate(respawnHealthBox, enemy.transform.position, enemy.transform.rotation);
        }
    }

}
