using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public class Confighelper : MonoBehaviour
{
    private string _configPath;
    private void Awake()
    {
        InitField();
        ReadConfig();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitField()
    {
        _configPath = Application.streamingAssetsPath + @"/config.ini";
        Debug.Log(_configPath);
    }

    private void ReadConfig()
    {
        Consts.Height = int.Parse(ReadIniData("config", "width", "500", _configPath));
        Consts.Width = int.Parse(ReadIniData("config", "height", "500", _configPath));
    }

    public static string ReadIniData(string section, string key, string def, string filePath)
    {
        if (File.Exists(filePath))
        {
            StringBuilder retVal = new StringBuilder();
            GetPrivateProfileString(section, key, def, retVal, 1024, filePath);
            return retVal.ToString();
        }
        return string.Empty;
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
}
