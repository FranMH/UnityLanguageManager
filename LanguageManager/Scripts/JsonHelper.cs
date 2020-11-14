using UnityEngine;
/// <summary>
/// Json string/language arra converion
/// </summary>
public static class JsonHelper
{
    #region public methods
    //converts json to language array
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.languages;
    }

    //converts json to array
    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.languages = array;
        return JsonUtility.ToJson(wrapper);
    }

    //converts array to json
    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.languages = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }
    #endregion

    #region private methods
    //Arra of languages
    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] languages;
    }
    #endregion
}