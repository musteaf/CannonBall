using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private GameObject cannonPrefab;
    [SerializeField] private TargetPlatform currentTargetPlatform;
    [SerializeField] private ProjectilePathDrawer projectilePathDrawer;
    private static BoardManager instance;
    private Cannon cannon;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameObject cannonGameObject = Instantiate(cannonPrefab);
        cannon = cannonGameObject.GetComponent<Cannon>();
        cannon.setTargetPlatform(currentTargetPlatform);
        cannon.setProjectilePathDrawer(projectilePathDrawer);
        cannon.Refresh();
    }

    public void RefreshLevel()
    {
        bool launched = cannon.Refresh();
        if (launched)
        {
            currentTargetPlatform.InitializePlatform();
            GameManager.instance.Controller.OnController();
        }
    }

    public Cannon Cannon
    {
        get => cannon;
        set => cannon = value;
    }
}