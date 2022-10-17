using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType {
    Death1,
    Death2,
    Death3,
    Hurt1,
    Hurt2,
    ItemPick,
    ItemUse1,
    ItemUse2,
    BountyHunt,
    Win1,
    Win2,
    GrenadeExplosion1,
    GrenadeExplosion2,
    Gun1,
    Gun2,
    MachineGun1,
    MachineGun2,
    Reload,
}
public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip bgm;
    public static AudioManager instance;
    private void Awake()
    {
        if (instance != null) {
            Destroy(gameObject);
            return;
        };
        GameMgr.instance.Init();
    }
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
        PlayBgm();
    }
    public void PlaySoundEffect(AudioType audioType) {
        audioSource.PlayOneShot(audioClips[(int)audioType]);
    }
    void PlayBgm() {
        audioSource.clip = bgm;
        audioSource.time = 18f;
        audioSource.Play();
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            GameMgr.instance.SaveGame();
        }
    }
    private void OnApplicationQuit()
    {
        GameMgr.instance.SaveGame();
    }
}
