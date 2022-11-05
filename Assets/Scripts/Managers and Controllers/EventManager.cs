using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TypedEvent : UnityEvent<float> { }
public class TypedEvent2 : UnityEvent<int, int> { }

public class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityEvent> eventDictionary;
    private Dictionary<string, TypedEvent> typedEventDictionary;
    private Dictionary<string, TypedEvent2> typedEventDictionary2;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
            typedEventDictionary = new Dictionary<string, TypedEvent>();
            typedEventDictionary2 = new Dictionary<string, TypedEvent2>();
        }
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (eventManager == null) return;
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }

    public static void StartListening(string eventName, UnityAction<float> listener)
    {
        TypedEvent thisEvent = null;
        if (instance.typedEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new TypedEvent();
            thisEvent.AddListener(listener);
            instance.typedEventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<float> listener)
    {
        if (eventManager == null) return;
        TypedEvent thisEvent = null;
        if (instance.typedEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, float data)
    {
        TypedEvent thisEvent = null;
        if (instance.typedEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(data);
        }
    }

    public static void StartListening(string eventName, UnityAction<int, int> listener)
    {
        TypedEvent2 thisEvent = null;
        if (instance.typedEventDictionary2.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new TypedEvent2();
            thisEvent.AddListener(listener);
            instance.typedEventDictionary2.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<int, int> listener)
    {
        if (eventManager == null) return;
        TypedEvent2 thisEvent = null;
        if (instance.typedEventDictionary2.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, int data1, int data2)
    {
        TypedEvent2 thisEvent = null;
        if (instance.typedEventDictionary2.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(data1, data2);
        }
    }
}
