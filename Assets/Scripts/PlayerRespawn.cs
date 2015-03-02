using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
		// Use this for initialization
		void Start (){
		}
		// Update is called once per frame
		void Update (){
			if(Input.GetKey (KeyCode.R)){
				var player = GameObject.Find("Player");
				var spawnpoint = GameObject.FindWithTag ("Respawn").transform;
				player.transform.position = spawnpoint.position;
			}
		}
}

