using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoadInList : MonoBehaviour
{
    public GameObject changeNameOfStreet;
    public GameObject changeName;
    public void Delete() 
    {
        RoadEditor.instance.DeleteByName(name);
        Destroy(gameObject);
    }
    public void OpenChangeNameOfStreet() 
    {
        changeName = Instantiate(changeNameOfStreet, transform.parent.parent.parent);
        changeName.GetComponent<ChangeName>().roadInList = this;
    }
    public void ChangeName(string name) 
    {
        RoadEditor.instance.ChangeName(gameObject.name, name);
        GetComponentInChildren<Text>().text = name;
        gameObject.name = name;
    }
}
