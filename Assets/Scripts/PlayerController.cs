using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
	private float moveSpeed = 3.5f; // movement speed of the player
	
	void LateUpdate()
    {
        Move();
        RotateToMouse();
	}

    private void RotateToMouse()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 diff = Camera.main.ScreenToWorldPoint(mousePosition) - transform.position;
        diff.Normalize();

        float rotation = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);
    }

    private void Move()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 translate = new Vector3(h, v, 0);
        translate = translate.normalized;
        transform.position += translate * moveSpeed * Time.deltaTime;
    }
}
