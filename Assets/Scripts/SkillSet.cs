using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using LitJson;

public class SkillSet : MonoBehaviour
{

	public GameObject canvasObject;

	const int HIDE_CATEGORY = 6;
	const int MAX_JOB_SKILL = 8;
//
//	[SerializeField]
	public int jobSkillPoint = 0, hobbySkillPoint = 0;

	public PlayerStatus playerStatus;

	[SerializeField]
	private Text JobSkillPointText, HobbySkillPointText, JobText;

	[SerializeField]
	private Button submitBtn;

	public JobSkill[] JobSkillArray = new JobSkill[]{};

	public int SelectJobSkillMaxNum;

	// Use this for initialization
	void Start ()
	{
		submitBtn.gameObject.SetActive (false);
		StartCoroutine (showSkillMasterList());
	}
	
	// Update is called once per frame
	void Update ()
	{
		JobSkillPointText.text = jobSkillPoint.ToString();
		HobbySkillPointText.text = hobbySkillPoint.ToString();
		if (jobSkillPoint == 0 && hobbySkillPoint == 0) {
			submitBtn.gameObject.SetActive (true);
		} else {
			submitBtn.gameObject.SetActive (false);
		}
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

			JsonData jsonData = LitJson.JsonMapper.ToObject(www.text);

			JobSkillArray = LitJson.JsonMapper.ToObject<JobSkill[]>(LitJson.JsonMapper.ToJson(jsonData["JobSkillMaster"]));
			Skill[] SkillList = LitJson.JsonMapper.ToObject<Skill[]>(LitJson.JsonMapper.ToJson(jsonData["SkillMaster"]));

			foreach(var skillData in SkillList) {
				if (skillData.CategoryID != HIDE_CATEGORY) {
					GameObject skillField = (GameObject)Instantiate(Resources.Load("Prefabs/SkillSet/SkillField"));
					skillField.transform.SetParent(canvasObject.transform,false);
					SkillFieldController fieldController = skillField.GetComponent<SkillFieldController>();
					fieldController.setSkillData (skillData);
				}
			}

			playerStatus = LitJson.JsonMapper.ToObject<PlayerStatus>(LitJson.JsonMapper.ToJson(jsonData["PlayerStatus"]));
			jobSkillPoint = playerStatus.JobSkillPoint;
			JobSkillPointText.text = jobSkillPoint.ToString();
			hobbySkillPoint = playerStatus.HobbySkillPoint;
			HobbySkillPointText.text = hobbySkillPoint.ToString();

			Job JobData = LitJson.JsonMapper.ToObject<Job>(LitJson.JsonMapper.ToJson(jsonData["JobMaster"]));
			JobText.text = JobData.JobName;

		}
	}
}

