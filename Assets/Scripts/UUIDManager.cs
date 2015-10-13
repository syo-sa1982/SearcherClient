using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UUIDManager : MonoBehaviour 
{
	private string _uuid;

	[SerializeField]
	private InputField nameField;

	GameObject canvasObject;

	void Awake()
	{

		canvasObject = GameObject.Find("Canvas");
		Debug.Log ("awake");
		Debug.Log (canvasObject);

	}

	public void NameSubmit()
	{
		Debug.Log ("名乗った");
		System.Guid guid = System.Guid.NewGuid();
		_uuid = guid.ToString();

		Debug.Log (_uuid);
		Debug.Log (nameField);


		PlayerPrefs.SetString("uuid",_uuid);
		PlayerPrefs.Save();

		StartCoroutine(AddNewUser());
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

		GameObject loading = (GameObject)Instantiate (Resources.Load ("Prefabs/Loading"));
		loading.transform.SetParent (canvasObject.transform,false);

		yield return www;
		Destroy (loading);

		Debug.Log (www);

		if (www.error != null) {
			Debug.Log("Error");
		} else {
			Debug.Log("Success");
			Application.LoadLevel ("CharaMake");

		}


	}




	public void Load()
	{
		_uuid = PlayerPrefs.GetString ("uuid");
		Debug.Log (_uuid);
	}
}
