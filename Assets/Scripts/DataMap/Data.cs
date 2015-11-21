using UnityEngine;
using System;
using System.Collections;

public class Skill
{
	public int ID;
	public int CategoryID;
	public string SkillName;
	public int Value;
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
	public DateTime CreatedAt,UpdatedAt,DeletedAt;
	
}

public class PlayerStatus
{
	public int ID,UserID,PlayerID,JobID,MaxHP,MaxMP,HP,MP,Sanity,Luck,Idea,Knowledge,JobSkillPoint,HobbySkillPoint,DamageBonus;
//	public DateTime CreatedAt,UpdatedAt,DeletedAt;
}

