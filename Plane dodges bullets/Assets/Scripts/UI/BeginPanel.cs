using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginPanel : BasePanel<BeginPanel>
{
    public UIButton btnStart;
    public UIButton btnRank;
    public UIButton btnSetting;
    public UIButton btnQuit;
    public override void Init()
    {
        btnStart.onClick.Add(new EventDelegate(()=> 
        {
            //开始界面
            choosePanel.Instance.ShowMe();
            HideMe();
        }));
        btnRank.onClick.Add(new EventDelegate(() =>
        {
            //排行榜界面
            RankPanel.Instance.ShowMe();
        }));
        btnSetting.onClick.Add(new EventDelegate(() =>
        {
            //设置界面
            SettingPanel.Instance.ShowMe();
        }));
        btnQuit.onClick.Add(new EventDelegate(() =>
        {
            //退出界面
            Application.Quit();
        }));
    }
}
