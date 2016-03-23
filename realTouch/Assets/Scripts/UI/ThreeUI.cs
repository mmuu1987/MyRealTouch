using UnityEngine;
using System.Collections;
using XHFrameWork;
using System;

public class ThreeUI : BaseUI {


    private ThreeUI3D d;
	// Use this for initialization
	void Start () {

       
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override EnumUIType GetUIType()
    {
        return EnumUIType.ThreeUI;
    }

    protected override void OnAwake()
    {
        MessageCenter.Instance.AddListener(MessageType.Net_ThreeUI3d_ThreeUI, ReceiveThree3DInfo);
    }

    protected override void OnLoadData()
    {
        
        d = new ThreeUI3D(XPosition, this);
    }

    protected override void OnRelease()
    {
        d.Destroy();

        d = null;
        MessageCenter.Instance.RemoveListener(MessageType.Net_ThreeUI3d_ThreeUI, ReceiveThree3DInfo);
        base.OnRelease();
    }

    public override EnumCamera GetCameraType()
    {
        return EnumCamera.CameraAbove;
    }
}
