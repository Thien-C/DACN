using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class GenericNotImplementedError<T>
{
    public static T TryGet(T value, string name)
    {
        if(value != null)
        {
            return value;
        }
        Debug.LogError(typeof(T) + " không được triển khai ở " + name);
        return default;
    }
}
