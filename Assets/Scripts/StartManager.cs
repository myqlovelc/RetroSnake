using UnityEngine;
using System.Collections;

public class StartManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(this + "Start");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnSimpleButtonClicked()
    {
        Debug.Log(this + "OnSimpleButtonClicked");
        GameManager._Instance.SimpleGame();
    }

    public void OnNormalButtonClicked()
    {
        Debug.Log(this + "OnNormalButtonClicked");
        GameManager._Instance.NormalGame();
    }

    public void OnHardButtonClicked()
    {
        Debug.Log(this + "OnHardButtonClicked");
        GameManager._Instance.HardGame();
    }
}
