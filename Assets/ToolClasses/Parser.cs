using System;
using System.Text.RegularExpressions;

public class Parser
{
    public static string ParseAndSubtract(string input)
    {
        // Используем регулярное выражение для поиска числа перед "x" и самой буквы "x"
        var match = Regex.Match(input, @"^(\d+)x$");

        // Если строка соответствует формату "числоx"
        if (match.Success)
        {
            // Извлекаем число и преобразуем его в целое число
            int number = int.Parse(match.Groups[1].Value);

            // Отнимаем 1 от числа
            number -= 1;

            // Формируем строку в формате "числоx" и возвращаем результат
            return number + "x";
        }
        else
        {
            // Если формат не соответствует, возвращаем ошибку или исходную строку
            return "Invalid input";
        }
    }
}