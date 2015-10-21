using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SkillFieldController : MonoBehaviour 
{

	private Dictionary<string,object> SkillData;
	private int DefaultValue;

	[SerializeField]
	private Text SkillName;

	[SerializeField]
	private InputField SkillValue;

	public void setSkillData(Dictionary<string,object> paramSkillData)
	{
		this.SkillData = paramSkillData;
		SkillName.text = (string)SkillData ["SkillName"];
		this.DefaultValue = System.Convert.ToInt32(SkillData ["Value"]);
		SkillValue.text = DefaultValue.ToString ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
