using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using LitJson;

public class HomeUI : CommonUI 
{

	// Use this for initialization
	void Start () 
	{
		StartCoroutine(showUserInfo());
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	IEnumerator showUserInfo()
	{
		string _uuid;
		if (PlayerPrefs.HasKey ("uuid")) {
			_uuid = PlayerPrefs.GetString ("uuid");
		} else {
			yield break;
		}
		
		string url = ConfURL.URL_DEBUG+ConfURL.HOME_USER_INDEX;
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
		}
	}
	
}
