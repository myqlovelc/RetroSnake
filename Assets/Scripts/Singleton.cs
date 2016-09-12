using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Singleton : MonoBehaviour {

    private static Dictionary<string, GameObject> _InstanceDict;

    void Awake()
    {
        if (_InstanceDict == null) {
            _InstanceDict = new Dictionary<string, GameObject>();
        }

        GameObject instance;
        if (_InstanceDict.TryGetValue(name, out instance)) {
            Debug.LogWarning(string.Format("{0}: Multi-Instance of GameObject ({1}) is not allowed!", this, instance));
            Destroy(gameObject);
            return;
        }

        _InstanceDict.Add(name, gameObject);
        DontDestroyOnLoad(gameObject);
    }
}
