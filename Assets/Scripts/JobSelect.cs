using UnityEngine;
using System.Collections;

public class JobSelect : MonoBehaviour 
{

	public GameObject canvasObject;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (showJobList());
	}
	
	// Update is called once per frame
	void Update () 
	{
	
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

		WWW www = new WWW(url);

		yield return www;

		if (www.error != null) {
			Debug.Log ("Error");
		} else {
			Debug.Log ("Success");

			Debug.Log (www.text);
		}
	}
}
