using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    public GameObject origin;

    void Start()
    {


        string str = "你好，萨达萨达萨达萨达萨达\nsakdjsaldjlaskjdlksajd\nsajldlsajdlsajdlsajd";
        UIExtentControl.Instance.ShowBigDialog(str, null);
    }
}
