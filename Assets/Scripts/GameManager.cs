using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum GAME_LEVEL
{
    LEVEL_SIMPLE,
    LEVEL_NORMAL,
    LEVEL_HARD
}

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static GameManager _Instance
    {
        get;
        private set;
    }

    public GAME_LEVEL Level = GAME_LEVEL.LEVEL_NORMAL;

    private string GANE_SCENE = "game";

    private string MENU_SCENE = "start";

    private string OVER_SCENE = "over";

    public int CurrentScore = 0;

	// Use this for initialization
	void Start () {
        Debug.Log(this + "Start");
        if (_Instance != null)
        {
            Debug.LogWarning(this + ": Multi-Instance not allowed");
            Destroy(this);
            return;
        }

        _Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SimpleGame()
    {
        Debug.Log(this + "SimpleGame");
        Level = GAME_LEVEL.LEVEL_SIMPLE;
        UnityEngine.SceneManagement.SceneManager.LoadScene(GANE_SCENE, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void NormalGame()
    {
        Debug.Log(this + "NormalGame");
        Level = GAME_LEVEL.LEVEL_NORMAL;
        UnityEngine.SceneManagement.SceneManager.LoadScene(GANE_SCENE, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void HardGame()
    {
        Debug.Log(this + "HardGame");
        Level = GAME_LEVEL.LEVEL_HARD;
        UnityEngine.SceneManagement.SceneManager.LoadScene(GANE_SCENE, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void RestartGame()
    {
        Debug.Log(this + "RestartGame");
        UnityEngine.SceneManagement.SceneManager.LoadScene(GANE_SCENE, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void BackToMenu()
    {
        Debug.Log(this + "BackToMenu");
        UnityEngine.SceneManagement.SceneManager.LoadScene(MENU_SCENE, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void GameOver(int Score)
    {
        Debug.Log(this + "GameOver");
        CurrentScore = Score;
        UnityEngine.SceneManagement.SceneManager.LoadScene(OVER_SCENE, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
