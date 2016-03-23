using UnityEngine;
using System.Collections;
using XHFrameWork;

public class OneUI3D : BaseThreeD
{


    protected CameraControl cc
    {
        get
        {
            CameraControl cc_ = camera.GetComponent<CameraControl>();

            if (cc_ != null)
            {
                return cc_;
            }
               
            else
                throw new SingletonException("没有得到相机控制器");
        }
  
    }//相机控制组件

    public OneUI3D(int xPosition, BaseUI baseUI)
        : base(xPosition, baseUI)
    {
       
            ResManager.Instance.LoadCoroutineInstance(UIPathDefines.THREE_DIMENSIONAL + "One_", (Object) =>
            {
                target = (GameObject)Object;
            });
        
    }

    protected override void StateChanged(object sender, EnumObjectState newState, EnumObjectState oldState)
    {
        
    }

    protected override void SendMessaget()
    {
        
    }
}
