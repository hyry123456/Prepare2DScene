using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    public GameObject origin;

    void Start()
    {
        Control.EnemyControl enemyControl = (Control.EnemyControl)Common.SceneObjectPool.Instance.
            GetObject("Enemy", origin, new Vector3(12, -4, 0), Quaternion.identity);
    }
    private void Update()
    {
    }
}
