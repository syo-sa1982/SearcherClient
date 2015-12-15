using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class JobSelect : MonoBehaviour 
{

	public GameObject JobListContent;

	[SerializeField]
	private ToggleGroup mToggleGroup;
	[SerializeField]
	private Button submitBtn;

	// Use this for initialization
	void Start () 
	{
		submitBtn.gameObject.SetActive (false);
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

		WWW www = new WWW(url);

		yield return www;

			Debug.Log (www.text);
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
				jobField.GetComponent<Toggle> ().group = mToggleGroup;


				JobFieldController fieldController = jobField.GetComponent<JobFieldController> ();
				fieldController.setJobData (JobData);
			}
		}
	}

	public void ChangeSelectJob(object checkJob)
	{
		submitBtn.gameObject.SetActive (true);
		int selectJob = System.Convert.ToInt32(checkJob);
		Debug.Log (selectJob);

		PlayerPrefs.SetInt("jobid",selectJob);
		PlayerPrefs.Save();
	}

	public void SendJobID()
	{
		SceneManager.LoadScene("CharaMake");
	}

}
