using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UI ui;
    public Player _playerRef;

    private const int NUM_GOAL = 5;

    private Dictionary<SEED_TYPE, int> _currentScore;

    void Start()
    {
        instance = this;
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
        _playerRef.Reset ();
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
            if (score.Value < NUM_GOAL)
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
        }
    }
}
