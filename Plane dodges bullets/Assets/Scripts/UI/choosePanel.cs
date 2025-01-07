using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class choosePanel : BasePanel<choosePanel>
{
    public UIButton btnClose;
    public UIButton btnLeft;
    public UIButton btnRight;
    public UIButton btnStart;
    private List<GameObject> Hp=new List<GameObject>();
    private List<GameObject> speed=new List<GameObject>();
    private List<GameObject> volume=new List<GameObject>();

    private GameObject nowplane;//当前选中的飞机
    public Transform Hero;

    private float time;
    public Camera uiCamera;
    public override void Start()
    {
        for (int i = 1; i <= 10; i++)
        {
            //因为gamobject不是组件所以不能GetComponent获取得到Gamobject
            Transform Ts = this.transform.Find("Info/Hp/spr" + i + "/for");
            Transform Ts2 = this.transform.Find("Info/Speed/spr" + i + "/for");
            Transform Ts3 = this.transform.Find("Info/Volume/spr" + i + "/for");
            //transform.gameObject可以返回与该 Transform 相关联的 GameObject 对象。
            Hp.Add(Ts.gameObject);
            speed.Add(Ts2.gameObject);
            volume.Add(Ts3.gameObject);
        }
        base.Start();
    }
    public override void Init()
    {
        btnClose.onClick.Add(new EventDelegate(() => 
        {
            BeginPanel.Instance.ShowMe();
            HideMe();
            OnDelate();
        }));   
        btnLeft.onClick.Add(new EventDelegate(() => 
        {
            --GameDataMgr.Instance.nowIndex;
            if (GameDataMgr.Instance.nowIndex<0)
            {
                GameDataMgr.Instance.nowIndex=GameDataMgr.Instance.roleData.roles.Count-1;
            }
            OnDelate();
            changeplane();
        }));
        btnRight.onClick.Add(new EventDelegate(() =>
        {
            ++GameDataMgr.Instance.nowIndex;
            if (GameDataMgr.Instance.nowIndex > GameDataMgr.Instance.roleData.roles.Count - 1)
            {
                GameDataMgr.Instance.nowIndex = 0;
            }
            OnDelate();
            changeplane();
        }));
        btnStart.onClick.Add(new EventDelegate(() => 
        {
            SceneManager.LoadScene("GameScence");
        }));
        HideMe();
    }
    public override void ShowMe()
    {
        base.ShowMe();
        GameDataMgr.Instance.nowIndex = 0;
        changeplane();
    }
    public void OnDelate()
    {
        if (nowplane!=null)
        {
            Destroy(nowplane);
        }
    }
    private void changeplane()
    {
        //得到飞机数据
        RoleInfo roleInfo = GameDataMgr.Instance.nowIndexs();
        //创建飞机
        nowplane = Instantiate(Resources.Load<GameObject>(roleInfo.rPos));
        nowplane.transform.SetParent(Hero,false);
        nowplane.layer = LayerMask.NameToLayer("UI");
        nowplane.transform.localPosition = new Vector3(60f, 54f, 198);
        nowplane.transform.localRotation =Quaternion.Euler(new Vector3(20f,-146f,0f));
        nowplane.transform.localScale =Vector3.one*roleInfo.scale;
        nowplane.SetActive(true);
        //更新属性
        for (int i = 0; i < 10; i++)
        {
            //血量是4 前三个就都是true，后面是false
            Hp[i].SetActive(i<roleInfo.Hp);
            speed[i].SetActive(i < roleInfo.Speed);
            volume[i].SetActive(i < roleInfo.volume);
        }
    }
    public bool istrue;
    private void Update()
    {
        time+=Time.deltaTime;
        nowplane.transform.Translate(Vector3.up*Time.deltaTime*Mathf.Sin(time)*0.02f);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(uiCamera.ScreenPointToRay(Input.mousePosition),1000,1<<LayerMask.NameToLayer("UI")))
            {
                istrue = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            istrue = false;
        }
        if (Input.GetMouseButton(0) && istrue)
        {
            nowplane.transform.rotation*=Quaternion.AngleAxis(-Input.GetAxis("Mouse X")*20,Vector3.up);
        }
    }
}
