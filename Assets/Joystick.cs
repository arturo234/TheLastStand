using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Joystick : MonoBehaviour {

	public GameObject joyStick;
	public GameObject joyBase;
	public Text debugger;
	Touch t;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			if (joyStick.transform.collider2D.OverlapPoint(Input.GetTouch(0).position)) {
				joyStick.transform.position = Input.GetTouch(0).position;
			} else {
					joyStick.transform.position = joyBase.transform.position;
			}
			debugger.text = joyStick.transform.collider2D.OverlapPoint(Input.GetTouch(0).position).ToString();
		}
	}
}
