using UnityEngine;
using System.Collections;

public class MoveSample : MonoBehaviour
{	
	void Start(){
		//键值对儿的形式保存iTween所用到的参数
		Hashtable args = new Hashtable();
 
		//这里是设置类型，iTween的类型又很多种，在源码中的枚举EaseType中
		//例如移动的特效，先震动在移动、先后退在移动、先加速在变速、等等
		args.Add("easeType", iTween.EaseType.easeOutSine);
 
		//移动的速度，
        //args.Add("speed",10f);
		//移动的整体时间。如果与speed共存那么优先speed
		args.Add("time",1f);
		//这个是处理颜色的。可以看源码的那个枚举。
        //args.Add("NamedValueColor","_SpecColor");
        ////延迟执行时间
        //args.Add("delay", 0.1f);
		//移动的过程中面朝一个点
        //args.Add("looktarget",Vector3.zero);
 
		//三个循环类型 none loop pingPong (一般 循环 来回)	
		//args.Add("loopType", "none");
		//args.Add("loopType", "loop");	
		args.Add("loopType", "pingPong");
 
		//处理移动过程中的事件。
		//开始发生移动时调用AnimationStart方法，5.0表示它的参数
		args.Add("onstart", "AnimationStart");
		args.Add("onstartparams", 5.0f);
		//设置接受方法的对象，默认是自身接受，这里也可以改成别的对象接受，
		//那么就得在接收对象的脚本中实现AnimationStart方法。
		args.Add("onstarttarget", gameObject);
 
		//移动结束时调用，参数和上面类似
		args.Add("oncomplete", "AnimationEnd");
		args.Add("oncompleteparams", "end");
		args.Add("oncompletetarget", gameObject);
 
		//移动中调用，参数和上面类似
        //args.Add("onupdate", "AnimationUpdate");
        //args.Add("onupdatetarget", gameObject);
        //args.Add("onupdateparams", true);
 
		// x y z 标示移动的位置。
 
		args.Add("x",5);
		args.Add("y",5);
		args.Add("z",1);
 
		//当然也可以写Vector3
		//args.Add("position",Vectoe3.zero);
 
		//最终让改对象开始移动
		iTween.MoveTo(gameObject,args);	
	}
 
    //对象移动中调用
	void AnimationUpdate(bool f)
	{
		Debug.Log("update :" + f);
	}
	//对象开始移动时调用
	void AnimationStart(float f)
	{
		Debug.Log("start :" + f);
	}
	//对象移动时调用
	void AnimationEnd(string f)
	{
		Debug.Log("end : " + f);
	}
}



