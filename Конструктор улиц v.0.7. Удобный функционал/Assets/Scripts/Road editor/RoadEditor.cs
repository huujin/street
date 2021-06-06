using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadEditor : MonoBehaviour
{
    // Start is called before the first frame update
    public static RoadEditor instance;
    public GameObject roadPrefab;
    public List<GameObject> roads;

    public Image firstPositionButton;
    public Image secondPositionButton;

    public Vector3 startPosition;
    public Vector3 endPosition;

    public GameObject content;
    public GameObject roadInListPrefab;

    public GameObject changeNameOfStreet;

    int number;

    public InputField inputFieldWidth;
    public float width;
    public InputField inputFieldHight;
    public float hight;

    private void Awake()
    {
        instance = this;
        roads = new List<GameObject>();
        startPosition = Vector3.zero;
        endPosition = Vector3.zero;
        number = 0;
        firstPositionButton.color = Color.white;
        secondPositionButton.color = Color.white;
        width = 1;
        hight = 1;
    }
    public void CreateRoad()
    {
        number++;
        BuildRoad(startPosition,endPosition, Convert.ToInt32(width), Convert.ToInt32(hight), number.ToString());
    }
    public void BuildRoad(Vector3 startPosition = new Vector3() , Vector3 endPosition = new Vector3(), int width = 1, int hight = 1, string name = "") 
    {
        GameObject temp = Instantiate(roadPrefab, startPosition, Quaternion.identity, transform);
        roads.Add(temp);
        temp.name = name;
        Road tempRoad = temp.GetComponent<Road>();
        tempRoad.CreadteRoadInList(content, roadInListPrefab);
        tempRoad.EditRoad(startPosition, endPosition, width, hight);
        MyOwnClass.DifferentThings.Roads.Add(new MyOwnClass.SerializableRoad(startPosition,endPosition,width,hight,temp.name));
    }
    public void SetStartPosition()
    {
        // установка начльной позиции
        startPosition = FindObjectOfType<CameraFly>().gameObject.transform.position;
        if (startPosition == endPosition)
        {
            firstPositionButton.GetComponent<Image>().color = Color.red;
        }
        else
        {
            ClearColors();
        }
    }
    public void SetEndPosition()
    {
        //установка конечной позиции
        endPosition = FindObjectOfType<CameraFly>().gameObject.transform.position;
        if (startPosition == endPosition)
        {
            secondPositionButton.color = Color.red;
        }
        else
        {
            ClearColors();
        }
    }
    public void ClearColors()
    {
        firstPositionButton.color = Color.white;
        secondPositionButton.color = Color.white;
    }

    public void DeleteByName(string name)
    {
        foreach (var item in roads)
        {
            if (item.name == name)
            {
                //удаляем из списка дорог, а потом со сцены
                roads.Remove(item);
                Destroy(item);
                break;
            }
        }
    }
    public void ChangeName(string currentName, string newName)
    {
        foreach (var item in roads)
        {
            if (item.name == currentName)
            {
                item.name = newName;
                break;
            }
        }
        foreach (MyOwnClass.SerializableRoad item in MyOwnClass.DifferentThings.Roads)
        {
            if (item.name == currentName)
            {
                item.name = newName;
                break;
            }
        }
    }

    public void SetWidth()
    {
        width = (float)Convert.ToDouble(inputFieldWidth.text);
    }

    public void SetHight()
    {
        hight = (float)Convert.ToDouble(inputFieldHight.text);
    }

}
