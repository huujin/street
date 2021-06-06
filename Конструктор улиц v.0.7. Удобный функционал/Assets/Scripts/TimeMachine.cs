using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyOwnClass;
using Mapbox.Utils;
using SimpleFileBrowser;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TriLibCore;
using TriLibCore.General;
using UnityEngine.UI;
using System;

public class TimeMachine : MonoBehaviour
{
    string path;
    //OpenFileDialog file = new OpenFileDialog();

    GameObject Спасоглинищевский9 = Resources.Load("HousePrefabs/Большой Спасоглинищевский переулок 9") as GameObject;
    //House Спасоглин9 = new House();
    
    List<House> Buildings = new List<House>();
    public void Awake()
    {
        House Спасоглин9 = new House(Спасоглинищевский9);
        Buildings.Add(Спасоглин9);
        
        for (int i = 0; i < Buildings.Count; i++)
        {
            Instantiate(Buildings[i], Buildings[i].Position, Quaternion.identity);
        }
    }
    public void TakeFromExcel()
    {
        Excel.Application ObjExcel = new Excel.Application();
        //if (file.ShowDialog() == DialogResult.OK)
        //{
        //    path = file.FileName;
        //}
        //ObjExcel.Visible = true;
        Excel.Workbook ObjWorkBook;
        Excel.Worksheet ObjWorkSheet;
        ObjWorkBook = ObjExcel.Workbooks.Open(path);
        ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1];
        for (int i = 0; i < 150; i++)
        {
            Vector3 temp = new Vector3((float)ObjWorkSheet.Cells[i, 4], (float)ObjWorkSheet.Cells[i, 5], 300f);
            Buildings[i].Position = temp;
        }
        ObjExcel.UserControl = true;
    }
}
