using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

using LitJson;

public class SkillSet : MonoBehaviour
{


	const int MAX_JOB_SKILL = 8;
	
	public GameObject ListObject;
	public int jobSkillPoint = 0, hobbySkillPoint = 0;
	public PlayerStatus playerStatus;
	public JobSkill[] jobSkillArray = new JobSkill[]{};
	public int SelectJobSkillMaxNum = 8;

	public List<int> selectedSkillID = new List<int>();

	[SerializeField]
	private Text JobSkillPointText, HobbySkillPointText, JobText;
	[SerializeField]
	private Button submitBtn;

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

	IEnumerator showSkillMasterList()
	{
		string _uuid;
		if (PlayerPrefs.HasKey ("uuid")) {
			_uuid = PlayerPrefs.GetString ("uuid");
		} else {
			yield break;
		}

		string url = ConfURL.HOST_NAME+ConfURL.PLAYER_SKILL_SETTING;
		WWWForm form = new WWWForm ();
		form.AddField ("UUID", _uuid);

		WWW www = new WWW(url, form);

		yield return www;

		if (www.error != null) {
			Debug.Log("Error");
		} else {
			Debug.Log("Success");

			Debug.Log(www.text);
			JsonData jsonData = LitJson.JsonMapper.ToObject(www.text);

			jobSkillArray = LitJson.JsonMapper.ToObject<JobSkill[]>(LitJson.JsonMapper.ToJson(jsonData["JobSkillMaster"]));
			Skill[] SkillList = LitJson.JsonMapper.ToObject<Skill[]>(LitJson.JsonMapper.ToJson(jsonData["SkillMaster"]));

			foreach(var skillData in SkillList) {
				GameObject skillField = (GameObject)Instantiate(Resources.Load("Prefabs/SkillSet/SkillField"));
				skillField.transform.SetParent(ListObject.transform,false);
				SkillFieldController fieldController = skillField.GetComponent<SkillFieldController>();
				fieldController.setSkillData (skillData);
			}

			Debug.Log("selectSkill is " + SelectJobSkillMaxNum);

			playerStatus = LitJson.JsonMapper.ToObject<PlayerStatus>(LitJson.JsonMapper.ToJson(jsonData["PlayerStatus"]));
			jobSkillPoint = playerStatus.JobSkillPoint;
			JobSkillPointText.text = jobSkillPoint.ToString();
			hobbySkillPoint = playerStatus.HobbySkillPoint;
			HobbySkillPointText.text = hobbySkillPoint.ToString();

			Job JobData = LitJson.JsonMapper.ToObject<Job>(LitJson.JsonMapper.ToJson(jsonData["JobMaster"]));
			JobText.text = JobData.JobName;

		}
	}
	
	public void SubmitSkillSet()
	{
		StartCoroutine(sendSkillSetApi());
	}

	IEnumerator sendSkillSetApi()
	{
		string _uuid;
		if (PlayerPrefs.HasKey ("uuid")) {
			_uuid = PlayerPrefs.GetString ("uuid");
		} else {
			yield break;
		}
		string url = ConfURL.HOST_NAME+ConfURL.PLAYER_SKILL_SUBMIT;
		WWWForm form = new WWWForm ();
		form.AddField ("UUID", _uuid);

		var skillList = ListObject.GetComponentsInChildren(typeof(SkillFieldController));
		List<Skill> skillApi = new List<Skill>();
		foreach(SkillFieldController skill in skillList){
			skillApi.Add(skill.SkillData);
			Debug.Log( skill.SkillData.SkillName);
		}
		form.AddField ("json_api", LitJson.JsonMapper.ToJson(skillApi));
		form.AddField ("player_id", playerStatus.ID);
		
		WWW www = new WWW(url, form);

		yield return www;
		
		
		SceneManager.LoadScene ("Home");
	}



}

