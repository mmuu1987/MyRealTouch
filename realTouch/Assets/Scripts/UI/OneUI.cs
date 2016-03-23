using UnityEngine;
using System.Collections;
using XHFrameWork;

public class OneUI :BaseUI {

    private OneUI3D d;

    protected override void OnAwake()
    {
        ThreeObjectState = EnumThreeObjectState.LoadOver;

        base.OnAwake();
    }
    public override EnumUIType GetUIType()
    {
        return EnumUIType.OneUI;
    }

    public override EnumCamera GetCameraType()
    {
        return EnumCamera.None;
    }
}
