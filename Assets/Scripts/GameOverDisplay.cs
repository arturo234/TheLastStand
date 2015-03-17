using UnityEngine;
using System.Collections;

public class GameOverDisplay : MonoBehaviour {
	// Test comment
	// Use this for initialization
	public void OnClickRetry()
	{
		//Application.LoadLevel("BasicLevel");
		Application.LoadLevel("LevelSelectMenu");
	}
	public void OnClickQuit()
	{
		Application.Quit ();
	}
}
