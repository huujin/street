using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyOwnClass
{

    public abstract class DifferentThings
    {
        public static float mouseSensitivity = 1f;
        public static float CameraSpeed = 5f;

        public static InputField InputX;
        public static InputField InputY;
        public static InputField InputZ;
        public static InputField InputSize;

        public static Text description;
        public static bool MovementArrows;
        public static bool IsDescriptionActive;

        public static List<GameObject> allRealObjects = new List<GameObject>(); //Список реальных объектов GameObjectb
        public static List<ObjectOfConstructor> Objects = new List<ObjectOfConstructor>();
        public static List<SerializableRoad> Roads = new List<SerializableRoad>();
        public static List<Building> Buildings = new List<Building>();

        public static int numberOfActiveObject;

        public static bool IsThereAnyActiveObjects
        {

            get
            {
                return ArrowsController.instance.activeObject != null;
            }
        }


    }

    [Serializable]
    public class ObjectOfConstructor
    {
        public SerializableVector3 position;
        public SerializableQuaternion rotation;
        public float size;
        public string nameOfModel;
        public string description;
        public string streetName;
        public ObjectOfConstructor()
        {

        }
        public ObjectOfConstructor(Vector3 position, Quaternion rotation, string nameOfModel, float size, string description)
        {
            this.position = new SerializableVector3(position);
            this.rotation = new SerializableQuaternion(rotation);
            this.nameOfModel = nameOfModel;
            this.size = size;
            this.description = description;
        }
    }
    [Serializable]
    public class SerializableRoad
    {
        public SerializableVector3 startPosition;
        public SerializableVector3 endPosition;
        public int width;
        public int hight;
        public string name;
        public SerializableRoad() 
        {
        
        }
        public SerializableRoad(Vector3 startPosition, Vector3 endPosition, int width, int hight, string name) 
        {
            this.startPosition = new SerializableVector3(startPosition);
            this.endPosition = new SerializableVector3(endPosition);
            this.width = width;
            this.hight = hight;
            this.name = name;
        }
    }

    [Serializable]
    public class SerializableMesh
    {
        public SerializableVector3[] vertices;
        public int[] triangles;
        public SerializableMesh()
        {

        }
        public SerializableMesh(Mesh mesh) 
        {
            vertices = SerializableVector3.FromVector3Array(mesh.vertices);
            triangles = mesh.triangles;
        }

        public static Mesh ToMesh(SerializableMesh serializableMesh) 
        {
            Mesh result = new Mesh();
            result.vertices = SerializableVector3.ToVector3Array(serializableMesh.vertices);
            result.triangles = serializableMesh.triangles;
            return result;
        }

    }

    [Serializable]
    public class SerializableVector3
    {
        public float x;
        public float y;
        public float z;

        public SerializableVector3()
        {
        }

        public SerializableVector3(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }
        public static SerializableVector3[] FromVector3Array(Vector3[] vector3s)
        {
            SerializableVector3[] result = new SerializableVector3[vector3s.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new SerializableVector3(vector3s[i]);
            }
            return result;
        }
        public static Vector3[] ToVector3Array(SerializableVector3[] vector3s)
        {
            Vector3[] result = new Vector3[vector3s.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Vector3(vector3s[i].x, vector3s[i].y, vector3s[i].z);
            }
            return result;
        }
        public Vector3 ToVector3() 
        {
            return new Vector3(x, y, z);
        }
        public static SerializableVector3 operator +(SerializableVector3 a, SerializableVector3 b) 
        {
            return new SerializableVector3(a.ToVector3() + b.ToVector3());
        }
    }
    [Serializable]
    public class SerializableQuaternion
    {
        public float x;
        public float y;
        public float z;
        public float w;
        public SerializableQuaternion() 
        {
        
        }
        public SerializableQuaternion(Quaternion quaternion) 
        {
            x = quaternion.x;
            y = quaternion.y;
            z = quaternion.z;
            w = quaternion.w;
        }
        public Quaternion ToQuaternion()
        {
            return new Quaternion(x, y, z, w);
        }
    }
    [Serializable]
    public class SerializableMaterial
    {
        public Texture texture;
        public Color color;
        public SerializableMaterial()
        {
        
        }
        public SerializableMaterial(Material material) 
        {
            texture = material.mainTexture;
            color = material.color;
        }

        [Obsolete]
        public Material ToMaterial() 
        {
            Material result = Material.Create("");
            result.mainTexture = texture;
            result.color = color;
            return result;
        }
    }
    public class SerializableTexture 
    {
        public SerializableTexture() 
        {
            
        }

    }
}
