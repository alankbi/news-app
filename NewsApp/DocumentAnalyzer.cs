using System;
using System.IO;
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
        double[][] scoreMatrix;

        HashSet<string> stopWords;
        Porter2 stemmer = new Porter2();

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

            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "stopwords.txt");
            var stopWordsAsArray = File.ReadAllLines(path);
            stopWords = new HashSet<string>(stopWordsAsArray);

            foreach (NewsArticle article in articles) 
            {
                string text = article.Title + " " + article.Description;

                string[] words = text.Split(null);
                words = EditForAnalysis(words);

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

            scoreMatrix = new double[articles.Count][];
            for (int i = 0; i < scoreMatrix.Length; i++) 
            {
                scoreMatrix[i] = new double[docFrequency.Count];
            }

            ConstructMatrix();
        }

        /**
         * Returns the cosine similarity between two arrays of doubles
         * representing the tf-idf values for two documents.
         */
        public double Similarity(double[] docA, double[] docB)
        {
            double dotProduct, magnitudeA, magnitudeB;
            dotProduct = magnitudeA = magnitudeB = 0;

            for (int j = 0; j < docA.Length; j++)
            {
                double a = docA[j];
                double b = docB[j];

                dotProduct += a * b;
                magnitudeA += a * a;
                magnitudeB += b * b;
            }
            return dotProduct / (Math.Sqrt(magnitudeA) * Math.Sqrt(magnitudeB));
        }

        /**
         * Returns the cosine similarity between an array of doubles representing
         * tf-idf values and an int representing a document in scoreMatrix.
         */
        public double Similarity(double[] docA, int docB)
        {
            return Similarity(docA, scoreMatrix[docB]);
        }

        /**
         * Returns the cosine similarity between two ints representing 
         * documents in scoreMatrix. 
         */
        public double Similarity(int docA, int docB)
        {
            return Similarity(scoreMatrix[docA], scoreMatrix[docB]);
        }

        /**
         * Used to compare between articles. If docA is better, meaning it's
         * closer to its cluster's centroid, returns a negative number so that
         * it's put first when sorting. 
         */
        public int Compare(int docA, int docB, double[] centroid)
        {
            double simA = Similarity(centroid, docA);
            double simB = Similarity(centroid, docB);
            if (simA > simB)
            {
                return -1;
            }
            else if(simA < simB)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int NumberOfTerms()
        {
            return docTerms.Count;
        }

        public double[] GetDocumentTfIdf(int index)
        {
            return scoreMatrix[index];
        }

        /**
         * Constructs the tf-idf matrix for the given documents and terms found.
         */
        private void ConstructMatrix()
        {
            // Inverted traversal to allow saving of idf calculation
            for (int j = 0; j < scoreMatrix[0].Length; j++) 
            {
                string word = docTerms[j];
                double idf = IdfValue(word);
                for (int i = 0; i < scoreMatrix.Length; i++) 
                {
                    double tf = tfValue(word, i);
                    scoreMatrix[i][j] = tf * idf;
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
            //return (double)docTerms.Count / freq;
            //return 1;
        }

        /**
         * Takes an array of words and modifies them to later use for analysis. 
         * Converts to lowercase, removes common suffixes (such as -ed, -ing), 
         * and soon to be more. TODO: remove punctuation
         */
        public string[] EditForAnalysis(string[] words) 
        {
            var newWords = new List<string>();
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].ToLower();
                if (stopWords.Contains(words[i]) || words[i].Equals(""))
                {
                    continue;
                }

                /*// Remove ed suffix
                if (words[i].Length > 5 && words[i].Substring(words[i].Length - 2, 2) == "ed")
                {
                    words[i] = words[i].Substring(0, words[i].Length - 2);
                }
                // Remove ing suffix
                if (words[i].Length > 5 && words[i].Substring(words[i].Length - 3, 3) == "ing")
                {
                    words[i] = words[i].Substring(0, words[i].Length - 3);
                }*/
                newWords.Add(stemmer.stem(words[i]));
            }

            return newWords.ToArray();
        }

        private void DebugMatrix()
        {
            foreach (KeyValuePair<string, int> pair in docFrequency)
            {
                Console.WriteLine(pair.Key + " " + pair.Value);
            }

            for (int i = 0; i < scoreMatrix.Length; i++)
            {
                for (int j = 0; j < scoreMatrix[i].Length; j++)
                {
                    Console.Write(scoreMatrix[i][j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
