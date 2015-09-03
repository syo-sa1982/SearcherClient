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
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public IEnumerator AddNewUser()
	{
	}

	public void Save()
	{
		if (PlayerPrefs.HasKey ("uuid")) {
			Debug.Log ("持ってる");
			return;
		}

		PlayerPrefs.SetString("uuid",_uuid);
		PlayerPrefs.Save();
	}

	public void Load()
	{
		_uuid = PlayerPrefs.GetString ("uuid");
		Debug.Log (_uuid);
	}
}
