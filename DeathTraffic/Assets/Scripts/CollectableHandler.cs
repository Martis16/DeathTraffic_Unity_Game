using System;
using TMPro;
using UnityEngine;

public class CollectableHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private TMP_Text _bestScoreText;

    private int _currentScore = 0;
    private int _bestScore;

    private const string BEST_SCORE = "bs";

	private void Start()
	{
		_bestScore = PlayerPrefs.GetInt(BEST_SCORE, 0);
		UpdateUI();
	}

	private void UpdateUI()
	{
		_currentScoreText.text = $"Score: {_currentScore:00000}";
		_bestScoreText.text = $"Best:  {_bestScore:00000}";
	}

	private void OnTriggerEnter(Collider collision)
	{
		Debug.Log(collision.gameObject.name);
		if (collision.TryGetComponent(out Collectable component))
		{
			_currentScore += component.Score;
			UpdateUI();
			Destroy(collision.gameObject);
		}	
	}

	private void OnDestroy()
	{
        if (_currentScore > _bestScore)
        {
            PlayerPrefs.SetInt(BEST_SCORE, _currentScore);
        }
    }
}
