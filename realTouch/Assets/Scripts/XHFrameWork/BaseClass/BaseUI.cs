//
// /**************************************************************************
//
// BaseUI.cs
//
// Author: xiaohong  <704872627@qq.com>
//
// Unity课程讨论群:  152767675
//
// Date: 15-8-6
//
// Description:Provide  functions  to connect Oracle
//
// Copyright (c) 2015 xiaohong
//
// **************************************************************************/


using UnityEngine;
using System.Collections;
using System;

namespace XHFrameWork
{
	public abstract class BaseUI : MonoBehaviour
	{
        /// <summary>
        /// 加载的3D空间中相对应界面的3D物体的状态
        /// </summary>
        protected EnumThreeObjectState ThreeObjectState = EnumThreeObjectState.None;

        public EnumThreeObjectState _ThreeObjectState
        {
            get { return ThreeObjectState; }
           
        }

        private int xPosition;//生成UI后相对的uiroot的位置

        public int XPosition
        {
            get { return (int)gameObject.transform.localPosition.x; }
        }
        
        //3D世界对应的相机 

        // 保存UI常用节点,调节自适应分辨率
        private UIPanel originPanel;
        public UIPanel OriginPanel
        {
            get { return originPanel; }
        }

        private Transform background;//UI背景

        // 如果需要可以添加一个BoxCollider屏蔽事件
        private bool isLock = false;
        protected bool isShown = false;

		#region Cache gameObject & transfrom

		private Transform _CachedTransform;
		/// <summary>
		/// Gets the cached transform.
		/// </summary>
		/// <value>The cached transform.</value>
		public Transform cachedTransform
		{
			get
			{
				if (!_CachedTransform)
				{
					_CachedTransform = this.transform;
				}
				return _CachedTransform;
			}
		}
		
		private GameObject _CachedGameObject;
		/// <summary>
		/// Gets the cached game object.
		/// </summary>
		/// <value>The cached game object.</value>
		public GameObject cachedGameObject
		{
			get
			{
				if (!_CachedGameObject)
				{
					_CachedGameObject = this.gameObject;
				}
				return _CachedGameObject;
			}
		}

		#endregion
		
		#region UIType & EnumObjectState
		/// <summary>
		/// The state.
		/// </summary>
		protected EnumObjectState state = EnumObjectState.None;

		/// <summary>
		/// Occurs when state changed.
		/// </summary>
		public event StateChangedEvent StateChanged;

		/// <summary>
		/// Gets or sets the state.
		/// </summary>
		/// <value>The state.</value>
		public EnumObjectState State
		{
			protected set
			{
				if (value != state)
				{
					EnumObjectState oldState = state;
					state = value;
					if (null != StateChanged)
					{
						StateChanged(this, state, oldState);
					}
				}
			}
			get { return this.state; }
		}
	
		/// <summary>
		/// Gets the type of the user interface.
		/// </summary>
		/// <returns>The user interface type.</returns>
		public abstract EnumUIType GetUIType ();

        public abstract EnumCamera GetCameraType();

		#endregion


        #region 动画组件

        private TweenPosition tweenPosition;

        public TweenPosition TweenPosition
        {
            get { return tweenPosition; }
            
        }

       
        #endregion

        public bool IsLock
        {
            get { return isLock; }
            set { isLock = value; }
        }

        private int minDepth = 1;
        public int MinDepth
        {
            get { return minDepth; }
            set { minDepth = value; }
        }

		/// <summary>
		/// UI层级置顶
		/// </summary>
		protected virtual void SetDepthToTop()
		{
			
		}

		
		// Use this for initialization
		void Start ()
		{
			OnStart ();
		}

		void Awake()
		{
			this.State = EnumObjectState.Initial;

            originPanel = this.GetComponent<UIPanel>();

            if(this.gameObject.activeSelf)
            this.gameObject.SetActive(true);

            tweenPosition = this.gameObject.AddComponent<TweenPosition>();

            string s = this.GetUIType().ToString();//获得该UI类型

            background = this.transform.Find(s + "BG");//命名规则，背景必须是ui名字加上BG两个字


            tweenPosition.animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);//设置动画曲线

            tweenPosition.enabled = false;//让其先不能播放动画

