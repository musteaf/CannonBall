using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private BoardManager boardManager;
    private Controller controller;
    private TimeManager timeManager;
    // Start is called before the first frame update
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
        boardManager = GetComponent<BoardManager>();
        controller = GetComponent<Controller>();
        timeManager = GetComponent<TimeManager>();
    }

    public BoardManager BoardManager
    {
        get => boardManager;
        set => boardManager = value;
    }

    public Controller Controller
    {
        get => controller;
        set => controller = value;
    }

    public TimeManager TimeManager
    {
        get => timeManager;
        set => timeManager = value;
    }
}
