using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public GameObject a;
    GameObject zxcqwe;
    int year;
    Vector3 xyz;
    //public House ()
    //{
    //    zxcqwe = a;
    //}
    public House(GameObject house)
    {
        zxcqwe = house;
    }
    public int Year
    {  
        get { return year; }
        set { year = value; }
    }
    public Vector3 Position
    {
        get { return xyz; }
        set { xyz = value; }
    }
    public GameObject Address
    {
        get { return zxcqwe; }
        set { zxcqwe = value; }
    }
}
