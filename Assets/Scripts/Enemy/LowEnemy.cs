using UnityEngine;
using System.Collections;

public class LowEnemy : BaseEnemyClass {
    public GameObject healthBar;

    void Start () {
        this.transform.position = new Vector3(Random.Range(-40, 40), 1, Random.Range(-40, 40));
        this.transform.Rotate(Random.Range(0, 360), 0, 0);
    }

    public LowEnemy () {
        MaxHitpoints = 100;
        HitPoints = 100;
        AtkDamage = 10;
    }
}
