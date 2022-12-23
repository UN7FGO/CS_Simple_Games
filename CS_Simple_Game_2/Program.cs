﻿/*  https://github.com/UN7FGO/CS_Simple_Games
    
    Вторая наша игра потребует от нас взаимодействия с пользователем.
    Нам необходимо будет научиться получать от него ответы на наши вопросы в виде чисел.

    Мы будем использовать новые конструкции языка C#, такие как:
    - if () then / else -  условный оператор или оператор ветвления
    - Convert.ToInt32() - преобразование введенных символов в целое число
    - Random rnd = new Random(); и rnd.Next(); - для генерации случайных чисел
*/

Console.Clear();
Console.WriteLine("Сейчас мы проверим занание Вами таблицы умножения.");
Console.WriteLine();

// создаем объект для счетчика случайных чисел
Random rnd = new Random();

// генерируем два случайных числа от 1 до 9 и запоминаем их
int a = rnd.Next(1, 9);
int b = rnd.Next(1, 9);

// задаем вопрос и ждем ответ
Console.Write($"Чему будет равно произведение {a} на {b} ? = ");
int otvet = Convert.ToInt32(Console.ReadLine());

// проверяем ответ
if (otvet == a * b) Console.Write("Правильно! ");
else Console.Write("Неправильно! ");

// выводим правильный ответ
Console.WriteLine($" {a} x {b} = {a * b}");
