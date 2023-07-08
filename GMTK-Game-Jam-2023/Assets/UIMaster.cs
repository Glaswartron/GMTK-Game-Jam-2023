using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UIMaster : MonoBehaviour
{
    public static UIMaster instance;

    public Image heart1;
    public Image heart2;
    public TextMeshProUGUI coinText;

    public Sprite fullHeart;
    public Sprite damagedHeart;

    private void Awake()
    {
        instance = this;
    }

    public void LoseHeart(int heart)
    {
        if(heart == 1)
        {
            heart2.sprite = damagedHeart;
        }
        else
        {
            heart1.sprite = damagedHeart;
        }
    }
    public void GainHeart()
    {
        heart2.sprite = fullHeart;
    }
    
    public void SetCoinText(string ct)
    {
        coinText.text = ct;
    }

}
