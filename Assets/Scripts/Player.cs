using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public Vector3 targetDirection;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		targetDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		
		if (targetDirection.magnitude > 0.1) {
			GetComponent<Animation>().Play("Take 001");
		}
	}
}
