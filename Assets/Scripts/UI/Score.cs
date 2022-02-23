using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private bool _isLose =false;

    private int _score = 0;

    public UnityEvent<int> SaveScoreEvent;

    private void OnEnable()
    {
        Obstacle.UpScoreAction += UpScore;
    }

    private void OnDisable()
    {
        Obstacle.UpScoreAction -= UpScore;
    }

    public void UpScore()
    {
        if (_isLose)
        {
            return;
        }

        _score++;
        _scoreText.SetText(_score.ToString());
    }

    public void SaveScore()
    {
        SaveScoreEvent.Invoke(_score);
    }

    public void StopUpScore()
    {
        _isLose = true;
    }
}
