using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] BulletPool bulletPool;
    [SerializeField] Transform gunPoint;
    [SerializeField] Transform firePoint;
    [SerializeField] ParticleSystem blood;
    [SerializeField] GameConfig gameConfig;
    GameConfig.WeaponConfig weaponConfig;
    Transform currentGun;
    private void Start()
    {
        bulletPool.ClearPool();
    }
    public void ChangeGun(int gunIndex) {
        if (gunIndex >= transform.childCount) {
            Debug.LogError("current gunIndex more than childCount ");
            return;
        }
        currentGun?.gameObject.SetActive(false);
        currentGun = transform.GetChild(gunIndex);
        currentGun.gameObject.SetActive(true);
        weaponConfig = gameConfig.gunWeaponConfigs[gunIndex];
    }
    public void Fire() {
        Bullet bullet = bulletPool.CreateBullet();
        bullet.transform.position = gunPoint.position;
        bullet.Init(firePoint.position + firePoint.forward * weaponConfig.fireRange, weaponConfig.speed);
        bullet.CompleteEvent += BulletComplete;
        bullet.HitEvent += Hit;
    }

    void BulletComplete(Bullet bullet) {
        bulletPool.ReclaimBullet(bullet);
    }
    void Hit(Bullet bullet,ZombieController zombie)
    {
        if (zombie.hp <= 0) return;
        zombie.Hit(weaponConfig.hurtValue);
        blood.transform.position = bullet.transform.position;
        blood.transform.rotation = Quaternion.LookRotation(-transform.forward);
        blood.Emit(5);
        bulletPool.ReclaimBullet(bullet);
    }

}
