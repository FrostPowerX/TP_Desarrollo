using UnityEngine;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject playerPref;
    [SerializeField] GameObject UI;

    [SerializeField] List<LevelSettingsSO> levelSettingsSOs;
    [SerializeField] List<int> creatingOnScenesIds;

    [SerializeField] GameObject player;

    [SerializeField] int usedSettings;

    [SerializeField] int activeSceneId;

    public int ActiveScene {  get { return activeSceneId; } }

    private void Start()
    {
        SceneController.Instance.OnLoadingDoneIndex += CheckSceneLoading;
    }

    private void CheckSceneLoading(int scene)
    {
        Time.timeScale = 1.0f;

        activeSceneId = scene;

        for (int i = 0; creatingOnScenesIds.Count > i; i++)
        {
            if(creatingOnScenesIds[i] == scene)
                CreatePlayer();
        }
    }

    void CreatePlayer()
    {
        player = Instantiate(playerPref, levelSettingsSOs[usedSettings].playerPos, Quaternion.Euler(Vector3.zero));
        GameObject ui = Instantiate(UI);

        ui.GetComponent<UIController>().SetTarget(player);
    }

    public Transform GetPlayerTransform()
    {
        if (player)
            return player.transform;
        else
            return null;
    }
}
