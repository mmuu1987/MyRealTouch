using UnityEngine;
using System.Collections;
using XHFrameWork;

public class FourUI : BaseUI {

    private FourUI3D d;

    public override EnumUIType GetUIType()
    {
        return EnumUIType.FourUI;
    }

    protected override void OnAwake()
    {
        MessageCenter.Instance.AddListener(MessageType.Net_FoutUI3d_FoutUI, ReceiveThree3DInfo);
    }

    protected override void OnLoadData()
    {
       

        d = new FourUI3D(XPosition, this);
        base.OnLoadData();
    }

    protected override void OnRelease()
    {
        d.Destroy();

        d = null;

        MessageCenter.Instance.RemoveListener(MessageType.Net_FoutUI3d_FoutUI, ReceiveThree3DInfo);

        base.OnRelease();
    }

    public override EnumCamera GetCameraType()
    {
        return EnumCamera.CameraAbove;
    }
}
