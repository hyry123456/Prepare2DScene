using Common.ResetInput;
using DefferedRender;
using UnityEngine;

namespace Control {
    public class ImmediateChangePlayer : ControlBase
    {
        /// <summary>   /// 可以用来改变成的物体都存储在这里，等待切换   /// </summary>
        public GameObject[] canChangeObject;
        private int nowIndex;      //当前显示中的物体
        private Motor.MoveBase[] motors;
        private Interaction.InteractionControl[] interactionControls;
        private PlayerSkillControl[] skillControls;
        public float dieY = -100;
        public Animator player_anim;
        public GameObject anmiGO;      //为骨骼动画准备的GO；
        private Rigidbody2D[] Rbs;

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
            if (canChangeObject == null || canChangeObject.Length == 0) return;
            motors = new Motor.MoveBase[canChangeObject.Length];
            interactionControls = new Interaction.InteractionControl[canChangeObject.Length];
            skillControls = new PlayerSkillControl[canChangeObject.Length];
            Rbs = new Rigidbody2D[canChangeObject.Length];
            for (int i = 0; i < canChangeObject.Length; i++)
            {
                motors[i] = canChangeObject[i]?.GetComponent<Motor.MoveBase>();
                interactionControls[i] = canChangeObject[i]?.GetComponent<Interaction.InteractionControl>();
                skillControls[i] = canChangeObject[i]?.GetComponent<PlayerSkillControl>();
                Rbs[i] = canChangeObject[i]?.GetComponent<Rigidbody2D>();
            }

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
            {
                skillControls[nowIndex]?.ReleaseChooseSkill();
            }
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
                        canChangeObject[i].transform.position = canChangeObject[nowIndex].transform.position;
                        nowIndex = i;
                        drawData.beginPos = canChangeObject[nowIndex].transform.position;
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
            
            
            motors[nowIndex]?.Move(horizontal);
            if(horizontal != 0 && !jump )
            {
                Debug.LogError("动画播放");
                //player_anim.SetBool("Run", ture);
            }

            if (jump) { 
                motors[nowIndex]?.DesireJump();
                Debug.LogError("跳跃动画播放！");
                //player_anim.SetBool("Jump", ture);

            }
            
            if (Rbs[nowIndex].velocity.y < 0 && !((Motor.Rigibody2DMotor)motors[nowIndex]).OnGround)
            {
                //anim.SetBool("Jumping", false);
                //anim.SetBool("Falling", true);
                Debug.LogError("下落动画播放");
            }
            if (esc)
                UIExtentControl.Instance?.ShowOrClose();

            if (interacte && interactionControls != null)
                interactionControls[nowIndex]?.RunInteraction();

            if (transform.position.y < dieY)
                SceneChangeControl.Instance.ReloadActiveScene();
        }

        public override Vector3 GetPosition()
        {
            return canChangeObject[nowIndex].transform.position;
        }
    }
}