using UnityEngine;
using System.Collections;


public static class ConfURL
{
	private const bool IS_DEBUG = false;
	public const string HOST_NAME = IS_DEBUG ? "http://localhost/" : "http://ik1-302-11287.vs.sakura.ne.jp/";
	public const string USER_AUTH = "user/auth";
	public const string USER_ADD  = "user/add";

	public const string PLAYER_JOBLIST = "player/joblist";
	public const string PLAYER_BASE_MAKE = "player/base_make";
	public const string PLAYER_GENERATE = "player/generate";
	public const string PLAYER_LIST = "player/list";
	public const string PLAYER_SKILL_SETTING = "player/skill_setting";
	public const string PLAYER_SKILL_SUBMIT = "player/skill_submit";
	
	public const string HOME_USER_INDEX = "home/user/info";
	public const string HOME_SCENARIO_LIST = "home/scenario/list";
	public const string HOME_PLAYER_LIST = "home/player/list";

}
