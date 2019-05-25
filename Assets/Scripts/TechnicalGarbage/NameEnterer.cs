using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameEnterer : MonoBehaviour
{
    public delegate void LetterSent();
    public LetterSent letterSent;

    public LetterBox currentBox;

    bool toggled;
    bool cycleEnabled;
    Coroutine timer;

    int Index
    {
        get { return Index % Letters.Symbols.Length; }
        set { Index = value; }
    }

    void Update()
    {
        if (toggled && Input.GetAxisRaw("v") == 0)
        {
            // toggle is so you cant hold the stick and cycle the letters by frame
            toggled = false;
            cycleEnabled = false;
            StopCoroutine(timer);
        }
        else if (!toggled && Input.GetAxisRaw("V") > 0)
        {
            Index++;
            currentBox.selectedLetter = Letters.GetLetter(Index);
            letterSent?.Invoke();
            toggled = true;
            timer = StartCoroutine(CycleTimer());
        }
        else if (!toggled && Input.GetAxisRaw("V") < 0)
        {
            Index--;
            currentBox.selectedLetter = Letters.GetLetter(Index);
            letterSent?.Invoke();
            toggled = true;
            timer = StartCoroutine(CycleTimer(false));
        }
    }

    IEnumerator CycleTimer(bool positiveCycle = true)
    {
        yield return new WaitForSeconds(WorldData.timeUntilLetterCycle);
        cycleEnabled = true;
        StartCoroutine(CycleLetters(positiveCycle));
    }

    IEnumerator CycleLetters(bool positiveCycle = true)
    {
        while (cycleEnabled)
        {
            yield return new WaitForSeconds(WorldData.LetterCycleSpeed);
            Index += positiveCycle ? 1 : -1;
            currentBox.selectedLetter = Letters.GetLetter(Index);
            letterSent?.Invoke();
        }
    }

    public static class Letters
    {
        public static char[] Symbols { get; private set; } = new char[]
        {
        'a', 'b', 'c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z','å','ä','ö',
    '0','1','2','3','4','5','6','7','8','9','$'
        };

        public static char GetLetter(int i)
        {
            return Symbols[i];
        }
    }
}