			OnAwake ();
		}
		
		// Update is called once per frame
		void Update ()
		{
			if (EnumObjectState.Ready == this.state) 
			{
				OnUpdate(Time.deltaTime);
			}
		}

		/// <summary>
		/// Release this instance.
		/// </summary>
		public void Release()
		{
			this.State = EnumObjectState.Closing;
			GameObject.Destroy (cachedGameObject);//删除UI

            if(background != null)
            GameObject.Destroy(background.gameObject);//删除掉背景,因跟主UI不是同一个物体

            
			OnRelease ();
		}
		
		protected virtual void OnStart()
		{

		}

		protected virtual void OnAwake()
		{


            

			this.State = EnumObjectState.Loading;

            
			//播放音乐
			this.OnPlayOpenUIAudio();
		}
		
		protected virtual void OnUpdate(float deltaTime)
		{
			
		}

		protected virtual void OnRelease()
		{
			this.OnPlayCloseUIAudio();
		}

		
		/// <summary>
		/// 播放打开界面音乐
		/// </summary>
		protected virtual void OnPlayOpenUIAudio()
		{
			
		}
		
		/// <summary>
		/// 播放关闭界面音乐
		/// </summary>
		protected virtual void OnPlayCloseUIAudio()
		{
			
		}

		protected virtual void SetUI( Transform cameraBG, params object[] uiParams)
        {
            #region  处理UI整体的自适应
            //设定UI的初始位置
            if (originPanel.width == 1024 && originPanel.height == 768)//判断是否是全屏界面
            {
                // Debug.Log("偏移量为：" + (int)uiParams[0]);
                if (uiParams.Length > 0)//是否有X轴参数传入
                {
                    Vector3 v = this.gameObject.transform.localPosition;

                     xPosition = (int)uiParams[0];//参数为位置参数

                   // Debug.Log("有参数传入的的UI界面:" + this.transform.name + "参数是" + xMove);

                     this.gameObject.transform.localPosition = new Vector3(xPosition, v.y, v.z);

                     originPanel.SetAnchor(this.transform.parent.parent.gameObject, xPosition, 0, xPosition, 0);//自适应屏幕分辨率，从大方面
                }
                else
                {
                    originPanel.SetAnchor(this.transform.parent.parent.gameObject, 0, 0, 0, 0);//自适应屏幕分辨率，从大方面
                }
            }
            #endregion

            #region 处理背景UI

            if (background != null)
            {
                background.parent = cameraBG;

                background.gameObject.layer = 5;//必须是UI背景层
            }
           
            #endregion



            this.State = EnumObjectState.Loading;
		}
		
		public virtual void SetUIparam(params object[] uiParams)
		{
			
		}
        /// <summary>
        /// 导入数据，在这个项目里是导入三维空间的数据
        /// </summary>
		protected virtual void OnLoadData()
		{
           
		}

		public void SetUIWhenOpening(Transform cameraBG, object[] uiParams)
		{
            
			SetUI(cameraBG,uiParams);
			CoroutineController.Instance.StartCoroutine(AsyncOnLoadData());
		}

		private IEnumerator AsyncOnLoadData()
		{
           

			yield return new WaitForSeconds(0);

			if (this.State == EnumObjectState.Loading || this.state == EnumObjectState.MoveLefting || this.state == EnumObjectState.MoveRigting)
			{
				this.OnLoadData();
				this.State = EnumObjectState.Ready;
			}
		}

        public void AminationUI(bool isForward,int xPosition = 0)
        {

            if (isForward)
            {
                State = EnumObjectState.MoveLefting;

            }
            else
            {
                State = EnumObjectState.MoveRigting;
            }
            EventDelegate ev = null;

            #region  动画完成后执行的操作

            ev = new EventDelegate(() =>
            {
                State = EnumObjectState.Ready;
                tweenPosition.RemoveOnFinished(ev);
            });
            tweenPosition.onFinished.Add(ev);
            #endregion
            TweenPosition.Begin(this.gameObject, 0.35f, new Vector3(xPosition, 0, 0));
        }

        protected virtual void ReceiveThree3DInfo(Message message)
        {
            ThreeObjectState = (EnumThreeObjectState)message.Content;
        }
        
	}
}

