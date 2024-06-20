namespace GoldDiggers
{
    internal class Program
    {
        //monopoli
        static int maxX = 100;
        static int maxY = 100;
        static int[,] field;
        static int diamondCount = 0;
        static int diamondsCollected = 0;
        static int ourGuyRow;
        static int ourGuyCol;
        static void Main()
        {
            string answer = null;
            while (answer != "done")
            {
                GetFieldBounds();
                Console.WriteLine("You win!");
                Console.WriteLine("\n\n\n" +
                    "Do you want more?");
                answer = Console.ReadLine();
            }
        }
        static void GetFieldBounds()
        {
            Console.WriteLine("Input height of field");
            int height = int.Parse(Console.ReadLine());
            Console.WriteLine("Input length of field");
            int length = int.Parse(Console.ReadLine());

            while (height < 10 || height > maxX)
            {
                Console.WriteLine("Invalid input for height, please try again");
                height = int.Parse(Console.ReadLine());
            }
            while (length < 10 || length > maxY)
            {
                Console.WriteLine("Invalid input for length, please try again");
                length = int.Parse(Console.ReadLine());
            }
            CreateField(length, height);
        }
        static void CreateField(int cols, int rows)
        {
            field = new int[rows, cols];
            Random rnd = new Random();
            int num;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    num = rnd.Next(1, 100);
                    if (num >= 60)
                    {
                        field[row, col] = 1;//ground
                        continue;
                    }
                    if (num >= 30)
                    {
                        field[row, col] = 2;//grass
                        continue;
                    }
                    if (num >= 10)
                    {
                        field[row, col] = 3;//tree
                    }
                    else //tedo idea
                    {
                        field[row, col] = 4;//stone
                    }
                    num = rnd.Next(1, 10);
                    if (num == 1)
                    {
                        field[row, col] = 5; //diamond
                        diamondCount++;
                    }
                }
            }
            int rndCol = rnd.Next(0, cols);
            int rndRow = rnd.Next(0, rows);
            field[rndRow, rndCol] = 0; //ourguy

            ourGuyCol = rndCol;
            ourGuyRow = rndRow;
            Console.WriteLine("Generating Field...");

            Console.WriteLine($"There are {diamondCount} diamonds on the map");
            DrawField(rows, cols);
        }
        static void DrawField(int rows, int cols)
        {

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    switch (field[row, col])
                    {
                        case 0:
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write("☺"); //ourguy
                            break;
                        case 1:
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write("▒"); //ground
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("▓"); //grass
                            break;
                        case 3:
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("♣"); //tree
                            break;
                        case 4:
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("٥"); //stone
                            break;
                        case 5:
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("♦"); //diamond
                            break;
                    }
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            MoveOurGuy(rows, cols);
        }
        static void MoveOurGuy(int rows, int cols)
        {
            while (diamondCount - diamondsCollected != 0)
            {
                int moveRow = ourGuyRow;
                int moveCol = ourGuyCol;
                Console.WriteLine("Give input for direction and steps");
                string[] input = Console.ReadLine().Split().ToArray();
                string dir = input[0];
                int steps = int.Parse(input[1]);
                switch (dir)
                {
                    case "up":
                        moveRow -= steps;
                        break;
                    case "down":
                        moveRow += steps;
                        break;
                    case "left":
                        moveCol -= steps;
                        break;
                    case "right":
                        moveCol += steps;
                        break;
                    default:
                        Console.WriteLine("[Fall] OurGuy");
                        break;
                }
                if (moveRow < 0 || moveRow > rows)
                {
                    continue;
                }
                if (moveCol < 0 || moveCol > cols)
                {
                    continue;
                }

                field[ourGuyRow, ourGuyCol] = 1;
                ourGuyCol = moveCol;
                ourGuyRow = moveRow;

                if (field[ourGuyRow, ourGuyCol] == 5)
                {
                    diamondsCollected++;
                }
                field[ourGuyRow, ourGuyCol] = 0;

                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine($"There are {diamondCount - diamondsCollected} diamonds left");
                DrawField(rows, cols);
            }

        }
    }
}