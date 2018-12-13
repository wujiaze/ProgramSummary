using System.Collections.Generic;
using SuperScrollView;
using UnityEngine;
using Random = UnityEngine.Random;

// 同样的方法也适合 纯文本
public class UiScrollViewTest : MonoBehaviour
{
    public List<Sprite> Sprite;
    public LoopListView2 listview;
    private string simulateDate = "你好";
    void Start()
    {
        // 步骤0：创建默认的ScrollView，添加 LoopListView2 组件
        // 步骤1：将预制体拖入 LoopListView2 中,只能通过将预设体拖入
        // 步骤2：初始化 LoopListView2 
        // -1：表示可以无限拖动   OnGetItemByIndex 表示对 Item 初始化数据时调用的方法
        // 10: 表示有限个
        listview.InitListView(-1, OnGetItemByIndex);
        //listview.InitListView(10, OnGetItemByIndex); 
    }
    private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
    {
        LoopListViewItem2 item = listView.NewListViewItem("Image");
        ImageBase itemScript = item.GetComponent<ImageBase>();
        if (item.IsInitHandlerCalled == false)
        {
            item.IsInitHandlerCalled = true;
            itemScript.Init(); // 初始化 itemScript，不改变的数据
        }
        int sss = Random.Range(0, 2);
        itemScript.SetImage(item,Sprite[sss], simulateDate); // 需修改的数据
        return item;
    }

    void Update()
    {
        // 改变数据
        if (Input.GetKeyDown(KeyCode.A))
        {
            simulateDate = "Hello";
            listview.RefreshAllShownItem();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            simulateDate = "你好";
            listview.RefreshAllShownItem();
        }
    }
}
