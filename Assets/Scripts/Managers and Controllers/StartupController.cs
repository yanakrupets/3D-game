using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartupController : MonoBehaviour
{
    [SerializeField] private Slider progressBar;

    void Awake()
    {
        EventManager.StartListening(StartupEvent.MANAGERS_PROGRESS, OnManagersProgress);
        EventManager.StartListening(StartupEvent.MANAGERS_STARTED, OnManagersStarted);
    }

    void OnDestroy()
    {
        EventManager.StopListening(StartupEvent.MANAGERS_PROGRESS, OnManagersProgress);
        EventManager.StopListening(StartupEvent.MANAGERS_STARTED, OnManagersStarted);
    }

    private void OnManagersProgress(int numReady, int numModules)
    {
        float progress = (float)numReady / numModules;
        progressBar.value = progress;
    }

    private void OnManagersStarted()
    {
        Managers.Mission.GoToNext();
    }
}
