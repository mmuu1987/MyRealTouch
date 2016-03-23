using UnityEngine;
using System.Collections;
using XHFrameWork;
using System.Collections.Generic;
public class FiveUI3D : BaseThreeD {

   

    public FiveUI3D(int xPosition, BaseUI baseUI)
        : base(xPosition, baseUI)
    {
        ResManager.Instance.LoadCoroutineInstance(UIPathDefines.THREE_DIMENSIONAL + "Five_", (Object) =>
        {
            target = (GameObject)Object;

            // cc.target = target.transform;//把目标点给相机
        });
    }

    public override void Destroy()
    {
        GameObject.Destroy(camera.transform.parent.gameObject);

        GameObject.Destroy(target);
    }

   
    protected override void SendMessaget()
    {
        Message message = new Message("Net_FiveUI3d_FiveUI", this);

        message.Content = State;

        message.Send();
    }
}
