using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillSet : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{

		StartCoroutine (showSkillMasterList());
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public IEnumerator showSkillMasterList()
	{
		string url = ConfURL.URL_DEBUG+ConfURL.PLAYER_SKILL_SETTING;

		WWW www = new WWW(url);

		yield return www;

		if (www.error != null) {
			Debug.Log("Error");
		} else {
			Debug.Log("Success");
			var skillMasterList = MiniJSON.Json.Deserialize (www.text) as Dictionary<string,object>;

			foreach(KeyValuePair<string, object> data in skillMasterList) {
				Debug.Log(data.Key);
				Debug.Log(data.Value);
			}
		}
	}
}
