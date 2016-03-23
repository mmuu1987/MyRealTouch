//
// /**************************************************************************
//
// CoroutineController.cs
//
// Author: xiaohong  <704872627@qq.com>
//
// Unity课程讨论群:  152767675
//
// Date: 15-8-8
//
// Description:Provide  functions  to connect Oracle
//
// Copyright (c) 2015 xiaohong
//
// **************************************************************************/
using UnityEngine;
using System.Collections;
using System;


namespace XHFrameWork
{
	public class CoroutineController : DDOLSingleton<CoroutineController>
	{
        /// <summary>
        /// 判断字符串是否是纯数字的方法
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public bool bolNum(string temp)
        {
            for (int i = 0; i < temp.Length; i++)
            {
                byte tempByte = Convert.ToByte(temp[i]);

                if (tempByte < 48 || tempByte > 57)//如果byte不在数字范围，表明包含非数字字符
                    return false;
            }
            return true;
        }
	}
}

