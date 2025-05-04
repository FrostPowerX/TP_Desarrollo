using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SoundController : MonoBehaviour
{
    SoundManager _soundManager;
    [SerializeField] AudioSource _as;

    [SerializeField] string clipName;

    [SerializeField] bool playOnStart;

    void Start()
    {
        if (_as == null && GetComponent<AudioSource>()) _as = GetComponent<AudioSource>();
        else if (!GetComponent<AudioSource>()) Debug.LogError($"No se pudo asignar el Audio Source a {gameObject.name}.");

        _soundManager = SoundManager.Instance;
        _soundManager.SetSound(clipName, _as);

        if (playOnStart && _as != null) PlaySound();
    }

    public void ChangeSound(string newSound)
    {
        if (_as == null)
        {
            Debug.LogError($"No se puede cambiar el sonido porque el objeto {gameObject.name} no contiene un Audio Source");
            return;
        }

        _soundManager.SetSound(newSound, _as);
    }

    public void PlaySound()
    {
        if (_as == null)
        {
            Debug.LogError($"No se puede iniciar el audio porque el objeto {gameObject.name} no contiene un Audio Source");
            return;
        }

        _as.Play();
    }
    public void StopSound()
    {
        if (_as == null)
        {
            Debug.LogError($"No se puede Pausar el audio porque el objeto {gameObject.name} no contiene un Audio Source");
            return;
        }

        _as.Stop();
    }
}