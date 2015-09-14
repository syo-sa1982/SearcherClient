using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using MiniJSON;

public class CharaMake : MonoBehaviour {

	[SerializeField]
	private int Strength;
	[SerializeField]
	private int Constitution;
	[SerializeField]
	private int Power;
	[SerializeField]
	private int Dextality;
	[SerializeField]
	private int Appeal;
	[SerializeField]
	private int Size;
	[SerializeField]
	private int Intelligence;
	[SerializeField]
	private int Education;

	private Dictionary<string, string> RollData = new Dictionary<string, string> ();

	void Awake () 
	{
		string roll3D6 = "6,3";// 3D6
		string roll2D6Plus6 = "6,2,6";// 2D6+6
		string roll3D6Plus3 = "6,3,3";// 3D6+3

		RollData.Add ("Strength" , roll3D6);
		RollData.Add ("Constitution", roll3D6);
		RollData.Add ("Power", roll3D6);
		RollData.Add ("Dextality", roll3D6);
		RollData.Add ("Appeal", roll3D6);

		RollData.Add ("Size", roll2D6Plus6);
		RollData.Add ("Intelligence", roll2D6Plus6);

		RollData.Add ("Education", roll3D6Plus3);

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

			var jsonData = MiniJSON.Json.Deserialize (www.text) as Dictionary<string,object>;
			Debug.Log(www.text);
			
		}


	}
}
