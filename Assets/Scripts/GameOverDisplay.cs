using UnityEngine;
using System.Collections;

public class GameOverDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey (KeyCode.R)) 
		{
			Application.LoadLevel("BasicLevel");
		}
	}
	void OnGUI()
	{
		GUI.Label (new Rect (200, 200, 100, 100), "Game Over-Press R to restart");
		//GUI.Label (new Rect (100, 150, 100, 30), "Score: " + (int)(playerScore * 100));
		
	}
}
