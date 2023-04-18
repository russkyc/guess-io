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

namespace org.russkyc.guessio.Models.Entities;

public partial class WordInfo : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<LetterInfo>? _letters;

    [ObservableProperty]
    private string? _word;

    public WordInfo(string word)
    {
        Word = word;
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
            letter.Hide(new Random().NextDouble() > 0.5);
        }
    }

    public void UnHide()
    {
        foreach (LetterInfo letter in Letters!)
        {
            letter.Hide(false);
        }
    }

    public bool Match(string word)
    {
        return Word!.Equals(word, StringComparison.OrdinalIgnoreCase);
    }
}
