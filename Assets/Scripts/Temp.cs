using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    public GameObject origin;

    void Start()
    {


        string str = "��ã���������������������\nsakdjsaldjlaskjdlksajd\nsajldlsajdlsajdlsajd";
        UIExtentControl.Instance.ShowBigDialog(str, null);
    }
}
