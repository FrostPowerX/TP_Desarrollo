using UnityEngine;

[CreateAssetMenu(fileName = "NameOfSound", menuName = "New Sound")]

public class SoundSO : ScriptableObject
{
    public string keyCode;
    public AudioClip clip;

    [Range(0f, 256f)]
    public int priority = 128;
    [Range(-3f, 3f)]
    public float pitch = 1;
    [Range(0f, 1f)]
    public float volume = 1;
    [Range(0f, 1f)]
    public float spatialBlend;

    public float minDistance = 0.5f;
    public float maxDistance = 3.0f;

    public bool loop;

    public SoundType type = SoundType.SFX;

    public void PlaySound()
    {
        SoundManager.Instance.PlaySound(keyCode);
    }
}