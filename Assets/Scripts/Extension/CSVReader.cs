using System.Text;
using System.IO;
using UnityEngine;

public static class CSVReader 
{
    // �ļ�·��xxx/��Ŀ����/Asset
    public static string AssetPath { get => Application.dataPath; }
    /// <summary>
    /// ��ȡһ�������ע��csv�ļ���β����Ҫ�п��У��������һ��
    /// </summary>
    /// <param name="localPath">Asset֮���·��</param>
    /// <returns>string�Ķ�ά����</returns>
    public static string[][] ReadTable(string localPath)
    {
        string path = AssetPath + localPath;
        string tmp = File.ReadAllText(path, Encoding.UTF8);
        tmp.Replace('\r', '\0');
        string[] Lines = tmp.Split('\n');
        string[][] ret = new string[Lines.Length][];
        for(int i = 0; i < Lines.Length; i++)
        {
            ret[i] = Lines[i].Split(',');
        }

        return ret;
    }
    /// <summary>
    /// ��ȡһ���ı�������split(',')
    /// </summary>
    /// <param name="localPath">Asset֮���·��</param>
    /// <returns>split����ַ�������</returns>
    public static string[] ReadLine(string localPath)
    {
        string path = AssetPath + localPath;
        string tmp = File.ReadAllText(path,Encoding.UTF8);
        tmp.Replace("\r", "");
        tmp.Replace("\n", "");
        string[] ret = tmp.Split(',');
        
        return ret;
    }
}
