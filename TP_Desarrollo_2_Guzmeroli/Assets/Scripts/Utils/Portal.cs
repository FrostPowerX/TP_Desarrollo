using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            SceneController.Instance.LoadSceneAsyncSingle(GameManager.Instance.ActiveScene + 1);
    }
}
