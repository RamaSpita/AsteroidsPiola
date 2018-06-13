using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Main : MonoBehaviour {

    private static Main _instance;
    public static Main Instance
    {
        get
        {
            return _instance;
        }
    }
    
    void Awake()
    {
        _instance = this;
        LocalizationManager loc = new LocalizationManager();
    }

    public void DownloadTextsDatabase()
    {
        //StartCoroutine(DownloadCoroutine());
        LoadFromDisk();        
    }

    public void LoadFromDisk()
    {
        string data = File.ReadAllText("Save/localization.json");
        List<object> parsedData = (List<object>)MiniJSON.Json.Deserialize(data);
        LocalizationManager.Instance.SetTexts(parsedData);
    }

    public IEnumerator DownloadCoroutine()
    {
        //WARNING: La url caduco, es solo de ejemplo.
        Debug.Log ("Comienza a descargar");
		WWW wwwObject = new WWW("https://www.dropbox.com/s/ijbukpsz4a57zzk/localization.json?dl=1");
        yield return wwwObject;
		Debug.Log ("Termina de descargar");        
        List<object> parsedData = (List<object>)MiniJSON.Json.Deserialize(wwwObject.text);
        LocalizationManager.Instance.SetTexts(parsedData);
    }
}
