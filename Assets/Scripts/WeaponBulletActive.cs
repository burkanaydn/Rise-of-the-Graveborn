using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBulletActive : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    private void Update()
    {
        if (LevelCompleteManager.Instance.levelIsOver)
            bullet.SetActive(false);
    }

    private void OnEnable()
    {
        if (bullet != null)
        {
            bullet.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if (bullet != null)
        {
            bullet.SetActive(false);
        }
    }
}
