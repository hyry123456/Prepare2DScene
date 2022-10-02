using UnityEngine;

namespace Interaction
{

    /// <summary>
    /// 传送门交互，用来将主角传送到某个位置，同时调用切换主角控制的方法
    /// </summary>
    public class Gateway : InteractionBase
    {
        /// <summary>    /// 要变成的主角    /// </summary>
        public Control.PlayerControl changeToPlayer;
        /// <summary>   /// 需要调整的屏幕效果  /// </summary>
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