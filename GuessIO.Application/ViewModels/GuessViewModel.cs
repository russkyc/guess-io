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

namespace org.russkyc.guessio.ViewModels;

public partial class GuessViewModel : ObservableObject
{
    [ObservableProperty]
    private WordInfoViewModel? _word;

    [ObservableProperty]
    private string? _guessWord;

    [ObservableProperty]
    private string? _prompText;

    [ObservableProperty]
    private string? _helperText;

    [ObservableProperty]
    private ObservableCollection<GuessInfo>? _guessCollection;

    [ObservableProperty]
    private ObservableCollection<WordInfoViewModel>? _wordsCollection;

    public GuessViewModel(ObservableCollection<WordInfoViewModel> words, ObservableCollection<GuessInfo> guesses)
    {
        WordsCollection = words;
        GuessCollection = guesses;
        Generate();
    }

    private void Generate()
    {
        var words = WordsCollection?.Where(word => !word.Guessed).ToList();

        if (words!.Count > 0)
        {
            PrompText = "Guess the Word:";
            HelperText = "Press ENTER to guess and TAB to generate a new word";
            Word = words[new Random().Next(words.Count)];
            Word?.Hide();
        }
        else
        {
            PrompText = "No Words to Guess";
            HelperText = "Add Words to GuessWords.txt in order to play.";
            Word = new WordInfoViewModel("");
        }
        GuessCollection?.Clear();
        ClearFields();
    }

    [RelayCommand]
    private void Skip()
    {
        Word?.Skip();
        Generate();
    }

    [RelayCommand]
    private void Guess()
    {
        if (Word!.Match(GuessWord!))
        {
            Generate();
            Word.Guessed = true;
        } else if (GuessWord?.Length > 2)
        {
            Word!.Guessed = true;
            Word!.Guessed = false;
            GuessCollection?.Add(new GuessInfo(GuessWord));
            ClearFields();
        }
    }

    private void ClearFields()
    {
        GuessWord = "";
    }
}
