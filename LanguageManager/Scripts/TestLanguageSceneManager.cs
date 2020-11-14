using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Just for control the example scene
/// </summary>
public class TestLanguageSceneManager : MonoBehaviour
{
    #region serialized variables
    [SerializeField] private Text valueText = null;
    [SerializeField] private Dropdown languageDropdown = null;
    [SerializeField] private Dropdown keyDropdown = null;
    [SerializeField] private Button showButton = null;
    #endregion

    #region private variables
    private string key = "";
    #endregion

    #region unity methods
    void Awake()
    {
        AddListeners();
    }

    private void Start()
    {
        GetAllLanguages();
    }
    #endregion

    #region private methods
    //Assigns listeners to dropdown and buttos
    private void AddListeners() 
    {
        showButton.onClick.AddListener(() => { ShowText(); });
        languageDropdown.onValueChanged.AddListener(ChangeLanguage);
        keyDropdown.onValueChanged.AddListener(ChangeKey);
    }

    //Assigns languages to languages dropdown
    private void GetAllLanguages() 
    {
        languageDropdown.ClearOptions();
        List<Dropdown.OptionData> dropdownoptions = new List<Dropdown.OptionData>();
        for (int i = 0; i < (int)LanguageManager.Language.total; i++)
        {
            dropdownoptions.Add(new Dropdown.OptionData(((LanguageManager.Language)i).ToString()));
        }
        languageDropdown.AddOptions(dropdownoptions);
        GetAllKeys();
    }

    //Assigns keys to keys dropdown
    private void GetAllKeys() 
    {
        keyDropdown.ClearOptions();
        List<Dropdown.OptionData> dropdownoptions = new List<Dropdown.OptionData>();
        string[] keys = LanguageManager.GetAllKeys();
        for (int i = 0; i < keys.Length; i++) 
        {
            dropdownoptions.Add(new Dropdown.OptionData(keys[i]));
        }
        keyDropdown.AddOptions(dropdownoptions);
        ChangeKey(keyDropdown.value);
    }

    // Get selected key from key dropdown
    private void ChangeKey(int value)
    {
        key = keyDropdown.options[keyDropdown.value].text;
        valueText.text = "";
    }

    // Change actual language from LanguageManager
    private void ChangeLanguage(int value) 
    {
        LanguageManager.ChangeLanguage(value);
        LanguageManager.Translate();
        GetAllKeys();
        valueText.text = "";
    }

    //Get valua from key and set the text
    private void ShowText() 
    {
        if (key != "") 
        {
            valueText.text = LanguageManager.GetValue(key);
        }
    }
    #endregion
}
