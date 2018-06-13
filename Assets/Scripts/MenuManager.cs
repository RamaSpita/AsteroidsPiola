using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject[] languageButons;
    public void ChooseLanguageOptions()
    {
        for (int i = 0; i < languageButons.Length; i++)
        {
            languageButons[i].SetActive(!languageButons[i].activeSelf);
        }
    }
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
