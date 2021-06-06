using Mapbox.Utils;
using MyOwnClass;
using UnityEngine;
using UnityEngine.UI;

public class ArrowsController : MonoBehaviour
{
    public static ArrowsController instance;
    public GameObject arrowsPosition;
    public GameObject arrowsRotation;
    public GameObject activeObject;
    InputField inputX;
    InputField inputY;
    InputField inputZ;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        inputX = DifferentThings.InputX;
        inputY = DifferentThings.InputY;
        inputZ = DifferentThings.InputZ;
        HideAllArrows();
        //inputSize = DifferentThings.InputSize;
    }
    public void UpdateText()
    {
        if (activeObject != null)
        {
            if (DifferentThings.MovementArrows)
            {
                Vector2d position = MapController.instance.abstractMap.WorldToGeoPosition(activeObject.transform.position);
                inputX.text = position.x.ToString();
                inputY.text = (MapController.instance.abstractMap.QueryElevationInMetersAt(position) *
                    (activeObject.transform.position.y / (MapController.instance.abstractMap.QueryElevationInUnityUnitsAt(position) * MapController.YScale))).ToString();
                inputZ.text = position.y.ToString();
            }
            else
            {
                inputX.text = Mathf.RoundToInt(activeObject.transform.rotation.eulerAngles.x).ToString();
                inputY.text = Mathf.RoundToInt(activeObject.transform.rotation.eulerAngles.y).ToString();
                inputZ.text = Mathf.RoundToInt(activeObject.transform.rotation.eulerAngles.z).ToString();
            }
        }
        else 
        {
            inputX.text = "None";
            inputY.text = "None";
            inputZ.text = "None";
        }
    }
    public void UseArrows()
    {
        arrowsPosition.transform.position = activeObject.transform.position; //перемещаем стрелки к объекту
        arrowsRotation.transform.position = activeObject.transform.position; //перемещаем стрелки к объекту
        if (DifferentThings.MovementArrows)
        {
            arrowsPosition.transform.localScale = Vector3.one;
            arrowsRotation.transform.localScale = Vector3.zero;
        }
        else
        {
            arrowsPosition.transform.localScale = Vector3.zero;
            arrowsRotation.transform.localScale = Vector3.one;
        }
    }
    public void HideAllArrows()
    {
        arrowsPosition.transform.localScale = Vector3.zero;
        arrowsRotation.transform.localScale = Vector3.zero;
    }
}
