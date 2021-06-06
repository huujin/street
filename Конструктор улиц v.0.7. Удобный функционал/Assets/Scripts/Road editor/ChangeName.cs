using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeName : MonoBehaviour
{
    public RoadInList roadInList;

    public InputField inputField;

    private void Awake()
    {
        inputField = GetComponentInChildren<InputField>();
    }
    private void Start()
    {
        if (FindObjectsOfType<ChangeName>().Length > 1) 
        {
            roadInList.changeName = null;
            Destroy(gameObject);
        }
    }
    public void Submit() 
    {
        roadInList.ChangeName(inputField.text);
        Destroy(gameObject);
    }
    public void DestroyPanel() 
    {
        Destroy(gameObject);
    }
}
