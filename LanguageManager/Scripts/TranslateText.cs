using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Control the Text component
/// </summary>
public class TranslateText : MonoBehaviour
{
    #region serialized variables
    [SerializeField] private string key = ""; //key from LanguageManager dictionary
    #endregion

    #region private variables
    private Text text = null; //text component
    #endregion

    #region unity methods
    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Start()
    {
        Translate();
    }
    #endregion

    #region public methods
    // Get valua from LanguageManager and change text
    public void Translate() 
    {
        if(text != null && key != "") 
        {
            text.text = LanguageManager.GetValue(key);
        }
    }
    #endregion
}
