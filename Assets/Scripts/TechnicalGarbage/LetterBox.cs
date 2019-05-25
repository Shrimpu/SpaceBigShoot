using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterBox : MonoBehaviour
{
    public char selectedLetter = NameEnterer.Letters.Symbols[0];

    public NameEnterer nameEnterer;
    public TMP_Text text;

    public void Selected()
    {
        nameEnterer.currentBox = this;
        nameEnterer.letterSent = UpdateLetterText;
    }

    void UpdateLetterText()
    {
        text.text = selectedLetter.ToString();
    }
}
