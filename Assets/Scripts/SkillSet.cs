using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SkillSet : MonoBehaviour
{

	public GameObject canvasObject;

	const int HIDE_CATEGORY = 13;

	[SerializeField]
	public int JobSkillPoint, HobbySkillPoint;

	[SerializeField]
	private Text JobSkillPointText, HobbySkillPointText;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine (showSkillMasterList());
	}
	
	// Update is called once per frame
	void Update ()
	{
		JobSkillPointText.text = JobSkillPoint.ToString();
		HobbySkillPointText.text = HobbySkillPoint.ToString();
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

			var skillList = skillSetAPI ["SkillMaster"] as List<object>;
			var playerStatus = skillSetAPI ["PlayerStatus"] as Dictionary<string,object>;

			JobSkillPoint = System.Convert.ToInt32(playerStatus["JobSkillPoint"]);
			HobbySkillPoint = System.Convert.ToInt32 (playerStatus["HobbySkillPoint"]);

			JobSkillPointText.text = JobSkillPoint.ToString();
			HobbySkillPointText.text = HobbySkillPoint.ToString();

			foreach(var data in skillList) {
				var SkillData = data as Dictionary<string,object>;
				if (System.Convert.ToInt32(SkillData["ID"]) != HIDE_CATEGORY) {
					GameObject skillField = (GameObject)Instantiate(Resources.Load("Prefabs/SkillSet/SkillField"));
					skillField.transform.SetParent(canvasObject.transform,false);

					SkillFieldController fieldController = skillField.GetComponent<SkillFieldController>();
					fieldController.setSkillData (SkillData);
				} 

			}
		}
	}
}
