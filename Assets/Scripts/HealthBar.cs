using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	private Animator animator;
	Player playerScript;
	int health;
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		playerScript = GameObject.Find("Player").GetComponent<Player>();
		//playerScript = transform.parent.GetComponent<Player>();
		health = (int)playerScript.health;
	}
	
	// Update is called once per frame
	void Update () {
		health = (int)playerScript.health;
		animator.SetInteger("PlayerHealth", health);
	}
}
