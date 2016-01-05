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
			//プレイヤーの向きを変えて
			transform.rotation = Quaternion.LookRotation(targetDirection);
			//CharacterControllerコンポーネントを呼び出し
			CharacterController conroller = GetComponent<CharacterController>();
			//移動
			conroller.Move(transform.forward * Time.deltaTime * 3f);
		}
	}
}
