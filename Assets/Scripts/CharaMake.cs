using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

// using MiniJSON;
// using LitJson;

public class CharaMake : MonoBehaviour 
{

	[SerializeField]
	private InputField Strength,Constitution,Power,Dextality,Appeal,Size,Intelligence,Education;

	[SerializeField]
	private Text Hp,Mp,Sanity,Luck,Knowledge,Idea,JobSkillPoint,HobbySkillPoint,DamageBonus;

	[SerializeField]
	private Button submitBtn;
	
	private PlayerBase baseStatus;
	private PlayerStatus status;

	private Dictionary<string, string> RollDic;
	private Dictionary<string, InputField> BaseStatus;
	private Dictionary<string, Text> CharaStatus;

	private int SelectJob;

	void Awake () 
	{
		SelectJob = PlayerPrefs.GetInt("jobid");
		Debug.Log (SelectJob);
		submitBtn.gameObject.SetActive (false);

		BaseStatus = new Dictionary<string, InputField> ()
		{
			{"Strength", Strength},
			{"Constitution", Constitution},
			{"Power", Power},
			{"Dextality", Dextality},
			{"Appeal", Appeal},
			{"Size", Size},
			{"Intelligence", Intelligence},
			{"Education", Education}
		};

		CharaStatus = new Dictionary<string, Text> () 
		{
			{"HP", Hp},
			{"MP",Mp},
			{"Sanity",Sanity},
			{"Luck",Luck},
			{"Knowledge",Knowledge},
			{"Idea",Idea},
			{"JobSkillPoint",JobSkillPoint},
			{"HobbySkillPoint",HobbySkillPoint},
			{"DamageBonus",DamageBonus}
		};
	}

	public void ParamGenerate()
	{
		Debug.Log ("押した");

		StartCoroutine (GetDiceRollResult());

	}

	public IEnumerator GetDiceRollResult()
	{
		string _uuid;
		if (PlayerPrefs.HasKey ("uuid")) {
			_uuid = PlayerPrefs.GetString ("uuid");
		} else {
			yield break;
		}
		string url = ConfURL.URL_DEBUG+ConfURL.PLAYER_BASE_MAKE;
		
		WWWForm form = new WWWForm ();
		form.AddField ("UUID", _uuid);
		form.AddField ("JobID", SelectJob);
		
		WWW www = new WWW(url, form);
		
		yield return www;

		if (www.error != null) {
			Debug.Log ("error");
		} else {
			Debug.Log ("success");
			Debug.Log (www.text);
			var charaAPI = JsonUtility.FromJson<RollResult>(www.text);
			
			baseStatus = charaAPI.BaseStatus;
			status = charaAPI.Status;
			
			InputBaseStatus(baseStatus);
			InputStatus(status);
		}
	}
	
	public void InputBaseStatus(PlayerBase baseStatus)
	{
		Strength.text = baseStatus.Strength.ToString();
		Constitution.text = baseStatus.Constitution.ToString();
		Power.text = baseStatus.Power.ToString();
		Dextality.text = baseStatus.Dextality.ToString();
		Appeal.text = baseStatus.Appeal.ToString();
		Size.text = baseStatus.Size.ToString();
		Intelligence.text = baseStatus.Intelligence.ToString();
		Education.text = baseStatus.Education.ToString();
	}

	public void InputStatus(PlayerStatus status)
	{
		Hp.text = status.HP.ToString();
		Mp.text = status.MP.ToString();
		Sanity.text = status.Sanity.ToString();
		Luck.text = status.Luck.ToString();
		Knowledge.text = status.Knowledge.ToString();
		Idea.text = status.Idea.ToString();
		JobSkillPoint.text = status.JobSkillPoint.ToString();
		HobbySkillPoint.text = status.HobbySkillPoint.ToString();
		DamageBonus.text = status.DamageBonus.ToString();
	}


	public void StatusSubmit()
	{
		StartCoroutine (SendPlayerStatus());
	}

	public IEnumerator SendPlayerStatus()
	{
		string _uuid;
		if (PlayerPrefs.HasKey ("uuid")) {
			_uuid = PlayerPrefs.GetString ("uuid");
		} else {
			yield break;
		}

		string url = ConfURL.URL_DEBUG+ConfURL.PLAYER_GENERATE;
		WWWForm form = new WWWForm ();
		form.AddField ("UUID", _uuid);
		form.AddField ("JobID", SelectJob);

		foreach (KeyValuePair<string,InputField> data in BaseStatus) {
			form.AddField (data.Key, data.Value.text.ToString());
		}
		WWW www = new WWW(url, form);

		yield return www;

		if (www.error != null) {
			Debug.Log("Error");
		} else {
			Debug.Log("Success");
			SceneManager.LoadScene("SkillSet");
		}

	}
}
