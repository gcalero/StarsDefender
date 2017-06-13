using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public static int score;
	private Text text;

	public void Score(int points) {
		score += points;
		ShowScore();
	}

	public static void Reset() {
		score = 0;
		//ShowScore();
	}

	void ShowScore() {
		text.text = score.ToString();;
	}

	// Use this for initialization
	void Start () {
		text = gameObject.GetComponent<Text>();
		ShowScore();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
