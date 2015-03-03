using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	public float health, moveSpeed;
	public Text text;

	Vector3 mousePosition, diff, translate;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Move();
		RotateToMouse();

		if(Input.GetKey (KeyCode.R) || health <= 0) {				//Replace with an actual trigger i.e. Death
			resetPlayer();
		}

		text.text = "Lives: " + health;
	}
	
	private void RotateToMouse()
	{
		mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
		diff = Camera.main.ScreenToWorldPoint(mousePosition) - transform.position;
		diff.Normalize();
		
		float rotation = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotation);
	}
	
	private void Move()
	{
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
		
		translate = new Vector3(h, v, 0);
		translate = translate.normalized;
		transform.position += translate * moveSpeed * Time.deltaTime;
	}

	private void resetPlayer() {
		var spawnpoint = GameObject.FindWithTag ("Respawn").transform;
		transform.position = spawnpoint.position;
	}
}