using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillSet : MonoBehaviour
{

	public GameObject canvasObject;

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
		string _uuid;
		if (PlayerPrefs.HasKey ("uuid")) {
			_uuid = PlayerPrefs.GetString ("uuid");
		} else {
			yield break;
		}

		string url = ConfURL.URL_DEBUG+ConfURL.PLAYER_SKILL_SETTING;
		WWWForm form = new WWWForm ();
		form.AddField ("UUID", _uuid);

		WWW www = new WWW(url, form);

		yield return www;

		if (www.error != null) {
			Debug.Log("Error");
		} else {
			Debug.Log("Success");

			Debug.Log(www.text);
			var skillSetAPI = MiniJSON.Json.Deserialize (www.text) as Dictionary<string,object>;
			Debug.Log(skillSetAPI);

			var skillList = skillSetAPI ["SkillMaster"] as Dictionary<string,object>;
			var playerStatus = skillSetAPI ["PlayerStatus"] as Dictionary<string,object>;
			foreach(var data in skillList) {
				var SkillData = data.Value as Dictionary<string,object>;
				Debug.Log(data.Key);
				Debug.Log (SkillData ["SkillName"]);
				foreach(var dataValue in SkillData) {
					Debug.Log(dataValue.Key);
					Debug.Log(dataValue.Value);
				}
				GameObject skillField = (GameObject)Instantiate(Resources.Load("Prefabs/SkillSet/SkillField"));
				skillField.transform.SetParent(canvasObject.transform,false);
			}
		}
	}
}
