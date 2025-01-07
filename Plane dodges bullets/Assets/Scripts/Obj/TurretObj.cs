using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Turret
{
    TopLift,
    Top,
    TopRight,
    Left,
    Right,
    BottomLeft,
    Bottom,
    BottomRight
}
public class TurretObj : MonoBehaviour
{
    public Camera palyerCamer;
    public Turret type;
    private Vector3 nowPos;
    private Vector3 Direction;

    private float changAngle;
    private ButtleInfo buttleInfo;
    private Vector3 nowDir;//上一次发射方向
    /// <summary>
    /// 存储发射数据
    /// </summary>
    private TurreInfo turreInfo;
    private float nowCd;
    private int nowNum;
    private float nowDelay;
    private void Update()
    {
        UpdatePos();
        ResetTurret();
        DetectFire();
    }
    private void UpdatePos()
    {
        nowPos.z =palyerCamer.WorldToScreenPoint(playerObj.Instance.transform.position).z;
        switch (type)
        {
            case Turret.TopLift:
                nowPos.x = 0;
                nowPos.y = Screen.height;
                Direction = Vector3.right;
                break;
            case Turret.Top:
                nowPos.x = Screen.width/2;
                nowPos.y = Screen.height;
                Direction = Vector3.right;
                break;
            case Turret.TopRight:
                nowPos.x = Screen.width;
                nowPos.y = Screen.height;
                Direction = Vector3.left;
                break;
            case Turret.Left:
                nowPos.x = 0;
                nowPos.y = Screen.height / 2;
                Direction = Vector3.right;
                break;
            case Turret.Right:
                nowPos.x = Screen.width;
                nowPos.y = Screen.height / 2;
                Direction = Vector3.left;
                break;
            case Turret.BottomLeft:
                nowPos.x = 0;
                nowPos.y = 0;
                Direction = Vector3.right;
                break;
            case Turret.Bottom:
                nowPos.x = Screen.width / 2;
                nowPos.y = 0;
                Direction = Vector3.right;
                break;
            case Turret.BottomRight:
                nowPos.x = Screen.width;
                nowPos.y = 0;
                Direction = Vector3.left;
                break;
        }
        this.transform.position = palyerCamer.ScreenToWorldPoint(nowPos);
    }
    private void ResetTurret()
    {
        if (nowCd!=0&&nowNum!=0)
        {
            return;
        }
        if (turreInfo!=null)
        {
            nowDelay -= Time.deltaTime;
            if (nowDelay>0)
            {
                return;
            }
        }
        //随机取出一个炮塔数据
        List<TurreInfo> turreInfos = GameDataMgr.Instance.turretData.turreInfos;
        turreInfo = turreInfos[Random.Range(0, turreInfos.Count)];
        nowCd= turreInfo.cd;
        nowNum = turreInfo.num;
        nowDelay = turreInfo.delay;

        //根据开火数据，随机取出子弹
        string[] str = turreInfo.ids.Split(',');
        int beginID = int.Parse(str[0]);
        int endID = int.Parse(str[1]);
        int randomID = Random.Range(beginID, endID + 1);
        //随机取出子弹
        buttleInfo = GameDataMgr.Instance.buttleData.buttles[randomID - 1];
        switch (type)//散弹角度数据
        {
            case Turret.Top:
            case Turret.Left:
            case Turret.Right:
            case Turret.Bottom:
                changAngle = 180f / (turreInfo.num+1);
                break;
            case Turret.TopLift:        
            case Turret.TopRight:
            case Turret.BottomLeft:
            case Turret.BottomRight:
                changAngle = 90f / (turreInfo.num+1);
                break;
        }
    }
    private void DetectFire()
    {
        if (nowCd==0&&nowNum==0)
        {
            return;
        }
        nowCd-=Time.deltaTime;
        if (nowCd>0)
        {
            return;
        }
        GameObject obj;
        buttleObj objButtle;
        switch (turreInfo.type)
        {
            case 1://顺序发射
                obj = Instantiate(Resources.Load<GameObject>(buttleInfo.resName));
                objButtle = obj.AddComponent<buttleObj>();//得到子弹脚本
                objButtle.creatButtle(buttleInfo);//初始化子弹数据
                obj.transform.position = this.transform.position;
                obj.transform.rotation = Quaternion.LookRotation(playerObj.Instance.transform.position-
                                                                this.transform.position);
                --nowNum;//子弹减1
                nowCd = nowNum == 0 ? 0 : turreInfo.cd;//只有子弹数量为0才将cd致0
                break;
            case 2://散弹发射
                //散弹有两种情况，有cd和无cd
                if (nowCd==0)
                {
                    print("12");
                    for (int i = 0; i < nowNum; i++)
                    {
                        obj = Instantiate(Resources.Load<GameObject>(buttleInfo.resName));
                        objButtle = obj.AddComponent<buttleObj>();
                        objButtle.creatButtle(buttleInfo);
                        obj.transform.position = this.transform.position;
                        nowDir= Quaternion.AngleAxis(changAngle*i,Vector3.up) * Direction;
                        obj.transform.rotation = Quaternion.LookRotation(nowDir);
                    }
                    nowCd = nowNum = 0;
                }
                else
                {
                    obj = Instantiate(Resources.Load<GameObject>(buttleInfo.resName));
                    objButtle = obj.AddComponent<buttleObj>();
                    objButtle.creatButtle(buttleInfo);
                    obj.transform.position = this.transform.position;
                    nowDir = Quaternion.AngleAxis(changAngle*turreInfo.num-nowNum, Vector3.up) * Direction;
                    obj.transform.rotation = Quaternion.LookRotation(nowDir);
                    --nowNum;
                    nowCd = nowNum == 0 ? 0 : turreInfo.cd;
                }
                break;
        }
    }
}
