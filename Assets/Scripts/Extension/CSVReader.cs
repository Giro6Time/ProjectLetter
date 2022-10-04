using System.Text;
using System.IO;
using UnityEngine;

public static class CSVReader 
{
    // 文件路径xxx/项目名字/Asset
    public static string AssetPath { get => Application.dataPath; }
    /// <summary>
    /// 读取一整个表格，注意csv文件的尾部不要有空行，否则会多读一行
    /// </summary>
    /// <param name="localPath">Asset之后的路径</param>
    /// <returns>string的二维数组</returns>
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
    /// 读取一行文本，并且split(',')
    /// </summary>
    /// <param name="localPath">Asset之后的路径</param>
    /// <returns>split后的字符串数组</returns>
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
