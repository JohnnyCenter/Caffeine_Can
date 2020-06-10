using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class numberSlider : MonoBehaviour
{
    public TextMeshProUGUI numberText;
    public float animationTime = 0.5f;

    public float desiredNumber, initialNumber, currentNumber;

    public void Start()
    {
        initialNumber = currentNumber = 0f;
        desiredNumber = 0;     
    }

    public void AddToValue(float value)
    {
        initialNumber = currentNumber;
        desiredNumber += value;
    }

    public void Update()
    {
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

            numberText.text = currentNumber.ToString("0");
        }
    }
}
