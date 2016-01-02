using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour 
{

	public GameObject defaultWallPrefab;
	private int default_x_max = 19;
	private int default_z_max = 19;

	// Use this for initialization
	void Start () 
	{
		CreateMapOuterframe();
	}
	
	void CreateMapOuterframe()
	{
		for(int dx = 0; dx <= default_x_max; dx++){
			Instantiate(defaultWallPrefab, new Vector3(dx, 0, 0), Quaternion.identity);
			Instantiate(defaultWallPrefab, new Vector3(dx, 0, default_z_max), Quaternion.identity);
		}
		
		for(int dz = 0; dz <= default_z_max; dz++){
			Instantiate(defaultWallPrefab, new Vector3(0, 0, dz), Quaternion.identity);
			Instantiate(defaultWallPrefab, new Vector3(default_x_max, 0, dz), Quaternion.identity);
		}
		
	}
}
