using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	void Start ()
	{
		transform.Rotate(new Vector3(0, Random.Range(0, 180), 0));
		transform.position = new Vector3(Random.Range(-9, 9), .5f, Random.Range(-9, 9));
	}
	void Update () 
	{
		transform.Rotate (new Vector3 (0, 30, 0) * Time.deltaTime);
	}
}
