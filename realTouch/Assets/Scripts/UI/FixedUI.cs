using UnityEngine;
using System.Collections;
using XHFrameWork;

public class FixedUI : BaseUI
{
    private bool isHide = true;//该固定界面是否隐藏起来了

    public override EnumUIType GetUIType()
    {
        return EnumUIType.FixedUI;
    }

    public override EnumCamera GetCameraType()
    {
        return EnumCamera.None;
    }

    protected override void OnAwake()
    {
        ThreeObjectState = EnumThreeObjectState.LoadOver;

        base.OnAwake();

        NGUIEventListener.Get(this.transform.Find("Indoor").gameObject).SetEventHandle(EnumTouchEventType.OnClick, Indoor);

        NGUIEventListener.Get(this.transform.Find("ApartmentLayout").gameObject).SetEventHandle(EnumTouchEventType.OnClick, ApartmentLayout);

        NGUIEventListener.Get(this.transform.Find("Monomer").gameObject).SetEventHandle(EnumTouchEventType.OnClick, Monomer);

        NGUIEventListener.Get(this.transform.Find("Gardens").gameObject).SetEventHandle(EnumTouchEventType.OnClick, Gardens);

        NGUIEventListener.Get(this.transform.Find("Project").gameObject).SetEventHandle(EnumTouchEventType.OnClick, Project);

        NGUIEventListener.Get(this.transform.Find("Region").gameObject).SetEventHandle(EnumTouchEventType.OnClick, Region);

        NGUIEventListener.Get(this.transform.Find("OnClick").gameObject).SetEventHandle(EnumTouchEventType.OnClick, OnClickAmination);
    }

    protected override void SetUI(Transform cameraBG, params object[] uiParams)
    {
        // this.transform.localPosition = new Vector3(-566, 0, 0);

        OriginPanel.SetAnchor(this.transform.parent.parent.gameObject, -110, 140, -1024, -140);
    }

    #region  按钮事件
    void OnClickAmination(GameObject _listener, object _args, params object[] _params)
    {
        if (isHide)
        {
            TweenPosition.Begin(this.gameObject, 0.25f, new Vector3(110, 0, 0));
            isHide = false;
        }
        else
        {
            TweenPosition.Begin(this.gameObject, 0.25f, new Vector3(0, 0, 0));
            isHide = true;
        }


    }
    void Indoor(GameObject _listener, object _args, params object[] _params)
    {

    }
    /// <summary>
    /// 户型
    /// </summary>
    /// <param name="_listener"></param>
    /// <param name="_args"></param>
    /// <param name="_params"></param>
    void ApartmentLayout(GameObject _listener, object _args, params object[] _params)
    {
       CoroutineController.Instance.StartCoroutine( UIManager.Instance.RealTouchOpenUI(EnumUIType.FiveUI));

    }
    /// <summary>
    /// 单体
    /// </summary>
    /// <param name="_listener"></param>
    /// <param name="_args"></param>
    /// <param name="_params"></param>
    void Monomer(GameObject _listener, object _args, params object[] _params)
    {
         CoroutineController.Instance.StartCoroutine(UIManager.Instance.RealTouchOpenUI(EnumUIType.FourUI));

    }
    /// <summary>
    /// 园林
    /// </summary>
    /// <param name="_listener"></param>
    /// <param name="_args"></param>
    /// <param name="_params"></param>
    void Gardens(GameObject _listener, object _args, params object[] _params)
    {
         CoroutineController.Instance.StartCoroutine(UIManager.Instance.RealTouchOpenUI(EnumUIType.ThreeUI));

    }
    /// <summary>
    /// 项目
    /// </summary>
    /// <param name="_listener"></param>
    /// <param name="_args"></param>
    /// <param name="_params"></param>
    void Project(GameObject _listener, object _args, params object[] _params)
    {
         CoroutineController.Instance.StartCoroutine(UIManager.Instance.RealTouchOpenUI(EnumUIType.TwoUI));

    }
    /// <summary>
    /// 区域
    /// </summary>
    /// <param name="_listener"></param>
    /// <param name="_args"></param>
    /// <param name="_params"></param>
    void Region(GameObject _listener, object _args, params object[] _params)
    {
        CoroutineController.Instance.StartCoroutine( UIManager.Instance.RealTouchOpenUI(EnumUIType.OneUI));
    }

    #endregion
}
