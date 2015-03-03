using UnityEngine;
using System.Collections;

public class PlayerTakingDamage : MonoBehaviour {

	public double maxHP = 5;
	public double currentHP;
	// Use this for initialization
	void Start () {
		currentHP = maxHP;
	}

    // Collision detection
	public void OnCollisionEnter(Collision col) {
        // This collision detection SHOULD BE WORKING, I tested it with a local object I made myself
        // but it doesn't seem to be working with the assets. For now I kept a placeholder
        // name "Arrow" which I uh, tried before and didn't work. 
        // Also tested using key inputs and taking damage works.
		if (col.gameObject.name == "Arrow") {
			currentHP--;
			Destroy(col.gameObject);
		};
	}
}