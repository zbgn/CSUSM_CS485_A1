using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
	public GameObject toPool;
	public int poolSz = 5;
	public float spawnRate = 3f;
	public float objectMin = -1f;
	public float objectMax = 3.5f;

	private GameObject[] goArray;
	private int currentObject = 0;

	private Vector2 objectPoolPosition = new Vector2 (-15,-25);
	private float spawnXPosition = 10f;

	private float timeSinceLastSpawned;


	void Start() {
		timeSinceLastSpawned = 0f;

		goArray = new GameObject[poolSz];
		for (int i = 0; i < poolSz; i++)
			goArray [i] = (GameObject)Instantiate (toPool, objectPoolPosition, Quaternion.identity);
		}
		
	void Update() {
		timeSinceLastSpawned += Time.deltaTime;

		if (!GameController.instance.gameOver && timeSinceLastSpawned >= spawnRate) {
			timeSinceLastSpawned = 0f;
			float spawnYPosition = Random.Range(objectMin, objectMax);
			goArray[currentObject].transform.position = new Vector2(spawnXPosition, spawnYPosition);
			currentObject ++;
			if (currentObject >= poolSz) currentObject = 0;
		}
	}
}
