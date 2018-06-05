using System.Collections.Generic;

public class EventsManager
{
    public delegate void EventReceiver(params object[] parameterContainer);
    private static Dictionary<EventType, EventReceiver> _events;

    public static void SubscribeToEvent(EventType eventType, EventReceiver listener)
    {
        if (_events == null)
            _events = new Dictionary<EventType, EventReceiver>();

        if (!_events.ContainsKey(eventType))
            _events.Add(eventType, null);

        _events[eventType] += listener;
    }

    public static void UnsubscribeToEvent(EventType eventType, EventReceiver listener)
    {
        if (_events != null)
        {
            if (_events.ContainsKey(eventType))
                _events[eventType] -= listener;
        }
    }

    public static void TriggerEvent(EventType eventType)
    {
        TriggerEvent(eventType, null);
    }

    public static void TriggerEvent(EventType eventType, params object[] parametersWrapper)
    {
        if (_events == null)
        {
            UnityEngine.Debug.LogWarning("No events subscribed");
            return;
        }

        if (_events.ContainsKey(eventType))
        {
            if (_events[eventType] != null)
                _events[eventType](parametersWrapper);
        }
    }
}

public enum EventType
{
    ScoreModified,
    NaveDefeated,
    NaveLifeModified,
    NaveDamaged,
    Win,
    Lose

}