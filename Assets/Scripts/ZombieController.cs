using RootMotion.Dynamics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ZombieType { 
    Normal,
    Doctor,
    Soldier
}
[System.Serializable]
public struct SkinArr {
   public  GameObject[] skins;
}
public class ZombieController : MonoBehaviour
{
    enum State { 
        Sleep,
        Active,
        Hurt,
        Dead
    }
    public System.Action DeadEvent;
    public System.Action<ZombieController> RemoveEvent;
    [SerializeField] SkinArr[] skins;
    [SerializeField] Transform target;
    [SerializeField] Animator animator;
    [SerializeField] GameConfig gameConfig;
    [SerializeField] PuppetMaster puppetMaster;
    [SerializeField] Rigidbody body;
    [SerializeField] Transform[] props;
    float _walkSpeed;
    float _activeDistance;
    float _hp;
   ZombieType _zombieType;
    State state = State.Sleep;
    public float hp => _hp;
    GameController gameController;
    public void Init(SpawnPosition spawnPosition,Transform target,GameController gameController) {
        //³õÊ¼»°½©Ê¬Æ¤·ô
        this.target = target;
        _zombieType = spawnPosition.zombieType;
        transform.position = spawnPosition.transform.position;
        int intZombieType = (int)_zombieType;
        skins[intZombieType].skins[Random.Range(0,skins[intZombieType].skins.Length)].SetActive(true);
        transform.LookAt(target);
        _walkSpeed = gameConfig.zombieConfig.walkSpeed;
        _activeDistance = gameConfig.zombieConfig.activeDistance;
        _hp = gameConfig.levelConfigs[GameMgr.instance.selectLevel].zombieHp[intZombieType];
        this.gameController = gameController;
    }


    void Dead(Vector3 force) {
        state = State.Dead;
        animator.SetBool("isDead", true);
        animator.speed = 0;
        body.isKinematic = false;
        puppetMaster.pinWeight =1f;
        puppetMaster.muscleWeight = 0.5f;
        body.AddForce(force, ForceMode.Impulse);
        StartCoroutine(FadeOutMuscleWeight());
        //µôÂä
        CreateProps();
        DeadEvent?.Invoke();
    }
    private IEnumerator FadeOutMuscleWeight()
    {
        while (puppetMaster.pinWeight > 0f)
        {
            puppetMaster.pinWeight = Mathf.MoveTowards(puppetMaster.pinWeight, 0f, Time.deltaTime * 2);
            puppetMaster.muscleWeight = Mathf.MoveTowards(puppetMaster.muscleWeight, 0f, Time.deltaTime * 2);
            yield return null;
        }
    }
    //private IEnumerator FadeOutPinWeight()
    //{
    //    while (puppetMaster.pinWeight > 0f)
    //    {
          
    //        yield return null;
    //    }
    //}

    void CreateProps() {
        int randomIndex = Random.Range(1, 101);
        int[] weight = gameConfig.zombieTypeConfigs[(int)_zombieType].propsWeight;
        int count = 0;
        for (int i = 0; i < weight.Length; i++)
        {
            count += weight[i];
            if (randomIndex <= count)
            {
                if (i > 0) {
                   Transform prop=Instantiate(props[i - 1]);
                    Vector3 position = transform.position - transform.forward * 2;
                    position.y = 0;
                    prop.position = position;
                }
                return;
            };
        }
    }

    public  void Hit(int hurtValue) {
        if (state == State.Dead) return;
        lastHurtTime = Time.time;
        _hp-=hurtValue;
        if (_hp <= 0)
        {
            Dead(-transform.forward * 6 + transform.up * 4);
        }
        else
        {
            state = State.Hurt;
            animator.speed = 0.5f;
            puppetMaster.pinWeight = 0.5f;
        }
    }

    public void Crash() {
        if (state == State.Dead) return;
        lastHurtTime = Time.time;
        _hp = 0;
        Dead(-transform.forward * 12 + transform.up * 4);
    }

    float  lastHurtTime;
    private void Update()
    {
        if (gameController.gameState != GameState.Gaming) return;
        if (target == null) return;
        switch (state) {
            case State.Sleep:
                if (transform.position.z - target.position.z < _activeDistance)
                {
                    state = State.Active;
                }
                break;
            case State.Active:
                Vector3 pos = target.position;
                pos.y = transform.position.y;
                transform.position = Vector3.MoveTowards(transform.position, pos, _walkSpeed * Time.deltaTime);
                transform.LookAt(target);
                break;
            case State.Hurt:
                if (Time.time - lastHurtTime > 0.5f) {
                    state = State.Active;
                    animator.speed = 1;
                    puppetMaster.pinWeight = 1f;
                };
                break;
            case State.Dead:
                break;
        }
        if (transform.position.z +10< target.position.z)
        {
            RemoveEvent?.Invoke(this);
        }
    }
}
