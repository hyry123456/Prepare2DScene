using UnityEngine;

namespace Control
{
    /// <summary> /// 角色开始的行为，有一盏灯光时时跟着主角 /// </summary>
    public class Touch : PlayerEffectBase
    {
        public Light light;
        public float decreaseSpeed = 2;
        float beginLightIntensity;
        protected override void OnDisable()
        {
            if(light == null) return;
            beginLightIntensity = light.intensity;
            Common.SustainCoroutine.Instance.AddCoroutine(DecreaseLight);
        }

        private void Update()
        {
            Vector3 toPos = light.transform.position;
            toPos.x = transform.position.x;
            toPos.y = transform.position.y;
            light.transform.position = toPos;
        }

        bool DecreaseLight()
        {
            if(light.intensity > 0)
            {
                light.intensity -= Time.deltaTime * decreaseSpeed;
                return false;
            }
            else
            {
                light.gameObject.SetActive(false);
                light.intensity = beginLightIntensity;
                return true;
            }
        }

        protected override void OnEnable()
        {
            light.gameObject.SetActive(true);
        }
    }
}