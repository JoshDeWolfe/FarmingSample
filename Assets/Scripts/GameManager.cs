using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UI ui;
    public Player playerRef;

    public OnGameWin onGameWin;

    private Dictionary<SEED_TYPE, int> _currentScore;

    void Start()
    {
        instance = this;
        onGameWin += playerRef.OnGameWin;
        ResetGame ();
    }

    public void ResetGame ()
    {
        if (_currentScore == null)
            _currentScore = new Dictionary<SEED_TYPE, int> ();

        foreach (SEED_TYPE type in SEED_TYPE.GetValues (typeof (SEED_TYPE)))
        {
            if (type == SEED_TYPE.NONE)
            {
                continue;
            }
            else if (!_currentScore.ContainsKey (type))
            {
                _currentScore.Add (type, 0);
            }
            else
            {
                _currentScore[type] = 0;
            }
        }
        UpdateScore ();
        playerRef.Reset ();
    }

    public void OnPlantHarvested (SEED_TYPE seedType)
    {
        _currentScore[seedType] += 1;
        UpdateScore ();
    }

    public void UpdateScore ()
    {
        bool hasWon = true;

        foreach (KeyValuePair<SEED_TYPE, int> score in _currentScore)
        {
            if (score.Value < Global.WIN_GOAL)
            {
                hasWon = false;
                break;
            }
        }

        if (!hasWon)
        {
            ui.OnScoreUpdate (_currentScore);
        }
        else
        {
            ui.ShowWin ();
            ResetGame ();
            onGameWin?.Invoke ();
        }
    }
}
