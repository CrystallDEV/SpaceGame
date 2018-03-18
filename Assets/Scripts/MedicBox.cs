using UnityEngine;
using System.Collections;

public class MedicBox : MonoBehaviour {
	// Update is called once per frame
	void FixedUpdate () {
        this.GetComponent<Transform>().Rotate(new Vector3(1, 2, 1));
	}
}
