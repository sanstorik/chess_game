using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Board;
using DG.Tweening;

public class EndGame : MonoBehaviour {
    public Text mainText;
    public Text whoIsWinnerText;
    public Text switchTurnText;

	// Use this for initialization
	void Start () {
        GameData.OnGameEndedEvent += (winnerIsWhite) =>
        {
            switchTurnText.gameObject.SetActive(false);

            whoIsWinnerText.text = winnerIsWhite ? "YOU WON!" : "YOU LOST!";
            whoIsWinnerText.gameObject.SetActive(true);
            whoIsWinnerText.DOFade(1f, 2.5f);

            mainText.gameObject.SetActive(true);
            mainText.DOFade(1f, 2.5f);
        };

        GameData.OnChangeTurnEvent += (isWhiteTurn) =>
        {
            if (GameData.isGameEnded)
                return;

            DOTween.CompleteAll();

            switchTurnText.text = isWhiteTurn ? "WHITE TURN" : "BLACK TURN";
            switchTurnText.gameObject.SetActive(true);

            Sequence sequence = DOTween.Sequence();
            sequence.Append(switchTurnText.DOFade(1f, 2.5f));
            sequence.Append(switchTurnText.DOFade(0f, 2f).OnComplete(() => switchTurnText.gameObject.SetActive(false)));
            sequence.Play();
        };
	}
}
