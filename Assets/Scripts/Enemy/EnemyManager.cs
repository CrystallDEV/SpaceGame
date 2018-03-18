using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    public void AdjustCurrentHealth ( float adj, BaseEnemyClass enemyType ) {
        LowEnemy lowEnemy = enemyType.GetComponent<LowEnemy>();
        MidEnemy midEnemy = enemyType.GetComponent<MidEnemy>();

        if (lowEnemy != null) {
            enemyType = enemyType.GetComponent<LowEnemy>();
        }
        if (midEnemy != null) {
            enemyType.GetComponent<MidEnemy>();
        }
        float curHealth = enemyType.HitPoints;

        curHealth -= adj;
        if (curHealth < 0 || curHealth == 0) {
            curHealth = 0;

            //Instant destroy enemy, otherwise multiple experience addition (2x-3x for one enemy);
            //Enemy getting destroyed in explosionscript
            //Destroy(enemyType.gameObject);
        }

        if (curHealth == 0) {
            GameInformation.enemyAmount = GameInformation.enemyAmount - 1;
            if (enemyType.tag == "LowEnemy") {
                GameInformation.curExperience += 50;
            }
            if (enemyType.tag == "MidEnemy") {
                GameInformation.curExperience += 100;
            }
            GameInformation.dropHealthBox(Random.Range(0.0f, 1.0f), enemyType.gameObject);
        }

        if (curHealth > enemyType.MaxHitpoints) {
            curHealth = enemyType.MaxHitpoints;
        }

        if (enemyType.MaxHitpoints < 0) {
            enemyType.MaxHitpoints = 0;
        }

        enemyType.HitPoints = curHealth;
        float calc_Health = curHealth / enemyType.MaxHitpoints;
        if (lowEnemy != null && lowEnemy.healthBar != null) {
            SetHealthBar(calc_Health, lowEnemy.healthBar);
        }
        if (midEnemy != null && midEnemy.healthBar != null) {
            SetHealthBar(calc_Health, midEnemy.healthBar);
        }
    }
    public void SetHealthBar ( float myHealth, GameObject healthBar ) {

        healthBar.transform.localScale = new Vector3(Mathf.Clamp(myHealth, 0f, 1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}