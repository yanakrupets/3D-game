using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text healthLabel;
    [SerializeField] private InventoryPopup popup;
    [SerializeField] private Text levelEnding;
    [SerializeField] private AudioClip sound;

    void Awake()
    {
        EventManager.StartListening(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        EventManager.StartListening(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        EventManager.StartListening(GameEvent.LEVEL_FAILED, OnLevelFailed);
        EventManager.StartListening(GameEvent.GAME_COMPLETE, OnGameComplete);
    }

    void OnDestroy()
    {
        EventManager.StopListening(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        EventManager.StopListening(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        EventManager.StopListening(GameEvent.LEVEL_FAILED, OnLevelFailed);
        EventManager.StopListening(GameEvent.GAME_COMPLETE, OnGameComplete);
    }

    void Start()
    {
        OnHealthUpdated();

        levelEnding.gameObject.SetActive(false);
        popup.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Managers.Audio.PlaySound(sound);
            bool isShowing = popup.gameObject.activeSelf;
            popup.gameObject.SetActive(!isShowing);
            popup.Refresh();
        }
    }

    private void OnHealthUpdated() { 
        string message = "Health: " + Managers.Player.health + "/" + Managers.Player.maxHealth;
        healthLabel.text = message;
    }

    private void OnLevelComplete()
    {
        StartCoroutine(CompleteLevel());
    }

    private IEnumerator CompleteLevel()
    {
        levelEnding.gameObject.SetActive(true);
        levelEnding.text = "Level Complete!";

        yield return new WaitForSeconds(2);

        Managers.Mission.GoToNext();
    }

    private void OnLevelFailed()
    {
        StartCoroutine(FailLevel());
    }

    private IEnumerator FailLevel()
    {
        levelEnding.gameObject.SetActive(true);
        levelEnding.text = "Level Failed";

        yield return new WaitForSeconds(2);

        Managers.Player.Respawn();
        Managers.Mission.RestartCurrent();
    }

    public void SaveGame()
    {
        Managers.Data.SaveGameState();
    }

    public void LoadGame()
    {
        Managers.Data.LoadGameState();
    }

    private void OnGameComplete()
    {
        levelEnding.gameObject.SetActive(true);
        levelEnding.text = "You Finished the Game!";
    }
}
