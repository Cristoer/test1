using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameConfig : ScriptableObject
{
    [Header("是否全新存档开始")]
    public bool isClearArchive = false;
    [Header("观看广告增加金币数量")]
    public int AdAddCoin=1000;
    [Header("玩家配置")]
    public PlayerConfig playerConfig;
    [Header("僵尸配置")]
    public ZombieConfig zombieConfig;
    [Header("关卡配置")]
    public List<LevelConfig> levelConfigs;
    [Header("金币奖励系数")]
    public int coinFactor;
    [Header("僵尸类型配置")]
    public ZombieTypeConfig[] zombieTypeConfigs;
    [Header("道具配置")]
    public PropsConfig[] propsConfigs;
    [Header("枪械类武器配置")]
    public WeaponConfig[] gunWeaponConfigs;
    [Header("投掷类武器配置")]
    public WeaponConfig[] casterWeaponConfigs;


    [System.Serializable]
    public struct PropsConfig {
        [Header("id")]
        public int propsId;
        [Header("名称")]
        public string name;
        [Header("描述")]
        public string subcribe;
        [Header("使用时长(一次性道具为0，持续道具为使用秒数)")]
        public int useTime;
        [Header("最大数量")]
        public int limitCount;
    }
    [System.Serializable]
    public struct WeaponConfig {
        [Header("id")]
        public int weaponId;
        [Header("武器名称")]
        public string weaponName;
        [Header("伤害")]
        public int hurtValue;
        [Header("伤害范围")]
        public int hurtRange;
        [Header("攻击间隔")]
        public float attackInternal;
        [Header("子弹射程 米")]
        public int fireRange;
        [Header("子弹速度 米/秒")]
        public int speed;
    } 
    [System.Serializable]
    public struct LevelConfig {
        [Header("关卡数")]
        public int level;
        [Header("僵尸血量==0普通僵尸  1医生僵尸 2军人僵尸")]
        public int[] zombieHp;
        [Header("星级对应僵尸数(1星击杀数量，2星击杀数量，3星击杀数量)")]
        public int[] starNeedkillCount;
        [Header("解锁所需星级数")]
        public int unlockNeedStar;
        //[Header("星级奖励金币数(默认奖励，1星奖励，2星奖励，3星奖励)")]
        //public int[] starGetGoldCount;
    }
    [System.Serializable]
    public struct ZombieTypeConfig
    {
        [Header("僵尸类型id")]
        public int typeId;
        [Header("僵尸名称")]
        public string name;
        [Header("道具掉落权重 0无道具 1医疗包 2冲锋枪 3手雷")]
        public int[] propsWeight;
    }
    [System.Serializable]
    public struct ZombieConfig
    {
        [Header("移动速度 米/秒")]
        public float walkSpeed ;
        [Header("激活距离 米")]
        public float activeDistance;
    }
    [System.Serializable]
    public struct PlayerConfig {
        [Header("玩家血量")]
        public int maxHp;
        [Header("玩家前进速度 米/秒")]
        public int verticalSpeed;
        [Header("玩家水平速度(滑动1像素多少米，数值越大越快)")]
        public float horizontalSpeed;
        [Header("无敌时间")]
        public float InvincibleTime;
    }
}
