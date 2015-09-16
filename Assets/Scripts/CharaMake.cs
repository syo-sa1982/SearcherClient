using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using MiniJSON;

public class CharaMake : MonoBehaviour {

	[SerializeField]
	private InputField Strength;
	[SerializeField]
	private InputField Constitution;
	[SerializeField]
	private InputField Power;
	[SerializeField]
	private InputField Dextality;
	[SerializeField]
	private InputField Appeal;
	[SerializeField]
	private InputField Size;
	[SerializeField]
	private InputField Intelligence;
	[SerializeField]
	private InputField Education;

	private Dictionary<string, string> RollData;
	private Dictionary<string, InputField> BaseStatus;

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
			Debug.Log(charaAPI);
			Debug.Log(charaAPI["Strength"]);


			foreach(KeyValuePair<string, object> data in charaAPI) {
				
				Debug.Log(data.Key);
				Debug.Log(data.Value);
				Debug.Log(data.Value.ToString());
//				BaseStatus[data.Key].text = data.Value.ToString();
			}
		}
	}
}
