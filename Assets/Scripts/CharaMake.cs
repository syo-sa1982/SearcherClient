using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using MiniJSON;

public class CharaMake : MonoBehaviour {

	[SerializeField]
	private InputField Strength,Constitution,Power,Dextality,Appeal,Size,Intelligence,Education;

	[SerializeField]
	private Text Hp,Mp,Sanity,Luck,Knowledge,Idea,JopSkillPoint,HobbySkillPoint,DamageBonus;

	private Dictionary<string, string> RollData;
	private Dictionary<string, InputField> BaseStatus;
	private Dictionary<string, Text> CharaStatus;

	void Awake () 
	{
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
			{"JopSkillPoint",JopSkillPoint},
			{"HobbySkillPoint",HobbySkillPoint},
			{"DamageBonus",DamageBonus}
		};



	}

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

			var charaAPI = MiniJSON.Json.Deserialize (www.text) as Dictionary<string,object>;

			var BaseStatusAPI = charaAPI ["BaseStatus"] as Dictionary<string,object>;
			var CharaStatusAPI = charaAPI ["CharaStatus"] as Dictionary<string,object>;

			Debug.Log (BaseStatusAPI);
			Debug.Log (CharaStatusAPI);

			foreach(KeyValuePair<string, object> data in BaseStatusAPI) {
				BaseStatus[data.Key].text = data.Value.ToString ();
			}
			foreach(KeyValuePair<string, object> data in CharaStatusAPI) {
				CharaStatus[data.Key].text = data.Value.ToString ();
			}
		}
	}
}
