using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class csvController
{

    static csvController csv;
    public List<string[]> arrayData;

    private csvController()   //���������췽��Ϊ˽��
    {
        arrayData = new List<string[]>();
    }

    public static csvController GetInstance()   //����������ȡ����
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
            string file_url = path + "//" + fileName;  //����·�����ļ�
            sr = File.OpenText(file_url);
            Debug.Log("File Find in " + file_url);
        }
        catch
        {
            Debug.Log("File cannot find ! ");
            return;
        }

        string line;
        while ((line = sr.ReadLine()) != null)   //���ж�ȡ
        {
            arrayData.Add(line.Split(','));   //ÿ�ж��ŷָ�,split()�������� string[]
        }
        sr.Close();
        sr.Dispose();
    }

    public string getString(int row, int col)
    {
        return arrayData[row][col];
    }
    public int getInt(int row, int col)
    {
        return int.Parse(arrayData[row][col]);
    }
}