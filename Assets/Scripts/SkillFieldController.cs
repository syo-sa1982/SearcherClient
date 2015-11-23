using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using LitJson;


public class SkillFieldController : MonoBehaviour 
{

	private Skill SkillData;
	private int DefaultValue;
	private int CurrentValue;
	private SkillSet skillset;

	const int MAX_VALUE = 99;

	[SerializeField]
	private Text SkillName;

	[SerializeField]
	private InputField SkillValue;

	private bool isSelectJobSkill = false;
	private bool isSelectedSkill = false;
	private bool isRequiredJobSkill = false;

	void Awake()
	{
		skillset = GameObject.Find("Canvas").GetComponent<SkillSet> ();
	}

	public void setSkillData(Skill paramSkillData)
	{
		this.SkillData = paramSkillData;
		SkillName.text = SkillData.SkillName;

		this.DefaultValue = SkillData.Value;
		this.CurrentValue = DefaultValue;
		SkillValue.text = DefaultValue.ToString ();
		SetSkillType();

		Debug.Log("SkillName:" + SkillData.SkillName);
		Debug.Log("isSelectJobSkill:" + isSelectJobSkill);
		Debug.Log("isRequiredJobSkill:" + isRequiredJobSkill);
	}

	public void CountUp()
	{
		CurrentValue++;
		Debug.Log (isPointLost());
		if (isOutofRange() || isPointLost()){CurrentValue--; return;}

		if (isRequiredJobSkill) {
			skillset.jobSkillPoint--;
		} else if(isSelectJobSkill && (skillset.selectedSkillID.Contains(SkillData.ID) || skillset.selectedSkillID.Count < skillset.SelectJobSkillMaxNum)){
				skillset.jobSkillPoint--;
				if (!skillset.selectedSkillID.Contains(SkillData.ID)) { skillset.selectedSkillID.Add(SkillData.ID); }
		} else {
			skillset.hobbySkillPoint--;
		}
		SkillData.Value = CurrentValue;
		SkillValue.text = CurrentValue.ToString ();
	}

	public void CountDown()
	{
		CurrentValue--;
		if (isOutofRange()){CurrentValue++; return;}

		if (isRequiredJobSkill) {
			skillset.jobSkillPoint++;
		} else if(isSelectJobSkill && skillset.selectedSkillID.Contains(SkillData.ID)) {
			skillset.jobSkillPoint++;
			if (CurrentValue == DefaultValue) { skillset.selectedSkillID.Remove(SkillData.ID); }
		} else {
			skillset.hobbySkillPoint++;
		}

		SkillData.Value = CurrentValue;
		SkillValue.text = CurrentValue.ToString ();
	}

	bool isOutofRange()
	{
		return (CurrentValue > MAX_VALUE || CurrentValue < DefaultValue) ? true : false;
	}

	bool isPointLost()
	{
		if (isRequiredJobSkill || (isSelectJobSkill && (skillset.selectedSkillID.Contains(SkillData.ID) || skillset.selectedSkillID.Count < skillset.SelectJobSkillMaxNum))) {
			return (skillset.jobSkillPoint < 1) ? true : false;
		} else {
			return (skillset.hobbySkillPoint < 1) ? true : false;
		}
	}

	 void SetSkillType()
	{
		foreach(var data in skillset.jobSkillArray){
			if(SkillData.ID == data.SkillID && data.SkillType > 0 ){
				isSelectJobSkill = true;
				return;
			} else if(SkillData.ID == data.SkillID){
				isRequiredJobSkill = true;
				skillset.SelectJobSkillMaxNum--;
				return;
			}
		}
		return;
	}

}
