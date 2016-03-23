using UnityEngine;
using System.Collections;

public class Test1 : MonoBehaviour

{

    private void Awake()
    {
        string str = "我没有赋值";

       

        StartCoroutine(Test( str));

        StartCoroutine(DebguStr(str));

        

    }
    private IEnumerator Test( string str)
    {
        yield return new WaitForSeconds(2f);

        str = "我赋值了";
    }

   

    private IEnumerator DebguStr(string str)
    {
        while (true)
        {
            yield return null;

            Debug.Log(str);
        }
    }


   
}
