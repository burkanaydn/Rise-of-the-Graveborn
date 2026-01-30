using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cinemachinescript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // "P" tuþuna basýnca ekran görüntüsü alýr
        {
            string filename = "Screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
            ScreenCapture.CaptureScreenshot(filename);
            Debug.Log("Screenshot saved: " + filename);
        }
    }
}
