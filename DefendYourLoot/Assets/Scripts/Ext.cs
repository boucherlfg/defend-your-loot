using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Ext {
    public static Vector2 RandomInRect(this Rect rect) {
        return new Vector2(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMin, rect.yMax));
    }

     public static T GetRandom<T>(this IEnumerable<T> list) {
        var index = UnityEngine.Random.Range(0, list.Count());
        return list.ElementAt(index);
    }
    public static T DefaultConstructor<T>() where T : class => (T)typeof(T).GetConstructor(new Type[]{}).Invoke(new object[]{});
}