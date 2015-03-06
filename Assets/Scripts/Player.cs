using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : GenericCharacter {
	public float moveSpeed, ammo, ammoLimit;
	public Text text;
	public GameObject playerArrow;
	Vector3 mousePosition, diff, translate;

    public ObjectPool playerProjectilePool;

	public float minX; //left boundary 
	public float maxX; //right boundary 
	public float minY; // up boundary 
	public float maxY; // down boundary

	// Use this for initialization
	void Start () {
		//set initial health
		health = 10;
		/// starting ammo can be set in inspector
		//ammo = 3;
        arrowVelocity = 10f;

        if (playerProjectilePool == null)
            playerProjectilePool = new ObjectPool(playerArrow, false, 16);
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

		//fire arrow
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if(ammo > 0)
			{
                GameObject arrow = playerProjectilePool.PullObject();
                arrow.transform.position = transform.position;
                arrow.transform.rotation = transform.rotation;
                arrow.GetComponent<BasicProjectile>().SpawnArrow(Time.time, (GenericCharacter)this, playerProjectilePool);
                arrow.SetActive(true);
                arrow.rigidbody2D.AddRelativeForce(new Vector2(Mathf.Cos(theta.z * Mathf.PI / 180), Mathf.Sin(theta.z * Mathf.PI / 180)) * .1f);
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
			DestroyProjectile(col.gameObject);
		}
	}

    public override void DestroyProjectile(GameObject objToDestroy)
    {

        objToDestroy.rigidbody2D.velocity = Vector2.zero;
        playerProjectilePool.PushObject(objToDestroy.gameObject.GetComponent(typeof(IPoolableObject)) as IPoolableObject);
    }

	public void BoundaryCheck() 
	{ 
			
			float xboundary = Mathf.Clamp(transform.position.x,minX,maxX);
			float yboundary = Mathf.Clamp(transform.position.y,minY,maxY);
		    transform.position = new Vector3 (xboundary, yboundary, 0);

	
	
	}
}