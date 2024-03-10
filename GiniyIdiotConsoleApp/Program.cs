using System;
using System.Formats.Tar;
using System.Globalization;

namespace GeniyIdiotConsoleApp
{
    class Program
    {
        static string[] GetQuestions()
        {
            string curFile = @"..\..\..\QuestionsAndAnswers.txt";
            bool existsDataFile = File.Exists(curFile);
            if (existsDataFile)
            {
                string[] readText = File.ReadAllLines(@"..\..\..\QuestionsAndAnswers.txt");//считываем из файла построчно вопросы
                string[] questions = new string[readText.Length / 2];
                for (int i = 0, j = 0; i < readText.Length; i += 2, j++)
                {
                    questions[j] = readText[i];
                }
                return questions;
            }
            else
            {
                string[] questions = new string[0];
                return questions;
            }
        }// возвращает вопросы
        static double[] GetAnswers() // возвращает ответы// теперь возвращает double
        {
            string curFile = @"..\..\..\QuestionsAndAnswers.txt";
            bool existsDataFile = File.Exists(curFile);
            if (existsDataFile)
            {
                string[] readText = File.ReadAllLines(@"..\..\..\QuestionsAndAnswers.txt");//считываем из файла построчно ответы
                double[] answers = new double[readText.Length / 2];
                for (int i = 1, j = 0; i < readText.Length; i += 2, j++)
                {
                    answers[j] = Convert.ToDouble(readText[i]);
                }
                return answers;
            }
            else
            {
                double[] answers = new double[0];
                return answers;
            }
        }
        static int[] Shuffle(int countQuestions) // Генерация случайного порядка для вопросов
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
        }// возвращает диагнозы
        static double VerificationAnswer(string userAnswer)
        {
            int maxLengthAnswer = 9;
            if (userAnswer.Length > maxLengthAnswer)
            {
                userAnswer = userAnswer.Remove(maxLengthAnswer - 1);
            }
            double correctAnswer;
            bool statusAnswer = double.TryParse(userAnswer, out correctAnswer);
            return correctAnswer;
        }// Проверка на корректный ввод
        static void Main() // начинать программу с main
        {
            Console.WriteLine("Введите имя пользователя");
            string userName = Console.ReadLine();
            userName = char.ToUpper(userName[0]) + userName.Remove(0, 1); // Обращение всегда с Заглавной буквы
            while (true)
            {
                int countRigthAnswer = 0;
                string[] questions = GetQuestions();
                double[] answers = GetAnswers();
                string[] diagnose = GetDiagnose();
                int countQuestions = answers.Length;
                int[] randoms = Shuffle(countQuestions);
                if (answers.Length > 0 && diagnose.Length > 0) 
                {
                    Console.WriteLine("На вопрос даётся 10 сек. Если готовы нажмите клавишу ENTER.");
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }//считали клавишу и сравнили с ENTER
                    //Здесь предполагается таймер
                    for (int i = 0; i < countQuestions; i++)
                    {
                        Console.WriteLine($"Вопрос №{i + 1}");//выделить в отдельный метод
                        Console.WriteLine(questions[randoms[i]]);
                        string userAnswer = Console.ReadLine();
                        if (VerificationAnswer(userAnswer) == answers[randoms[i]])
                        {
                            countRigthAnswer++;
                        }
                    }
                    Console.WriteLine($"Количество правильных ответов: {countRigthAnswer}");
                    Console.WriteLine($"{userName}! Ваш диагноз: {diagnose[countRigthAnswer]}");
                    Console.WriteLine($"{userName}! Хотите пройти тест еще раз? Введите Да или Нет");//Вынети в отдельный метод
                    string escapeCommand = Console.ReadLine().ToLower();// привести к одному регистру
                    if (escapeCommand == "да" || escapeCommand == "lf")
                    {
                        continue;
                    }
                    else if (escapeCommand == "нет" || escapeCommand == "ytn")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод!"); // зациклить пока не будет ДА или НЕТ
                        break;
                    }
                }
                else
                {
                    Console.Write("Ошибка! Файлы не найдены!");//Не закрывать сразу, например через 10 секунд
                    break;
                }
            }
        }
    }
}