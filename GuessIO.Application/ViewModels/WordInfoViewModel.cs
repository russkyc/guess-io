// MIT License
//
// Copyright (c) 2023 Russell Camo (Russkyc)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Globalization;

namespace org.russkyc.guessio.ViewModels;

public partial class WordInfoViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<LetterInfo>? _letters;

    [ObservableProperty]
    private string? _word;

    [ObservableProperty]
    private bool _guessed;

    [ObservableProperty]
    private bool _hidden;

    public WordInfoViewModel(string word)
    {
        Word = word;
        Guessed = false;
        Letters = new ObservableCollection<LetterInfo>();
        foreach (var letter in word.ToCharArray())
        {
            Letters.Add(new LetterInfo(letter));
        }
    }

    public void Hide()
    {
        foreach (LetterInfo letter in Letters!)
        {
            var specialchars = "!?-=_".ToCharArray();
            if (!specialchars.Contains(letter.Letter![0]))
            {
                letter.Hide(new Random().NextDouble() > 0.5);
            }
        }
    }

    public void Unhide()
    {
        foreach (LetterInfo letter in Letters!)
        {
            letter.Hide(false);
        }
    }

    public bool Match(string word)
    {
        if (string.Compare(Word, word, CultureInfo.CurrentCulture, 
                CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols) == 0)
        {
            Unhide();
            Hidden = false;
            Guessed = true;
            return Guessed;
        }
        else
        {
            Hidden = true;
            Guessed = false;
            return Guessed;
        }
    }

    public void Skip()
    {
        Guessed = false;
        Hide();
    }
}
