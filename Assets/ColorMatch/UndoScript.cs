using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoScript<T>
{
    public static void AddObjectToUndoArray(T[] array, T Aobj)
    {
        bool callBack = false;
        for (int i = array.Length - 1; i >= 0; i--)
        {
            if (array[i] == null)
            {
                array[i] = Aobj;
                callBack = true;
                i = -1;
            }
        }
        if (!callBack)
        {
            for (int i = array.Length - 1; i >= 0; i--)
            {
                if (i - 1 >= 0)
                {
                    array[i] = array[i - 1];
                }
                if (i == 0)
                {
                    array[i] = Aobj;
                }
            }
        }
    }
    public static T GetObjectFromUndoArray(T[] array)
    {
        T ls;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != null)
            {
                ls = array[i];
                array[i] = default(T);
                return ls;
            }
        }
        return default(T);
    }
}
