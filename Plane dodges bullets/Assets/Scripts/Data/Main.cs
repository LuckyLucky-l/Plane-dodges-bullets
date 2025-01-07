using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private void Start()
    {
        //实例化飞机
        RoleInfo roleInfo = GameDataMgr.Instance.nowIndexs();
        GameObject plane = Instantiate(Resources.Load<GameObject>(roleInfo.rPos));
        //挂载飞机脚本
        playerObj player = plane.AddComponent<playerObj>();
        player.playerC = GameObject.Find("CameraPlayer").GetComponent<Camera>();
        player.moveSpeed =roleInfo.Speed;
        player.rotionSpeed =20;
        player.nowHp = roleInfo.Hp;
        player.maxHp = 10;
        //跟新面板
        GamePanel.Instance.changHp(roleInfo.Hp);
    }
}
