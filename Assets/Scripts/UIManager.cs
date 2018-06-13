using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public float score;
    public float winNeededScore;
    public Image lifebar;
    public Text scoreText;

    private void Start()
    {
        EventsManager.SubscribeToEvent("ScoreUpdate", ScoreUpdate);
        EventsManager.SubscribeToEvent("ShipLifeModified", OnShipLifeUpdated);
        EventsManager.SubscribeToEvent("Lose", Unsuscribe);
        EventsManager.SubscribeToEvent("Win", Unsuscribe);

    }
    public void ScoreUpdate(params object[] parameters)
    {
        score += (float)parameters[0];
        scoreText.text = score.ToString();
        if (score >= winNeededScore)
        {
            EventsManager.TriggerEvent("Win"); 
        }

    }
    public void Unsuscribe(params object[] parameters)
    {
        EventsManager.UnsubscribeToEvent("ScoreUpdate", ScoreUpdate);
        EventsManager.UnsubscribeToEvent("ShipLifeModified", OnShipLifeUpdated);
        EventsManager.UnsubscribeToEvent("Lose", Unsuscribe);
        EventsManager.UnsubscribeToEvent("Win", Unsuscribe);

    }
    private void OnShipLifeUpdated(params object[] parameters)
    {
        var currentHp = (float)parameters[0];
        var totalHp = (float)parameters[1];
        lifebar.fillAmount = (currentHp / totalHp);
    }

}
