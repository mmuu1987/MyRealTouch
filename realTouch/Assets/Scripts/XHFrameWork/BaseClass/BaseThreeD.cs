using UnityEngine;
using System.Collections;
using XHFrameWork;
using System.Collections.Generic;

public abstract class BaseThreeD  {

    protected Camera camera = null;

    protected GameObject target = null;//相机旋转的目标位置

    protected BaseUI baseUI;//获取相对应的UI

    protected EnumCamera cameraType = EnumCamera.None;

    private EnumThreeObjectState state = EnumThreeObjectState.None;

    /// <summary>
    /// Gets or sets the state.
    /// </summary>
    /// <value>The state.</value>
    public EnumThreeObjectState State
    {
         set
        {
            if (value != state)
            {
                EnumThreeObjectState oldState = state;
                state = value;

                StateChange(state, oldState);
            }
        }
        get { return this.state; }
    }

    public BaseThreeD(int xPosition,BaseUI baseUI)//UI生成在屏幕的左边还是右边，左边为假，右边为真
    {
        if (baseUI.GetCameraType() == EnumCamera.CameraAbove)
        {
            ResManager.Instance.LoadCoroutineInstance(UIPathDefines.THREE_DIMENSIONAL + "Camera3D", (Object) =>
            {
                GameObject obj = (GameObject)Object;

                camera = obj.GetComponent<Camera>();

                camera.name = "Camera" + baseUI.GetUIType().ToString();

                camera.cullingMask = 1 << (10 + (int)baseUI.GetUIType());//加十是因为层就在对应的层加10


                if (xPosition < 0)
                {
                    camera.transform.localPosition = new Vector3(41, 0, 10);

                    camera.rect = new Rect(-1, 0, 1, 1);
                }
                else
                {
                    camera.transform.localPosition = new Vector3(0, 0, 10);

                    camera.rect = new Rect(1, 0, 1, 1);
                }
       
            });
        }
        else if (baseUI.GetCameraType() == EnumCamera.CameraPerson)
        {
            ResManager.Instance.LoadCoroutineInstance(UIPathDefines.THREE_DIMENSIONAL + "CameraPerson", (Object) =>
            {
                GameObject obj = (GameObject)Object;

                obj.name = "Camera" + baseUI.GetUIType().ToString();

               camera = obj.transform.Find("Main Camera").GetComponent<Camera>();

               camera.cullingMask = 1 << (10 + (int)baseUI.GetUIType());//加十是因为层就在对应的层加10

               if (xPosition < 0)
               {
                   camera.rect = new Rect(-1, 0, 1, 1);
               }
               else
               {
                   camera.rect = new Rect(1, 0, 1, 1);
               }
                   
            });
        }
        baseUI.StateChanged += StateChanged;

        CoroutineController.Instance.StartCoroutine(IsLoadOver());
    }

    public virtual void Destroy()
    {
        GameObject.Destroy(camera.gameObject);

        GameObject.Destroy(target);
    }

    protected virtual void StateChanged(object sender, EnumObjectState newState, EnumObjectState oldState)
   {
       BaseUI baseUI = (BaseUI)sender;

       if (newState == EnumObjectState.MoveLefting && oldState == EnumObjectState.Ready)//开始进入界面切换动画
       {
           if (baseUI.XPosition > 0)//Xposition为UI开始动画的位置
           {
               GameObjectControl._instance.StartTime(1, 0, 0.35f, CameraViewport);
              // Debug.Log( baseUI.GetUIType() +"向左移动我执行了1--0");
           }
           else if (baseUI.XPosition == 0)
           {
               GameObjectControl._instance.StartTime(0, -1, 0.35f, CameraViewport);

              // Debug.Log(baseUI.GetUIType() + "向左移动我执行了0---1");
           }
       }
       else if (newState == EnumObjectState.MoveRigting && oldState == EnumObjectState.Ready)//动画向右移动
       {
           if (baseUI.XPosition < 0)
              
           {
               GameObjectControl._instance.StartTime(-1, 0, 0.35f, CameraViewport);

              // Debug.Log(baseUI.GetUIType() + "向右边移动我执行了-1--0");
           }
           else if (baseUI.XPosition == 0)
           {
               GameObjectControl._instance.StartTime(0, 1, 0.35f, CameraViewport);

               //Debug.Log(baseUI.GetUIType() + "向右边移动我执行了0--1");
           }
              
       }
   }

    protected void D3Tween(bool isForward)
   {
       Transform pos;
       
       pos = camera.transform;

       Vector3 v1 = pos.InverseTransformPoint(pos.position);

       Vector3 v2;

       if (isForward)
       {
           v2 = new Vector3(v1.x + 1024 / 50.0f, v1.y, v1.z);
       }
       else
       {
           v2 = new Vector3(v1.x - 1024 / 50.0f, v1.y, v1.z);
       }

       Vector3 v3 = pos.TransformPoint(v2);
   
       TweenPosition.Begin(pos.gameObject, 0.35f, v3);
   }

    private void CameraViewport(float value)
   {
       camera.rect = new Rect(value, 0, 1, 1);
   }

    protected abstract void SendMessaget();

    IEnumerator IsLoadOver()
    {
        while (true)
        {
            if (camera != null && target != null)
            {
                State = EnumThreeObjectState.LoadOver;

                yield break;
            }
            else
            {
                yield return false;
            }
        }
    }

    protected void StateChange(EnumThreeObjectState newState, EnumThreeObjectState oldState)
   {
       if (newState == EnumThreeObjectState.LoadOver)
       {
           SendMessaget();
       }
   }
   
    
}
