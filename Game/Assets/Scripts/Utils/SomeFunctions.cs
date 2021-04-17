using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SomeFunctions
{
    public static int GetID(string name)
    {
        //return (int)char.GetNumericValue(name[name.Length - 1]);
        
        string ID = name.Substring(name.LastIndexOf(' ') + 1);
        return int.Parse(ID);
    }
}
