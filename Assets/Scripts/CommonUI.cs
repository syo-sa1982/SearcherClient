using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CommonUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void ChangeScene(int SceneNumber)
	{
		switch(SceneNumber){
			case 1:
				SceneManager.LoadScene("Home");
				break;
			case 2:
				SceneManager.LoadScene("Scenario");
				break;
			case 3:
				SceneManager.LoadScene("CharaSelect");
				break;
		}
	}
}
