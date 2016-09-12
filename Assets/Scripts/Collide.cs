using UnityEngine;
using System.Collections;

public class Collide : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wall") {
            Debug.Log(this + "OnTriggerEnter Wall");
            OnGameEnd();
        }
        else if (other.tag == "food")
        {
            Debug.Log(this + "OnTriggerEnter Food");
            OnFoodEnter();
        }
    }

    public System.Action FoodEnter;
    private void OnFoodEnter()
    {
        if (FoodEnter != null)
        {
            FoodEnter(); //TODO: handle multiple selection
        }
    }

    public System.Action GameEnd;
    private void OnGameEnd()
    {
        if (GameEnd != null)
        {
            GameEnd(); //TODO: handle multiple selection
        }
    }
}
