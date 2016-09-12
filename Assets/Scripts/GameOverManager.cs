using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverManager : MonoBehaviour {

    public Text ScoreText;

	// Use this for initialization
	void Start () {
        Debug.Log(this + "Start");
        ScoreText.text = "Score: " + GameManager._Instance.CurrentScore;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnRestartButtonClicked()
    {
        Debug.Log(this + "OnRestartButtonClicked");
        GameManager._Instance.RestartGame();
    }

    public void OnMenuButtonClicked()
    {
        Debug.Log(this + "OnMenuButtonClicked");
        GameManager._Instance.BackToMenu();
    }
}
