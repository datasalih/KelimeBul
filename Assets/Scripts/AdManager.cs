using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
    public GameController gameController;
  //  public TextMesh currentWordText;

    RewardedAd rewardedAd;
    private string _adUnitId = "ca-app-pub-4863292193751850/3868606289";
    private GameObject adbtn;

    void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            LoadRewardedAd();
        });

        adbtn = GameObject.FindGameObjectWithTag("AdButton");

    }

    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }


        // create our request used to load the ad.
        var adRequest = new AdRequest.Builder().Build();

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error.GetMessage());
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
                RegisterEventHandlers(rewardedAd);
            });
    }


    public void ShowRewardedAd()
    {


        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show(HandleUserEarnedReward);
            adbtn = GameObject.FindGameObjectWithTag("AdButton");
            adbtn.GetComponent<Button>().interactable = false;
            
        }
    }

    private void HandleUserEarnedReward(Reward reward)
    {
        Row currentRow = gameController.rows[gameController.currentRow];

        // Get the first position in the current row
        int position = 0;

        // Check if the first position is already filled
        while (currentRow.texts[position].text != "")
        {
            position++;
            if (position >= 5)
            {
                Debug.Log("No empty positions in the current row.");
                return;
            }
        }

        // Update the current word with the first character in the correct position
        gameController.wordInput[position] = gameController.word[0];

        // Display the updated letter in the current row
        currentRow.texts[position].text = gameController.wordInput[position].ToString();
        currentRow.images[position].sprite = null;
        currentRow.images[position].color = Color.green;

        // Increment the correct counter
        gameController.correctCounter++;

        // Display the rewarded message
        Debug.Log("Reward earned! Current word updated. Correct counter incremented.");
    }






    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
            LoadRewardedAd();
        };
    }

  
}

