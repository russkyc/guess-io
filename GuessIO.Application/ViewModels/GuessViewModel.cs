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

using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using org.russkyc.guessio.Models;
using Russkyc.AttachedUtilities.FileStreamExtensions;

namespace org.russkyc.guessio.ViewModels;

public partial class GuessViewModel : ObservableObject
{
    private static readonly string _datasetPath =
        @$"{AppDomain.CurrentDomain.BaseDirectory}GuessWords.txt";
    private static readonly Random _random = new Random();

    [ObservableProperty]
    private bool? _guessed;

    [ObservableProperty]
    private bool? _correct;

    [ObservableProperty]
    private WordInfo? _word;

    [ObservableProperty]
    private string? _guessWord;

    [ObservableProperty]
    private ObservableCollection<LetterInfo>? _letters;

    [ObservableProperty]
    private ObservableCollection<GuessInfo>? _guessCollection;

    [ObservableProperty]
    private ObservableCollection<WordInfo>? _wordsCollection;

    public GuessViewModel()
    {
        WordsCollection = new ObservableCollection<WordInfo>();
        GuessCollection = new ObservableCollection<GuessInfo>();
        Letters = new ObservableCollection<LetterInfo>();
        Initialize();
        Generate();
    }

    private void Initialize()
    {
        var words = _datasetPath.StreamListLines().Select(w => new WordInfo(w));
        foreach (var word in words)
        {
            WordsCollection?.Add(word);
        }
    }

    [RelayCommand]
    private void Generate()
    {
        Word = WordsCollection?[_random.Next(WordsCollection.Count)];
        Letters?.Clear();
        foreach (var letter in Word!.ObfuscatedWord)
        {
            Letters?.Add(new LetterInfo(letter));
        }
        GuessWord = "";
        Guessed = true;
        GuessCollection?.Clear();
        Correct = false;
    }

    [RelayCommand]
    private void Guess()
    {
        if (string.Equals(GuessWord, Word?.Word, StringComparison.OrdinalIgnoreCase))
        {
            Letters?.Clear();
            foreach (var letter in Word!.ObfuscatedWord)
            {
                Letters?.Add(new LetterInfo(letter));
            }
            Generate();
            Correct = true;
        }
        else
        {
            if (GuessWord?.Length > 2)
            {
                GuessCollection?.Add(new GuessInfo(GuessWord));
            }

            Guessed = false;
            GuessWord = "";
            Guessed = true;
            Correct = false;
        }
    }
}
