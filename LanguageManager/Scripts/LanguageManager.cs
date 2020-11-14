using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
/// <summary>
/// Manage languages text from json
/// </summary>
public class LanguageManager : MonoBehaviour
{
    #region enum
    public enum Language { english = 0, spanish = 1, catalan = 2, french = 3, german = 4, total = 5 } //all available languages
    #endregion

    #region instance
    public static LanguageManager instance = null; //static instance
    #endregion

    #region serialize variables
    [SerializeField] private string jsonName = "testlanguage.json"; //file name from streaming located on streaming assets
    [SerializeField] private Language actualLanguage = Language.english; // actual selected languange
    #endregion

    #region private variables
    private Dictionary<string, string>[] languages = new Dictionary<string, string>[(int)Language.total]; //contains all json data
    private TranslateText[] activeText = null; //All active TranslateText from scene
    #endregion

    #region unity methods
    private void Awake()
    {
        instance = this;
        ReadJson();
    }

    private void Start()
    {
        GetAllText();
        TranslateAllText();
    }
    #endregion

    #region private methods
    //Create or clear dicionaries
    private void CreateNewDictionaries() 
    { 
        for(int i = 0; i < languages.Length;i++) 
        {
            if (languages[i] == null) 
            {
                languages[i] = new Dictionary<string, string>();
            }
            else 
            {
                languages[i].Clear();
            }
        } 
    }

    //Get value of key on actual language
    private string GetActuaLanguageValue(string key) 
    {
        if (languages[(int)actualLanguage] != null && languages[(int)actualLanguage].ContainsKey(key))
        {
            return languages[(int)actualLanguage][key];
        }
        return key+" don't exist on actual language";
    }

    //Get actual language
    private void ChangeActualLanguage(Language newLanguage) 
    {
        actualLanguage = newLanguage;
    }

    //Get all keys from actual language
    private string[] GetKeysFromActualLanguage() 
    {
        return languages[(int) actualLanguage].Keys.ToArray();
    }

    //Start coroutine to read json file
    private void ReadJson()
    {
        StopCoroutine(ReadJsonCo());
        if (isActiveAndEnabled) { StartCoroutine(ReadJsonCo()); }
    }

    //Fill language dictionaries fram json
    private void FillDictionary(string json)
    {
        LanguagesData[] languageData = JsonHelper.FromJson<LanguagesData>(json);
        CreateNewDictionaries();
        for (int i = 0; i < languageData.Length; i++)
        {
            languages[0].Add(languageData[i].id, languageData[i].English);
            languages[1].Add(languageData[i].id, languageData[i].Spanish);
            languages[2].Add(languageData[i].id, languageData[i].Catalan);
            languages[3].Add(languageData[i].id, languageData[i].French);
            languages[4].Add(languageData[i].id, languageData[i].German);
        }
    }

    //Translate objects TranslateText
    private void TranslateAllText() 
    {
        if (activeText != null) 
        {
            for(int i = 0; i < activeText.Length; i++) 
            {
                activeText[i].Translate();
            }
        }
    }

    //Get all active TranslateText from scene
    private void GetAllText() 
    {
        //transform.root.gameObject
        activeText = FindObjectsOfType<TranslateText>();
    }
    #endregion

    #region coroutines
    //Read json file and call to fill dictionary
    private IEnumerator ReadJsonCo()
    {
        UnityWebRequest w = UnityWebRequest.Get(Application.streamingAssetsPath + "/" + jsonName);
        yield return w.SendWebRequest();
        if (string.IsNullOrEmpty(w.error))
        {
            FillDictionary(w.downloadHandler.text);
        }
        else
        {
            Debug.Log("Can't find " + jsonName);
        }
    }
    #endregion

    #region static methods
    public static string GetValue(string value) { return instance.GetActuaLanguageValue(value); } //Get value of key on actual language
    public static void ChangeLanguage(Language newLanguage) { instance.ChangeActualLanguage(newLanguage); } //Get actual language
    public static void ChangeLanguage(int newLanguage) { instance.ChangeActualLanguage((Language)newLanguage); } //Get actual language
    public static string[] GetAllKeys() { return instance.GetKeysFromActualLanguage(); } //Get all keys from actual language
    public static void Translate() { instance.TranslateAllText(); } //Translate objects TranslateText
    public static void GetText() { instance.GetAllText(); } //Get all active TranslateText from scene
    #endregion
}

#region data structure
[System.Serializable]
public class LanguagesData
{
    public string id;
    public string English;
    public string Spanish;
    public string Catalan;
    public string French;
    public string German;
}
#endregion

