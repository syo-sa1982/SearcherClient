using UnityEngine;
using System.Collections;

public class CharaSelect : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (GetCharaList ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator GetCharaList()
	{
		Debug.Log ("GetCharaList");

		string url = ConfURL.URL_DEBUG+ConfURL.USER_ADD;
		WWWForm form = new WWWForm ();

		form.AddField ("roll_count", "3");

		WWW www = new WWW(url, form);
		yield return www;

	}
}
