using UnityEngine;
using System.Collections;
using XHFrameWork;

public class FiveUI : BaseUI
{

    FiveUI3D d;

   
    

    public override EnumUIType GetUIType()
    {
        return EnumUIType.FiveUI;
    }

    protected override void OnAwake()
    {
       

        MessageCenter.Instance.AddListener("Net_FiveUI3d_FiveUI", ReceiveThree3DInfo);
    }
    protected override void OnLoadData()
    {
        d = new FiveUI3D(XPosition, this);

        base.OnLoadData();
    }

    protected override void OnRelease()
    {
        d.Destroy();

        d = null;

        MessageCenter.Instance.RemoveListener("Net_FiveUI3d_FiveUI", ReceiveThree3DInfo);

        base.OnRelease();
    }

    public override EnumCamera GetCameraType()
    {
        return EnumCamera.CameraPerson;
    }

   
}
