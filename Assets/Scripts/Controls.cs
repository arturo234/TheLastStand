using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Controls : MonoBehaviour {
	public GameObject mobilePanel, pcPanel;
	Vector3 translate;
	public GameObject joyStick;
	public Button catchButton;
	public bool grab;
	Joystick script;

	// Use this for initialization
	void Start () {
		if (Application.isMobilePlatform) {
			mobilePanel.SetActive(true);
			pcPanel.SetActive(false);
		} else {
			mobilePanel.SetActive(false);
			pcPanel.SetActive(true);
		}
		script = joyStick.transform.GetComponent<Joystick>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.isMobilePlatform) {
			translate = script.getTransform();
		} else {
			translate = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
		}

		grab = Input.GetButtonDown ("Fire2");
	}

	public Vector3 getTranslate() {
		return translate;
	}

	public Vector3 getGamePadTranslate() {
		if (!Application.isMobilePlatform)
			return new Vector3 (Input.GetAxis ("JoystickX"), Input.GetAxis ("JoystickY"), 0);
		else {
			return translate;
		}
	}

	public void Catch() {
		grab = true;
	}
}