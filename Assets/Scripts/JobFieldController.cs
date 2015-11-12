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
		this.JobData = paramJobData;
		JobName.text = (string)JobData["JobName"];
	}

	public void ChangeSelectedJob()
	{
		GameObject _parent = transform.root.gameObject;
		JobSelect jobSelect = _parent.GetComponent<JobSelect> ();
		jobSelect.ChangeSelectJob (JobData["ID"]);
	}
}
