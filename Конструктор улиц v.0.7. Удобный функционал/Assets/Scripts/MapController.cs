using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Data;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    public AbstractMap abstractMap;

    public static MapController instance;

    [SerializeField] InputField latitude;
    [SerializeField] InputField longitude;
    [SerializeField] Slider zoom;

    [SerializeField] Slider west;
    [SerializeField] Slider north;
    [SerializeField] Slider east;
    [SerializeField] Slider south;
    [SerializeField] Slider y;

    [SerializeField] Texture2D grassTexture;
    [SerializeField] Slider textureSlider;

    private void Awake()
    {
        instance = this;
        abstractMap = GetComponent<AbstractMap>();
    }
    private void Start()
    {
        SetTexture();
    }

    public void RebuildMap()
    {
        if (latitude.text.Length > 0 && longitude.text.Length > 0 && latitude.text[latitude.text.Length - 1] != '.' && longitude.text[longitude.text.Length - 1] != '.')
        {
            abstractMap.UpdateMap(new Mapbox.Utils.Vector2d(Convert.ToDouble(latitude.text), Convert.ToDouble(longitude.text)), zoom.value);

            abstractMap.Options.extentOptions.
                defaultExtents.rangeAroundCenterOptions.west = Convert.ToInt32(west.value);
            abstractMap.Options.extentOptions.
                defaultExtents.rangeAroundCenterOptions.north = Convert.ToInt32(north.value);
            abstractMap.Options.extentOptions.
                defaultExtents.rangeAroundCenterOptions.east = Convert.ToInt32(east.value);
            abstractMap.Options.extentOptions.
                defaultExtents.rangeAroundCenterOptions.south = Convert.ToInt32(south.value);
        }
        ResizeMap();
    }
    public void ResizeMap()
    {
        transform.localScale = new Vector3(transform.localScale.x, y.value * transform.localScale.x, transform.localScale.z);
    }
    public static float YScale
    {
        get
        {
            return instance.y.value;
        }
    }
    public void SetTexture()
    {
        if (textureSlider.value == 1)
        {
            try
            {
                abstractMap.ImageLayer.SetLayerSource(ImagerySourceType.None);
            }
            catch
            {

            }
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<UnityTile>())
                {
                    transform.GetChild(i).GetComponent<MeshRenderer>().material.mainTexture = grassTexture;
                }
            }
        }
        else
        {
            abstractMap.ImageLayer.SetLayerSource(ImagerySourceType.MapboxSatellite);
        }
    }
}
