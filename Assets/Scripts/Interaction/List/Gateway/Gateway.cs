using UnityEngine;

namespace Interaction
{

    /// <summary>
    /// �����Ž��������������Ǵ��͵�ĳ��λ�ã�ͬʱ�����л����ǿ��Ƶķ���
    /// </summary>
    public class Gateway : InteractionBase
    {
        /// <summary>    /// Ҫ��ɵ�����    /// </summary>
        public Control.PlayerControl changeToPlayer;
        /// <summary>   /// ��Ҫ��������ĻЧ��  /// </summary>
        public DefferedRender.PostFXSetting fXSetting;


        public override void InteractionBehavior()
        {
            Control.PlayerControl.ChangeToPlayer(changeToPlayer);
        }

        protected override void OnEnable()
        {

        }
    }
}