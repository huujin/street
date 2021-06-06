using UnityEngine;
using UnityEngine.UI;

public class ChangeSteetBuildings : MonoBehaviour
{
    public InputField inputField;

    private void Awake()
    {
        inputField = GetComponentInChildren<InputField>();
    }
    public void Submit()
    {
        if (MainScript.instance.usedObject != null)
        {
            MainScript.instance.usedObject.GetComponent<Building>().streetName = inputField.text;
        }
    }
    public void Activate()
    {
        if (MainScript.instance.usedObject != null)
        {
            inputField.text = MainScript.instance.usedObject.GetComponent<Building>().streetName;
        }
    }
}
