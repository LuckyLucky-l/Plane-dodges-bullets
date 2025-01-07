using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : BasePanel<SettingPanel>
{
    public UIToggle MusicToogle;
    public UIToggle SoundToogle;
    public UISlider MusicSlider;
    public UISlider SoundSlider;
    public UIButton btnClose;
    public override void Init()
    {
        MusicToogle.onChange.Add(new EventDelegate(() => 
        {
            //加载音乐
            GameDataMgr.Instance.setMusicIsOpen(MusicToogle.value);
        }));
        SoundToogle.onChange.Add(new EventDelegate(() =>
        {
            GameDataMgr.Instance.setSoundIsOpen(SoundToogle.value);
            //打开关闭音效
        }));
        MusicSlider.onChange.Add(new EventDelegate(() => 
        {
            //改变音乐大小
            GameDataMgr.Instance.changMusicValue(MusicSlider.value);
        }));
        SoundSlider.onChange.Add(new EventDelegate(() =>
        {
            //改变音效大小
            GameDataMgr.Instance.changSoundValue(SoundSlider.value);
        }));
        btnClose.onClick.Add(new EventDelegate(() =>
        {
            HideMe();
        }));
        HideMe();
    }
    public override void ShowMe()
    {
        base.ShowMe();
        //显示自己时更新面板内容
        MusicSlider.value = GameDataMgr.Instance.musicData.changMusic;
        SoundSlider.value = GameDataMgr.Instance.musicData.changSound;
        MusicToogle.value = GameDataMgr.Instance.musicData.isMusicOpen;
        SoundToogle.value = GameDataMgr.Instance.musicData.isSoundOpen;
    }
    public override void HideMe()
    {
        base.HideMe();
        //隐藏自己时保存面板设置
        GameDataMgr.Instance.SaveDataMusic();
    }
}
