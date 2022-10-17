using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterWeaponController : MonoBehaviour
{
    [SerializeField] Bullet boom;
    [SerializeField] Transform firePoint;
    [SerializeField] GameConfig gameConfig;
    GameConfig.WeaponConfig weaponConfig;
    [SerializeField] ParticleSystem boomEffect;
    [SerializeField] GameController gameController;
    private void Start()
    {
        weaponConfig = gameConfig.casterWeaponConfigs[0];
    }
    public void Caster() {
        Bullet bullet = Instantiate(boom);
        bullet.transform.position = firePoint.position;
        bullet.Init(firePoint.position + firePoint.forward * weaponConfig.fireRange, weaponConfig.speed);
        bullet.CompleteEvent += BulletComplete;
        bullet.HitEvent += Hit;
    }

    void BulletComplete(Bullet bullet)
    {
        Boom(bullet);
    }
    void Hit(Bullet bullet, ZombieController zombie)
    {
        if (zombie.hp <= 0) return;
        Boom(bullet);
    }
    void Boom(Bullet bullet) {
        List<ZombieController> zombies = gameController.GetBoomBulletNearlyZombies(bullet.transform, weaponConfig.hurtRange);
        boomEffect.transform.position = bullet.transform.position;
        boomEffect.gameObject.SetActive(true);
        for (int i = 0; i < zombies.Count; i++)
        {
            zombies[i].Hit(weaponConfig.hurtValue);
        }
        Destroy(bullet.gameObject);
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0)) {
    //        gameController.AddProp(PropsType.Boom);
    //    }
    //}
}
