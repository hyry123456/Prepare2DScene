using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    [SerializeField]
    Material mat;
    [SerializeField]
    Texture2D texture;
    void Start()
    {
        mat.SetVector("_TargetPos", Vector3.one * 5);
        mat.SetVector("_BeginPos", Vector3.zero);
        mat.SetTexture("_MainTex", texture);
        //mat.color
    }
    private void Update()
    {
        Debug.DrawLine(Vector3.zero, Vector3.one * 5);
    }
}
