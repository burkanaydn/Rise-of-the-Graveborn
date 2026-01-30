using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCanvas : MonoBehaviour
{
    private static GlobalCanvas instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); // Zaten bir tane varsa, bu yeniyi yok et
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

    }
}
