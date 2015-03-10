using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public void OnClickStart()
	{
		Application.LoadLevel("BasicLevel");
	}

}