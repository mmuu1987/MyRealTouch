//
// /**************************************************************************
//
// UIManager.cs
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

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace XHFrameWork
{
    /// <summary>
    /// User interface manager.
    /// </summary>
    public class UIManager : Singleton<UIManager>
    {
        /// <summary>
        /// 是否加载完成
        /// </summary>
        bool isLoadOver = true;//是否加载完成，默认加载完成

        private int cenUI = 1;//屏幕中间的UI默认是OneUI

        private Transform cameraBG;

        private Transform cameraSP;
        /// <summary>
        /// NGUI
        /// </summary>
        private Transform uIRoot;

        public Transform UIRoot
        {
            get { return uIRoot; }
        }

       

        #region UIInfoData class
        /// <summary>
        /// User interface UIInfoData.
        /// </summary>
        class UIInfoData
        {

            /// <summary>
            /// Gets the type of the user interface.
            /// </summary>
            /// <value>The type of the user interface.</value>
            public EnumUIType UIType { get; private set; }

            public Type ScriptType { get; private set; }
            /// <summary>
            /// Gets the path.
            /// </summary>
            /// <value>The path.</value>
            public string Path { get; private set; }
            /// <summary>
            /// Gets the user interface parameters.
            /// </summary>
            /// <value>The user interface parameters.强制约定数组的第一个参数必须是UI打开的时候X位移的位置，第二个参数为UI打开的时候Y轴移动的位置
            /// 重中之重</value>
            public object[] UIParams { get; private set; }
            public UIInfoData(EnumUIType _uiType, string _path, params object[] _uiParams)
            {
                this.UIType = _uiType;
                this.Path = _path;
                this.UIParams = _uiParams;
                this.ScriptType = UIPathDefines.GetUIScriptByType(this.UIType);
            }
        }
        #endregion

        /// <summary>
        /// The dic open U is.
        /// </summary>
        private Dictionary<EnumUIType, GameObject> dicOpenUIs = null;

        /// <summary>
        /// The stack open U is.
        /// </summary>
        private Stack<UIInfoData> stackOpenUIs = null;

        /// <summary>
        /// Init this Singleton.
        /// </summary>
        /// 
        public override void Init()
        {
            dicOpenUIs = new Dictionary<EnumUIType, GameObject>();
            stackOpenUIs = new Stack<UIInfoData>();

            GetUIRoot();
        }

        #region Get UI & UIObject By EnunUIType
        /// <summary>
        /// Gets the U.
        /// </summary>
        /// <returns>The U.</returns>
        /// <param name="_uiType">_ui type.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public T GetUI<T>(EnumUIType _uiType) where T : BaseUI
        {
            GameObject _retObj = GetUIObject(_uiType);
            if (_retObj != null)
            {
                return _retObj.GetComponent<T>();
            }
            return null;
        }

        /// <summary>
        /// Gets the user interface object.
        /// </summary>
        /// <returns>The user interface object.</returns>
        /// <param name="_uiType">_ui type.</param>
        public GameObject GetUIObject(EnumUIType _uiType)
        {
            GameObject _retObj = null;
            if (!dicOpenUIs.TryGetValue(_uiType, out _retObj))
                throw new Exception("dicOpenUIs TryGetValue Failure! _uiType :" + _uiType.ToString());
            return _retObj;
        }
        #endregion


        #region Preload UI Prefab By EnumUIType
        /// <summary>
        /// Preloads the U.
        /// </summary>
        /// <param name="_uiTypes">_ui types.</param>
        public void PreloadUI(EnumUIType[] _uiTypes)
        {
            for (int i = 0; i < _uiTypes.Length; i++)
            {
                PreloadUI(_uiTypes[i]);
            }
        }

        /// <summary>
        /// Preloads the U.
        /// </summary>
        /// <param name="_uiType">_ui type.</param>
        public void PreloadUI(EnumUIType _uiType)
        {
            string path = UIPathDefines.GetPrefabPathByType(_uiType);
            Resources.Load(path);
            //ResManager.Instance.ResourcesLoad(path);
        }

        #endregion

        #region Open UI By EnumUIType
        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiTypes">User interface types.</param>
        public void OpenUI(EnumUIType[] uiTypes)
        {
            OpenUI(false, uiTypes, null);
        }

        /// <summary>
        /// 打开多个界面，并传递参数。
        /// </summary>
        /// <param name="uiTypes">User interface types.</param>
        public void OpenUI(EnumUIType[] uiTypes, params object[] uiObjParams)
        {
            OpenUI(false, uiTypes, uiObjParams);
        }

        /// <summary>
        /// Opens the U.
        /// </summary>
        /// <param name="uiType">User interface type.</param>
        /// <param name="uiObjParams">User interface object parameters.可变参数的第一个参数一定是跟UI位置有关的参数，命令UI出现在相对界面上的x,坐标</param>
        public void OpenUI(EnumUIType uiType, params object[] uiObjParams)
        {


            EnumUIType[] uiTypes = new EnumUIType[1];
            uiTypes[0] = uiType;
            OpenUI(false, uiTypes, uiObjParams);



        }

        /// <summary>
        /// Opens the user interface close others.
        /// </summary>
        /// <param name="uiTypes">User interface types.</param>
        public void OpenUICloseOthers(EnumUIType[] uiTypes)
        {
            OpenUI(true, uiTypes, null);
        }

        /// <summary>
        /// Opens the user interface close others.
        /// </summary>
        /// <param name="uiType">User interface type.</param>
        /// <param name="uiObjParams">User interface object parameters._ui parameters.，第一个参数必定要是UI位置有关的参数，而且必须是X轴方向</param>
        public void OpenUICloseOthers(EnumUIType uiType, params object[] uiObjParams)
        {
            EnumUIType[] uiTypes = new EnumUIType[1];
            uiTypes[0] = uiType;
            OpenUI(true, uiTypes, uiObjParams);
        }

        /// <summary>
        /// Opens the U.
        /// </summary>
        /// <param name="_isCloseOthers">If set to <c>true</c> _is close others.</param>
        /// <param name="_uiTypes">_ui types.</param>
        /// <param name="_uiParams">_ui parameters.，第一个参数必定要是UI位置有关的参数，而且必须是X轴方向</param>
        private void OpenUI(bool _isCloseOthers, EnumUIType[] _uiTypes, params object[] _uiParams)
        {


            // Close Others UI.
            if (_isCloseOthers)
            {
                CloseUIAll();
            }

            // push _uiTypes in Stack.
            for (int i = 0; i < _uiTypes.Length; i++)
            {
                EnumUIType _uiType = _uiTypes[i];
                if (!dicOpenUIs.ContainsKey(_uiType))
                {
                    string _path = UIPathDefines.GetPrefabPathByType(_uiType);
                    stackOpenUIs.Push(new UIInfoData(_uiType, _path, _uiParams));
                }
            }

            // Open UI.
            if (stackOpenUIs.Count > 0)
            {
                CoroutineController.Instance.StartCoroutine(AsyncLoadData());
            }
        }

        private IEnumerator<int> AsyncLoadData()
        {
            UIInfoData _uiInfoData = null;

            UnityEngine.Object _prefabObj = null;

            GameObject _uiObject = null;


            if (stackOpenUIs != null && stackOpenUIs.Count > 0)
            {
                do
                {
                    GameObject obj = null;
                    _uiInfoData = stackOpenUIs.Pop();
                    _prefabObj = Resources.Load(_uiInfoData.Path);



                    if (_prefabObj != null)
                    {
                        if (_prefabObj as GameObject)
                        {
                            obj = (GameObject)_prefabObj;
                        }

                        _uiObject = NGUITools.AddChild(cameraSP.gameObject, _prefabObj as GameObject);



                        BaseUI _baseUI = _uiObject.GetComponent<BaseUI>();
                        if (null == _baseUI)
                        {
                            _baseUI = _uiObject.AddComponent(_uiInfoData.ScriptType) as BaseUI;


                        }
                        if (null != _baseUI)
                        {
                            _baseUI.SetUIWhenOpening(cameraBG, _uiInfoData.UIParams);//把背景相机传过去，让UI内部更好的处理背景，把Y轴的位置信息传递过去，让UI自己处理


                        }
                        else
                        {
                            Debug.Log("_baseUI是空的");
                        }
                        dicOpenUIs.Add(_uiInfoData.UIType, _uiObject);
                    }
                    else
                    {
                        //Debug.Log("_prefabObj是空的");
                    }

                }
                while (stackOpenUIs.Count > 0);
            }
            yield return 0;
        }

        #endregion


        #region Close UI By EnumUIType
        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="uiType">User interface type.</param>
        public void CloseUI(EnumUIType _uiType)
        {
            GameObject _uiObj = null;
            if (!dicOpenUIs.TryGetValue(_uiType, out _uiObj))
            {
                //Debug.Log("dicOpenUIs TryGetValue Failure! _uiType :" + _uiType.ToString());
                return;
            }
            CloseUI(_uiType, _uiObj);
        }

        /// <summary>
        /// Closes the U.
        /// </summary>
        /// <param name="_uiTypes">_ui types.</param>
        public void CloseUI(EnumUIType[] _uiTypes)
        {
            for (int i = 0; i < _uiTypes.Length; i++)
            {
                CloseUI(_uiTypes[i]);
            }
        }

        /// <summary>
        /// 关闭所有UI界面
        /// </summary>
        public void CloseUIAll()
        {
            List<EnumUIType> _keyList = new List<EnumUIType>(dicOpenUIs.Keys);
            foreach (EnumUIType _uiType in _keyList)
            {
                GameObject _uiObj = dicOpenUIs[_uiType];
                CloseUI(_uiType, _uiObj);
            }
            dicOpenUIs.Clear();
        }

        private void CloseUI(EnumUIType _uiType, GameObject _uiObj)
        {
            if (_uiObj == null)
            {
                dicOpenUIs.Remove(_uiType);
            }
            else
            {
                BaseUI _baseUI = _uiObj.GetComponent<BaseUI>();
                if (_baseUI != null)
                {
                    _baseUI.StateChanged += CloseUIHandler;
                    _baseUI.Release();
                }
                else
                {
                    GameObject.Destroy(_uiObj);
                    dicOpenUIs.Remove(_uiType);
                }
            }
        }

        /// <summary>
        /// Closes the user interface handler.
        /// </summary>
        /// <param name="_sender">_sender.</param>
        /// <param name="_newState">_new state.</param>
        /// <param name="_oldState">_old state.</param>
        private void CloseUIHandler(object _sender, EnumObjectState _newState, EnumObjectState _oldState)
        {
            if (_newState == EnumObjectState.Closing)
            {
                BaseUI _baseUI = _sender as BaseUI;
                dicOpenUIs.Remove(_baseUI.GetUIType());
                _baseUI.StateChanged -= CloseUIHandler;
            }
        }
        #endregion

        #region 获得UIroot
        /// <summary>
        /// 获取UIROOT,并且获得里面的背景相机和平面相机
        /// </summary>
        private void GetUIRoot()
        {
            uIRoot = GameObject.Find("UI Root").transform;

            cameraBG = uIRoot.Find("CameraBG");


            if (cameraBG == null || cameraBG.camera.depth != 0)//cameraBG相机深度必须为0
                throw new SingletonException("背景相机找不到或者背景相机的深度不为0");

            cameraSP = uIRoot.Find("CameraSP");
            if (cameraSP == null || cameraSP.camera.depth != 2)//cameraBG相机深度必须为0
                throw new SingletonException("平面相机找不到或者平面相机的深度不为0");
        }
        #endregion

        #region RealTouch打开UI，关闭UI操作
        
       
        /// <summary>
        /// UI界面动画之间的切换和内部逻辑
        /// </summary>
        /// <param name="toIn">进到屏幕的UI</param>
        /// <param name="toOut">出到屏幕的UI</param>
        /// <param name="uPFront">预加载的UI</param>
        /// <param name="isForward">向左播放为true，向右播放为false</param>
        /// <param name="del">需要删除的UI</param>
        /// <param name="action">打开UI后的事件</param>
        /// <param name="xPosition">预加载UI所在的X轴的位置</param>
         IEnumerator RealTouchOpenUI(EnumUIType toIn, EnumUIType toOut, EnumUIType uPFront, bool isForward, EnumUIType del = EnumUIType.None, Action<EnumUIType> action = null)
        {
            TweenPosition tp = dicOpenUIs[toIn].GetComponent<TweenPosition>();

            EventDelegate ev = null;
            #region  当动画切换完成后，执行的一些方法
           
            ev = new EventDelegate(() =>
            {
                if (action != null)
                    action(toIn);
                if (isForward)
                    OpenUI(uPFront, 1024);
                else
                {
                    OpenUI(uPFront, -1024);
                }
                CloseUI(del);

                tp.RemoveOnFinished(ev);
            });

            tp.onFinished.Add(ev);
            #endregion

            yield return CoroutineController.Instance.StartCoroutine(CheckUIState());

            if (isForward && toOut != EnumUIType.None)//UI向左移动出去
            {
                dicOpenUIs[toOut].GetComponent<BaseUI>().AminationUI(isForward, -1024);
            }
            else if (!isForward && toOut != EnumUIType.None)//UI向右移动出去
            {
                dicOpenUIs[toOut].GetComponent<BaseUI>().AminationUI(isForward, 1024);
            }
            if (toIn != EnumUIType.None)
            {
                dicOpenUIs[toIn].GetComponent<BaseUI>().AminationUI(isForward);
            }
            cenUI = (int)toIn;
        }
         /// <summary>
         /// 把数字转换为UI类型，其中需要检测运算后，是否有数字对应的UI类型的存在，不存在则赋值none，这个方法用在固定界面按钮中打开界面
         /// </summary>
         /// <param name="openUI">打开的UI</param>
         /// <param name="isForward">向左运动为真，向右为假</param>
         /// <param name="action">完成后的事件</param>
         public void RealTouchOpenUIForDrow(bool isForward, Action<EnumUIType> action = null)
         {
             EnumUIType toIn = EnumUIType.None;

             EnumUIType toOut = EnumUIType.None;

             EnumUIType uPFront = EnumUIType.None;

             EnumUIType del = EnumUIType.None;

             if (isForward)
             {
                 if (!CoroutineController.Instance.bolNum(((EnumUIType)(cenUI + 1)).ToString()))//判断UI类型是否越界，当他不包含纯数字的时候，说明有UI类型存在则可以进行界面动画
                 {
                     toOut = (EnumUIType)(cenUI);

                     if (!CoroutineController.Instance.bolNum(((EnumUIType)(cenUI + 2)).ToString()))//如果不包含数字，则证明有该UI类型
                     {
                         uPFront = (EnumUIType)(cenUI + 2);
                     }
                     if (!CoroutineController.Instance.bolNum(((EnumUIType)(cenUI + 1)).ToString()))//如果不包含数字，则证明有该UI类型
                     {
                         toIn = (EnumUIType)(cenUI + 1);
                     }
                     if (cenUI - 1 > 0)//证明有该UI类型
                     {
                         del = (EnumUIType)(cenUI - 1);
                     }
                     CoroutineController.Instance.StartCoroutine(RealTouchOpenUI(toIn, toOut, uPFront, isForward, del));
                 }
             }
             else
             {
                 if (cenUI > 1)//则可以进行界面动画
                 {
                     toOut = (EnumUIType)(cenUI);

                     if (cenUI - 2 > 0)
                     {
                         uPFront = (EnumUIType)(cenUI - 2);
                     }
                     if (!CoroutineController.Instance.bolNum(((EnumUIType)(cenUI + 1)).ToString()))//如果不包含数字，则证明有该UI类型
                     {
                         del = (EnumUIType)(cenUI + 1);
                     }
                     if (cenUI - 1 > 0)//证明有该UI类型
                     {
                         toIn = (EnumUIType)(cenUI - 1);
                     }
                     cenUI = (int)toIn;

                     CoroutineController.Instance.StartCoroutine(RealTouchOpenUI(toIn, toOut, uPFront, isForward, del));
                 }
             }
         }
         /// <summary>
         /// 鼠标点击打开的UI的方法，固定界面按钮专用
         /// </summary>
         public IEnumerator RealTouchOpenUI(EnumUIType openUI)
         {
             if (!isLoadOver)
                 yield break;
             if (openUI == (EnumUIType)cenUI)
                 yield break;

             #region  如果打开的UI里面包含有了该UI，那么就证明该UI不是在左边，就是在右边
             if (dicOpenUIs.ContainsKey(openUI))
             {
                 if (dicOpenUIs[openUI].transform.localPosition.x > 0)//如果在右边
                 {
                     cenUI = (int)openUI - 1;//设置中心的UI

                     RealTouchOpenUIForDrow(true);
                 }
                 else if (dicOpenUIs[openUI].transform.localPosition.x < 0)//如果在左边
                 {
                     cenUI = (int)openUI + 1;//设置中心的UI

                     RealTouchOpenUIForDrow(false);
                 }
             }
             #endregion

             #region 如果打开的UI里面都没有需要打开的UI，则先删除掉除开固定UI和中间UI的UI，中间的UI不能删除，做切换动画用
             else
             {
                 EnumUIType rigtUI = EnumUIType.None;

                 EnumUIType leftUI = EnumUIType.None;

                 List<EnumUIType> list = new List<EnumUIType>();

                 foreach (var item in dicOpenUIs.Keys)//从字典取得UI
                 {
                     list.Add(item);
                 }
                 foreach (EnumUIType item in list)
                 {
                     if (item != (EnumUIType)cenUI && item != EnumUIType.FixedUI)//删除掉出来if条件的UI
                         CloseUI(item);
                 }
                 if ((int)openUI > cenUI)//打开的UI在中间UI的左边
                 {
                     if (!CoroutineController.Instance.bolNum(((EnumUIType)((int)openUI + 1)).ToString()))//判断中间UI左边是否还有UI
                     {
                         rigtUI = (EnumUIType)(openUI + 1);
                     }
                     leftUI = (EnumUIType)(openUI - 1);

                     OpenUI(openUI, 1024);

                     yield return CoroutineController.Instance.StartCoroutine(CheckUIState());

                     RealTouchOpenUIForButton(openUI, (EnumUIType)cenUI, leftUI, rigtUI, true);
                 }
                 else  //打开的UI在中间UI的右边
                 {
                     if ((EnumUIType)openUI - 1 > 0)//判断中间UI右边边是否还有UI
                         leftUI = (EnumUIType)(openUI - 1);

                     rigtUI = (EnumUIType)(openUI + 1);

                     OpenUI(openUI, -1024);

                     yield return CoroutineController.Instance.StartCoroutine(CheckUIState());

                     RealTouchOpenUIForButton(openUI, (EnumUIType)cenUI, leftUI, rigtUI, false);
                 }
             }
             #endregion

         }
         /// <summary>
         /// 按钮点击切换页面专用的方法
         /// </summary>
         /// <param name="toIn"></param>
         /// <param name="toOut"></param>
         /// <param name="leftFront"></param>
         /// <param name="rigtFront"></param>
         /// <param name="isForward"></param>
         /// <param name="action"></param>
         void RealTouchOpenUIForButton(EnumUIType toIn, EnumUIType toOut, EnumUIType leftFront, EnumUIType rigtFront, bool isForward, Action<EnumUIType> action = null)
         {

             TweenPosition tp = dicOpenUIs[toIn].GetComponent<TweenPosition>();

             EventDelegate ev = null;

             #region  当动画切换完成后，执行的一些方法
             ev = new EventDelegate(() =>
             {
                 if (action != null)
                     action(toIn);
                 CloseUI(toOut);
                 OpenUI(leftFront, -1024);
                 OpenUI(rigtFront, 1024);
                 tp.RemoveOnFinished(ev);
             });
             tp.onFinished.Add(ev);
             #endregion

             if (isForward && toOut != EnumUIType.None)//UI向左移动出去
             {
                 dicOpenUIs[toOut].GetComponent<BaseUI>().AminationUI(isForward, -1024);
             }
             else if (!isForward && toOut != EnumUIType.None)//UI向右移动出去
             {
                 dicOpenUIs[toOut].GetComponent<BaseUI>().AminationUI(isForward, 1024);
             }
             if (toIn != EnumUIType.None)
             {
                 dicOpenUIs[toIn].GetComponent<BaseUI>().AminationUI(isForward);
             }
             cenUI = (int)toIn;//最后进到中间的UI
         }
         /// <summary>
         /// 检测打开的UI是否都加载完数据，配合上一层的协程方法运用，先阻塞到下一步，检测完全加载后才进行到下一步
         /// </summary>
         /// <returns></returns>
        #endregion
       
        IEnumerator CheckUIState()
        {
            isLoadOver = false;//先关闭加载确认

            List<BaseUI> baseUIAll = new List<BaseUI>();

            foreach (var item in dicOpenUIs.Values)
            {
                baseUIAll.Add(item.GetComponent<BaseUI>());
            }
            while (true)
            {
                if (baseUIAll.Count > 0)
                {
                    for (int i = 0; i < baseUIAll.Count; i++)
                    {
                        if (baseUIAll[i]._ThreeObjectState == EnumThreeObjectState.LoadOver)
                            baseUIAll.Remove(baseUIAll[i]);
                    }
                    yield return null;
                }
                else
                {
                        isLoadOver = true;
                        yield break;
                }
            }
        }
    }
}

