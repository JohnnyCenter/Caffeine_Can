using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region Dash Related
    [SerializeField]
    private TextMeshProUGUI dashCounter;
    public int dashCount, dashTrack;
    #endregion
    #region Score Text
    public TextMeshProUGUI FinalScore;
    public TextMeshProUGUI BaseScore;
    public TextMeshProUGUI DropletScore;
    public TextMeshProUGUI EnemyScore;
    public TextMeshProUGUI SaveScore;
    #endregion
    [SerializeField]
    public Button retryButton;
    public CanvasGroup resultScreen;
    public CanvasGroup UiElements;
    public Image Bean;
    private Animator BeanAnim;
    [SerializeField]
    private scoreController score;
    private float desiredNumber, initialNumber, currentNumber;
    private float animationTime = 0.5f;

    private void Awake()
    {
        BeanAnim = Bean.GetComponent<Animator>();
    }

    private void Start()
    {
        UiElements.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(false);
        resultScreen.gameObject.SetActive(false);

        initialNumber = currentNumber = 0f;
        desiredNumber = 0;
    }

    private void Update()
    {
        dashCounter.text = dashCount.ToString();

        #region NumberSlider
        if (currentNumber != desiredNumber)
        {
            if (initialNumber < desiredNumber)
            {
                currentNumber += (animationTime * Time.deltaTime) * (desiredNumber - initialNumber);
                if (currentNumber >= desiredNumber)
                    currentNumber = desiredNumber;
            }
            else
            {
                currentNumber -= (animationTime * Time.deltaTime) * (initialNumber - desiredNumber);
                if (currentNumber <= desiredNumber)
                    currentNumber = desiredNumber;
            }

            FinalScore.text = currentNumber.ToString("0");
        }
        #endregion
    }

    public void AddToValue(float value)
    {
        initialNumber = currentNumber;
        desiredNumber += value;
    }

    public void DashUse()
    {
        dashCount -= 1;
        dashTrack += 1;
    }

    public void Finsihed()
    {
        StartCoroutine(ShowResults());
    }

    public IEnumerator ShowResults()
    {
        UiElements.gameObject.SetActive(false);
        resultScreen.gameObject.SetActive(true);
        score.CalculateScore(); //Calculates score
        BaseScore.text = score.baseScore.ToString();
        DropletScore.text = score.dropletScore.ToString();
        EnemyScore.text = score.enemyScore.ToString();
        SaveScore.text = score.saveScore.ToString();
        yield return new WaitForSeconds(2);
        AddToValue(score.finalScore); //Shows score
        yield return new WaitForSeconds(2);
        BeanAnim.SetInteger("Victory", 1);
    }

}
