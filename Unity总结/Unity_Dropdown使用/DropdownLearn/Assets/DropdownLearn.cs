/*
 *   Dropdown学习
 *      一、结构
 *          Dropdown：由于默认的 Dropdown 没有添加选项背景图片，所以若需要添加选项背景图片的话需要自己添加Image组件，并作为Dropdown第一个位置的子对象，然后拖入Dropdown的Caption Image
 *          Image：只保存当前选项的图片内容(自己添加的)，所以若是选项的其他(颜色，字体大小等等都需要手动赋值) 
 *          Label：只保存当前选项的文字内容，所以若是选项的其他(颜色，字体大小等等都需要手动赋值) 
 *          Template：模板
 *                    Item 表示每一个选项的模板
 *                         Item Image 每一个选项的图片内容(自己添加的，原因同第一条一样),并且拖入 Dropdown 的 Item Image
 *                         Item Label 每一个选项的文字内容
 *      三、属性和方法(主要的)
 *          属性：
 *              captionImage    只保存当前选项的图片内容
 *              captionText     只保存当前选项的文字内容
 *              itemImage       保存选项的图片内容
 *              itemText        保存选项的文字内容
 *              options         选项列表
 *              templeta        模板
 *              value           选项的索引
 *              onValueChanged  索引值改变的回调
 *          方法：
 *              AddOptions          添加下拉选项列表
 *              ClearOptions        清空选项列表
 *              Show/Hide           显示/隐藏选项列表
 *              RefreshShownValue   刷新选项列表
 *      四、应用
 *          1、纯文本及颜色修改(itemText 和 captionText)
 *          2、文本+图片
 *          3、修改 Dropdown 的大小
 *      五、注意点
 *          当下拉框超过 Canvas的区域时，下拉列表的显示会自动反向，这是UI内部的设置
 */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownLearn : MonoBehaviour
{
    public Sprite Sprite;
    private Dropdown _dropdownDefault;
    private Dropdown _dropdownImage;
    private Dropdown _dropdownSize;

    private void Awake()
    {
        _dropdownDefault = transform.Find("Dropdown默认的下拉组件").GetComponent<Dropdown>();
        _dropdownImage = transform.Find("Dropdown添加了Image").GetComponent<Dropdown>();
        _dropdownSize = transform.Find("DropdownSize").GetComponent<Dropdown>();
    }

    void Start ()
    {
        DropdownApply1();
        DropdownApply2();
        DropdownApply3();
    }

    private List<Dropdown.OptionData> _list1;
    /// <summary>
    /// 应用1,添加纯文本选项
    /// </summary>
    private void DropdownApply1()
    {
        // 第一步：清空默认的选项列表
        _dropdownDefault.ClearOptions();
        // 第二步：添加自己的选项列表  Dropdown.OptionData 是一个类中的内部类
        Dropdown.OptionData tempdate1 = new Dropdown.OptionData("A");
        Dropdown.OptionData tempdate2 = new Dropdown.OptionData("B");
        Dropdown.OptionData tempdate3 = new Dropdown.OptionData("C");
        _list1 = new List<Dropdown.OptionData>(){ tempdate1 , tempdate2 , tempdate3 };
        _dropdownDefault.AddOptions(_list1);
        // 第三步(可选),下列方法是 统一修改选项的颜色和字体大小
        Text txt = _dropdownDefault.transform.Find("Template/Viewport/Content/Item/Item Label").GetComponent<Text>();
        txt.fontSize = 5;
        txt.color = Color.red;
        // 第四步(可选),修改默认显示选项，默认是0，即选项列表的第一个
        _dropdownDefault.value = 1;
        // 添加选择回调事件
        _dropdownDefault.onValueChanged.AddListener(ChooseCallBack1);
    }

    private void ChooseCallBack1(int index)
    {
        
        switch (_list1[index].text)
        {
            case "A":
                // DropDown的显示文字和内容会自动改变，但是颜色字体需要手动修改
                _dropdownDefault.captionText.fontSize = 5;
                _dropdownDefault.captionText.color = Color.red;
                break;
            case "B":
                _dropdownDefault.captionText.fontSize = 14;
                _dropdownDefault.captionText.color = Color.black;
                break;
            case "C":
                _dropdownDefault.captionText.fontSize = 14;
                _dropdownDefault.captionText.color = Color.black;
                break;
        }

        
    }



    private List<Dropdown.OptionData> _list2;

    /// <summary>
    /// 应用2，添加文本+图片
    /// </summary>
    private void DropdownApply2()
    {
        // 第一步：清空默认的选项列表
        _dropdownImage.ClearOptions();
        // 第二步：添加自己的选项列表  Dropdown.OptionData 是一个类中的内部类
        Dropdown.OptionData tempdate1 = new Dropdown.OptionData("你");
        Dropdown.OptionData tempdate2 = new Dropdown.OptionData("我", Sprite);
        Dropdown.OptionData tempdate3 = new Dropdown.OptionData("他", Sprite);
        _list2 = new List<Dropdown.OptionData>() { tempdate1, tempdate2, tempdate3 };
        _dropdownImage.AddOptions(_list2);
    }

    /// <summary>
    /// 应用3，设置下拉框的大小
    /// </summary>
    private void DropdownApply3()
    {
        // 第一步 设置显示框的大小
        RectTransform rect = _dropdownSize.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(300, 100);
        // 第二步 设置Item模板框的大小
        RectTransform itemRect = rect.Find("Template/Viewport/Content/Item").GetComponent<RectTransform>();
        itemRect.sizeDelta = new Vector2(0, 100);
        // 第三步 给模板的Content添加 VerticalLayoutGroup
        RectTransform content = rect.Find("Template/Viewport/Content").GetComponent<RectTransform>();
        VerticalLayoutGroup layout = content.gameObject.AddComponent<VerticalLayoutGroup>();
        // 因为是竖直，所以这么设置
        layout.childControlWidth = true;
        layout.childForceExpandWidth = true;
        layout.childControlHeight = false;
        layout.childForceExpandHeight = false;
        // 第四步 给模板的Content添加 ContentSizeFitter
        ContentSizeFitter fitter = content.gameObject.AddComponent<ContentSizeFitter>();
        fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    }




}
