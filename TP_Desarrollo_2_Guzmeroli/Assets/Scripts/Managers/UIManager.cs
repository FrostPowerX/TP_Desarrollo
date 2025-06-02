using UnityEditor;
using UnityEngine;

public class UIManager : MonoBehaviour
{
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

    }
}
