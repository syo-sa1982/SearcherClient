using UnityEngine;
using System;
using System.Collections;

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

