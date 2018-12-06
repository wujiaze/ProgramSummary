using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropdownExpand : Dropdown
{
    public bool m_AlwaysCallback =false;
    public new void Show()
    {
        base.Show();
        // 当显示下拉列表之后
        Transform toggleRoot = transform.Find("Dropdown List/Viewport/Content");
        Toggle[] toggleList = toggleRoot.GetComponentsInChildren<Toggle>(false);
        for (int i = 0; i < toggleList.Length; i++)
        {
            Toggle temp = toggleList[i];
            temp.onValueChanged.RemoveAllListeners();
            temp.isOn = false;
            temp.onValueChanged.AddListener(x => OnSelectItemEx(temp));
        }
    }
    public void OnSelectItemEx(Toggle toggle)
    {
        if (!toggle.isOn)
        {
            toggle.isOn = true;
            return;
        }

        int selectedIndex = -1;
        Transform tr = toggle.transform;
        Transform parent = tr.parent;
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i) == tr)
            {
                //因为第一个是不显示的模板Item
                selectedIndex = i - 1;
                break;
            }
        }

        if (selectedIndex < 0)
            return;
        if (value == selectedIndex && m_AlwaysCallback) // 当m_AlwaysCallback =true时，手动调用onValueChanged
            onValueChanged.Invoke(value);
        else
            value = selectedIndex;
        Hide();
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        Show();
    }
}
