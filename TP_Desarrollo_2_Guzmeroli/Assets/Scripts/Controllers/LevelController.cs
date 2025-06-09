using UnityEngine;

public class LevelController : Singleton<LevelController>
{
    [SerializeField] Transform spawnPosition;
    [SerializeField] Reception recept;
    [SerializeField] LevelSettingsSO settings;

    [SerializeField] short level;
    [SerializeField] float difficulty;
    [SerializeField] float remainingTime;

    [SerializeField] bool infiniteTime;

    [SerializeField] GameObject victoryPanel;
    [SerializeField] GameObject losePanel;

    void Start()
    {
        SetConfiguration();
    }

    private void Update()
    {
        if (!infiniteTime)
            remainingTime -= (remainingTime > 0) ? Time.deltaTime : remainingTime;

        if (remainingTime <= 0)
            Lose();
    }

    void SetConfiguration()
    {
        settings = GameManager.Instance.StartLevel();

        recept.SetMaxOrders(settings.totalOrders);
        recept.SetOrderTime(settings.timePerOrder);
        recept.SetMinQuantity(settings.minCountOrder);
        recept.SetMaxQuantity(settings.maxCountOrder);


        GameObject player = GameManager.Instance.GetPlayer();

        player.transform.position = spawnPosition.position;
        player.transform.rotation = spawnPosition.rotation;
    }

    void Lose()
    {

    }
}
