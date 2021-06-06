using MyOwnClass;
using UnityEngine;

[RequireComponent (typeof(Camera))]
public class CameraFly : MonoBehaviour
{
    [SerializeField] float speed = 15.0f;
    private Vector3 transfer;

    [SerializeField] float minimumX = -360F;
    [SerializeField] float maximumX = 360F;
    [SerializeField] float minimumY = -60F;
    [SerializeField] float maximumY = 60F;
    float rotationX = 0F;
    float rotationY = 0F;
    Quaternion originalRotation;

    [SerializeField] Camera camera;
    [SerializeField] float zoomDelta = 10f;
    [SerializeField] float zoomMax = 100f;
    [SerializeField] float zoomMin = 10f;

    private void Awake()
    {
        camera = GetComponent<Camera>();
        originalRotation = transform.rotation;   
    }

    void Update()
    {
        try
        {
            // Движения мыши -> Вращение камеры
            rotationX += Input.GetAxis("MouseX") * DifferentThings.mouseSensitivity;
            rotationY += Input.GetAxis("MouseY") * DifferentThings.mouseSensitivity;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);
            transform.rotation = originalRotation * xQuaternion * yQuaternion;
            // перемещение камеры
            transfer = transform.forward * Input.GetAxis("Vertical");
            transfer += transform.right * Input.GetAxis("Horizontal");
            transform.position += transfer * DifferentThings.CameraSpeed * Time.deltaTime;
            if ((Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
                && (Input.GetKey(KeyCode.LeftControl)|| Input.GetKey(KeyCode.RightControl))
                && camera.fieldOfView>zoomMin)
            {
                camera.fieldOfView -= zoomDelta;
            }
            if ((Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
                && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                && camera.fieldOfView < zoomMax)
            {
                camera.fieldOfView += zoomDelta;
            }
        }
        catch
        {

        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F) angle += 360F;
        if (angle > 360F) angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

}
