using UnityEditor;
using UnityEngine;

public class UIManager : MonoBehaviour
{
<<<<<<< Updated upstream
=======
    [SerializeField] int actualScene;

>>>>>>> Stashed changes
    public void Exit()
    {
        Application.Quit();
    }

    public void ChangeScene(int index)
    {
        SceneController.Instance.LoadSceneAsyncSingle(index);
    }

    public void LoadScene(int index)
    {
        SceneController.Instance.LoadSceneAsyncAdditive(index);
    }

    public void UnloadActualScene()
    {
<<<<<<< Updated upstream

=======
        SceneController.Instance.UnloadScene(actualScene);
>>>>>>> Stashed changes
    }
}
