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
using System.Linq;
using System.Text;

namespace org.russkyc.guessio.Models;

public class WordInfo
{
    public WordInfo(string word)
    {
        Word = word;
    }

    public string Word { get; }

    public string ObfuscatedWord
    {
        get
        {
            var builder = new StringBuilder { Capacity = Word.Length };

            do
            {
                builder.Clear();
                Word.ToList()
                    .ForEach(letter => builder.Append(Hide ? "?" : letter.ToString().ToUpper()));
            } while (
                builder.ToString().ToList().Where(c => c != '?').ToList().Count < (Word.Length / 2)
            );

            return builder.ToString();
        }
    }

    private bool Hide
    {
        get
        {
            var random = new Random();
            return random.NextDouble() >= 0.5;
        }
    }
}
