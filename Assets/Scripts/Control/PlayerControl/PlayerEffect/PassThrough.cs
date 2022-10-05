using Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  /// 穿越控制 /// </summary>
public class PassThrough : PlayerEffectBase
{
    /// <summary>  /// 这个物体需要进行显示的标签   /// </summary>
    public string thisPassTags;

    public List<SpriteRenderer> showLists;
    public List<SpriteRenderer> closeLists;

    bool Show()
    {
        Color closeCol = showLists[0].color;
        float alpha = closeCol.a;
        alpha += Time.deltaTime;

        for(int i=0; i<showLists.Count; i++)
        {
            Color temp = showLists[i].color;
            temp.a = alpha;
            showLists[i].color = temp;
        }

        if(alpha >= 1.0f)
        {
            return true;
        }
        return false;
    }

    bool Close()
    {
        Color closeCol = closeLists[0].color;
        float alpha = closeCol.a;
        alpha -= Time.deltaTime;

        for (int i = 0; i < closeLists.Count; i++)
        {
            Color temp = closeLists[i].color;
            temp.a = alpha;
            closeLists[i].color = temp;
        }

        if (alpha <= 0.0f)
        {
            for (int i = 0; i < closeLists.Count; i++)
            {
                closeLists[i].gameObject.SetActive(false);
            }
            return true;
        }
        return false;
    }

    public override void OnEnable()
    {
        List<GameObject> games;

        games = ObjectClassify.Instance.allObjects[thisPassTags];
        showLists = new List<SpriteRenderer>();
        for (int i = 0; i < games.Count; i++)
        {
            showLists.Add(games[i].GetComponent<SpriteRenderer>());
            Color color = showLists[i].color; color.a = 0;
            showLists[i].color = color;
            games[i].SetActive(true);
        }

        Common.SustainCoroutine.Instance.AddCoroutine(Show);
    }

    public override void OnDisable()
    {
        List<GameObject> games;

        games = ObjectClassify.Instance.allObjects[thisPassTags];
        closeLists = new List<SpriteRenderer>();
        for (int i = 0; i < games.Count; i++)
        {
            closeLists.Add(games[i].GetComponent<SpriteRenderer>());
            Color color = closeLists[i].color; color.a = 1;
            closeLists[i].color = color;
        }

        Common.SustainCoroutine.Instance.AddCoroutine(Close);
    }
}
