using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum GameState { 
       Ready,
       Gaming,
       Pause,
       Finish
}
public class GameController : MonoBehaviour
{
    public  int killCount;
    public Action GameOverEvent;
    public Action GamePassEvent;
    public Action RelifeEvent;
    public Action StartGameEvent;
    public Action<PropsType> GetPropsEvent;
    public Action<PropsType> UsePropsEvent;
    public Action KillZombieEvent;
    public Action<int> PlayerUpdateHpEvent;
    public int[] propsCounts;
    [SerializeField]GameConfig gameConfig;
    List<ZombieController> zombieList;
    public GameState gameState=>_gameState;
    GameState _gameState;
    private void Start()
    {
        //GameMgr.instance.StartGame();
        ReadyGame();
    }

    void ReadyGame() {
        killCount = 0;
        propsCounts = new int[gameConfig.propsConfigs.Length];
        zombieList = new List<ZombieController>();
        //_gameState = GameState.Ready;
        StartGame();
    }
    public  void StartGame() {
        _gameState = GameState.Gaming;
        StartGameEvent?.Invoke();
    }


    public void AddZombie(ZombieController zombie) {
        zombieList.Add(zombie);
        zombie.DeadEvent += DeadZombie;
    }
     void DeadZombie() {
        killCount++;
        KillZombieEvent?.Invoke();
    }
    public void RemoveZombie(ZombieController zombie) {
        if (!zombieList.Contains(zombie)) {
            Debug.LogError("the zombie was already removed");
            return;
        }
        zombieList.Remove(zombie);
    }
    public bool UseProp(PropsType propsType) {
        if (propsCounts[(int)propsType] <= 0) return false;
        propsCounts[(int)propsType]--;
        UsePropsEvent?.Invoke(propsType);
        return true;
    }
    public bool AddProp(PropsType propsType)
    {
        if (propsCounts[(int)propsType] >= gameConfig.propsConfigs[(int)propsType].limitCount) return false;
        propsCounts[(int)propsType]++;
        GetPropsEvent?.Invoke(propsType);
        return true;
    }
    public void UpdateHp(int hp) {
        PlayerUpdateHpEvent?.Invoke(hp);
        if (hp <= 0) {
            GameOver();
        }
    }
   void GameOver() {
        //GameMgr.instance.GameOver();
        GameOverEvent?.Invoke();
        PauseGame();
    }

   public  void PauseGame() {
        _gameState = GameState.Pause;
    }
    public void PassGame() {
        GamePassEvent?.Invoke();
        PauseGame();
    }

    //¸´»î
    public void Relife() {
        StartGame();
        RelifeEvent?.Invoke();
    }


    public List<ZombieController> GetBoomBulletNearlyZombies(Transform center,float range) {
        List<ZombieController> zombies = new List<ZombieController>();
        for (int i = 0; i < zombieList.Count; i++) {
            if (zombieList[i].hp <= 0) continue;
            if (Vector3.Distance(zombieList[i].transform.position, center.position) <= range) {
                zombies.Add(zombieList[i]);
            }
        }
        return zombies;
    }
}
