using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameoverPanel : BasePanel<GameoverPanel>
{
    public UILabel labTime;
    public UIInput input;
    public UIButton btnOK;
    public override void Init()
    {
        btnOK.onClick.Add(new EventDelegate(() =>
        {
            GameDataMgr.Instance.SaveRank(input.value, GamePanel.Instance.nowTime);
            Time.timeScale = 1;            SceneManager.LoadScene("BeginScence");
            HideMe();
        }));
        HideMe();
    }
    public override void ShowMe()
    {
        base.ShowMe();
        Time.timeScale = 0;
        labTime.text = GamePanel.Instance.str;
    }
}
