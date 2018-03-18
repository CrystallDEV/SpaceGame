using UnityEngine;
using System.Collections;

public class MidEnemy : BaseEnemyClass {
    public GameObject healthBar;

    void Start () {
        this.transform.position = new Vector3(Random.Range(-40, 40), 1, Random.Range(-40, 40));
        this.transform.Rotate(Random.Range(0, 360), 0, 0);
    }

    public MidEnemy () {
        MaxHitpoints = 200;
        HitPoints = 200;
        AtkDamage = 10;
    }
}
