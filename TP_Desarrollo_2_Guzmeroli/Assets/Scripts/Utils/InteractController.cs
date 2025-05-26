using UnityEngine;
using UnityEngine.InputSystem;

public class InteractController : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] Transform startPos;
    [SerializeField] float maxDistance;

    InputAction interact;

    Color rayColor = Color.gray;

    IInteractable lastInteract;

    private void Awake()
    {
        inputActions.Enable();

        interact = inputActions.FindAction("Interact");

        interact.started += InteractAction;
    }

    private void FixedUpdate()
    {
        InteractRay();
    }

    void InteractRay()
    {
        RaycastHit hit;

        if (Physics.Raycast(startPos.position, startPos.forward, out hit, maxDistance))
        {
            IInteractable obj = hit.transform.GetComponent<IInteractable>();

            if (obj != null && obj.IsInteracteable())
            {
                lastInteract = obj;
                rayColor = Color.green;
            }
            else
            {
                rayColor = Color.red;
                lastInteract = null;
            }
        }
    }

    void InteractAction(InputAction.CallbackContext context)
    {
        if (lastInteract != null)
            lastInteract.OnInteract(gameObject);
    }

#if DEBUG

    private void OnDrawGizmos()
    {
        Debug.DrawRay(startPos.position, startPos.forward * maxDistance, rayColor);
    }

#endif
}
