using System.Text.RegularExpressions;
using UnityEngine.UI;

public static class TxtTool
{
    /// <summary>
    /// Text 组件 采用 BestFit ，且 Horizontal Overflow 采用 Warp
    /// </summary>
    /// <param name="text"></param>
    /// <param name="rowNum"></param>
    public static void TextBestFit(this Text text, int rowNum)
    {
        char[] words = text.text.ToCharArray();
        //正则 ：判断单个字符是否是中文
        Regex chineseRegex = new Regex("^[\u4E00-\u9FA5]+$");
        int count = 0;
        string resultStr = string.Empty;
        for (int i = 0; i < words.Length; i++)
        {
            string str = words[i].ToString();
            if (chineseRegex.IsMatch(str))  
            {
                count += 2; //是中文，占两个字符
            }
            else
            {
                count += 1;
            }
           
            if (count > rowNum)
            {
                resultStr += "\n";
                count = 0;
            }
            resultStr += str;
        }
        text.text = resultStr;
    }

}
