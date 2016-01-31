using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using LitJson;

public class HomeUI : CommonUI 
{
	
	[SerializeField]
	private Text nameText, jobText, hpText, sanText;
	

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
		
		string url = ConfURL.HOST_NAME+ConfURL.HOME_USER_INDEX;
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
			
			User userData = LitJson.JsonMapper.ToObject<User>(LitJson.JsonMapper.ToJson(jsonData["User"]));
			Job job = LitJson.JsonMapper.ToObject<Job>(LitJson.JsonMapper.ToJson(jsonData["Job"]));
			PlayerBase playerBase = LitJson.JsonMapper.ToObject<PlayerBase>(LitJson.JsonMapper.ToJson(jsonData["PlayerBase"]));
			PlayerStatus playerStatus = LitJson.JsonMapper.ToObject<PlayerStatus>(LitJson.JsonMapper.ToJson(jsonData["PlayerStatus"]));
			
			Debug.Log(userData.Name);
			Debug.Log(job.JobName);
			Debug.Log(playerBase.ID);
			Debug.Log(playerStatus.HP);
			Debug.Log(playerStatus.Sanity);
			nameText.text = userData.Name;
			jobText.text = job.JobName;
			hpText.text = playerStatus.HP.ToString();
			sanText.text = playerStatus.Sanity.ToString();
		}
	}
	
}
