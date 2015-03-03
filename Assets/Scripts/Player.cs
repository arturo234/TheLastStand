using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	public float health, moveSpeed, ammo, ammoLimit;
	public Text text;
	public GameObject playerArrow;
	Vector3 mousePosition, diff, translate;
	public float arrowVelocity;
	public Vector3 theta, arrowDir;
	public float minX; //left boundary 
	public float maxX; //right boundary 
	public float minY; // up boundary 
	public float maxY; // down boundary

	// Use this for initialization
	void Start () {
		//set initial health
		health = 10;
		ammo = 3;
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
		//catch arrow
		if (Input.GetKeyDown(KeyCode.E))///and arrow is in range
		{
			if(ammo <= ammoLimit )
			{
				//Destroy(arrow);
				ammo++;
			}
		}
		//fire arrow
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if(ammo > 0)
			{
				Instantiate(playerArrow, transform.position, transform.rotation);
				playerArrow.rigidbody2D.velocity = arrowDir * arrowVelocity;
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
	}
	
	private void Move()
	{
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
		
		translate = new Vector3(h, v, 0);
		translate = translate.normalized;
		transform.position += translate * moveSpeed * Time.deltaTime;
	}

	public void Catch()
	{


	}

	private void resetPlayer() {
		var spawnpoint = GameObject.FindWithTag ("Respawn").transform;
		transform.position = spawnpoint.position;
		//set initial health
		health = 10;
	}
	///cause player damage (collision with box collider)
	public void OnTriggerEnter2D(Collider2D col) {
		// This collision detection SHOULD BE WORKING, I tested it with a local object I made myself
		// but it doesn't seem to be working with the assets. For now I kept a placeholder
		// name "Arrow" which I uh, tried before and didn't work. 
		// Also tested using key inputs and taking damage works.
		if (col.gameObject.tag.Equals("EnemyArrow")) 
		{
			health--;
			Destroy(col.gameObject);
		}
	}
	public void BoundaryCheck() 
	{ 
			
			float xboundary = Mathf.Clamp(transform.position.x,minX,maxX);
			float yboundary = Mathf.Clamp(transform.position.y,minY,maxY);
		    transform.position = new Vector3 (xboundary, yboundary, 0);

	
	
	}
}