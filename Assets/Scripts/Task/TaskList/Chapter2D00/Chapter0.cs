using UnityEngine;

namespace Task
{
    /// <summary>
    /// ��һ�£�����������η����Ĺ��µĹ��±������Լ����ǵ�Ŀ��֮���
    /// </summary>
    public class Chapter0 : AsynChapterBase
    {
        public Chapter0()
        {
            chapterName = "���� ����֮��";
            chapterTitle = "����";
            taskPartCount = 2;
            chapterID = 0;
            chapterSavePath = Application.streamingAssetsPath + "/Task/Chapter/0.task";
            targetPart += "Chapter0_Part";
            runtimeScene = "MainScene";
        }

        /// <summary>  /// ������½ڵ�һ�£������˾�˵���ǿ�ʼ��Ϸ   /// </summary>
        public override void CheckAndLoadChapter()
        {
            AsynTaskControl.Instance.AddChapter(this);
        }

        public override void CompleteChapter()
        {
        }

        public override void ExitChapter()
        {
        }
    }
}