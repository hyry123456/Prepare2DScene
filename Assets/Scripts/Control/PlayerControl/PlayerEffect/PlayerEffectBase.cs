using UnityEngine;

namespace Control
{
    /// <summary>
    /// ��ɫЧ�����࣬����ִ�����еĽ�ɫЧ����Ϊ
    /// </summary>
    public abstract class PlayerEffectBase : MonoBehaviour
    {
        /// <summary>    /// ��ɫ����ʱҪʵ�ֵ�Ч��     /// </summary>
        protected abstract void OnEnable();

        /// <summary>    /// ����ɫ�ر�ʱִ�еķ���    /// </summary>
        protected abstract void OnDisable();
    }
}