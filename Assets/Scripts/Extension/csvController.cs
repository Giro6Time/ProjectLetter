using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class csvController
{

    static csvController csv;
    public List<string[]> arrayData;

    private csvController()   //单例，构造方法为私有
    {
        arrayData = new List<string[]>();
    }

    public static csvController GetInstance()   //单例方法获取对象
    {
        if (csv == null)
        {
            csv = new csvController();
        }
        return csv;
    }

    public void loadFile(string path, string fileName)
    {
        arrayData.Clear();
        StreamReader sr = null;
        try
        {
            string file_url = path + "/" + fileName;  //根据路径打开文件
            sr = File.OpenText(file_url);
            //Debug.Log("File Find in " + file_url);
        }
        catch
        {
            Debug.Log("File cannot find ! Input url:" + path + "/" + fileName);
            return;
        }

        string line;
        while ((line = sr.ReadLine()) != null)   //按行读取
        {
            arrayData.Add(line.Split(','));   //每行逗号分隔,split()方法返回 string[]
        }
        sr.Close();
        sr.Dispose();
    }

    public string getString(int row, int col)
    {

        string ret;
        try
        {
            ret = arrayData[row][col];
        }
        catch
        {
            Debug.Log("OutOfIndex! Index:"+row+","+col);
            throw;
        }
        return ret;
    }
    public int getInt(int row, int col)
    {
        int ret;
        try
        {
            ret = int.Parse(arrayData[row][col]);
        }
        catch 
        {
            Debug.Log("OutOfIndex!Index:" + row + "," + col);
            throw;
        }
        return ret;
    }
    public float getFloat(int row, int col)
    {
        float ret;
        try
        {
            ret = float.Parse(arrayData[row][col]);
        }
        catch
        {
            Debug.Log("OutOfIndex!Index:" + row + "," + col);
            throw;
        }
        return ret;
    }

    public int getRowLength(int row)
    {
        for(int i=0;i< arrayData[row].Length; i++)
            if (arrayData[row][i] == "") 
                return i;
        return arrayData[row].Length;
    }
}