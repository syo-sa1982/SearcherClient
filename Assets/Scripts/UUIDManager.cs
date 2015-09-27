using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UUIDManager : MonoBehaviour 
{
	private string _uuid;

	[SerializeField]
	private InputField nameField;

	void Awake()
	{
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
		if (!PlayerPrefs.HasKey ("uuid")) {
			System.Guid guid = System.Guid.NewGuid();
			_uuid = guid.ToString();
		}
		Debug.Log ("Save");
		Debug.Log (_uuid);
		Debug.Log (nameField);


		PlayerPrefs.SetString("uuid",_uuid);
		PlayerPrefs.Save();

		StartCoroutine(AddNewUser());
	
	}

	public IEnumerator AddNewUser()
	{
		Debug.Log ("AddNewUser");
		Debug.Log (_uuid);

		string url = ConfURL.URL_DEBUG+ConfURL.USER_ADD;
		WWWForm form = new WWWForm ();

		form.AddField ("uuid", _uuid);
		form.AddField ("name", nameField.text);
		form.AddField ("roll_count", "3");

		WWW www = new WWW(url, form);

		yield return www;

		Debug.Log (www);

		if (www.error != null) {
			Debug.Log("Error");
		} else {
			Debug.Log("Success");
		}


	}




	public void Load()
	{
		_uuid = PlayerPrefs.GetString ("uuid");
		Debug.Log (_uuid);
	}
}
