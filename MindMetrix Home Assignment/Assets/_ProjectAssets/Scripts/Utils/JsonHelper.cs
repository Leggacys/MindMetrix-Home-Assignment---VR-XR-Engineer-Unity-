using System;
using UnityEngine;

/// <summary>
/// Helper class for serializing and deserializing arrays to and from JSON format.
/// </summary>
public class JsonHelper
{
    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }

    public static string ToJson<T>(T[] array, bool prettyPrint = false)
    {
        Wrapper<T> wrapper = new Wrapper<T> { Items = array };
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }
}
