using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Translator : MonoBehaviour 
{
    public Text[] localizableTexts;
    public static UserLanguage language;

    private void Start()
    {
        UpdateTexts();
    }
    void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateTexts();
        }
	}

    private void UpdateTexts()
    {
        for (int i = 0; i < localizableTexts.Length; i++)
        {
            string key = gameObject.name + "." + localizableTexts[i].gameObject.name;
            localizableTexts[i].text = LocalizationManager.Instance.TryGetText(language, key);
        }
    }

    public void ChangeToEnglish()
    {
        language = UserLanguage.English;
        UpdateTexts();
    }
    public void ChangeToSpanish()
    {
        language = UserLanguage.Spanish;
        UpdateTexts();
    }

}

public enum UserLanguage
{
    English,
    Spanish
}
