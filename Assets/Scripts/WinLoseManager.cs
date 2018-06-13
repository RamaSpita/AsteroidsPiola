using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseManager : MonoBehaviour {

    public float timeToChangeScene = 1;
    public GameObject lose;
    public GameObject win;
    private void Start()
    {
        EventsManager.SubscribeToEvent("Win", Win);
        EventsManager.SubscribeToEvent("Lose", Lose);

    }
    public void Win(params object[] parameters)
    {
        Time.timeScale = 0;
        win.SetActive(true);
        StartCoroutine(ChangeSceneOnTime("Menu"));
        EventsManager.UnsubscribeToEvent("Win", Win);
        EventsManager.UnsubscribeToEvent("Lose", Lose);
    }

    private void Lose(params object[] parameters)
    {
        Time.timeScale = 0;
        lose.SetActive(true);
        StartCoroutine(ChangeSceneOnTime("Menu"));
        EventsManager.UnsubscribeToEvent("Lose", Lose);
        EventsManager.UnsubscribeToEvent("Win", Win);
    }

    public IEnumerator ChangeSceneOnTime(string scene)
    {
        var wait = new WaitForSecondsRealtime(timeToChangeScene);

        yield return wait;
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);

    }

}