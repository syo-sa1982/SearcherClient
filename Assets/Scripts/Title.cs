using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using MiniJSON;

public class Title : MonoBehaviour {

	private string _uuid;

	[SerializeField]
//	private AudioClip TitleSE;
	private AudioSource audioSource;

	public GameObject canvasObject;

	public void GameStart()
	{

		if (PlayerPrefs.HasKey ("uuid")) {
			StartCoroutine (AuthUser ());
		} else {
			Debug.Log ("持って無い");
			GameObject nameAddWindow = (GameObject)Instantiate (Resources.Load ("Prefabs/NameAdd"));
			nameAddWindow.transform.SetParent (canvasObject.transform,false);
		}
	}

	public IEnumerator AuthUser()
	{
		_uuid = PlayerPrefs.GetString ("uuid");
		Debug.Log (_uuid);

		string url = ConfURL.URL_DEBUG + ConfURL.USER_AUTH;
		WWWForm form = new WWWForm();

		form.AddField ("uuid", _uuid);

		WWW www = new WWW(url, form);

		GameObject loading = (GameObject)Instantiate (Resources.Load("Prefabs/Loading"));
		loading.transform.SetParent(canvasObject.transform,false);

		yield return www;
		Destroy (loading);

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
				audioSource.Play ();

				Application.LoadLevel ("CharaSelect");
			} else {
				Debug.Log ("登録ユーザーではない");
			}
		}

	}
}
