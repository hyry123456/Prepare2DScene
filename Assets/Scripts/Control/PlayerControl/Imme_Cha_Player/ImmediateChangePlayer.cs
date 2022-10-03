using Common.ResetInput;
using DefferedRender;
using UnityEngine;

namespace Control {
    public class ImmediateChangePlayer : ControlBase
    {
        /// <summary>   /// ���������ı�ɵ����嶼�洢������ȴ��л�   /// </summary>
        public GameObject[] canChangeObject;
        private int nowIndex;      //��ǰ��ʾ�е�����
        private Motor.MoveBase motor;
        private Interaction.InteractionControl interactionControl;
        private PlayerSkillControl skillControl;
        public float dieY = -100;

        [SerializeField]
        string horizontalName = "Horizontal";
        [SerializeField]
        string jumpName = "Jump";
        [SerializeField]
        string interacteName = "Interaction";

        ParticleDrawData drawData = new ParticleDrawData
        {
            beginSpeed = Vector3.up,
            speedMode = SpeedMode.PositionOutside,
            useGravity = false,
            followSpeed = true,
            radian = 6.28f,
            radius = 1f,
            lifeTime = 3,
            showTime = 3,
            frequency = 1f,
            octave = 8,
            intensity = 5,
            sizeRange = new Vector2(1f, 2f),
            colorIndex = (int)ColorIndexMode.HighlightToAlpha,
            sizeIndex = (int)SizeCurveMode.SmallToBig_Epirelief,
            textureIndex = 0,
            groupCount = 30,
        };

        private void OnEnable()
        {
            motor = GetComponent<Motor.MoveBase>();
            interactionControl = GetComponent<Interaction.InteractionControl>();
            skillControl = GetComponent<PlayerSkillControl>();
            isEnableInput = true;
            instance = this;
            if (canChangeObject == null || canChangeObject.Length == 0) return;
            //��ʾ��һ����ɫ���ر�������ɫ
            canChangeObject[0].SetActive(true);
            nowIndex = 0;
            for (int i = 1; i < canChangeObject.Length; i++)
            {
                canChangeObject[i].SetActive(false);
            }
        }


        /// <summary>   /// ʱʱˢ�µĿ������ԵĴ��λ��   /// </summary>
        private void Update()
        {
            if (!isEnableInput) return;
            if (Input.GetMouseButtonDown(0))
                skillControl?.ReleaseChooseSkill();
            if (canChangeObject == null || canChangeObject.Length == 0)
                return;

            for(int i=0; i<canChangeObject.Length; i++)
            {
                if(Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    if (canChangeObject[i].activeSelf)  //�������о�ֱ���˳�
                        return;
                    else
                    {
                        canChangeObject[nowIndex].SetActive(false);
                        canChangeObject[i].SetActive(true);
                        nowIndex = i;
                        drawData.beginPos = transform.position;
                        ParticleNoiseFactory.Instance.DrawShape(drawData);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// ����֡ˢ�µ����Լ���λ�ã�һЩû�б�Ҫ��֡����Ŀ�����������м���
        /// </summary>
        private void FixedUpdate()
        {
            if (!isEnableInput) return;
            float horizontal = MyInput.Instance.GetAsis(horizontalName);
            bool jump = MyInput.Instance.GetButtonDown(jumpName);
            bool esc = MyInput.Instance.GetButtonDown("ESC");
            bool interacte = MyInput.Instance.GetButtonDown(interacteName);

            motor.Move(horizontal);
            if (jump)
                motor.DesireJump();

            if (esc)
                UIExtentControl.Instance?.ShowOrClose();

            if (interacte && interactionControl != null)
                interactionControl.RunInteraction();

            if (transform.position.y < dieY)
                SceneChangeControl.Instance.ReloadActiveScene();
        }

        public override Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}