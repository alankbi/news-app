using System;
using System.Collections.Generic;

namespace NewsApp
{
    public class DocumentAnalyzer
    {
        // Keep track of word frequencies in each article
        List<Dictionary<string, int>> articleText;
        List<int> articleLength;

        // Keep track of word frequencies in the corpus
        Dictionary<string, int> docFrequency;
        List<string> docTerms;

        // Matrix of tf-idf scores where [i, j] represents the tf-idf value for
        // the jth term in the ith document. 
        double[,] scoreMatrix;

        /**
         * Creates an instance with a List holding the arrays of words for
         * each article and a Dictionary of the frequency of each word across
         * the documents.
         */
        public DocumentAnalyzer(List<NewsArticle> articles)
        {
            articleText = new List<Dictionary<string, int>>();
            articleLength = new List<int>();
            docFrequency = new Dictionary<string, int>();
            docTerms = new List<string>();

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
                articleLength.Add(words.Length);

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
                        docTerms.Add(word);
                    }
                }
            }

            scoreMatrix = new double[articles.Count, docFrequency.Count];

            ConstructMatrix();
        }

        public double similarity(int docA, int docB)
        {
            double dotProduct, magnitudeA, magnitudeB;
            dotProduct = magnitudeA = magnitudeB = 0;

            for (int j = 0; j < scoreMatrix.GetLength(1); j++)
            {
                double a = scoreMatrix[docA, j];
                double b = scoreMatrix[docB, j];

                dotProduct += a * b;
                magnitudeA += a * a;
                magnitudeB += b * b;
            }
            return dotProduct / (Math.Sqrt(magnitudeA) * Math.Sqrt(magnitudeB));
        }

        /**
         * Constructs the tf-idf matrix for the given documents and terms found.
         */
        private void ConstructMatrix()
        {
            // Inverted traversal to allow saving of idf calculation
            for (int j = 0; j < scoreMatrix.GetLength(1); j++) 
            {
                string word = docTerms[j];
                double idf = IdfValue(word);
                for (int i = 0; i < scoreMatrix.GetLength(0); i++) 
                {
                    double tf = tfValue(word, i);
                    scoreMatrix[i, j] = tf * idf;
                }
            }
        }

        /**
        * Returns the tf value for the term in the given document. 
        */
        private double tfValue(string word, int docNumber)
        {
            var frequencies = articleText[docNumber];
            if (frequencies.ContainsKey(word))
            {
                return (double)frequencies[word] / articleLength[docNumber];
            }
            else
            {
                return 0;
            }
        }

        /**
         * Returns the idf value for the term. 
         */
        private double IdfValue(string term)
        {
            int freq = docFrequency[term];
            return Math.Log((double)docTerms.Count / freq);
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

        private void DebugMatrix()
        {
            foreach (KeyValuePair<string, int> pair in docFrequency)
            {
                Console.WriteLine(pair.Key + " " + pair.Value);
            }

            for (int i = 0; i < scoreMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < scoreMatrix.GetLength(1); j++)
                {
                    Console.Write(scoreMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
