using UnityEngine;
using System.Collections;

public class RotatePlanets : MonoBehaviour {

    Transform planet;
    // Use this for initialization
    void Start () {
        planet = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update () {
        planet.Rotate(new Vector3(0, 0.15f, 0));
    }
}
