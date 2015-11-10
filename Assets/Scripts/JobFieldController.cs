using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class JobFieldController : MonoBehaviour 
{

	private Dictionary<string,object> JobData;

	[SerializeField]
	private Text JobName;

	public void setJobData(Dictionary<string,object> paramJobData)
	{
		Debug.Log (paramJobData);
		Debug.Log (paramJobData["JobName"]);
		this.JobData = paramJobData;
		JobName.text = (string)JobData["JobName"];
	}
}
