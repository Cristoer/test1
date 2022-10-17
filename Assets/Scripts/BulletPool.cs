using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BulletPool : ScriptableObject
{
    [SerializeField] Bullet bullet;
    private Stack<Bullet> bulletStack;

    public void ClearPool() {
        bulletStack?.Clear();
    }
    public Bullet CreateBullet() {
        if (bulletStack == null) bulletStack = new Stack<Bullet>();
        if (bulletStack.Count > 0)
        {
            Bullet bullet = bulletStack.Pop();
            bullet.gameObject.SetActive(true);
            return bullet;
        }
        else {
           return Instantiate(bullet);
        }
    }
    public void ReclaimBullet(Bullet bullet) {
        bullet.CompleteEvent = null;
        bullet.HitEvent = null;
        bullet.gameObject.SetActive(false);
        bulletStack.Push(bullet);
    }
}
