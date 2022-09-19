using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour
{
    static int loadCount = 0;

    public void UpdateLoad() {
        if (loadCount % 2 == 0) {
            ShowAd();
        }
        loadCount++;
    }

    private void ShowAd() {
        if (Advertisement.IsReady()) {
            Advertisement.Show();
        }
    }
}
