using System;
using System.Collections.Generic;
//using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.GroundEditor;

public class GroundEditor : MonoBehaviour
{
    public RawImage map;

    public GameObject markersParent;
    public GameObject markersPrefab;

    public List<Mark> markers;
    int number;

    public GameObject meshPrefab;

    public GameObject terrainParent;

    public static GroundEditor instance;

    private void Awake()
    {
        markers = new List<Mark>();
        instance = this;
        number = 0;
    }

    public void LoadMap()
    {

/*        OpenFileDialog openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            WWW www = new WWW("file:///" + openFileDialog.FileName);
            map.texture = www.texture;
        }*/
    }
    public void NewMarker()
    {
        GameObject temp = Instantiate(markersPrefab);
        temp.transform.parent = markersParent.transform;
        Mark mark = temp.GetComponent<Mark>();
        mark.rectTransform.transform.localPosition = Vector3.zero;
        markers.Add(mark);
    }
    double CosCounting(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 VectorA = new Vector3(b.x - a.x, b.y - a.y);
        Vector3 VectorB = new Vector3(b.x - c.x, b.y - c.y);
        return ((VectorA.x * VectorB.x) + (VectorA.y * VectorB.y)) / (Math.Sqrt(VectorA.x * VectorA.x + VectorA.y * VectorA.y) * Math.Sqrt(VectorB.x * VectorB.x + VectorB.y * VectorB.y));
    }
    public static List<Vector3> MarksToVector3(List<Mark> markers, RawImage map)
    {
        List<Vector3> result = new List<Vector3>();
        for (int i = 0; i < markers.Count; i++)
        {
            result.Add(new Vector3(
                markers[i].rectTransform.localPosition.x + map.rectTransform.rect.width,
                markers[i].hight,
                markers[i].rectTransform.localPosition.y + map.rectTransform.rect.height));
        }
        return result;
    }
    public static List<Vector3> Vector3BubbleSortByX(List<Vector3> list) 
    {
        Vector3 temp;
        for (int i = 1; i < list.Count; i++)
        {
            for (int j = 0; j < list.Count-i; j++)
            {
                if (list[j].x>list[j+1].y)
                {
                    temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                }
            }
        }
        return list;
    }
    public void GenerateMesh()
    {
        // vertices
        Vertex[] vertices = Vertex.FromVector3ToVertex(Vector3BubbleSortByX(MarksToVector3(markers, map)).ToArray()).ToArray();

        List<int> trias = new List<int>();

        int iA = 0, iB = 1, iP = 0;

        double minCos = double.MaxValue;
        for (int i = 0; i < vertices.Length; i++)
        {
            if (i != iA)
            {
                //расчет косинуса
                if (CosCounting(vertices[i].position, vertices[iA].position, vertices[iB].position) < minCos)
                {
                    minCos = CosCounting(vertices[i].position, vertices[iA].position, vertices[iB].position);
                    iP = i;
                }

            }
        }
        trias.Add(iA);
        trias.Add(iB);
        trias.Add(iP);

        vertices[iA].isUsed = true;
        vertices[iB].isUsed = true;
        vertices[iP].isUsed = true;

        trias.Add(iP);
        trias.Add(iB);
        trias.Add(iA);
        int iA_copy = iA;
        int iM = 0;
        do
        {
            minCos = double.MaxValue;
            for (int i = 0; i < vertices.Length; i++)
            {
                if (i != iA)
                {
                    //расчет косинуса
                    if (CosCounting(vertices[iA].position, vertices[iP].position, vertices[i].position) < minCos)
                    {
                        minCos = CosCounting(vertices[iA].position, vertices[iP].position, vertices[i].position);
                        iM = i;
                    }
                }
            }
            trias.Add(iA);
            trias.Add(iP);
            trias.Add(iM);

            vertices[iA].isUsed = true;
            vertices[iP].isUsed = true;
            vertices[iM].isUsed = true;

            trias.Add(iM);
            trias.Add(iP);
            trias.Add(iA);
            iA = iP;
            iP = iM;
        } while (!Vertex.IsAllVertexsUsed(vertices)/*iP != iA_copy*/);
        /*        for (int i = 0; i < 3*//*markers.Count*//*; i++)//TODO большое кол-во вершин
                {
                    vertices[i] = new Vector3(
                        markers[i].rectTransform.localPosition.x + map.rectTransform.rect.width,
                        markers[i].hight,
                        markers[i].rectTransform.localPosition.y + map.rectTransform.rect.height);
                }*/

         
        // triangles
        int[] triangles = trias.ToArray();//TODO большое кол-во вершин
        /*        for (int i = 0; i < 3*//*triangles.Length*//*; i++)//TODO большое кол-во вершин
                {
                    triangles[i] = i;
                }*/

/*        // очень временное решение
        for (int i = 3; i < 6*//*triangles.Length*//*; i++)//TODO большое кол-во вершин
        {
            Debug.Log(2 - (i - 3));
            triangles[i] = 2 - (i - 3);
        }*/

        //Mesh
        Mesh mesh = new Mesh();
        mesh.vertices = Vertex.FromVertexToVector3(vertices);
        mesh.triangles = triangles;
        GameObject temp = Instantiate(meshPrefab, terrainParent.transform);
        temp.name = "Terrain";
        temp.GetComponent<MeshFilter>().mesh = mesh;
    }
}
