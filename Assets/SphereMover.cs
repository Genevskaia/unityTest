using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SphereMover : MonoBehaviour
{
	private float speed;
	string pathToJson;
	string jsonString;

	private Vector3[] vec_path;
	private bool isOnPath = false;
	private int currPointIdx;

	// Use this for initialization
	void Start()
	{
		speed = 1;
		pathToJson = Application.streamingAssetsPath + "/ball_path.json";
		jsonString = File.ReadAllText (pathToJson);
		Path path = JsonUtility.FromJson<Path>(jsonString);
		vec_path = new Vector3[path.x.Length];
		for(int i = 0; i < path.x.Length; i++){
			vec_path[i] = new Vector3(path.x[i], path.y[i], path.z[i]);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (isOnPath)
		{
			if (Vector3.Distance (transform.position, vec_path[currPointIdx + 1]) < 0.01f)
			{
				currPointIdx++;
				if (currPointIdx == vec_path.Length - 1)
				{
					isOnPath = false;
					return;
				}
			}
				
			Vector3 move = (vec_path [currPointIdx + 1] - transform.position).normalized * speed * Time.deltaTime;

			transform.position += move;
		}
	}

	void OnMouseDown()
	{
		if (!isOnPath) {
			isOnPath = true;
			transform.position = vec_path[0];
			currPointIdx = 0;
		}
	}
}

[System.Serializable]
public class Path
{
	public float[] x;
	public float[] y;
	public float[] z;
}
