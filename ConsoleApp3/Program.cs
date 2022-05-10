using System;
using System.IO;

namespace ConsoleApp3
{
    //Сделать игровую карту с помощью двумерного массива. Сделать функцию рисования карты.
    //Помимо этого, дать пользователю возможность перемещаться по карте и взаимодействовать с элементами (например пользователь не может пройти сквозь стену)
    //Все элементы являются обычными символами
    class Program
    {
        static void Main(string[] args)
        {
            bool isActive = true;
            int playerX;
            int playerY;
            int playerDX = 0;
            int playerDY = 0;

            Console.CursorVisible = false;

            char[,] map = ReadMap("map", out playerX, out playerY);

            DrawMap(map);



            while (isActive)
            {
                Console.SetCursorPosition(playerY, playerX);
                Console.Write('@');

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            MoveUp(ref playerDX, ref playerDY);
                            break;
                        case ConsoleKey.DownArrow:
                            MoveDown(ref playerDX, ref playerDY);
                            break;
                        case ConsoleKey.LeftArrow:
                            MoveLeft(ref playerDX, ref playerDY);
                            break;
                        case ConsoleKey.RightArrow:
                            MoveRight(ref playerDX, ref playerDY);
                            break;
                    }

                    if (map[playerX + playerDX, playerY + playerDY] != '#')
                    {
                        Move(ref playerX, ref playerY, ref playerDX, ref playerDY);
                    }
                }
            }
        }

        static char[,] ReadMap(string mapName, out int playerX, out int playerY)
        {
            playerX = 0;
            playerY = 0;

            string[] mapFile = File.ReadAllLines($"Maps/{mapName}.txt");
            char[,] map = new char[mapFile.Length, mapFile[0].Length];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = mapFile[i][j];

                    if (map[i, j] == '@')
                    {
                        playerX = i;
                        playerY = j;
                    }
                }
            }

            return map;
        }

        static void DrawMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void Move(ref int playerX, ref int playerY, ref int playerDX, ref int playerDY)
        {
            Console.SetCursorPosition(playerY, playerX);
            Console.Write(" ");

            playerX += playerDX;
            playerY += playerDY;

            Console.SetCursorPosition(playerY, playerX);
            Console.Write('@');
        }

        static void MoveUp(ref int playerDX, ref int playerDY)
        {
            playerDX = -1;
            playerDY = 0;
        }

        static void MoveDown(ref int playerDX, ref int playerDY)
        {
            playerDX = 1;
            playerDY = 0;
        }

        static void MoveLeft(ref int playerDX, ref int playerDY)
        {
            playerDX = 0;
            playerDY = -1;
        }

        static void MoveRight(ref int playerDX, ref int playerDY)
        {
            playerDX = 0;
            playerDY = 1;
        }
    }
}
