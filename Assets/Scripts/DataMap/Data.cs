using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

[Serializable]
public class PlayerBase
{
	public int ID,UserID,Strength,Constitution,Power,Dextality,Appeal,Size,Intelligence,Education;
	public string Name;
	
}

[Serializable]
public class PlayerStatus
{
	public int ID,UserID,PlayerID,JobID,MaxHP,MaxMP,HP,MP,Sanity,Luck,Idea,Knowledge,JobSkillPoint,HobbySkillPoint,DamageBonus;
}


[Serializable]
public class CharamakeRollHistory
{
	public int[] Strength,Constitution,Power,Dextality,Appeal,Size,Intelligence,Education;
}

[Serializable]
public class RollResult 
{
	public PlayerBase BaseStatus;
	public PlayerStatus Status;
	public CharamakeRollHistory DiceHistory;
	
}