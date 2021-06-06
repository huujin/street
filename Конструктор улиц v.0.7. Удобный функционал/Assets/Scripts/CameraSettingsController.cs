using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSettingsController : MonoBehaviour
{
    [SerializeField] Camera camera;


    [SerializeField] Slider cameraZoom;
    [SerializeField] Dropdown projections;
    public void ChangeCameraZoom()
    {
        camera.fieldOfView = cameraZoom.value;
        camera.orthographicSize = cameraZoom.value;
    }

    public void ChangePerspective() 
    {
        if (projections.value==0)
        {
            camera.orthographic = false;
        }
        else 
        {
            camera.orthographic = true;
        }
    }
}
