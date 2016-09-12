using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum SNAKE_DIRECTION
{
    MOVE_UP,
    MOVE_DOWN,
    MOVE_LEFT,
    MOVE_RIGHT
}

public class RetroSnake : MonoBehaviour {

    public GameObject SnakeHead;

    public int InitBodyLength;

    private List<GameObject> SnakeBodyList = new List<GameObject>();

    public GameObject BodyTemplate;

    public GameObject FoodTemplate;

    private GameObject FoodObject;

    private GameObject food;

    public SNAKE_DIRECTION direction;

    private SNAKE_DIRECTION lastDirection;

    public float MoveUnit;

    private float speed;

    private float delta = 10.0f;

    private int Score = 0;

    public Text ScoreText;

	// Use this for initialization
	void Start () {
        Debug.Log(this + "Start");
        InitSpeed();

        SnakeHead.GetComponent<Collide>().FoodEnter += OnSnakeEatFood;
        SnakeHead.GetComponent<Collide>().GameEnd += OnGameOver;
        InitSnakeBodyList();
        RandomFood();
        SnakeMove();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log(this + "new direction " + direction);
            direction = SNAKE_DIRECTION.MOVE_UP;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log(this + "new direction " + direction);
            direction = SNAKE_DIRECTION.MOVE_DOWN;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(this + "new direction " + direction);
            direction = SNAKE_DIRECTION.MOVE_LEFT;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log(this + "new direction " + direction);
            direction = SNAKE_DIRECTION.MOVE_RIGHT;
        }
	}

    private void InitSpeed()
    {
        Debug.Log(this + "InitSpeed");
        if (GameManager._Instance == null)
        {
            Debug.Log(this + "GameManager is null");
            speed = 10.0f;
        }
        else
        {
            switch (GameManager._Instance.Level)
            {
                case GAME_LEVEL.LEVEL_SIMPLE:
                    speed = 5.0f;
                    break;
                case GAME_LEVEL.LEVEL_NORMAL:
                    speed = 10.0f;
                    break;
                case GAME_LEVEL.LEVEL_HARD:
                    speed = 20.0f;
                    break;
            }
        }

        delta = 1.0f / speed;
    }

    private void SnakeBodyGrow()
    {
        Debug.Log(this + "SnakeBodyGrow");
        GameObject obj = GameObject.Instantiate(BodyTemplate);
        obj.transform.position = SnakeBodyList[SnakeBodyList.Count - 1].transform.position;
        obj.transform.parent = transform;
        SnakeBodyList.Add(obj);
    }

    public void InitSnakeBodyList()
    {
        Debug.Log(this + "InitSnakeBodyList");
        for (int i = 0; i < InitBodyLength; i++ )
        {
            Debug.Log(i);
            GameObject obj = GameObject.Instantiate(BodyTemplate);
            obj.transform.parent = transform;
            obj.transform.Translate(MoveUnit * -(i), 0, 0);
            SnakeBodyList.Add(obj);
        }
    }

    public void SnakeMove()
    {
        Debug.Log(this + "SnakeMove");
        CancelInvoke("SnakeMove");
        CheckDirection();
        Vector3 oldpos = SnakeHead.transform.position;
        switch (direction)
        {
            case SNAKE_DIRECTION.MOVE_UP:
                for (int i = SnakeBodyList.Count - 1; i > 0; i--)
                {
                    SnakeBodyList[i].transform.position = SnakeBodyList[i - 1].transform.position;
                }
                SnakeBodyList[0].transform.position = oldpos;
                SnakeHead.transform.position = new Vector3(oldpos.x, oldpos.y + MoveUnit, oldpos.z);
                break;
            case SNAKE_DIRECTION.MOVE_DOWN:
                for (int i = SnakeBodyList.Count - 1; i > 0; i--)
                {
                    SnakeBodyList[i].transform.position = SnakeBodyList[i - 1].transform.position;
                }
                SnakeBodyList[0].transform.position = oldpos;
                SnakeHead.transform.position = new Vector3(oldpos.x, oldpos.y - MoveUnit, oldpos.z);
                break;
            case SNAKE_DIRECTION.MOVE_LEFT:
                for (int i = SnakeBodyList.Count - 1; i > 0; i--)
                {
                    SnakeBodyList[i].transform.position = SnakeBodyList[i - 1].transform.position;
                }
                SnakeBodyList[0].transform.position = oldpos;
                SnakeHead.transform.position = new Vector3(oldpos.x - MoveUnit, oldpos.y, oldpos.z);
                break;
            case SNAKE_DIRECTION.MOVE_RIGHT:
                for (int i = SnakeBodyList.Count - 1; i > 0; i--)
                {
                    SnakeBodyList[i].transform.position = SnakeBodyList[i - 1].transform.position;
                }
                SnakeBodyList[0].transform.position = oldpos;
                SnakeHead.transform.position = new Vector3(oldpos.x + MoveUnit, oldpos.y, oldpos.z);
                break;

        }

        if (BodyCollide())
        {
            Debug.Log(this + "BodyCollide true");
            CancelInvoke("SnakeMove");
            GameManager._Instance.GameOver(Score);
            return;
        }
        lastDirection = direction;
        Invoke("SnakeMove", delta);

    }

    public void CheckDirection()
    {
        Debug.Log(this + "CheckDirection");
        switch (lastDirection)
        {
            case SNAKE_DIRECTION.MOVE_UP:
                if (direction == SNAKE_DIRECTION.MOVE_DOWN) {
                    direction = SNAKE_DIRECTION.MOVE_UP;
                }
                break;
            case SNAKE_DIRECTION.MOVE_DOWN:
                if (direction == SNAKE_DIRECTION.MOVE_UP)
                {
                    direction = SNAKE_DIRECTION.MOVE_DOWN;
                }
                break;
            case SNAKE_DIRECTION.MOVE_LEFT:
                if (direction == SNAKE_DIRECTION.MOVE_RIGHT)
                {
                    direction = SNAKE_DIRECTION.MOVE_LEFT;
                }
                break;
            case SNAKE_DIRECTION.MOVE_RIGHT:
                if (direction == SNAKE_DIRECTION.MOVE_LEFT)
                {
                    direction = SNAKE_DIRECTION.MOVE_RIGHT;
                }
                break;
        }
        Debug.Log(this + "last direction: " + lastDirection + " new direction: " + direction);
    }

    public void RandomFood()
    {
        Debug.Log(this + "RandomFood");
        Destroy(FoodObject);

        Vector3 pos = new Vector3(Random.Range(-24, 24) * MoveUnit, Random.Range(-24, 24) * MoveUnit, 0);
        while (!CheckFood(pos)) {
            pos = new Vector3(Random.Range(-24, 24) * MoveUnit, Random.Range(-24, 24) * MoveUnit, 0);
        }

        Debug.Log(this + "RandomFood position: " + pos);

        FoodObject = GameObject.Instantiate(FoodTemplate);
        FoodObject.transform.position = pos;
    }

    private bool CheckFood(Vector3 pos)
    {
        if (PositionEqual(pos, SnakeHead.transform.position)) {
            return false;
        }
        foreach (var obj in SnakeBodyList) {
            if (PositionEqual(pos, obj.transform.position)) {
                return false;
            }
        }
        return true;
    }

    private bool PositionEqual(Vector3 pos1, Vector3 pos2) {
        if (pos1.x == pos2.x && pos1.y == pos2.y && pos1.z == pos2.z) {
            return true;
        }
        return false;
    }

    public void OnSnakeEatFood()
    {
        Debug.Log(this + "OnSnakeEatFood");
        SnakeBodyGrow();

        Score += 5;
        ScoreText.text = "Score: " + Score;

        RandomFood();
    }

    public void OnGameOver()
    {
        Debug.Log(this + "OnGameOver");
        CancelInvoke("SnakeMove");
        GameManager._Instance.GameOver(Score);
    }

    private bool BodyCollide()
    {
        bool collide = false;
        foreach (var body in SnakeBodyList) {
            if (PositionEqual(body.transform.position, SnakeHead.transform.position)) {
                collide = true;
                break;
            }
        }
        return collide;
    }
}
