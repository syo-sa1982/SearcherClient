using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using MiniJSON;

public class CharaMake : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ParamGenerate ()
	{
		Debug.Log ("押した");

		StartCoroutine (GetDiceRollResult());

	}


	public IEnumerator GetDiceRollResult()
	{
		string url = "http://localhost:8000/player/base_make";
		//		WWWForm form = new WWWForm ();
		//
		//		form.AddField ("uuid", _uuid);
		//
		//		Debug.Log ("持ってる");

		WWW www = new WWW(url);

		yield return www;

		if (www.error != null) {
			Debug.Log ("error");
		} else {
			Debug.Log ("success");

			var jsonData = MiniJSON.Json.Deserialize (www.text) as Dictionary<string,object>;
			Debug.Log(www.text);
			
		}


	}
}
