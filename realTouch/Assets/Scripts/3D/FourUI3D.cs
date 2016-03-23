using UnityEngine;
using System.Collections;
using XHFrameWork;

public class FourUI3D : BaseThreeD
{

    protected CameraControl cc
    {
        get
        {
            CameraControl cc_ = camera.transform.GetComponent<CameraControl>();

            if (cc_ != null)
            {
                return cc_;
            }

            else
                throw new SingletonException("没有得到相机控制器");
        }

    }//相机控制组件

    public FourUI3D(int xPosition, BaseUI baseUI)
        : base(xPosition, baseUI)
    {
            ResManager.Instance.LoadCoroutineInstance(UIPathDefines.THREE_DIMENSIONAL + "Four_", (Object) =>
            {
                target = (GameObject)Object;

                
            });
    }

    protected override void StateChanged(object sender, EnumObjectState newState, EnumObjectState oldState)
    {
        base.StateChanged(sender, newState, oldState);

        BaseUI baseUI = (BaseUI)sender;

        if (newState == EnumObjectState.MoveLefting && oldState == EnumObjectState.Ready)//开始进入界面切换动画
        {
            cc.enabled = false;

            D3Tween(true);
        }
        else if (newState == EnumObjectState.MoveRigting && oldState == EnumObjectState.Ready)//动画切换结束
        {
            cc.enabled = false;

            D3Tween(false);

        }
        else if (newState == EnumObjectState.Ready && (oldState == EnumObjectState.MoveLefting || oldState == EnumObjectState.MoveRigting))
        {
            if (baseUI.XPosition == 0)
            {
                cc.target = target.transform;

                cc.enabled = true;
            }
        }
    }

    protected override void SendMessaget()
    {
        Message message = new Message(MessageType.Net_FoutUI3d_FoutUI, this);

        message.Content = State;

        message.Send();
    }
}
