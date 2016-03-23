using UnityEngine;
using System.Collections;

public class MoveSample : MonoBehaviour
{	
	void Start(){
		//��ֵ�Զ�����ʽ����iTween���õ��Ĳ���
		Hashtable args = new Hashtable();
 
		//�������������ͣ�iTween�������ֺܶ��֣���Դ���е�ö��EaseType��
		//�����ƶ�����Ч���������ƶ����Ⱥ������ƶ����ȼ����ڱ��١��ȵ�
		args.Add("easeType", iTween.EaseType.easeOutSine);
 
		//�ƶ����ٶȣ�
        //args.Add("speed",10f);
		//�ƶ�������ʱ�䡣�����speed������ô����speed
		args.Add("time",1f);
		//����Ǵ�����ɫ�ġ����Կ�Դ����Ǹ�ö�١�
        //args.Add("NamedValueColor","_SpecColor");
        ////�ӳ�ִ��ʱ��
        //args.Add("delay", 0.1f);
		//�ƶ��Ĺ������泯һ����
        //args.Add("looktarget",Vector3.zero);
 
		//����ѭ������ none loop pingPong (һ�� ѭ�� ����)	
		//args.Add("loopType", "none");
		//args.Add("loopType", "loop");	
		args.Add("loopType", "pingPong");
 
		//�����ƶ������е��¼���
		//��ʼ�����ƶ�ʱ����AnimationStart������5.0��ʾ���Ĳ���
		args.Add("onstart", "AnimationStart");
		args.Add("onstartparams", 5.0f);
		//���ý��ܷ����Ķ���Ĭ����������ܣ�����Ҳ���Ըĳɱ�Ķ�����ܣ�
		//��ô�͵��ڽ��ն���Ľű���ʵ��AnimationStart������
		args.Add("onstarttarget", gameObject);
 
		//�ƶ�����ʱ���ã���������������
		args.Add("oncomplete", "AnimationEnd");
		args.Add("oncompleteparams", "end");
		args.Add("oncompletetarget", gameObject);
 
		//�ƶ��е��ã���������������
        //args.Add("onupdate", "AnimationUpdate");
        //args.Add("onupdatetarget", gameObject);
        //args.Add("onupdateparams", true);
 
		// x y z ��ʾ�ƶ���λ�á�
 
		args.Add("x",5);
		args.Add("y",5);
		args.Add("z",1);
 
		//��ȻҲ����дVector3
		//args.Add("position",Vectoe3.zero);
 
		//�����øĶ���ʼ�ƶ�
		iTween.MoveTo(gameObject,args);	
	}
 
    //�����ƶ��е���
	void AnimationUpdate(bool f)
	{
		Debug.Log("update :" + f);
	}
	//����ʼ�ƶ�ʱ����
	void AnimationStart(float f)
	{
		Debug.Log("start :" + f);
	}
	//�����ƶ�ʱ����
	void AnimationEnd(string f)
	{
		Debug.Log("end : " + f);
	}
}



