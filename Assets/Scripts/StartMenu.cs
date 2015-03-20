using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public GameObject mobilePanel, pcPanel;

	void Start () {
		if (Application.isMobilePlatform) {
			mobilePanel.SetActive(true);
			pcPanel.SetActive(false);
		} else {
			mobilePanel.SetActive(false);
			pcPanel.SetActive(true);
		}

	}
	public void OnClickStart()
	{
		//Application.LoadLevel("BasicLevel");
		Application.LoadLevel("LevelSelectMenu");
	}
}
