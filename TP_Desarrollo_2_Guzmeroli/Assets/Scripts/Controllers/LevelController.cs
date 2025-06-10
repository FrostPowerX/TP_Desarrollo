using UnityEngine;

public class LevelController : Singleton<LevelController>
{
    [SerializeField] Transform spawnPosition;
    [SerializeField] Reception recept;

    [SerializeField] short level;
    [SerializeField] float difficulty;
    [SerializeField] float remainingTime;

    [SerializeField] bool infiniteTime;

    [SerializeField] GameObject victoryPanel;
    [SerializeField] GameObject losePanel;

    void Start()
    {
        if (recept)
            recept.OnAllDone += Victory;

        SetConfiguration();
    }

    private void OnDestroy()
    {
        if (recept)
            recept.OnAllDone -= Victory;
    }

    private void Update()
    {
        if (!infiniteTime)
        {
            remainingTime -= (remainingTime > 0) ? Time.deltaTime : remainingTime;
            GameManager.Instance.GetUI().GetComponent<UIController>().UpdateTime(remainingTime);
        }

        if (remainingTime <= 0)
            Lose();
    }

    void SetConfiguration()
    {
        LevelSettingsSO settings = GameManager.Instance.StartLevel();

        if (recept)
        {
            recept.SetMaxOrders(settings.totalOrders);
            recept.SetOrderTime(settings.timePerOrder);
            recept.SetMinQuantity(settings.minCountOrder);
            recept.SetMaxQuantity(settings.maxCountOrder);
        }

        if (settings)
            remainingTime = settings.timeLimit;

        GameObject player = GameManager.Instance.GetPlayer();

        player.transform.position = spawnPosition.position;
        player.transform.rotation = spawnPosition.rotation;
    }

    void Lose()
    {
        if (!losePanel)
            return;

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        losePanel.SetActive(true);
    }

    void Victory()
    {
        if (!victoryPanel)
            return;

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        victoryPanel.SetActive(true);
    }
}
