using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameConfig : ScriptableObject
{
    [Header("�Ƿ�ȫ�´浵��ʼ")]
    public bool isClearArchive = false;
    [Header("�ۿ�������ӽ������")]
    public int AdAddCoin=1000;
    [Header("�������")]
    public PlayerConfig playerConfig;
    [Header("��ʬ����")]
    public ZombieConfig zombieConfig;
    [Header("�ؿ�����")]
    public List<LevelConfig> levelConfigs;
    [Header("��ҽ���ϵ��")]
    public int coinFactor;
    [Header("��ʬ��������")]
    public ZombieTypeConfig[] zombieTypeConfigs;
    [Header("��������")]
    public PropsConfig[] propsConfigs;
    [Header("ǹе����������")]
    public WeaponConfig[] gunWeaponConfigs;
    [Header("Ͷ������������")]
    public WeaponConfig[] casterWeaponConfigs;


    [System.Serializable]
    public struct PropsConfig {
        [Header("id")]
        public int propsId;
        [Header("����")]
        public string name;
        [Header("����")]
        public string subcribe;
        [Header("ʹ��ʱ��(һ���Ե���Ϊ0����������Ϊʹ������)")]
        public int useTime;
        [Header("�������")]
        public int limitCount;
    }
    [System.Serializable]
    public struct WeaponConfig {
        [Header("id")]
        public int weaponId;
        [Header("��������")]
        public string weaponName;
        [Header("�˺�")]
        public int hurtValue;
        [Header("�˺���Χ")]
        public int hurtRange;
        [Header("�������")]
        public float attackInternal;
        [Header("�ӵ���� ��")]
        public int fireRange;
        [Header("�ӵ��ٶ� ��/��")]
        public int speed;
    } 
    [System.Serializable]
    public struct LevelConfig {
        [Header("�ؿ���")]
        public int level;
        [Header("��ʬѪ��==0��ͨ��ʬ  1ҽ����ʬ 2���˽�ʬ")]
        public int[] zombieHp;
        [Header("�Ǽ���Ӧ��ʬ��(1�ǻ�ɱ������2�ǻ�ɱ������3�ǻ�ɱ����)")]
        public int[] starNeedkillCount;
        [Header("���������Ǽ���")]
        public int unlockNeedStar;
        //[Header("�Ǽ����������(Ĭ�Ͻ�����1�ǽ�����2�ǽ�����3�ǽ���)")]
        //public int[] starGetGoldCount;
    }
    [System.Serializable]
    public struct ZombieTypeConfig
    {
        [Header("��ʬ����id")]
        public int typeId;
        [Header("��ʬ����")]
        public string name;
        [Header("���ߵ���Ȩ�� 0�޵��� 1ҽ�ư� 2���ǹ 3����")]
        public int[] propsWeight;
    }
    [System.Serializable]
    public struct ZombieConfig
    {
        [Header("�ƶ��ٶ� ��/��")]
        public float walkSpeed ;
        [Header("������� ��")]
        public float activeDistance;
    }
    [System.Serializable]
    public struct PlayerConfig {
        [Header("���Ѫ��")]
        public int maxHp;
        [Header("���ǰ���ٶ� ��/��")]
        public int verticalSpeed;
        [Header("���ˮƽ�ٶ�(����1���ض����ף���ֵԽ��Խ��)")]
        public float horizontalSpeed;
        [Header("�޵�ʱ��")]
        public float InvincibleTime;
    }
}
