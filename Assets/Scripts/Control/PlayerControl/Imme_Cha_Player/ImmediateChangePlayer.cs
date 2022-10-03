using Common.ResetInput;
using DefferedRender;
using UnityEngine;

namespace Control {
    public class ImmediateChangePlayer : ControlBase
    {
        /// <summary>   /// 可以用来改变成的物体都存储在这里，等待切换   /// </summary>
        public GameObject[] canChangeObject;
        private int nowIndex;      //当前显示中的物体
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
            //显示第一个角色，关闭其他角色
            canChangeObject[0].SetActive(true);
            nowIndex = 0;
            for (int i = 1; i < canChangeObject.Length; i++)
            {
                canChangeObject[i].SetActive(false);
            }
        }


        /// <summary>   /// 时时刷新的控制属性的存放位置   /// </summary>
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
                    if (canChangeObject[i].activeSelf)  //正在运行就直接退出
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
        /// 物理帧刷新的属性计算位置，一些没有必要逐帧计算的可以在这里进行计算
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