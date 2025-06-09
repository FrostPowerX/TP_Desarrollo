using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static Singleton<T> instance;

    public static T Instance { get { return (T)instance; } }

    protected virtual void Initialize()
    {
        Debug.Log($"Initialize Base of {this.ToString()}.");
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log($"Destroyed {this.ToString()}. Already Exist a instance.");
            Destroy(this);
            return;
        }

        instance = this;

        Initialize();
    }
}
