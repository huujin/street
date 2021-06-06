using Mapbox.Utils;
using UnityEngine;
using UnityEngine.UI;

public class ImmediatePositionWithLocationProvider : MonoBehaviour
{
    [SerializeField] private Text latitude;
    [SerializeField] private Text longitude;
    [SerializeField] private Text altitude;
    private Vector2d position;

    void Update()
    {
        position = MapController.instance.abstractMap.WorldToGeoPosition(transform.position);
        latitude.text = "Latitude: " + position.x;
        longitude.text = "Longitude: " + position.y;
        altitude.text = "Altitude: " +
            MapController.instance.abstractMap.QueryElevationInMetersAt(position) *
            (transform.position.y / (MapController.instance.abstractMap.QueryElevationInUnityUnitsAt(position) * MapController.YScale));

        //Debug.Log(MapController.abstractMap.QueryElevationInMetersAt(position));
    }
}