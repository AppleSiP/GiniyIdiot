using System;

namespace GeniyIdiotConsoleApp
{
    class Program
    {
        static string[] GetQuestions(int countQuestions)
        {
            string[] questions = new string[countQuestions];
            questions[0] = "Сколько будет два плюс два умноженное на два?";
            questions[1] = "Бревно нужно распилить на 10 частей. Сколько распилов нужно сделать?";
            questions[2] = "На двух руках 10 пальцев. Сколько пальцев на 5 руках?";
            questions[3] = "Укол делают каждые полчаса. Сколько нужно минут, чтобы сделать три укола?";
            questions[4] = "Пять свечей горело, две потухли. Сколько свечей осталось?";
            return questions;
        }// Список вопросов
        static int[] GetAnswers(int countQuestions)
        {
            int[] answers = new int[countQuestions];
            answers[0] = 6;
            answers[1] = 9;
            answers[2] = 25;
            answers[3] = 60;
            answers[4] = 2;
            return answers;
        }// Список ответов
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
        }// Генерация случайного порядка для вопросов
        static string[] GetDiagnose(int countQuestions)
        {
            string[] diagnose = new string[countQuestions + 1];
            diagnose[0] = "кретин";
            diagnose[1] = "идиот";
            diagnose[2] = "дурак";
            diagnose[3] = "нормальный";
            diagnose[4] = "талант";
            diagnose[5] = "гений";
            return diagnose;
        }// Список диагнозов
        static void Main()
        {
            Console.WriteLine("Введите имя пользователя");
            string userName = Console.ReadLine();
            userName = char.ToUpper(userName[0]) + userName.Remove(0, 1); // Обращение всегда с Заглавной буквы
            while (true)
            {
                int countQuestions = 5; 
                int countRigthAnswer = 0;
                string[] questions = GetQuestions(countQuestions);
                int[] answers = GetAnswers(countQuestions);
                string[] diagnose = GetDiagnose(countQuestions);
                int[] randoms = Shuffle(countQuestions);
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
                Console.WriteLine($"{userName}! Хотите пройти тест еще раз? введите Да или Нет");
                string escapeCommand = Console.ReadLine();
                if (escapeCommand == "Да" || escapeCommand == "да" || escapeCommand == "Lf" || escapeCommand == "lf")
                    continue;
                else if (escapeCommand == "Нет" || escapeCommand == "нет" || escapeCommand == "Ytn" || escapeCommand == "ytn")
                    break;
                else
                {
                    Console.WriteLine("Некорректный ввод");
                    break;
                }
            }
        }
    }
}