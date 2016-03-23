//
// /**************************************************************************
//
// Defines.cs
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


namespace XHFrameWork
{

	#region Global delegate 委托
    
    
    /// <summary>
    /// 碰撞的委托
    /// </summary>
    /// <param name="moveName">移动物体的名字</param>
    /// <param name="colliderName">被碰撞物体的名字</param>
    public delegate void CollisionAminationSplit(string moveName, string colliderName);
    /// <summary>
    /// UI状态改变的回调
    /// </summary>
    /// <param name="sender">发送者</param>
    /// <param name="newState">新状态</param>
    /// <param name="oldState">旧状态</param>
	public delegate void StateChangedEvent (object sender,EnumObjectState newState,EnumObjectState oldState);
    /// <summary>
    /// 三维场景状态改变的回调
    /// </summary>
    /// <param name="sender">发送者</param>
    /// <param name="newState">新状态</param>
    /// <param name="oldState">旧状态</param>
    public delegate void Three3DStateChangeEvent(object sender, EnumThreeObjectState newState, EnumThreeObjectState oldState);
	
	public delegate void MessageEvent(Message message);

	public delegate void OnTouchEventHandle(GameObject _listener, object _args, params object[] _params);

	public delegate void PropertyChangedHandle(BaseActor actor, int id, object oldValue, object newValue);
	#endregion

	#region Global enum 枚举
    /// <summary>
    /// 三维类的状态
    /// </summary>
    public enum EnumThreeObjectState
    {
        None,
        /// <summary>
        /// 加载中
        /// </summary>
        Load,
        /// <summary>
        /// 加载完成
        /// </summary>
        LoadOver
    }
	/// <summary>
	/// 对象当前状态 
	/// </summary>
	public enum EnumObjectState
	{
		/// <summary>
		/// The none.
		/// </summary>
		None,
		/// <summary>
		/// The initial.
		/// </summary>
		Initial,
		/// <summary>
		/// The loading.
		/// </summary>
		Loading,
		/// <summary>
		/// The ready.
		/// </summary>
		Ready,
		/// <summary>
		/// The disabled.
		/// </summary>
		Disabled,
		/// <summary>
		/// The closing.
		/// </summary>
		Closing,
        /// <summary>
        /// 滑动状态 
        /// </summary>
        Animation,
        /// <summary>
        /// 向左移动状态中
        /// </summary>
        MoveLefting,
        /// <summary>
        /// 向右移动状态中
        /// </summary>
        MoveRigting

	}

	/// <summary>
	/// Enum user interface type.
	/// UI面板类型
	/// </summary>
	public enum EnumUIType : int
	{
        FixedUI = -1,
		/// <summary>
		/// The none.
		/// </summary>
		None = 0,
        /// <summary>
        ///第一个界面
        /// </summary>
        OneUI,
        /// <summary>
        /// 第二个界面
        /// </summary>
        TwoUI,
        /// <summary>
        /// 第三个UI
        /// </summary>
        ThreeUI,
        /// <summary>
        /// 第四个UI
        /// </summary>
        FourUI,
        /// <summary>
        /// 第五个UI
        /// </summary>
        FiveUI
        
	}

	public enum EnumTouchEventType
	{
		OnClick,
		OnDoubleClick,
		OnDown,
		OnUp,
		OnEnter,
		OnExit,
		OnSelect,  
		OnUpdateSelect,  
		OnDeSelect, 
		OnDrag, 
		OnDragEnd,
		OnDrop,
		OnScroll, 
		OnMove,
	}

	public enum EnumPropertyType : int
	{
		RoleName = 1, // 角色名
		Sex,     // 性别
		RoleID,  // Role ID
		Gold,    // 宝石(元宝)
		Coin,    // 金币(铜板)
		Level,   // 等级
		Exp,     // 当前经验

		AttackSpeed,//攻击速度
		HP,     //当前HP
		HPMax,  //生命最大值
		Attack, //普通攻击（点数）
		Water,  //水系攻击（点数）
		Fire,   //火系攻击（点数）
	}

	public enum EnumActorType
	{
		None = 0,
		Role,
		Monster,
		NPC,
	}

	public enum EnumSceneType
	{
		None = 0,
		StartGame,
		LoadingScene,
		LoginScene,
		MainScene,
		CopyScene,
		PVPScene,
		PVEScene,
	}

    public enum EnumCamera:int
    {
        None = 0,
        CameraAbove,
        CameraPerson
    }
	
	#endregion

	#region Defines static class & cosnt

	/// <summary>
	/// 路径定义。
	/// </summary>
	public static class UIPathDefines
	{
		/// <summary>
		/// UI预设。
		/// </summary>
		public const string UI_PREFAB = "Prefabs/";
		/// <summary>
		/// UI小控件预设。
		/// </summary>
		public const string UI_CONTROLS_PREFAB = "UIPrefab/Control/";
		/// <summary>
		/// ui子页面预设。
		/// </summary>
		public const string UI_SUBUI_PREFAB = "UIPrefab/SubUI/";
		/// <summary>
		/// icon路径
		/// </summary>
		public const string UI_IOCN_PATH = "UI/Icon/";

        public const string THREE_DIMENSIONAL = "ThreeD/";

		/// <summary>
		/// Gets the type of the prefab path by.
		/// </summary>
		/// <returns>The prefab path by type.</returns>
		/// <param name="_uiType">_ui type.</param>
		public static string GetPrefabPathByType(EnumUIType _uiType)
		{
			string _path = string.Empty;
			switch (_uiType)
			{

                case EnumUIType.OneUI:

                    _path = UI_PREFAB + "OneUI";

                    break;
                case EnumUIType.TwoUI:

                    _path = UI_PREFAB + "TwoUI";

                    break;
                case EnumUIType.ThreeUI:

                    _path = UI_PREFAB + "ThreeUI";

                    break;
                case EnumUIType.FourUI:

                    _path = UI_PREFAB + "FourUI";

                    break;
                case EnumUIType.FiveUI:
                    _path = UI_PREFAB + "FiveUI";
                    break;
                case EnumUIType.FixedUI:
                    _path = UI_PREFAB + "FixedUI";
                    break;
                

			default:
			//	Debug.Log("Not Find EnumUIType! type: " + _uiType.ToString());
				break;
			}
			return _path;
		}

		/// <summary>
		/// Gets the type of the user interface script by.
		/// </summary>
		/// <returns>The user interface script by type.</returns>
		/// <param name="_uiType">_ui type.</param>
		public static System.Type GetUIScriptByType(EnumUIType _uiType)
		{
			System.Type _scriptType = null;
			switch (_uiType)
			{

                case EnumUIType.OneUI:
                    _scriptType = typeof(OneUI);
                    break;
                case EnumUIType.TwoUI:
                    _scriptType = typeof(TwoUI);
                    break;
                case EnumUIType.ThreeUI:
                    _scriptType = typeof(ThreeUI);
                    break;
                case EnumUIType.FourUI:
                    _scriptType = typeof(FourUI);
                    break;

                case EnumUIType.FiveUI:
                    _scriptType = typeof(FiveUI);
                    break;
                case EnumUIType.FixedUI:
                    _scriptType = typeof(FixedUI);
                    break;


                
			default:
			//	Debug.Log("Not Find EnumUIType! type: " + _uiType.ToString());
				break;
			}
			return _scriptType;
		}

	}

	#endregion
	//public delegate void OnTouchEventHandle(EventTriggerListener _listener, object _args, params object[] _params);
	

	public class Defines : MonoBehaviour {

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}
