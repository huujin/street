using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GroundEditor
{
    class Vertex
    {
        public Vector3 position;
        public bool isUsed;
        public Vertex() 
        {
            position = Vector3.zero;
            isUsed = false;
        }
        public Vertex(Vector3 position)
        {
            this.position = position;
            isUsed = false;
        }
        public Vertex(Vector3 position, bool isUsed)
        {
            this.position = position;
            this.isUsed = isUsed;
        }
        public static List<Vertex> FromVector3ToVertex(Vector3[] vectors) 
        {
            List<Vertex> result = new List<Vertex>();
            foreach (var item in vectors)
            {
                result.Add(new Vertex(item));
            }
            return result;
        }
        public static bool IsAllVertexsUsed(Vertex[] vertices) 
        {
            int temp = 0;
            foreach (var item in vertices)
            {
                if (item.isUsed)
                {
                    temp++;
                }
            }
            return vertices.Length == temp;
        }
        public static Vector3[] FromVertexToVector3(Vertex[] vertices) 
        {
            Vector3[] result = new Vector3[vertices.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = vertices[i].position;
            }
            return result;
        }
    }
}
