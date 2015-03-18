using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Joystick : MonoBehaviour {
	
	public GameObject joyBase;
	public Text debugger;
	Vector3 standardPosition;
	public float angle;
	Vector3 dir;

	// Use this for initialization
	void Start () {
		standardPosition = new Vector3 (joyBase.transform.position.x, joyBase.transform.position.y, -1);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			transform.position = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			transform.position = new Vector3(transform.position.x,transform.position.y, -1);
		} else {
			transform.position = standardPosition;
		}

		Vector3 dir = standardPosition - transform.position;
		angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
	}

	public Vector3 getTransform() {
		if (Vector2.Distance(transform.position, standardPosition) < 1) {
			return new Vector3 (0, 0, 0);
		} else {
			return new Vector3(Mathf.Cos(angle * Mathf.PI/180) * -1, Mathf.Sin(angle * Mathf.PI/180) * -1);
		}
	}
}