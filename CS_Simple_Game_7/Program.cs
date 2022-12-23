/*  https://github.com/UN7FGO/CS_Simple_Games

    Пошаговая приключенческая игра "Охота на Вампуса".
    Суть игры: 
    Вы попали в сеть пещер и у Вас есть только 3 стрелы.
    В пещерах встречаются:
    - ямы, провалившись в которые Вы погибаете;
    - летучие мыши, которые могут унести Вас в любую другую пещеру
    - Вампус - злобный монстр, при столкновении с которым Вы погибнете.
    Ваша задача убить Вампуса первым, не провалившись в яму.
    
    Мы будем использовать новые методы и конструкции языка C#, такие как:
    - Console.ForegroundColor - изменение цвета выводимого текста
    - void - научимся создавать методы, которые возвращают значения или которые не возвращают значений
*/

// создаем двумерный массив для игрового поля, с уже заданной комбинацией замков
// 0 - пустое поле, 1 - стена пещеры, 2 - яма, 3 - летучая мышь, 4 - Вампус, 5 - игрок
int[,] cave = new int[12, 12] { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                { 1, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                { 1, 0, 0, 3, 0, 0, 3, 0, 2, 0, 0, 1 },
                                { 1, 0, 2, 0, 0, 0, 2, 0, 0, 0, 3, 1 },
                                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                { 1, 0, 0, 0, 2, 0, 0, 0, 0, 3, 0, 1 },
                                { 1, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                { 1, 0, 0, 0, 0, 0, 3, 0, 2, 0, 0, 1 },
                                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                { 1, 2, 0, 0, 0, 0, 2, 0, 4, 0, 0, 1 },
                                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },};
// создаем объект для счетчика случайных чисел
Random rnd = new Random();
// ответ игрока
int answer = 0;
// координаты нашего героя
int x = 1;
int y = 1;
// переменные для команд
int com = 0;
int dir = 0;
// количество стрел
int arrow = 3;
// был ли выстрел
bool shot = false;
// нас унесла летучая мышь
bool bat = false;
// погиб ли наш игрок в яме или от лап Вампуса?
bool deadPit = false;
bool deadWamp = false;
// удалось ли нам победить Вампуса?
bool winWamp = false;

// выводим на экран правила игры
Console.Clear();
Console.WriteLine("Пошаговая приключенческая игра 'Охота на Вампуса'.");
Console.WriteLine();
Console.WriteLine("Вы попали в сеть пещер и у Вас есть только 3 стрелы.");
Console.WriteLine("В пещерах встречаются:");
Console.WriteLine("- ямы - провалившись в которые Вы погибаете;");
Console.WriteLine("- летучие мыши - которые могут унести Вас в любую другую пещеру");
Console.WriteLine("- Вампус - злобный монстр, при столкновении с которым Вы погибнете.");
Console.WriteLine("Ваша задача убить Вампуса первым, не провалившись в яму.");
Console.WriteLine();
Console.WriteLine("Вы вводите с клавиатуры команды, состоящие из двух цифр (двузначное число).");
Console.WriteLine("Команда состоит из цифры действия и цифры направления. Команда 99 - выход из игры");
Console.WriteLine();
Console.WriteLine("Для начала игры, нажмите клавишу [ Enter ]");
var s = Console.ReadLine();

while (!deadPit && !deadWamp && !winWamp && arrow > 0)
{
    // выводим карту с расположением игрока на ней
    Console.Clear();
    Console.WriteLine("Карта пещер");
    PrintCave(cave);
    // Если перед этим был сделан выстрел
    if (shot) Console.WriteLine("К сожалению Ваша стрела улетела в пустоту пещеры, в которой небыло Вампуса.");
    if (shot) shot = false;
    // Проверяем не перенесла ли нас летающая мышь
    if (bat) Console.WriteLine("Вы попали в пещеру к летающей мыши и она унесла Вас в другое место.");
    if (bat) bat = false;
    // Проверяем наличие в соседней пещере ямы
    if (CheckPit(cave, y, x)) Console.WriteLine("Подул легкий сквозняк от ямы в соседней пещере.");
    // Проверяем наличие в соседней пещере летучей мыши
    if (CheckBat(cave, y, x)) Console.WriteLine("Вы слышите шорох крыльев летучей мыши из соседней пещеры.");
    // Проверяем наличие в соседней пещере Вампуса
    if (CheckWampus(cave, y, x)) Console.WriteLine("Из соседней пещеры слышен рык раздраженного Вампуса.");
    // выводим информацию к каждому ходу игрока
    Console.WriteLine();
    Console.WriteLine($"Осталось стрел - {arrow}");
    Console.WriteLine("Действие: 1 - идти, 2 - выстрелить.");
    Console.WriteLine("Направление: 1 - вверх, 2 - вправо, 3 - вниз, 4 - влево.");
    com = 0;
    dir = 0;
    // пока игрок не введет правильную команду повторяем ввод
    while (com < 1 || com > 2 || dir < 1 || dir > 4)
    {
        Console.Write("Введите команду (двузначное число, 99 - выход) :");
        answer = Convert.ToInt32(Console.ReadLine());
        // декодируем команды
        com = answer / 10;
        dir = answer % 10;
        if (answer == 99) break;
    }
    // если игрок "сдался" выходим из игры
    if (answer == 99) break;
    // освобождаем пещеру, где находился игрок
    cave[y, x] = 0;
    // отрабатываем введенную игроком команду на перемещение
    if (com == 1)
    {
        switch (dir)
        {
            // идем вверх
            case 1:
                // попали в яму или к Вампусу, значит погибли
                if (cave[y - 1, x] == 2) deadPit = true;
                if (cave[y - 1, x] == 4) deadWamp = true;
                // попались в когти летучей мыши, нас уносят в неизвестном направлении
                if (cave[y - 1, x] == 3) BatTransfer();
                // пустая пещера, можно идти
                if (cave[y - 1, x] == 0) y--;
                break;
            // идем вправо
            case 2:
                if (cave[y, x + 1] == 2) deadPit = true;
                if (cave[y, x + 1] == 4) deadWamp = true;
                if (cave[y, x + 1] == 3) BatTransfer();
                if (cave[y, x + 1] == 0) x++;
                break;
            // идем вниз
            case 3:
                if (cave[y + 1, x] == 2) deadPit = true;
                if (cave[y + 1, x] == 4) deadWamp = true;
                if (cave[y + 1, x] == 3) BatTransfer();
                if (cave[y + 1, x] == 0) y++;
                break;
            // идем влево
            case 4:
                if (cave[y, x - 1] == 2) deadPit = true;
                if (cave[y, x - 1] == 4) deadWamp = true;
                if (cave[y, x - 1] == 3) BatTransfer();
                if (cave[y, x - 1] == 0) x--;
                break;
            default:
                break;
        }
    }
    // помещаем игрока в новую пещеру
    cave[y, x] = 5;
    // отрабатываем команду на выстрел в соседнюю пещеру
    if (com == 2)
    {   
        shot = true;
        switch (dir)
        {
            // стреляем вверх
            case 1:
                // попали ли мы в Вампуса ?
                if (cave[y - 1, x] == 4) winWamp = true;
                // уменьшаем количество стрел
                arrow--;
                break;
            // стреляем вправо
            case 2:
                if (cave[y, x + 1] == 4) winWamp = true;
                arrow--;
                break;
            // стреляем вниз
            case 3:
                if (cave[y + 1, x] == 4) winWamp = true;
                arrow--;
                break;
            // стреляем влево
            case 4:
                if (cave[y, x - 1] == 4) winWamp = true;
                arrow--;
                break;
            default:
                break;
        }
    }

}
// обрабатываем окончание игры
Console.WriteLine();
if (deadPit) Console.WriteLine("К сожалению, Вы свалились в бездонную яму и погибли.  Вы проиграли.");
if (deadWamp) Console.WriteLine("Вы зашли в пещеру, где Вас растерзал злой Вампус.  Вы проиграли.");
if (winWamp) Console.WriteLine("ПОЗДРАВЛЯЮ!!! Вы победили злого Вампуса! Теперь вы просто герой!");
if (!winWamp && arrow ==0) Console.WriteLine("У Вас закончились стрелы и Вам нечем будет убивать Вампуса. Вы проиграли.");

// если игрок вышел не закончив игру
if (answer == 99)
{
    Console.Clear();
    Console.WriteLine("Зря Вы не дошли до конца, все было так просто.");
    Console.WriteLine();
    PrintAllMap(cave);
}
Console.WriteLine();



// ---------------------------------------------------------------
// ----- далее описаны методы, используемые нами в программе -----
// ---------------------------------------------------------------

// метод переноса нас летучей мышью в пустую пещеру
void BatTransfer()
{
    x = 0; y = 0;
    while (cave[y, x] != 0)
    {
        x = rnd.Next(1, 10);
        y = rnd.Next(1, 10);
    }
    bat = true;
}

// метод проверки на наличие ямы в соседней с игроком пещере
bool CheckPit(int[,] arr, int row, int col)
{
    bool result = false;
    if (arr[row, col + 1] == 2 ||
        arr[row + 1, col] == 2 ||
        arr[row, col - 1] == 2 ||
        arr[row - 1, col] == 2) result = true;
    return result;
}

// метод проверки на наличие ямы в соседней с игроком пещере
bool CheckBat(int[,] arr, int row, int col)
{
    bool result = false;
    if (arr[row, col + 1] == 3 ||
        arr[row + 1, col] == 3 ||
        arr[row, col - 1] == 3 ||
        arr[row - 1, col] == 3) result = true;
    return result;
}

// метод проверки на наличие Вампуса в соседней с игроком пещере
bool CheckWampus(int[,] arr, int row, int col)
{
    bool result = false;
    if (arr[row, col + 1] == 4 ||
        arr[row + 1, col] == 4 ||
        arr[row, col - 1] == 4 ||
        arr[row - 1, col] == 4) result = true;
    return result;
}

// метод вывода на экран карты только с указанием стен и игрока
void PrintCave(int[,] arr)
{
    for (int i = 0; i < arr.GetLength(0); i++)
    {
        for (int j = 0; j < arr.GetLength(1); j++)
            switch (arr[i, j])
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" · ");
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("###");
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("<H>");
                    break;
                default:
                    Console.Write(" · ");
                    break;
            }
        Console.WriteLine();
    }
}

// метод вывода на экран всей карты
void PrintAllMap(int[,] arr)
{
    for (int i = 0; i < arr.GetLength(0); i++)
    {
        for (int j = 0; j < arr.GetLength(1); j++)
            switch (arr[i, j])
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" · ");
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("###");
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("( )");
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("^o^");
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("OnO");
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("<H>");
                    break;
                default:
                    Console.Write("   ");
                    break;
            }
        Console.WriteLine();
    }
}
