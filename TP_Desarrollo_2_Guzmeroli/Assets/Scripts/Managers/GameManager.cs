using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject playerPref;
    [SerializeField] GameObject UIpref;

    [SerializeField] List<LevelSettingsSO> levelSettingsSOs;

    [SerializeField] GameObject player;
    [SerializeField] GameObject UI;

    [SerializeField] int usedSettings;

    [SerializeField] int activeSceneId;

    public int ActiveScene { get { return activeSceneId; } }


    void CreatePlayer()
    {
        player = Instantiate(playerPref, Vector3.zero, Quaternion.Euler(Vector3.zero));
    }

    void CreatePlayerUI()
    {
        UI = Instantiate(UIpref);
        UI.GetComponent<UIController>().SetTarget(player);
    }

    public LevelSettingsSO StartLevel()
    {
        Time.timeScale = 1.0f;

        CreatePlayer();
        CreatePlayerUI();

        activeSceneId = SceneManager.GetActiveScene().buildIndex;

        return levelSettingsSOs[usedSettings];
    }

    public GameObject GetPlayer()
    {
        if (player)
            return player;
        else
            return null;
    }

    public GameObject GetUI()
    {
        if(UI)
            return UI;
        else return null;
    }
}
