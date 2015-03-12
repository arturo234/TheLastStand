using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : GenericCharacter {
	public float moveSpeed, ammo, ammoLimit;
	public Text text;
	Vector3 mousePosition, diff, translate;

	public float minX; //left boundary 
	public float maxX; //right boundary 
	public float minY; //up boundary 
	public float maxY; //down boundary

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Move();
		RotateToMouse();
		BoundaryCheck();
		if(Input.GetKey (KeyCode.R) || health <= 0) {
			//Replace with an actual trigger i.e. Death
			//change code to jump to game over
			Application.LoadLevel("GameOver");
			resetPlayer();
		}

		//fire arrow on left mouse button or spacebar press
		if (Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.Space))
		{
			if(ammo > 0)
			{
				fireArrow();
				arrow.tag = "PlayerArrow";
				ammo--;
			}
			else
			{
				Debug.Log("Out of ammo");
			}
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
        arrowDir = transform.rotation.eulerAngles;
	}
	
	private void Move()
	{		
		translate = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
		translate = translate.normalized;
		transform.position += translate * moveSpeed * Time.deltaTime;
	}


	private void resetPlayer() {
		var spawnpoint = GameObject.FindWithTag ("Respawn").transform;
		transform.position = spawnpoint.position;
		//set initial health
		health = 10;
	}

	///cause player damage (collision with box collider)
	public void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag.Equals("EnemyArrow")) 
		{
			health--;
			RePool(col.gameObject);
		}
	}

	public void BoundaryCheck() 
	{ 
		float xboundary = Mathf.Clamp(transform.position.x,minX,maxX);
		float yboundary = Mathf.Clamp(transform.position.y,minY,maxY);
		transform.position = new Vector3 (xboundary, yboundary, 0);
	}
}