using System;
using System.Collections.Generic;
using System.Linq;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();
        foreach (var word in text.Split(' '))
        {
            _words.Add(new Word(word));
        }
    }

    public string GetDisplayText()
    {
        return $"{_reference.GetDisplayText()} {string.Join(" ", _words.ConvertAll(w => w.GetDisplayText()))}";
    }

    public void HideRandomWords(int count)
    {
        var shownWords = _words.FindAll(w => w.IsShown());
        var rand = new Random();
        for (int i = 0; i < count && shownWords.Count > 0; i++)
        {
            int idx = rand.Next(shownWords.Count);
            shownWords[idx].Hide();
            shownWords.RemoveAt(idx);
        }
    }

    public bool IsCompletelyHidden()
    {
        return _words.TrueForAll(w => !w.IsShown());
    }
}
