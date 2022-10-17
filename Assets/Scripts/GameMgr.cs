using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class GameMgr
{
    public Action SelectLevelCompleteEvent;
    public Action<int> AddCoinEvent;
    public Action<int> SubCoinEvent;
    private static GameMgr _instance;
    public static GameMgr instance {
        get {
            if (_instance == null) _instance = new GameMgr();
            return _instance;
        }
    }
    Player _player = new Player();
    public Player player => _player;
    int _selectLevel;
    public int selectLevel => _selectLevel;
    GameConfig _gameConfig;
    GameConfig gameConfig {
        get {
            if (_gameConfig == null) _gameConfig = AssetDatabase.LoadAssetAtPath<GameConfig>(@"Assets/Config/gameConfig.asset");
            return _gameConfig;
        }
    }
    
    public void SaveGame() {
        archiveMgr.SaveGame(_player);
    }
    ArchiveMgr archiveMgr = new ArchiveMgr();
    public void Init() {
        if (gameConfig.isClearArchive)
        {
            _player = new Player();
        }
        else {
            _player = archiveMgr.LoadGame();
        }
    }

    public void AddCoin(int coin) {
        _player.coin += coin;
        AddCoinEvent?.Invoke(coin);
    }
    public void SubCoin(int coin) {
        if (player.coin < coin) return;
        _player.coin -= coin;
        SubCoinEvent?.Invoke(coin);
    }

    public void AdAddCoin() {
        if (gameConfig.AdAddCoin <= 0) return;
        AddCoin(gameConfig.AdAddCoin);
    }
    public void SelectLevel(int levelIndex,Action fail) {
        if (levelIndex > player.levelStars.Count)
        {
            return;
        }
        if (!StarIsEnough(levelIndex)) {
            fail?.Invoke();
            return;
        }
        _selectLevel = levelIndex;
        _isVectory = false;
        SelectLevelCompleteEvent?.Invoke();
    }
    public bool StarIsEnough(int levelIndex) {
        int count = 0;
        for (int i = 0; i < player.levelStars.Count; i++)
        {
            count += player.levelStars[i];
        }
        return count >= gameConfig.levelConfigs[levelIndex].unlockNeedStar;
    }

    public int GetStarCount(int  killCount) {
       int[] needKillArr= gameConfig.levelConfigs[selectLevel].starNeedkillCount;
        for (int i = 0; i < needKillArr.Length; i++) {
            if (killCount<needKillArr[i]) {
                return i;
            }
        }
        return needKillArr.Length;
    }
    bool _isVectory;
    public bool isVectory => _isVectory;
    void PassGame(int killCount) {
        _isVectory = true;
        int starCount = GetStarCount(killCount);
        if (player.levelStars.Count < gameConfig.levelConfigs.Count)
        {
            //如果当前结束关卡是已通关关卡 当星级数更多时更新星级
            if (selectLevel < player.levelStars.Count)
            {
                if (player.levelStars[selectLevel] < starCount)
                {
                    _player.levelStars[selectLevel] = starCount;
                }
            }
            else
            {
                //如果是未通关关卡 添加星级数据
                _player.levelStars.Add(starCount);
            }
        }
    }
    public void GetReward(int killCount) {
        PassGame(killCount);
        AddCoin(killCount * gameConfig.coinFactor);
    }
    public void GetDoubleReward(int killCount)
    {
        PassGame(killCount);
        AddCoin(killCount * gameConfig.coinFactor * 2);
    }
}
