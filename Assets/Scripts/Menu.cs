using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Menu : MonoBehaviour {

	public int btnW;
	public int btnH;
	private int ox;
	private int oy;
	private List<string> sceneName = new List<string>();

	void Start () {
		btnW = 200;
		btnH = 50;
		foreach (string file in Directory.GetFiles("./Assets/_Scenes")) {
			string fileNoExt = Path.GetFileNameWithoutExtension (file);
			if (!fileNoExt.Equals("GameMenu") && !fileNoExt.EndsWith(".unity"))
				sceneName.Add (fileNoExt);
		}
		sceneName.Add ("Quit");
		ox = Screen.width / 2 - btnW / 2;
		oy = Screen.height / 2 - btnH * sceneName.Count;

	}

	void OnGUI() {
		int index = 0;
		foreach (string title in sceneName) {
			if (GUI.Button (new Rect(ox, oy + (btnH + 10)*index, btnW, btnH), title)) {
				if (title.Equals ("Quit")) {
					#if UNITY_EDITOR
					UnityEditor.EditorApplication.isPlaying = false;
					#else
					Application.Quit();
					#endif
				} else {
					SceneManager.LoadScene (title, LoadSceneMode.Single);
				}
			}
			index++;
		}
	}
}