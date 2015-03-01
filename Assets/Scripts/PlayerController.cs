using UnityEngine;
using System.Collections;



public class PlayerController : MonoBehaviour{
	public float walkSpeed = 3.5f; //movement speed of the player
	
	
	void Start(){
		
	}
	
	
	void Update(){
		//WASD Controls
		Vector3 horizontal = new Vector3(walkSpeed * Time.deltaTime, 0.0f, 0.0f);//player's horizontal vector
		Vector3 vertical = new Vector3(0.0f,walkSpeed * Time.deltaTime, 0.0f);//player's vertical vector

		if (Input.GetKey (KeyCode.D)) {
			transform.position += horizontal;
		} 
		if (Input.GetKey (KeyCode.A)) {
			transform.position -= horizontal;
		} 
		if (Input.GetKey (KeyCode.W)) {
			transform.position += vertical;
		} 
		if (Input.GetKey (KeyCode.S)) {
			transform.position -= vertical;
		}
		
		//arrow key controls 
		
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.position += horizontal;
		} 
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.position -= horizontal;
		} 
		if (Input.GetKey (KeyCode.UpArrow)) {
			transform.position += vertical;
		} 
		if (Input.GetKey (KeyCode.DownArrow)) {
			transform.position -= vertical;
		}
	
		//Mouse Aiming 

		Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 8);//mouse's position

		Vector3 lookPosition = Camera.main.ScreenToWorldPoint(mousePosition);//player's position

		lookPosition = lookPosition - transform.position;

		float angle = Mathf.Atan2(lookPosition.y, lookPosition.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

	
	}
}
