
using UnityEngine;

public interface IInteractable
{
    public void OnInteract(GameObject owner);

    public bool IsInteracteable();
}
