using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class User
{
	public int ID;
	public string UUID;
	public string Name;
	public int RollCount;
}

public class Skill
{
	public int ID,CategoryID,Value;
	public string SkillName;
}

public class JobSkill
{
	public int ID,JobID,SkillID,SkillType;
}

public class Job
{
	public int ID;
	public string JobName;
	public int JobSkills;
}

public class PlayerBase
{
	public int ID,UserID,Strength,Constitution,Power,Dextality,Appeal,Size,Intelligence,Education;
	public string Name;
	
}

public class PlayerStatus
{
	public int ID,UserID,PlayerID,JobID,MaxHP,MaxMP,HP,MP,Sanity,Luck,Idea,Knowledge,JobSkillPoint,HobbySkillPoint,DamageBonus;
}

[Serializable]
public class RallData
{
	public string Strength = "6,3";
	public string Constitution = "6,3";
	public string Power = "6,3";
	public string Dextality = "6,3";
	public string Appeal = "6,3";
	public string Size = "6,2,6";
	public string Intelligence = "6,2,6";
	public string Education   = "6,2,3";
}