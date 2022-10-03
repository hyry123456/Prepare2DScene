using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    public GameObject origin;

    void Start()
    {

        GameObject obj = Common.SceneObjectMap.Instance.FindControlObject("TempInteracte");
        Debug.Log(obj.name);
    }
}
