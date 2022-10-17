using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GunController gun;
    [SerializeField] GameConfig gameConfig;
    [SerializeField] GameController gameController;
    [SerializeField] CasterWeaponController casterWeapon;
    float _attackInterval;
    int _hp;
    float _lastAttackTime;
    float _hurtInvincibleTime;
    int _gunIndex;
    private void Start()
    {
     
        _hp = gameConfig.playerConfig.maxHp;
        _hurtInvincibleTime = gameConfig.playerConfig.InvincibleTime;
        gameController.UpdateHp(_hp);
        gameController.UsePropsEvent += OnUseProps;
        gameController.RelifeEvent += OnRelife;
        UseGun(0);
    }
    void UseGun(int gunIndex) {
        _gunIndex = gunIndex;
        gun.ChangeGun(_gunIndex);
        _attackInterval = gameConfig.gunWeaponConfigs[_gunIndex].attackInternal;
    }
    void OnRelife() {
        _hp = gameConfig.playerConfig.maxHp;
        gameController.UpdateHp(_hp);
    }
    void OnUseProps(PropsType propsType) {
        switch (propsType) {
            case PropsType.Medical:
                _hp=gameConfig.playerConfig.maxHp;
                gameController.UpdateHp(_hp);
                break;
            case PropsType.GunPro:
                CancelInvoke("ChangeNormalGun");
                UseGun(1);
                Invoke("ChangeNormalGun",gameConfig.propsConfigs[(int)propsType].useTime);
                break;
            case PropsType.Boom:
                casterWeapon.Caster();
                break;
        }
    }
    void ChangeNormalGun() {
        UseGun(0);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EndPoint") {
            gameController.PassGame();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.transform.root.tag) {
            case "Zombie":
                ZombieController zombie = other.transform.root.GetComponent<ZombieController>();
                if (zombie.hp > 0) {
                    zombie.Crash();
                    Hurt();
                }
             
                break;
            case "Medical":
            case "GunPro":
            case "Boom":
                if (gameController.AddProp((PropsType)System.Enum.Parse(typeof(PropsType),other.transform.root.tag))) {
                    Destroy(other.transform.root.gameObject);
                } ;
                break;
          
        }
    }
    float lastInvincibleTime;
    void Hurt() {
        if (Time.time - lastInvincibleTime < _hurtInvincibleTime) return;
        _hp--;
        lastInvincibleTime = Time.time;
        gameController.UpdateHp(_hp);
    }
    void Update()
    {
        if (gameController.gameState != GameState.Gaming) return;
        float now = Time.time;
        if (now - _lastAttackTime > _attackInterval)
        {
             gun.Fire();
            _lastAttackTime = now;
        }
    }

    private void OnDestroy()
    {
        gameController.UsePropsEvent -= OnUseProps;
        gameController.RelifeEvent -= OnRelife;
    }
}
