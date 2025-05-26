using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static Singleton<T> instance = null;

    public static T Instance { get { return (T)instance; } }

    protected virtual void Initialize()
    {
        Debug.Log($"Initialize Base of {this.name}.");
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log($"Destroyed {this.name}. Already Exist a instance.");
            Destroy(this);
        }

        instance = this;

        Initialize();
    }
}
