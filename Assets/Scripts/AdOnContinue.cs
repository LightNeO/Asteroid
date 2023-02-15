using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdOnContinue : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _gameOverMenu;
    private RewardedAd rewardedAd;
    //[SerializeField] private Player _player;

    //private string RewardedUnitId = "ca-app-pub-9787935308166164/1273837071";
    private string RewardedUnitId = "ca-app-pub-9787935308166164/5313124555";
    private void OnEnable()
    {
        rewardedAd = new RewardedAd(RewardedUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(adRequest);
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
    }
    private void HandleUserEarnedReward(object sender, Reward e)
    {
        _pauseMenu.SetActive(false);
        _gameOverMenu.SetActive(false);
        Player.isAlive = true;
        GameManager._addWaitTime = 30;
        GameManager._lives = 1;
        Time.timeScale = 1;
        FindObjectOfType<GameManager>().Respawn();
    }
    public void ShowAd()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
    }
}
