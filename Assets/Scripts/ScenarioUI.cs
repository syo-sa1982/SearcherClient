using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScenarioUI : CommonUI 
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void pushMockBtn()
	{
		SceneManager.LoadScene ("QuestMap");
	}
}
