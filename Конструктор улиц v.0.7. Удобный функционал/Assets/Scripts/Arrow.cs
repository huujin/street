using MyOwnClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Arrow : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Vector3 positionDelta;
    [SerializeField] private Vector3 rotationDelta;

    public void OnPointerClick(PointerEventData eventData)
    {
        ArrowsController.instance.activeObject.transform.Rotate(rotationDelta);
        ArrowsController.instance.arrowsRotation.transform.Rotate(rotationDelta);
        DifferentThings.Objects[DifferentThings.numberOfActiveObject].rotation = new SerializableQuaternion(ArrowsController.instance.activeObject.transform.rotation);

        ArrowsController.instance.activeObject.transform.position += positionDelta;
        ArrowsController.instance.arrowsRotation.transform.Rotate(rotationDelta);
        DifferentThings.Objects[DifferentThings.numberOfActiveObject].position = new SerializableVector3(ArrowsController.instance.activeObject.transform.position);

        ArrowsController.instance.UpdateText();
        ArrowsController.instance.UseArrows();
    }
}
