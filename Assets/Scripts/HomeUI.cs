using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HomeUI : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	
	
	public void ChangeScene(int SceneNumber)
	{
		switch(SceneNumber){
			case 2:
				SceneManager.LoadScene("CharaSelect");
				break;
			case 3:
				SceneManager.LoadScene("CharaSelect");
				break;
		}
	}
}
