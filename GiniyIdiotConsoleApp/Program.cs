using System;
using System.Globalization;

namespace GeniyIdiotConsoleApp
{
    class Program
    {
        static string[] GetQuestions()
        {
            string[] readText = File.ReadAllLines(@"QuestionsAndAnswers.txt");//считываем из файла построчно вопросы
            string[] questions = new string[readText.Length / 2];
            for (int i = 0, j = 0; i < readText.Length; i = i + 2, j++)
            {
                questions[j] = readText[i];
            }
            return questions;
        }
        static int[] GetAnswers()
        {
            string[] readText = File.ReadAllLines(@"QuestionsAndAnswers.txt");//считываем из файла построчно ответы
            int[] answers = new int[readText.Length / 2];
            for (int i = 1, j = 0; i < readText.Length; i = i + 2, j++)
            {
                answers[j] = Convert.ToInt32(readText[i]);
            }
            return answers;
        }
        static int[] Shuffle(int countQuestions) //Генерация случайного порядка для вопросов
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
        }// 
        static string[] GetDiagnose()
        {
            string[] readText = File.ReadAllLines(@"Diagnose.txt");//считываем из файла построчно диагнозы
            string[] diagnose = new string[readText.Length];
            for (int i = 0; i < readText.Length; i++)
            {
                diagnose[i] = (readText[i]);
            }
            return diagnose;
        }
        static void Main()
        {
            Console.WriteLine("Введите имя пользователя");
            string userName = Console.ReadLine();
            userName = char.ToUpper(userName[0]) + userName.Remove(0, 1); // Обращение всегда с Заглавной буквы
            while (true)
            {
                int countRigthAnswer = 0;
                string[] questions = GetQuestions();
                int[] answers = GetAnswers();
                string[] diagnose = GetDiagnose();
                int countQuestions = answers.Length;
                int[] randoms = Shuffle(countQuestions);
                Console.WriteLine("На вопрос даётся 10 сек. Если готовы нажмите клавишу ENTER.");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }//считали клавишу и сравнили с ENTER
                //Здесь предполагается таймер
                for (int i = 0; i < countQuestions; i++)
                {
                    Console.WriteLine($"Вопрос №{i + 1}");
                    Console.WriteLine(questions[randoms[i]]);
                    int userAnswer = Convert.ToInt32(Console.ReadLine());
                    int rigthAnswer = answers[randoms[i]];
                    if (userAnswer == rigthAnswer)
                        countRigthAnswer++;
                }
                Console.WriteLine($"Количество правильных ответов: {countRigthAnswer}");
                Console.WriteLine($"{userName}! Ваш диагноз: {diagnose[countRigthAnswer]}");
                Console.WriteLine($"{userName}! Хотите пройти тест еще раз? Введите Да или Нет");
                string escapeCommand = Console.ReadLine();
                if (escapeCommand == "Да" || escapeCommand == "Lf" || escapeCommand == "да" || 
                    escapeCommand == "lf" || escapeCommand == "ДА" || escapeCommand == "LF")
                    continue;
                else if (escapeCommand == "Нет" || escapeCommand == "Ytn" || escapeCommand == "ytn" || 
                    escapeCommand == "нет" || escapeCommand == "YTN" || escapeCommand == "НЕТ")
                    break;
                else
                {
                    Console.WriteLine("Некорректный ввод!");
                    break;
                }
            }
        }
    }
}
