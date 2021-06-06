using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Road : MonoBehaviour
{
     GameObject roadInList;

    public void EditRoad(Vector3 startPosition, Vector3 endPosition, float width, float hight)
    {
        //при каждом обращении полностью удаляет дорогу и создает новую
        //Создание дороги
        // Создание точек, по которым создадим контуры дороги
        float deltaZ = Vector3.Distance(startPosition, endPosition);
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0,0,0),
            new Vector3(width,0,0),
            new Vector3(width,hight,0),
            new Vector3(0,hight,0),
            new Vector3(0,hight,deltaZ),
            new Vector3(width,hight,deltaZ),
            new Vector3(width,0,deltaZ),
            new Vector3(0,0,deltaZ),
        };
        // Создаем "треугольнички" для меша
        int[] triangles = new int[]
        {
            0, 2, 1, //face front
			0, 3, 2,
            2, 3, 4, //face top
			2, 4, 5,
            1, 2, 5, //face right
			1, 5, 6,
            0, 7, 4, //face left
			0, 4, 3,
            5, 4, 7, //face back
			5, 7, 6,
            0, 6, 7, //face bottom
			0, 1, 6,
        };

        //Mesh
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        GetComponent<MeshFilter>().mesh.RecalculateNormals();
        mesh.RecalculateNormals();
        transform.position = startPosition;
        gameObject.transform.position = startPosition;
        gameObject.transform.LookAt(endPosition);
    }
    public void CreadteRoadInList(GameObject parentOfRoadInList, GameObject prefab) 
    {
        roadInList = Instantiate(prefab);
        roadInList.transform.parent = parentOfRoadInList.transform;
        roadInList.transform.localScale = Vector3.one;
        roadInList.GetComponentInChildren<Text>().text = name;
        roadInList.name = name;
    }
    private void OnDestroy()
    {
        Destroy(roadInList);
        for (int i = MyOwnClass.DifferentThings.Objects.Count-1; i > 0; i--)
        {
            if (MyOwnClass.DifferentThings.Objects[i].streetName==name)
            {
                MyOwnClass.DifferentThings.Objects.RemoveAt(i);
            }
        }
    }
}
