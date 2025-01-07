using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GamePanel : BasePanel<GamePanel>
{
    public UILabel time;
    public UIButton btnReturn;
    private float t;
    [HideInInspector]
    public int nowTime;
    [HideInInspector]
    public string str;
    public List<GameObject> sprHp;
    public override void Start()
    {
        for (int i = 1; i <= 10; i++)
        {
            Transform t = this.transform.Find("Hp/spr" + i+"/for");
            sprHp.Add(t.gameObject);
        }
        base.Start();
    }

    public override void Init()
    {
        btnReturn.onClick.Add(new EventDelegate(() => 
        {
            ExitPanel.Instance.ShowMe();
            Time.timeScale = 0;
        }));
    }
    public void changHp(int hp)
    {
        for (int i = 0; i < sprHp.Count; i++)
        {
            sprHp[i].SetActive(i<hp);
        }
    }
    private void Update()
    {
        t +=Time.deltaTime;
        nowTime = (int)t;
        str = "";
        if (nowTime/3600>0)
        {
            str += nowTime/3600 + "h";
        }
        if (nowTime%3600/60>0 || str!="")
        {
            str += nowTime % 3600 / 60 + "f";
        }
        str += nowTime%60+ "s";
        time.text =str.ToString();
    }
}
