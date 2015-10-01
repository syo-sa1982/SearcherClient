using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using MiniJSON;

public class Title : MonoBehaviour {

	private string _uuid;

	public GameObject canvasObject;

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
			GameObject nameAddWindow = (GameObject)Instantiate (Resources.Load ("Prefabs/NameAdd"));
			nameAddWindow.transform.SetParent (canvasObject.transform,false);
//			Application.LoadLevel ("Signup");
		}
	}

	public IEnumerator AuthUser()
	{
		_uuid = PlayerPrefs.GetString ("uuid");
		Debug.Log (_uuid);



		string url = ConfURL.URL_DEBUG + ConfURL.USER_AUTH;
		WWWForm form = new WWWForm ();

		form.AddField ("uuid", _uuid);

		WWW www = new WWW(url, form);
		Instantiate (Resources.Load ("Prefabs/Loading"));
		yield return www;


		Debug.Log (www);

		if (www.error != null) {
			Debug.Log("Error");
		} else {
			Debug.Log("Success");
			var jsonData = MiniJSON.Json.Deserialize (www.text) as Dictionary<string,object>;
			Debug.Log(www.text);
			Debug.Log((string)jsonData["UUID"]);

			if ((string)jsonData ["UUID"] == _uuid) {
				Debug.Log ("登録ユーザーです");
				Application.LoadLevel ("CharaMake");
			} else {
				Debug.Log ("登録ユーザーではない");
			}


		}

	}
}
