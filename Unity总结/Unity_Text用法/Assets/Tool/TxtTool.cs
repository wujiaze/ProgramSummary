using System.Text.RegularExpressions;
using UnityEngine.UI;

public static class TxtTool
{
    /// <summary>
    /// Text 组件 采用 BestFit ，且 Horizontal Overflow 采用 Warp
    /// </summary>
    /// <param name="text"></param>
    /// <param name="rowMax"></param>
    public static void TextBestFit(this Text text, int rowMax)
    {
        char[] words = text.text.ToCharArray();
        //正则
        Regex isChinese = new Regex("^[\u4E00-\u9FA5]+$");
        int count = 0;
        string res = string.Empty;
        for (int i = 0; i < words.Length; i++)
        {
            string str = words[i].ToString();
            if (isChinese.IsMatch(str))//是中文，算两个字
            {
                count += 2;
            }
            else
            {
                count += 1;
            }
            res += str;
            if (count > rowMax && i != words.Length - 1)
            {
                res += "\n";
                count = 0;
            }
        }
        text.text = res;
    }

}
