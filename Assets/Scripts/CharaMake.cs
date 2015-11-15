using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using MiniJSON;

public class CharaMake : MonoBehaviour 
{

	[SerializeField]
	private InputField Strength,Constitution,Power,Dextality,Appeal,Size,Intelligence,Education;

	[SerializeField]
	private Text Hp,Mp,Sanity,Luck,Knowledge,Idea,JobSkillPoint,HobbySkillPoint,DamageBonus;

	[SerializeField]
	private Button submitBtn;

	private Dictionary<string, string> RollData;
	private Dictionary<string, InputField> BaseStatus;
	private Dictionary<string, Text> CharaStatus;

	void Awake () 
	{
		submitBtn.gameObject.SetActive (false);

		string roll3D6 = "6,3";// 3D6
		string roll2D6Plus6 = "6,2,6";// 2D6+6
		string roll3D6Plus3 = "6,3,3";// 3D6+3

		RollData = new Dictionary<string, string> () 
		{
			{"Strength" , roll3D6 },
			{"Constitution", roll3D6 },
			{"Power", roll3D6 },
			{"Dextality", roll3D6 },
			{"Appeal", roll3D6 },
			{"Size", roll2D6Plus6 },
			{"Intelligence", roll2D6Plus6 },
			{"Education", roll3D6Plus3 },
		};

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
		string url = ConfURL.URL_DEBUG+ConfURL.PLAYER_BASE_MAKE;
		WWWForm form = new WWWForm ();

		foreach (KeyValuePair<string,string> data in RollData) {
			form.AddField (data.Key, data.Value);
		}


		WWW www = new WWW(url, form);

		yield return www;

		if (www.error != null) {
			Debug.Log ("error");
		} else {
			Debug.Log ("success");
			Debug.Log (www.text);
			submitBtn.gameObject.SetActive (true);

			var charaAPI = MiniJSON.Json.Deserialize (www.text) as Dictionary<string,object>;

			var BaseStatusAPI = charaAPI ["BaseStatus"] as Dictionary<string,object>;
			var CharaStatusAPI = charaAPI ["CharaStatus"] as Dictionary<string,object>;

			Debug.Log (BaseStatusAPI);
			Debug.Log (CharaStatusAPI);

			foreach(KeyValuePair<string, object> data in BaseStatusAPI) {
				BaseStatus[data.Key].text = data.Value.ToString ();
			}
			foreach(KeyValuePair<string, object> data in CharaStatusAPI) {

				Debug.Log (data.Key);
				Debug.Log (data.Value);

				CharaStatus[data.Key].text = data.Value.ToString ();
			}
		}
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
		form.AddField ("JobID", JobSelect.SelectJob);

		foreach (KeyValuePair<string,InputField> data in BaseStatus) {
			form.AddField (data.Key, data.Value.text.ToString());
		}
		WWW www = new WWW(url, form);

		yield return www;

		if (www.error != null) {
			Debug.Log("Error");
		} else {
			Debug.Log("Success");
			Application.LoadLevel ("SkillSet");
		}

	}
}
