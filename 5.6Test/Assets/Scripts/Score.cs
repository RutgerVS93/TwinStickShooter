using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public static Score script;

    public int totalScore;

    [SerializeField]
    private Text scoreText;

	void Start ()
    {
        script = this;
        totalScore = 0;
	}

    public void AddScore(int scoreToAdd)
    {
        totalScore += scoreToAdd;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "" + totalScore;
    }
}
