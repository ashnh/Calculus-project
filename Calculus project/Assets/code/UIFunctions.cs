using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunctions : MonoBehaviour {

	public Snowman s;

	public GameObject x;
	public GameObject y;
	public GameObject z;

	public void startMelting () {
		s.startMelting = true;
		x.SetActive (true);
		y.SetActive (true);
		z.SetActive (true);
	}

	public void restartLevel () {
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);
	}
}
