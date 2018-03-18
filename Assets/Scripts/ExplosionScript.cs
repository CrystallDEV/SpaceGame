using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {
    public Transform explosion;

    void Update () {
        if (GetComponent<LowEnemy>() != null) {
            if (GetComponent<LowEnemy>().HitPoints == 0) {
                startExplosion(5, 20);
            }
        }
        if (GetComponent<MidEnemy>() != null) { 
            if (GetComponent<MidEnemy>().HitPoints == 0) {
                startExplosion(5, 20);
            }
        }
    }

    void startExplosion ( float radius, float force ) {

        Collider [] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider c in colliders) {
            if (c.GetComponent<Rigidbody>() == null) {
                continue;
            }
            if(c.GetComponent<PlayerController>() != null) {
                c.GetComponent<PlayerController>().AdjustCurrentHealth(10);
            }
            if(c.GetComponent<MidEnemy>() != null) {
                c.GetComponent<EnemyManager>().AdjustCurrentHealth(10, c.GetComponent<MidEnemy>());
                c.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius * 2, 0.5f, ForceMode.Impulse);
            }
            if(c.GetComponent<LowEnemy>() != null) {
                c.GetComponent<EnemyManager>().AdjustCurrentHealth(10, c.GetComponent<LowEnemy>());
            }
            c.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius, 0.5f, ForceMode.Impulse);
        }
        Object exp = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
}
