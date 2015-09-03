using UnityEngine;
using System.Collections;

public class UUIDManager : MonoBehaviour 
{
	private string _uuid;

	void Awake()
	{
		if (!PlayerPrefs.HasKey ("uuid")) {
			System.Guid guid = System.Guid.NewGuid();
			_uuid = guid.ToString();
		}
		Debug.Log ("awake");
		Debug.Log (_uuid);

	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Save()
	{
		Debug.Log ("Save");
		Debug.Log (_uuid);

		PlayerPrefs.SetString("uuid",_uuid);
		PlayerPrefs.Save();

		StartCoroutine(AddNewUser());
	
	}

	public IEnumerator AddNewUser()
	{
		Debug.Log ("AddNewUser");
		Debug.Log (_uuid);

		string url = "http://localhost:8000/user/add";
		WWWForm wwwForm = new WWWForm ();

		wwwForm.AddField ("uuid", _uuid);
		wwwForm.AddField ("roll_count", "3");

		WWW www = new WWW(url, wwwForm);

		yield return www;
	}




	public void Load()
	{
		_uuid = PlayerPrefs.GetString ("uuid");
		Debug.Log (_uuid);
	}
}
