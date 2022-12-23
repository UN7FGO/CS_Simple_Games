/*  https://github.com/UN7FGO/CS_Simple_Games

    Первая наша игра будет совсем простой и немного шуточнной.
    Надеюсь вы догадаетесь почему у программы получается угадывать результаты.

    Игра потребует от нас минимум знаний о языке программирования C# .
    Мы используем в программе следующие методы:
    - Console.Clear() - очищение окна терминала
    - Console.WriteLine() - вывод строки текста на экран, с переводом курсора на следующую строку
    - Console.ReadLine() - ввод данных с клавиатуры, с переводом курсора на следующую строку
*/

Console.Clear();
Console.WriteLine("Сейчас, программа с помощью телепатии угадает результат Вашей умственной деятельности.");
Console.WriteLine();

Console.WriteLine("Загадайте число от 1 до 9 ...   и нажмите [ Enter ].");
var key = Console.ReadLine();

Console.WriteLine("Теперь прибавте к нему 3 ...   и нажмите [ Enter ].");
key = Console.ReadLine();

Console.WriteLine("Умножте результат на 2 ...   и нажмите [ Enter ].");
key = Console.ReadLine();

Console.WriteLine("Прибавьте к результату 8 ...   и нажмите [ Enter ].");
key = Console.ReadLine();

Console.WriteLine("Разделите получившееся значение на 2 ...   и нажмите [ Enter ].");
key = Console.ReadLine();

Console.WriteLine("Отнимите загаданное Вами число ...   и нажмите [ Enter ].");
key = Console.ReadLine();

Console.WriteLine("Прибавьте к результату 3 ...   и нажмите [ Enter ].");
key = Console.ReadLine();

Console.WriteLine("В результате вычислений, у Вас получилось 10 .");