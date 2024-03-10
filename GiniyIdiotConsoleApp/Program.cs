using System;
using System.Formats.Tar;
using System.Globalization;

namespace GeniyIdiotConsoleApp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Введите имя пользователя");
            string userName = Console.ReadLine();
            userName = char.ToUpper(userName[0]) + userName.Remove(0, 1);
            while (true)
            {
                int countRigthAnswer = 0;
                string[,] questionsAndAnswers = GetQuestionsAndAnswers();
                string[] diagnose = GetDiagnose();
                int countQuestions = questionsAndAnswers.GetUpperBound(0) + 1;
                int[] randoms = Shuffle(countQuestions);
                if (countQuestions > 0 && diagnose.Length > 0)
                {
                    for (int i = 0; i < countQuestions; i++)
                    {
                        Console.WriteLine($"Вопрос №{i + 1}");
                        Console.WriteLine(questionsAndAnswers[randoms[i], 0]);
                        string userAnswer = Console.ReadLine();
                        if (GetCorrectAnswer(userAnswer) == Math.Round(Convert.ToDouble(questionsAndAnswers[randoms[i], 1]), 2))
                        {
                            countRigthAnswer++;
                        }
                    }
                    Console.WriteLine($"Количество правильных ответов: {countRigthAnswer}");
                    Console.WriteLine($"{userName}! Ваш диагноз: {diagnose[countRigthAnswer]}");
                    if (!GetUserChoise("Хотите начать сначала?"))
                        break;
                }
                else
                {
                    Console.Write("Ошибка! Файлы не найдены!");
                    break;
                }
            }
        }
        static string[,] GetQuestionsAndAnswers()
        {
            string curFile = @"..\..\..\QuestionsAndAnswers.txt";
            bool existsDataFile = File.Exists(curFile);
            if (existsDataFile)
            {
                string[] readText = File.ReadAllLines(@"..\..\..\QuestionsAndAnswers.txt");
                string[,] questionsAndAnswers = new string[readText.Length, 2];
                for (int i = 0; i < readText.Length; i++)
                {
                    questionsAndAnswers[i,0] = (readText[i].Remove(0, 8)).Split(" Ответ:")[0];
                    questionsAndAnswers[i,1] = (readText[i].Remove(0, 8)).Split(" Ответ:")[1];
                }
                
                return questionsAndAnswers;
            }
            else
            {
                string[,] questionsAndAnswers = new string[0,0];
                return questionsAndAnswers;
            }
        }
        static int[] Shuffle(int countQuestions) 
        {
            int[] randoms = new int[countQuestions];
            for(int j = 0; j < countQuestions; j++)
            {
                randoms[j] = j;
            }
            Random random = new Random();
            int rnd;
            for (int i = 0; i < countQuestions; i++)
            {
                rnd = randoms[countQuestions - 1];
                int intermediateRandom = random.Next(0, countQuestions - 1);
                randoms[countQuestions - 1] = randoms[intermediateRandom];
                randoms[intermediateRandom] = rnd;
            }
            return randoms;
        }
        static string[] GetDiagnose()
        {
            string curFile = @"..\..\..\QuestionsAndAnswers.txt";
            bool existsDataFile = File.Exists(curFile);
            if (existsDataFile)
            {
                string[] readText = File.ReadAllLines(@"..\..\..\Diagnose.txt");//считываем из файла построчно диагнозы
                string[] diagnose = new string[readText.Length];
                for (int i = 0; i < readText.Length; i++)
                {
                    diagnose[i] = (readText[i]);
                }
                return diagnose;
            }
            else
            {
                string[] diagnose = new string[0];
                return diagnose;
            }
        }
        static double GetCorrectAnswer(string userAnswer)
        {
            int maxLengthAnswer = Convert.ToString(double.MaxValue).Length-1;
            if (userAnswer.Length > maxLengthAnswer)
            {
                userAnswer = userAnswer.Remove(maxLengthAnswer - 1);
            }
            double correctAnswer;
            bool statusAnswer = double.TryParse(userAnswer, out correctAnswer);
            return Math.Round(correctAnswer, 2);
        }
        static bool GetUserChoise(string message)
        {
            while(true)
            {
                Console.WriteLine($"{message} Введите Да или Нет");
                string escapeCommand = Console.ReadLine().ToLower();
                if (escapeCommand == "да" || escapeCommand == "lf")
                {
                    return true;
                }
                if (escapeCommand == "нет" || escapeCommand == "ytn")
                {
                    return false;
                }
            }
        }
    }
}