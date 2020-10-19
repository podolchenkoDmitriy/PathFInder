using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Text _coinsText;

    public GameObject _losePanel;

    public GameObject level2Obj;
    public Text _notifyMessage;
    public GameObject _buttons;
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(this);
        }
    }

    private IEnumerator LevelPass()
    {
        _notifyMessage.gameObject.SetActive(false);
        _notifyMessage.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        _notifyMessage.gameObject.SetActive(false);
        level2Obj.SetActive(true);

    }
    public void SetOnText()
    {
        StartCoroutine(LevelPass());
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            Coins._currentCoins = PlayerPrefs.GetInt("Coins");

            StartCoroutine(UpdateCoinsRoutine(0));
        }
    }

    private IEnumerator UpdateCoinsRoutine(int coin)
    {
        if (coin >= 0)
        {
            for (int i = 1; i <= coin; i++)
            {
                Coins._currentCoins++;

                yield return new WaitForFixedUpdate();

                _coinsText.text = Coins._currentCoins.ToString();

            }
        }
        else
        {
            for (int i = -1; i >= coin; i--)
            {
                Coins._currentCoins--;
                yield return new WaitForFixedUpdate();

                _coinsText.text = Coins._currentCoins.ToString();

            }
        }
        _coinsText.text = Coins._currentCoins.ToString();

    }
    public void UpdateCoins(int coin)
    {
        StartCoroutine(UpdateCoinsRoutine(coin));
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Coins", Coins._currentCoins);
    }

}
public class Coins
{
    public static int _currentCoins;

    public const int _coinModifier = 5;



}
