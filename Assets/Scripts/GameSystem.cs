using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using MiniJSON;

public class GameSystem : MonoBehaviour {

	private string _uuid;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void GameStart()
	{

		if (PlayerPrefs.HasKey ("uuid")) {
			StartCoroutine (AuthUser ());
		} else {
			Debug.Log ("持って無い");
			Application.LoadLevel ("Signup");
		}
	}

	public IEnumerator AuthUser()
	{
		_uuid = PlayerPrefs.GetString ("uuid");
		Debug.Log (_uuid);

		string url = "http://localhost:8000/user/auth";
		WWWForm form = new WWWForm ();

		form.AddField ("uuid", _uuid);

		Debug.Log ("持ってる");

		WWW www = new WWW(url, form);

		yield return www;


		Debug.Log (www);

		if (www.error != null) {
			Debug.Log("Error");
		} else {
			Debug.Log("Success");
			var jsonData = MiniJSON.Json.Deserialize (www.text);
			Debug.Log(www.text);
			Debug.Log(jsonData);



		}

	}
}
