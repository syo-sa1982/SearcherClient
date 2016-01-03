using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour 
{
	public GameObject player;
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1.25f, player.transform.position.z - 1);
	}
}
