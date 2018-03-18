using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {
    public GameObject explosion;

    public float damage;
    public float maxLifeTime;
    public float currentLifeTime;
    public float radius;
    public float force;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        checkLifeTime();
	}

    void OnCollisionEnter(Collision collision ) {
        GameObject hit = collision.gameObject;
        //TODO maybe change some stuff here (not final)
        switch (hit.tag) {
            case "Player":
            startExplosion();
            Destroy(this.gameObject);
            break;
            case "LowEnemy":
            startExplosion();
            Destroy(this.gameObject);
            break;
            case "MidEnemy":
            startExplosion();
            Destroy(this.gameObject);
            break;
            case "Planet":
            startExplosion();
            Destroy(this.gameObject);
            break;
            case "Asteroid":
            startExplosion();
            Destroy(this.gameObject);
            break;
        }
        

    }

    public void startExplosion () {

        Collider [] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider c in colliders) {
            if (c.GetComponent<Rigidbody>() == null) {
                continue;
            }
            if (c.GetComponent<PlayerController>() != null) {
                c.GetComponent<PlayerController>().AdjustCurrentHealth(damage/30);
            }
            if (c.GetComponent<MidEnemy>() != null) {
                c.GetComponent<EnemyManager>().AdjustCurrentHealth(damage, c.GetComponent<MidEnemy>());
            }
            if (c.GetComponent<LowEnemy>() != null) {
                c.GetComponent<EnemyManager>().AdjustCurrentHealth(damage, c.GetComponent<LowEnemy>());
            }
            c.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius, 0.5f, ForceMode.Impulse);
        }
        Object exp = Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(exp, 1f);
        Destroy(gameObject,1f);
    }

    //checks how long the rocket did fly
    void checkLifeTime () {
        currentLifeTime += Time.deltaTime;
        if(currentLifeTime >= maxLifeTime) {
            startExplosion();
            Destroy(this.gameObject);
        }
    }
}
