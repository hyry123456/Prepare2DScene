using UnityEngine;

namespace Control
{
    /// <summary>
    /// 角色效果基类，用来执行所有的角色效果行为
    /// </summary>
    public abstract class PlayerEffectBase : MonoBehaviour
    {
        /// <summary>    /// 角色启动时要实现的效果     /// </summary>
        public abstract void OnEnable();

        /// <summary>    /// 当角色关闭时执行的方法    /// </summary>
        public abstract void OnDisable();
    }
}