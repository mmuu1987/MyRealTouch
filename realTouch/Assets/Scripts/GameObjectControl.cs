using UnityEngine;
using System.Collections;
using XHFrameWork;
using System;

public class GameObjectControl : MonoBehaviour
{

    public static GameObjectControl _instance;

    void Awake()
    {
        _instance = this;

        UIManager.Instance.OpenUI(EnumUIType.FixedUI);

        UIManager.Instance.OpenUI(EnumUIType.OneUI);

        UIManager.Instance.OpenUI(EnumUIType.TwoUI,1024);
    }
    void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x/Screen.width < 0.15f || Input.mousePosition.x/Screen.width > 0.85f)
            {
                if (Input.GetAxis("Mouse X") < -0.35f)//向左滑动
                {
                    UIManager.Instance.RealTouchOpenUIForDrow(true);

                }
                else if (Input.GetAxis("Mouse X") > 0.35f)//向右滑动
                {
                    UIManager.Instance.RealTouchOpenUIForDrow(false);

                }
            }
           
        }
    }

    public void StartTime(float form, float to, float time, Action<float> ac)
    {
        StartCoroutine(TimeUP(form,to,time,ac));
    }
    /// <summary>
    /// 数值插值方法
    /// </summary>
    /// <param name="form">改变前的数字</param>
    /// <param name="to">改变后的数字</param>
    /// <param name="time">花的时间</param>
    /// <param name="ac">把获得插值的数值给委托</param>
    /// <returns></returns>
    IEnumerator TimeUP(float form, float to, float time,Action<float> ac)
    {
        float thisTime = 0.02f;//返回了一帧，所以把它加回来

        float value;

        while (true)
        {
            yield return null;
            thisTime += Time.deltaTime;
            value =  Mathf.Lerp(form, to, thisTime / time);
            if (ac != null)
               ac(value);
            if (thisTime > time)

                yield break;

        }
    }
   
}
