using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;

	private Rigidbody rb;
	private int count;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();
		winText.text = "";
	}

	void FixedUpdate ()
	{
		float mvHorizontal = Input.GetAxis ("Horizontal");
		float mvVertical = Input.GetAxis ("Vertical");

		Vector3 mv = new Vector3 (mvHorizontal, 0.0f, mvVertical);

		rb.AddForce (mv * speed);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ( "Pick Up"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
	}

	void SetCountText ()
	{
		countText.text = ((count <= 1) ? "Coin: " : "Coins: ") + count.ToString ();
		if (count >= 12) winText.text = "You Win!";
	}

	void OnGUI ()
	{
		if (GUI.Button (new Rect (Screen.width - 120, 20, 100, 50), "Menu")) {
			SceneManager.LoadScene ("GameMenu", LoadSceneMode.Single);
		}
		if (count >= 12) {
			if (GUI.Button (new Rect ((Screen.width / 2) - 50, Screen.height / 2, 100, 50), "Exit")) {
				#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
				#else
				Application.Quit();
				#endif
			}
		}
	}
}