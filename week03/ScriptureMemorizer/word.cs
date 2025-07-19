public class Word
{
    private string _text;
    private bool _isShown;

    public Word(string text)
    {
        _text = text;
        _isShown = true;
    }

    public void Hide()
    {
        _isShown = false;
    }

    public bool IsShown()
    {
        return _isShown;
    }

    public string GetDisplayText()
    {
        return _isShown ? _text : new string('_', _text.Length);
    }
}
