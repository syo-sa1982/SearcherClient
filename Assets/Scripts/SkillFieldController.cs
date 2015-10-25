using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SkillFieldController : MonoBehaviour 
{

	private Dictionary<string,object> SkillData;
	private int DefaultValue;
	private int CurrentValue;

	const int MAX_VALUE = 99;

	[SerializeField]
	private Text SkillName;

	[SerializeField]
	private InputField SkillValue;

	public void setSkillData(Dictionary<string,object> paramSkillData)
	{
		this.SkillData = paramSkillData;
		SkillName.text = (string)SkillData ["SkillName"];
		this.DefaultValue = System.Convert.ToInt32(SkillData ["Value"]);
		this.CurrentValue = DefaultValue;
		SkillValue.text = DefaultValue.ToString ();
	}

	public void CountUp()
	{
		CurrentValue++;
		if (isImpossible()){CurrentValue--; return;}
		SkillData ["Value"] = (object)CurrentValue;
		SkillValue.text = CurrentValue.ToString ();
	}

	public void CountDown()
	{
		CurrentValue--;
		if (isImpossible()){CurrentValue++; return;}
		SkillData ["Value"] = (object)CurrentValue;
		SkillValue.text = CurrentValue.ToString ();
	}

	bool isImpossible()
	{
		return (CurrentValue >= MAX_VALUE || CurrentValue <= DefaultValue) ? true : false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
