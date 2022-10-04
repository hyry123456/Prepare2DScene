using UnityEngine;

namespace Control
{
    /// <summary> /// ��ɫ��ʼ����Ϊ����һյ�ƹ�ʱʱ�������� /// </summary>
    public class Touch : PlayerEffectBase
    {
        public Light light;
        public float decreaseSpeed = 2;
        float beginLightIntensity;
        protected override void OnDisable()
        {
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
            if (gameObject.activeSelf)
            {
                light.intensity = beginLightIntensity;
                return true;
            }
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