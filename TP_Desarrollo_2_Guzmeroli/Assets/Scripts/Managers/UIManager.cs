using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] SceneAsset actualScene;

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
        SceneController.Instance.UnloadScene(actualScene.name);
    }
}
