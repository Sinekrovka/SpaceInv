using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _lvlText;
    [SerializeField] private TextMeshProUGUI _lifeText;

    private int _score;
    private int _level;
    private int _life;

    private void Awake()
    {
        _score = 0;
        _level = 1;
        _life = 3;
        _scoreText.text = _score.ToString();
        _lvlText.text = _level.ToString();
        _lifeText.text = _life.ToString();
        RectTransform rect = GetComponent<RectTransform>();
        rect.anchoredPosition = Vector2.zero;
        
    }

    public void AddScore(int score)
    {
        _score += score;
        _scoreText.text = _score.ToString();
    }

    public void DamageLife()
    {
        _life--;
        _lifeText.text = _life.ToString();
        if (_life <= 0)
        {
            
        }
    }

    public void AddLevel()
    {
        _level++;
        _lvlText.text = _level.ToString();
    }
}
