using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text TimerText;
    public GameObject GameUI;
    public GameObject EndGameUI;

    private float _timeLeft = 90;
    private bool _playing = true;

    private Compactor _compactor;

    void Awake()
    {
        Instance = this;
        _compactor = FindObjectOfType<Compactor>();
    }

    public void WonGame()
    {
        GameUI.SetActive(false);
        EndGameUI.SetActive(true);
        EndGameUI.GetComponent<GameEnd>().Survived();
        _playing = false;
    }

    public void LostGame()
    {
        GameUI.SetActive(false);
        EndGameUI.SetActive(true);
        EndGameUI.GetComponent<GameEnd>().Died();
        _playing = false;
    }

    void FixedUpdate()
    {
        if (!_playing)
            return;

        _compactor.Tick();
    }

    void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }


        if (!_playing)
            return;


        Player.Instance.Tick();

        _timeLeft -= Time.deltaTime;
        TimerText.text = $"{(int)_timeLeft}";

        if (_timeLeft < 0)
        {
            WonGame();
        }

        if (Player.Instance.Health < 0)
        {
            LostGame();
        }
    }
}
