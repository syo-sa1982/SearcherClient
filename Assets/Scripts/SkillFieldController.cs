using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SkillFieldController : MonoBehaviour 
{

	private Dictionary<string,object> SkillData;
	private int DefaultValue;
	private int CurrentValue;
	private SkillSet skillset;

	const int MAX_VALUE = 99;

	[SerializeField]
	private Text SkillName;

	[SerializeField]
	private InputField SkillValue;

	void Awake()
	{
		skillset = GameObject.Find("Canvas").GetComponent<SkillSet> ();

//		Debug.Log (skillset.HobbySkillPoint);
//		Debug.Log (skillset.JobSkillPoint);
		Debug.Log (skillset.Job);
		Debug.Log (skillset.JobSkillList);
//		Debug.Log ("awake");
	}

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
		if (isOutofRange() || isPointLost()){CurrentValue--; return;}
		skillset.JobSkillPoint--;
		SkillData ["Value"] = (object)CurrentValue;
		SkillValue.text = CurrentValue.ToString ();
	}

	public void CountDown()
	{
		CurrentValue--;
		if (isOutofRange()){CurrentValue++; return;}

		skillset.JobSkillPoint++;

		SkillData ["Value"] = (object)CurrentValue;
		SkillValue.text = CurrentValue.ToString ();
	}

	bool isOutofRange()
	{
		return (CurrentValue > MAX_VALUE || CurrentValue < DefaultValue) ? true : false;
	}

	bool isPointLost()
	{
		return (skillset.JobSkillPoint < 1) ? true : false;
	}

}
