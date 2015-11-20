﻿using UnityEngine;
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

	private bool isSelectJobSkill = false;
	private bool isRequiredJobSkill = false;

	void Awake()
	{
		skillset = GameObject.Find("Canvas").GetComponent<SkillSet> ();
	}

	public void setSkillData(Dictionary<string,object> paramSkillData)
	{
		this.SkillData = paramSkillData;
		SkillName.text = (string)SkillData ["SkillName"];

		Debug.Log(this.SkillData["ID"] + ":" + this.SkillData["Value"]);

		this.DefaultValue = System.Convert.ToInt32(SkillData ["Value"]);
		this.CurrentValue = DefaultValue;
		SkillValue.text = DefaultValue.ToString ();
		SetSkillType();
		
		Debug.Log("isSelectJobSkill:" + isSelectJobSkill);
		Debug.Log("isRequiredJobSkill:" + isRequiredJobSkill);
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

	 void SetSkillType()
	{
		foreach(var data in skillset.JobSkillList){
			if(System.Convert.ToInt32(SkillData["ID"]) == System.Convert.ToInt32(data["SkillID"]) && System.Convert.ToInt32(data["SkillType"]) > 0 ){
				Debug.Log (" senntakusukiru");
				isSelectJobSkill = true;
				return;
			} else if(System.Convert.ToInt32(SkillData["ID"]) == System.Convert.ToInt32(data["SkillID"])) {
				Debug.Log ("hissusukiru");
				isRequiredJobSkill = true;
				return;
			}
		}

//		return false;
	}

}
