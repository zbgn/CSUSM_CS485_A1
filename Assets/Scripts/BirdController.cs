using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour {

	public float		upForce = 200f;
	private bool		isAlive = true;

	private Animator	anim;
	private Rigidbody2D	rb2d;

	void Start()
	{
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (isAlive) {
			if (Input.GetMouseButtonDown (0)) {
				anim.SetTrigger ("Fly");
				rb2d.velocity = Vector2.zero;
				rb2d.AddForce (new Vector2 (0, upForce));
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		rb2d.velocity = Vector2.zero;
		isAlive = false;
		anim.SetTrigger ("Die");
		GameController.instance.BirdDied ();
	}
}
