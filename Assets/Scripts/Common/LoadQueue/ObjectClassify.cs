using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ��ɫ����ṹ�壬����ȷ��������Ҫ���������
/// </summary>
public class ObjectClassify : MonoBehaviour
{
    public static ObjectClassify instance;
    public static ObjectClassify Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject game = new GameObject("ObjectClassify");
                game.AddComponent<ObjectClassify>();
            }
            return instance;
        }
    }

    private string[] allClassify = new string[]
    {
        "Red", "Blue", "Yellow", "Green"
    };
    public enum ClassifyMode
    {
        Red = 0, Blue = 1, Yellow = 2, Green = 3
    }
    public List<List<GameObject>> allObjects;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        allObjects = new List<List<GameObject>>();
        for(int i=0; i<allClassify.Length; i++)
        {
            List<GameObject> objects = new List<GameObject>( GameObject.FindGameObjectsWithTag(allClassify[i]));
            allObjects.Add(objects);
        }

    }

    private void OnDestroy()
    {
        instance = null;
        allObjects.Clear();
    }
}
