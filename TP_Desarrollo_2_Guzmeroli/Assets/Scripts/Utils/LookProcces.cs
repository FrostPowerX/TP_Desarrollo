using UnityEngine;

public class LookProcces : MonoBehaviour
{
    [SerializeField] bool invert;

    [SerializeField] float xRotation = 0f;

    public void Look(Vector2 rotation, Vector2 sensivility, GameObject body)
    {
        float Xmouse = rotation.x;
        float mouseY = rotation.y;

        if (invert) mouseY *= -1;

        xRotation -= (mouseY * Time.deltaTime) * sensivility.y;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        body.transform.Rotate(Vector3.up * (Xmouse * Time.deltaTime) * sensivility.x);
    }
}
