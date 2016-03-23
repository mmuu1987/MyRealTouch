using UnityEngine;
using System.Collections;
using XHFrameWork;

public class TwoUI : BaseUI {

    TwoUI3D d;
    public override EnumUIType GetUIType()
    {
        return EnumUIType.TwoUI;
    }
    protected override void OnAwake()
    {
        MessageCenter.Instance.AddListener(MessageType.Net_TwoUI3d_TwoUI, ReceiveThree3DInfo);
    }
    protected override void OnLoadData()
    {
        d = new TwoUI3D(XPosition, this);
       
        base.OnLoadData();
    }
    protected override void OnRelease()
    {
        d.Destroy();

        d = null;

        MessageCenter.Instance.RemoveListener(MessageType.Net_TwoUI3d_TwoUI, ReceiveThree3DInfo);

        base.OnRelease();
    }
    public override EnumCamera GetCameraType()
    {
        return EnumCamera.CameraAbove;
    }
}
