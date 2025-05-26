using UnityEngine;

public class CrossController : MonoBehaviour
{
    static CrossController instance;

    [SerializeField] GameObject defaultCross;
    [SerializeField] GameObject interactCross;
    [SerializeField] GameObject weaponCross;

    public static CrossController Instance { get { return instance; } }

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    public void ActiveDefaultCross(bool value)
    {
        defaultCross.SetActive(value);
        weaponCross.SetActive(!value);
    }

    public void ActiveInteractCross(bool value)
    {
        interactCross.SetActive(value);
    }

    public void ActiveWeaponCross(bool value)
    {
        weaponCross.SetActive(value);
        defaultCross.SetActive(!value);
    }
}
