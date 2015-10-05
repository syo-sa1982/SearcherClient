using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using MiniJSON;

public class CharaSelect : MonoBehaviour {

	private string _uuid;

	// Use this for initialization
	void Start () 
	{
		_uuid = PlayerPrefs.GetString ("uuid");
		StartCoroutine (GetCharaList ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator GetCharaList()
	{
		Debug.Log ("GetCharaList");

		string url = ConfURL.URL_DEBUG+ConfURL.PLAYER_LIST;
		WWWForm form = new WWWForm ();

		form.AddField ("UUID", _uuid);

		WWW www = new WWW(url, form);
		yield return www;

		Debug.Log (www);

		if (www.error != null) {
			Debug.Log("Error");
		} else {
			Debug.Log("Success");
			var charaAPI = MiniJSON.Json.Deserialize (www.text) as Dictionary<string,object>;


			foreach(KeyValuePair<string, object> data in charaAPI) {
				Debug.Log(data.Key);
				Debug.Log(data.Value);
			}

		}
	}
}
