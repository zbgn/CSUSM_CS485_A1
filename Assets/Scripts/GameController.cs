using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public static GameController instance;
	public Text scoreText;
	public GameObject gameOverText;

	private int score = 0;
	public bool gameOver = false;
	public float scrollSpeed = -1.5f;


	void Awake()
	{
		if (instance == null) instance = this;
		else if(instance != this) Destroy (gameObject);
	}

	void Update()
	{
		if (gameOver && Input.GetMouseButtonDown (0)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		} else if (gameOver && Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene ("GameMenu", LoadSceneMode.Single);
		}
	}

	public void BirdScored()
	{
		if (gameOver) return;
		scoreText.text = "Score: " + (++score).ToString();
	}

	public void BirdDied()
	{
		gameOverText.SetActive (true);
		gameOver = true;
	}
}
