using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr : MonoBehaviour
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance => instance;
    public MusicData musicData;
    public RankData rankData;
    public RoleData roleData;
    public ButtleData buttleData;
    public TurretData turretData;
    //当前选择的索引
    public int nowIndex=0;
    private GameDataMgr()
    {
        //先把数据取出来
        musicData = XmlDataMgr.Instance.LoadData(typeof(MusicData), "MusicData") as MusicData;
        rankData = XmlDataMgr.Instance.LoadData(typeof(RankData), "RankData") as RankData;
        roleData = XmlDataMgr.Instance.LoadData(typeof(RoleData), "RoleData") as RoleData;
        buttleData = XmlDataMgr.Instance.LoadData(typeof(ButtleData), "ButtleData") as ButtleData;
        turretData = XmlDataMgr.Instance.LoadData(typeof(TurretData), "TurretData") as TurretData;
    }
    #region 排行榜管理
    //存储排行榜数据
   public void SaveRank(string name,int time)
    {
        RankDataOne rankDataOne = new RankDataOne();
        rankDataOne.name = name;
        rankDataOne.time = time;
        rankData.list.Add(rankDataOne);
        //排序
        rankData.list.Sort((a, b)=>
        {
            if (a.time>b.time)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        });

        if (rankData.list.Count>=20)
        {
            rankData.list.RemoveRange(20, rankData.list.Count - 20);
        }
        XmlDataMgr.Instance.SaveData(rankData, "RankData");
    }
    #endregion

    #region 音乐管理
    //存数据的类
    public void SaveDataMusic()
    {
        XmlDataMgr.Instance.SaveData(musicData, "MusicData");
    }
    //设置按钮的值
    public void setMusicIsOpen(bool isopen)//音乐开关
    {
        //改变数据
        musicData.isMusicOpen = isopen;
        BkMusic.Instance.ismuteMusic(musicData.isMusicOpen);
    }
    public void setSoundIsOpen(bool isopen)
    {
        musicData.isSoundOpen = isopen;
        print("123");
        BkSound.Instance.isOpenSound(musicData.isSoundOpen);
    }
    public void changMusicValue(float value)//音乐大小
    {
        //改变数据
        musicData.changMusic = value;
        BkMusic.Instance.changValueMusic(musicData.changMusic);
    }
    public void changSoundValue(float value)
    {
        musicData.changSound = value;
        BkSound.Instance.changeSound(musicData.changSound);
    }
    #endregion
    #region 返回当前索引对象
    public RoleInfo nowIndexs()
    {
        return roleData.roles[nowIndex];
    }
    #endregion

}
