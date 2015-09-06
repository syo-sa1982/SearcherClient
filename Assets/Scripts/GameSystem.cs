using UnityEngine;
using System.Collections;

public class GameSystem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void GameStart()
	{
		
		StartCoroutine (CheckUser());
	}

	public IEnumerator CheckUser()
	{
		if (PlayerPrefs.HasKey ("uuid")) {
			Debug.Log ("持ってる");
			yield return null;
		} else {
			Debug.Log ("持って無い");
			yield return null;
			Application.LoadLevel ("Signup");
		}
	}
}
