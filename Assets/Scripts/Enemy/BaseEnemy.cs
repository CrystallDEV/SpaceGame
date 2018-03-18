using UnityEngine;
using System.Collections;

public class BaseEnemyClass : MonoBehaviour {
    private float maxHitpoints;
    private float hitPoints;
    private float atkDamage;

    public float MaxHitpoints { get; set; }
    public float HitPoints { get; set; }
    public float AtkDamage { get; set; }
}
