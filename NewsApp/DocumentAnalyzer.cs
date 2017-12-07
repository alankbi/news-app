using System;
using System.Collections.Generic;

namespace NewsApp
{
    public class DocumentAnalyzer
    {
        // Consider creating a class for this to store not only the dictionary
        // but also an int value for total # of words in each document
        List<Dictionary<string, int>> articleText;
        Dictionary<string, int> docFrequency;

        /**
         * Creates an instance with a List holding the arrays of words for
         * each article and a Dictionary of the frequency of each word across
         * the documents.
         */
        public DocumentAnalyzer(List<NewsArticle> articles)
        {
            articleText = new List<Dictionary<string, int>>();
            docFrequency = new Dictionary<string, int>();

            foreach (NewsArticle article in articles) 
            {
                string text = article.Title + " " + article.Description;

                string[] words = text.Split(null);
                EditForAnalysis(words);

                // Create frequency of words for this particular document
                var uniqueWords = new Dictionary<string, int>();
                foreach (string word in words)
                {
                    if (uniqueWords.ContainsKey(word))
                    {
                        uniqueWords[word]++;
                    }
                    else
                    {
                        uniqueWords[word] = 1;
                    }
                }
                articleText.Add(uniqueWords);

                // Add frequency of words across all documents 
                foreach (string word in uniqueWords.Keys)
                {
                    if (docFrequency.ContainsKey(word))
                    {
                        docFrequency[word]++;
                    }
                    else
                    {
                        docFrequency[word] = 1;
                    }
                }
            }

            // TESTING

            foreach (KeyValuePair<string, int> pair in docFrequency)
            {
                Console.WriteLine(pair.Key + " " + pair.Value);
            }
        }


        /**
         * Takes an array of words and modifies them to later use for analysis. 
         * Converts to lowercase, removes common suffixes (such as -ed, -ing), 
         * and soon to be more. TODO: remove punctuation
         */
        public void EditForAnalysis(string[] words) 
        {
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].ToLower();
                // Remove ed suffix
                if (words[i].Length > 5 && words[i].Substring(words[i].Length - 2, 2) == "ed")
                {
                    words[i] = words[i].Substring(0, words[i].Length - 2);
                }
                // Remove ing suffix
                if (words[i].Length > 5 && words[i].Substring(words[i].Length - 3, 3) == "ing")
                {
                    words[i] = words[i].Substring(0, words[i].Length - 3);
                }
            }
        }
    }
}
