using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttleObj : MonoBehaviour
{
    public ButtleInfo buttle;//存储子弹
    //子弹的行为
    //初始化创建子弹
    public void creatButtle(ButtleInfo buttle)
    {
        this.buttle = buttle;
        Invoke("Delate",buttle.lifeTime);
    }
    public void Delate()
    {
        Destroy(this.gameObject);
    }
    //延迟销毁 死亡特效
    public void DeadEff()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>(buttle.deadEff));
        GameObject objSound= Instantiate(Resources.Load<GameObject>("Sound/BkSound"));
        Destroy(objSound, 2);
        obj.transform.position = this.transform.position;
        Destroy(obj, 1);
        Delate();
    }
    //碰到玩家，玩家受伤
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerObj obj = other.gameObject.GetComponent<playerObj>();
            obj.Wound();
            DeadEff();
            Delate();
        }
    }
    private float time;
    private void Update()
    {
        //子弹的移动
        //直线
        //曲线
        //左抛物线
        //右抛物线
        //跟随玩家
        this.transform.Translate(Vector3.forward * buttle.ZmoveSpeed * Time.deltaTime); //直线
        switch (buttle.type)
        {
            case 2:
                time += Time.deltaTime;
                this.transform.Translate(Vector3.right * buttle.XmoveSpeed * Time.deltaTime *
                                            Mathf.Sin(time * buttle.rotateSpeed));
                break;
            case 3:
                this.transform.rotation *= Quaternion.AngleAxis(buttle.rotateSpeed * Time.deltaTime, Vector3.up);
                break;
            case 4:
                this.transform.rotation *= Quaternion.AngleAxis(-buttle.rotateSpeed * Time.deltaTime, Vector3.up);
                break;
            case 5:
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation
                                                            (playerObj.Instance.transform.position - this.transform.position),
                                                            buttle.rotateSpeed * Time.deltaTime);
                break;
        }
    }
}
