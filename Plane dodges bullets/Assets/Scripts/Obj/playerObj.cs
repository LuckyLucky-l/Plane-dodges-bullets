using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerObj : MonoBehaviour
{
    private static playerObj instance;
    public static playerObj Instance => instance;
    private void Awake()
    {
        instance = this;
    }
    public int nowHp;
    public int maxHp;
    public int moveSpeed;
    public int rotionSpeed;
    public bool isDead;//判断是不是已经死亡

    public Camera playerC;

    //接收传过来的wsad值
    private float hValue;
    private float vValue;

    public Vector3 nowpos;
    private Vector3 POS;
    private Quaternion target;
    public void Wound()
    {
        
        if (isDead)
        {

            return;
        }
        nowHp -= 1;
        GamePanel.Instance.changHp(nowHp);
        if (nowHp<=0)//如果血量见底
        {
            Dead();
        }
    }
    public void Dead()
    {
        isDead = true;
        GameoverPanel.Instance.ShowMe();
    }
    private void Update()
    {
        hValue = Input.GetAxisRaw("Horizontal");
        vValue = Input.GetAxisRaw("Vertical");
        if (hValue==0)
        {
            target = Quaternion.identity;
        }
        else
        {
            target =hValue<0? Quaternion.AngleAxis(20, Vector3.forward): Quaternion.AngleAxis(-20, Vector3.forward);
        }
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,target,rotionSpeed*Time.deltaTime);

        //记录位置
        POS = this.transform.position;
        //前后移动
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical"));
        //左右移动
        this.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal"),Space.World);

        //限制范围
        nowpos =playerC.WorldToScreenPoint(this.transform.position);
        //左右屏幕极限判断
        if (nowpos.x<=0 ||nowpos.x>Screen.width)
        {
            this.transform.position=new Vector3(POS.x,this.transform.position.y, this.transform.position.z) ;
        }
        if (nowpos.y<=0||nowpos.y>Screen.height)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, POS.z);
        }
        //射线检测，点爆子弹
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;//存射线信息
            if (Physics.Raycast(playerC.ScreenPointToRay(Input.mousePosition),out hitInfo,1000,1<<LayerMask.NameToLayer("buttle")))
            {
                //得到检测物体的脚本
                buttleObj buttle = hitInfo.transform.GetComponent<buttleObj>();
                buttle.Delate();
                buttle.DeadEff();
            }
        }
    }
}
