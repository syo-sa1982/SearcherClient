using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class JobSelect : MonoBehaviour 
{

	public GameObject JobListContent;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (showJobList());
	}

	public IEnumerator showJobList()
	{
		string _uuid;
		if (PlayerPrefs.HasKey ("uuid")) {
			_uuid = PlayerPrefs.GetString ("uuid");
		} else {
			yield break;
		}

		string url = ConfURL.URL_DEBUG+ConfURL.PLAYER_JOBLIST;
		WWWForm form = new WWWForm ();
		form.AddField ("UUID", _uuid);

		WWW www = new WWW(url, form);

		yield return www;

		if (www.error != null) {
			Debug.Log ("Error");
		} else {
			Debug.Log ("Success");

			Debug.Log (www.text);
			var jobAPI = MiniJSON.Json.Deserialize(www.text) as Dictionary<string,object>;
			Debug.Log(jobAPI);

			var JobList = jobAPI["JobMaster"] as List<object>;


			foreach(var data in JobList) {
				var JobData = data as Dictionary<string,object>;
				Debug.Log(JobData);
				Debug.Log(JobData["JobName"]);

				GameObject jobField = (GameObject)Instantiate(Resources.Load("Prefabs/JobSelect/JobField"));
				jobField.transform.SetParent(JobListContent.transform,false);
				JobFieldController fieldController = jobField.GetComponent<JobFieldController> ();
				fieldController.setJobData (JobData);
			}
		}
	}
}
