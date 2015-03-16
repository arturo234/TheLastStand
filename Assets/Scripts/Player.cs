using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : GenericCharacter {
	public float moveSpeed, ammo, ammoLimit;
	public Text[] livesUI, ammosUI;
	Text liveUI, ammoUI;
	public GameObject controls;
	Controls script;
	Vector3 mousePosition, diff, translate;

	public float minX; //left boundary 
	public float maxX; //right boundary 
	public float minY; //up boundary 
	public float maxY; //down boundary

	// Use this for initialization
	void Start () {
		if (!Application.isMobilePlatform) {
			liveUI = livesUI [0];
			ammoUI = ammosUI [0];
		} else {
			liveUI = livesUI [1];
			ammoUI = ammosUI [1];
			ammo ++;
		}
		script = controls.transform.GetComponent<Controls>();
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

		//"Fire1" is the left mouse button, left ctrl, or gamepad button 0 (A button on xbox360 remote)
		if (Input.GetButtonDown("Fire1"))//(Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.Space))
		{
			Fire();
		}
		ammoUI.text = "Ammo: " + ammo;
		liveUI.text = "Lives: " + health;
		////////////////////////////////////////////cheat codes!
		if (Input.GetKey (KeyCode.F) && Input.GetKey (KeyCode.H)) 
		{
			health = 1000;
		}
		if (Input.GetKey (KeyCode.F) && Input.GetKey (KeyCode.B)) 
		{
			ammo = 1000;
		}
		/////////////////////////////////////////////////////////
	}
	
	private void RotateToMouse()
	{
		float rotation;
		translate = script.getGamePadTranslate();
		if (translate.x != 0.0 || translate.y != 0.0)
		{
			rotation = Mathf.Atan2(translate.y, translate.x) * Mathf.Rad2Deg;
			//transform.rotation = Quaternion.AngleAxis(90.0 - angle, Vector3.up);
			transform.rotation = Quaternion.Euler(0f, 0f, rotation);
			arrowDir = transform.rotation.eulerAngles;
		}
		else
		{
			mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

			diff = Camera.main.ScreenToWorldPoint(mousePosition) - transform.position;
			diff.Normalize();
			
			rotation = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rotation);
			arrowDir = transform.rotation.eulerAngles;
		}
	}
	
	private void Move()
	{	
		///works with both keyboard and gamepad
		translate = script.getTranslate ();
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

	public void Fire() {
		if(ammo > 0)
		{
			fireArrow("PlayerArrow");
			ammo--;
		}
		else
		{
			Debug.Log("Out of ammo");
		}
	}
	
	public void addAmmo(int difference) {
		if(ammo > 0)
			ammo += difference;
	}
}