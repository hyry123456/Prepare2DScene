using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using Common;

namespace UI
{
    public class NPCDialogUI : ObjectPoolBase
    {
        Queue<string> readyStrings;
        /// <summary>  /// �洢�����ı��õĽṹ   /// </summary>
        StringBuilder sb;
        /// <summary>  /// ��ǰ�ı��е�������ɫ  /// </summary>
        Color changeColor;
        /// <summary>  /// ��ǰ͸���е��ı�   /// </summary>
        string alphaChar;
        /// <summary> /// ����ӵ��ַ��ڵ�ǰ��ʾ���ı��ı��   /// </summary>
        int nowIndex;
        /// <summary>  /// ÿһ���ı���ȫ��������ʾ����Ҫ�ȴ����л�ʱ��  /// </summary>
        float MaxLineWaitTime = 2f;
        float nowLineWaitTime = 0;  //��ǰ�ĵȴ�ʱ��
        /// <summary>   /// ÿһ���ַ���ʾʱ��Ҫ��ʱ��   /// </summary>
        float perCharWaitTime = 0.4f;
        /// <summary>  /// ��ǰ��ʾ���ַ�   /// </summary>
        StringBuilder nowShowString;
        /// <summary>  /// ��ʾ�õ�UI����  /// </summary>
        Text text;
        /// <summary>  /// ����Ķ���λ��  /// </summary>
        Transform followPosition;
        /// <summary>  /// �ڸö���ͷ���ĸ߶�  /// </summary>
        float upHeight;

        /// <summary>   /// ����ʱִ�е���Ϊ    /// </summary>
        protected INonReturnAndNonParam endBehavior;

        public override void InitializeObject(Vector3 positon, Quaternion quaternion)
        {
            base.InitializeObject(positon, quaternion);
            text = GetComponentInChildren<Text>();
        }
        public override void InitializeObject(Vector3 positon, Vector3 lookAt)
        {
            base.InitializeObject(positon, lookAt);
            text = GetComponentInChildren<Text>();
        }


        private void Update()
        {
            //�ж��Ƿ���Ҫ����ĳ����ɫ
            if(followPosition != null)
                transform.position = followPosition.position + Vector3.up * upHeight;
            if(sb == null)  
            {
                //Ϊ�������ֿ��ܣ�һ���ǻ��ڵȴ�������
                if (readyStrings != null && readyStrings.Count > 0)
                {
                    if (nowLineWaitTime > MaxLineWaitTime)
                    {
                        sb = new StringBuilder(readyStrings.Dequeue());
                        nowShowString = new StringBuilder("");
                        nowIndex = 0;
                    }
                    else
                    {
                        //���ڵȴ��У�ֱ���˳�
                        nowLineWaitTime += Time.deltaTime;
                        return;
                    }
                }
                //һ���ǽ����ˣ���Ҫ����
                else
                {
                    if(nowLineWaitTime > MaxLineWaitTime)
                    {
                        CloseObject();  //�رո�UI���ص�����
                        if(endBehavior != null)
                        {
                            endBehavior();
                            endBehavior = null;
                        }
                    }
                    nowLineWaitTime += Time.deltaTime;
                    return;
                }

            }
            if (sb == null) return;
            if(alphaChar == null)
            {
                changeColor = text.color; changeColor.a = 0;
                if (nowIndex >= sb.Length)      //������ʾ�����ˣ��Ƴ�����
                {
                    sb = null;
                    nowLineWaitTime = 0;
                    return;
                }
                alphaChar = sb[nowIndex].ToString();
                nowIndex++;
            }
            changeColor.a += Time.deltaTime * (1.0f / perCharWaitTime);
            if(changeColor.a >= 1)
            {
                nowShowString.Append(alphaChar);
                alphaChar = null;
                text.text = nowShowString.ToString();
            }
            else
            {
                text.text = nowShowString + "<color=\"#" + ColorUtility.ToHtmlStringRGBA(changeColor)
                    + "\">" + alphaChar + "</color>";
            }
        }

        public void ShowDialog(string strs, Transform follow, float upHeight, INonReturnAndNonParam endBehavior)
        {
            List<string> strLists = new List<string>( strs.Split('\n') );
            nowIndex = 0;
            readyStrings = new Queue<string>();
            for (int i=0; i<strLists.Count; i++)
            {
                readyStrings.Enqueue(strLists[i]);
            }
            alphaChar = null;
            sb = null;
            nowLineWaitTime = MaxLineWaitTime + 1;      //һ��ʼ����ʾ����
            followPosition = follow;
            this.upHeight = upHeight;
            this.endBehavior = endBehavior;
        }

        public void ShowDialog(string strs, Vector3 postion, INonReturnAndNonParam endBehavior)
        {
            List<string> strLists = new List<string>(strs.Split('\n'));
            nowIndex = 0;
            readyStrings = new Queue<string>();
            for (int i = 0; i < strLists.Count; i++)
            {
                readyStrings.Enqueue(strLists[i]);
            }
            alphaChar = null;
            sb = null;
            nowLineWaitTime = MaxLineWaitTime + 1;      //һ��ʼ����ʾ����
            transform.position = postion;
            followPosition = null;      //�������ɫ
            this.endBehavior = endBehavior;
        }

        protected override void OnEnable()
        {

        }
    }
}