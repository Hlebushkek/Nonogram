using System.Collections.Generic;
using UnityEngine;

public static class TextScript
{
    private static string _fontname = "apixelfont-Sheet";
    private static char[] letterCharENG = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!?+-=÷*;:()[]«»/~%\"'|,.".ToCharArray();
    private static Sprite[] letterSprite;
    private static Dictionary<char, Sprite> charToSprite = new Dictionary<char, Sprite>();
    public static void CreateDictionary()
    {
        letterSprite = Resources.LoadAll<Sprite>(_fontname);

        for (int i = 0; i < letterSprite.Length; i++)
        {
            charToSprite.Add(letterCharENG[i], letterSprite[i]);
        }
    }
    public static Sprite GetLetterObj(char c)
    {
        return charToSprite[c];
    }
}
