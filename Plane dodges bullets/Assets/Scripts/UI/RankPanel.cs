using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
{
    public UIButton btnQuit;
    //排行单条控件信息
    private List<RankDataOnes> itemRank=new List<RankDataOnes>();
    public Transform scrollView;//显示区域
    public override void Init()
    {
        btnQuit.onClick.Add(new EventDelegate(() => 
        {
            HideMe();
        }));
        HideMe();
    }
    public override void ShowMe()
    {
        base.ShowMe();
        //先把数据取出来
        List<RankDataOne> rank= GameDataMgr.Instance.rankData.list;
        for (int i = 0; i < rank.Count; i++)
        {
            //有控件就初始化
            if (itemRank.Count>i)
            {
                itemRank[i].setInfo(i + 1, rank[i].name, rank[i].time);
            }
            //没有控件就创建控件
            else
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("UI/rankList"));
                //设置父对象
                obj.transform.SetParent(scrollView, false);
                obj.transform.localPosition = new Vector3(-70, 500-60*i, 0);
                //实例化对象就可以得到身上挂载的脚本
                RankDataOnes item = obj.GetComponent<RankDataOnes>();
                item.setInfo(i + 1, rank[i].name, rank[i].time);
            }
        }
    }
}
