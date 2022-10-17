using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RootMotion.Dynamics;

public class Bullet : MonoBehaviour
{
    Vector3 targetPosition;
    float speed;
    public Action<Bullet> CompleteEvent;
    public Action<Bullet,ZombieController> HitEvent;
    public virtual void Init(Vector3 targetPos,float speed) {
        targetPosition = targetPos;
        this.speed = speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root.tag == "Zombie")
        {
            //ZombieController zombie = ;
            //if (zombie.hp <= 0) return;
            //speed = 0;
            //zombie.Hit(hurtValue);
            HitEvent?.Invoke(this, collision.transform.root.GetComponent<ZombieController>());
        }
    }
    //����ǹ�ں�ʵ����׼ǹ����һ�����룬�����ӵ�����������ʵ����׼ǹ��λ���ƶ���ǰ�����밴�����ٶȽ���
    void Update()
    {
        Vector3 position = transform.position;
        if (Vector3.Distance(position, targetPosition) < 0.1f) {
            speed = 0;
            CompleteEvent?.Invoke(this);
            return;
        }
        position = Vector3.MoveTowards(position, targetPosition, speed * Time.deltaTime);
        transform.position = position;
    }
}
