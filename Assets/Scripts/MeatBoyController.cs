using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MeatBoyController : MonoBehaviour {

	public class GroundState {
		private GameObject p;
		private float  w;
		private float h;
		private float l;

		public GroundState(GameObject playerRef) {
			p = playerRef;
			w = p.GetComponent<Collider2D>().bounds.extents.x + 0.1f;
			h = p.GetComponent<Collider2D>().bounds.extents.y + 0.2f;
			l = 0.05f;
		}

		public bool onWall() {
			bool left = Physics2D.Raycast(new Vector2(p.transform.position.x - w, p.transform.position.y), -Vector2.right, l);
			bool right = Physics2D.Raycast(new Vector2(p.transform.position.x + w, p.transform.position.y), Vector2.right, l);

			return (left || right);
		}

		public bool onGround() {
			bool g1 = Physics2D.Raycast(new Vector2(p.transform.position.x, p.transform.position.y - h), -Vector2.up, l);
			bool g2 = Physics2D.Raycast(new Vector2(p.transform.position.x + (w - 0.2f), p.transform.position.y - h), -Vector2.up, l);
			bool g3 = Physics2D.Raycast(new Vector2(p.transform.position.x - (w - 0.2f), p.transform.position.y - h), -Vector2.up, l);
			return (g1 || g2 || g3);
		}

		public bool isTouching() {
			return (onGround () || onWall ());
		}

		public int getWallDir() {
			bool left = Physics2D.Raycast(new Vector2(p.transform.position.x - w, p.transform.position.y), -Vector2.right, l);
			bool right = Physics2D.Raycast(new Vector2(p.transform.position.x + w, p.transform.position.y), Vector2.right, l);

			return (left ? -1 : (right ? 1 : 0));
		}
	}

	public float speed = 14f;
	public float accel = 6f;
	public float airAccel = 3f;
	public float jump = 5f;
	public Text tryText;

	private bool endGame = false;
	private bool paused = false;
	private GroundState groundState;
	private Vector3 initialPosition;
	private Quaternion initialRotation;
	private int scoreTry = 0;

	void Start() {
		initialPosition = transform.position;
		initialRotation = transform.rotation;
		groundState = new GroundState(transform.gameObject);
	}

	private Vector2 input;

	void Update() {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			paused = !paused;
			Time.timeScale = Convert.ToInt32 (!paused);
		}
		if (endGame || paused)
			return;
		input.x = (Input.GetKey (KeyCode.LeftArrow) ? -1 : (Input.GetKey (KeyCode.RightArrow) ? 1 : 0));
		if (Input.GetKeyDown (KeyCode.Space))
			input.y = 1;
		transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, (input.x == 0) ? transform.localEulerAngles.y : (input.x + 1) * 90, transform.localEulerAngles.z);
	}

	void FixedUpdate() {
		if (endGame || paused)
			return;
		GetComponent<Rigidbody2D>().AddForce(new Vector2(((input.x * speed) - GetComponent<Rigidbody2D>().velocity.x) * (groundState.onGround() ? accel : airAccel), 0));
		GetComponent<Rigidbody2D>().velocity = new Vector2((input.x == 0 && groundState.onGround()) ? 0 : GetComponent<Rigidbody2D>().velocity.x, (input.y == 1 && groundState.isTouching()) ? jump : GetComponent<Rigidbody2D>().velocity.y);

		if(groundState.onWall() && !groundState.onGround() && input.y == 1)
			GetComponent<Rigidbody2D>().velocity = new Vector2(-groundState.getWallDir() * speed * 0.75f, GetComponent<Rigidbody2D>().velocity.y);

		input.y = 0;
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag ("Respawn")) {
			tryText.text = "Try: " + (++scoreTry);
			transform.position = initialPosition;
			transform.rotation = initialRotation;
		}
		if (other.gameObject.CompareTag ("Finish"))
			endGame = true;
	}

	void OnGUI () {
		if (GUI.Button (new Rect (Screen.width - 120, 20, 100, 50), "Menu")) {
			SceneManager.LoadScene ("GameMenu", LoadSceneMode.Single);
		}
		if (endGame) {
			if (GUI.Button (new Rect ((Screen.width / 2) - 50, Screen.height / 2, 100, 50), "Exit")) {
				#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
				#else
				Application.Quit();
				#endif
			}
		} else if (paused) {
			if (GUI.Button (new Rect ((Screen.width / 2) - 50, Screen.height / 2, 100, 50), "Restart")) {
				SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
			}
		}
	}
}
