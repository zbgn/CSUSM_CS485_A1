using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if(other.GetComponent<BirdController>() != null) GameController.instance.BirdScored();
	}
}
