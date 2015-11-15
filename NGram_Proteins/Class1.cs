using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NGram_Proteins
{
    public class Class1
    {

        public static IEnumerable<string> makeNgrams1(string text, byte nGramSize)
        {
            if (nGramSize == 0) throw new Exception("nGram size was not set");

            StringBuilder nGram = new StringBuilder();
            Queue<int> wordLengths = new Queue<int>();

            int wordCount = 0;
            int lastWordLen = 0;

            //append the first character, if valid.
            //avoids if statement for each for loop to check i==0 for before and after vars.

            for (int i = 0; i < text.Length - 1; i++)
            {
            if (text != "" && char.IsLetterOrDigit(text[i]))
            {
                nGram.Append(text[i]);
                lastWordLen++;
                wordCount++;
                wordLengths.Enqueue(lastWordLen);
               // lastWordLen = 0;
            }

           

                        if (wordCount >= nGramSize)
                        {
                            yield return nGram.ToString();
                          //  nGram.Remove(0, wordLengths.Dequeue() + 1);
                            nGram.Remove(0, 1);
                            wordCount -= 1;
                        }

                     
                    }
              
        }

        

               
    }
}
