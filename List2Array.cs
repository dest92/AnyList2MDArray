using System;

namespace listarray
{
    public static class ListHandling
    {
        public static string[,] ListArray<T>(List<T> list, string[] titles)
        {

            string[,] array = new string[list.Count + 1, titles.Length];

            //check if the lenght of the titles array is equal to the number of properties in the object
            if (titles.Length != typeof(T).GetProperties().Length)
            {
                throw new Exception("The number of titles is not equal to the number of properties in the object");
            }



            for (int i = 0; i < titles.Length; i++)
            {
                array[0, i] = titles[i];
            }

            for (int i = 1; i < list.Count + 1; i++)
            {
                for (int j = 0; j < titles.Length; j++)
                {
                    //set each value from the list to the array

                    for (int k = 0; k < list[i - 1].GetType().GetProperties().Length; k++)
                    {
                        if (list[i - 1].GetType().GetProperties()[k].Name == titles[j])
                        {
                            array[i, j] = list[i - 1].GetType().GetProperties()[k].GetValue(list[i - 1]).ToString();
                        }

                        //if name of the property no match with the title, set value from property
                        else
                        {
                            array[i, j] = list[i - 1].GetType().GetProperties()[j].GetValue(list[i - 1]).ToString();
                        }


                    }


                }
            }
            return array;
        }

        public static void ArraytoList<T>(string[,] array, List<T> list)
        {
            //check if the lenght of the titles array is equal to the number of properties in the object
            if (array.GetLength(1) != typeof(T).GetProperties().Length)
            {
                throw new Exception("The number of titles is not equal to the number of properties in the object");
            }

            for (int i = 1; i < array.GetLength(0); i++)
            {
                T obj = (T)Activator.CreateInstance(typeof(T));

                for (int j = 0; j < array.GetLength(1); j++)
                {
                    for (int k = 0; k < obj.GetType().GetProperties().Length; k++)
                    {
                        if (obj.GetType().GetProperties()[k].Name == array[0, j])
                        {
                            obj.GetType().GetProperties()[k].SetValue(obj, array[i, j]);
                        }
                    }
                }
                list.Add(obj);
            }
        }

        public static void DrawTable(string[,] array)
        {
            //Draw a table with borders, columns, rows and titles

            //set column width
            int[] columnWidth = new int[array.GetLength(1)];

            for (int i = 0; i < array.GetLength(1); i++)
            {
                columnWidth[i] = array[0, i].Length;
            }


            for (int i = 1; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j].Length > columnWidth[j])
                    {
                        columnWidth[j] = array[i, j].Length;
                    }
                }
            }

            for (int i = 0; i < array.GetLength(1); i++)
            {
                columnWidth[i] += 2;
            }

            for (int i = 0; i < array.GetLength(1); i++)
            {
                // Console.Write("+");
                for (int j = 0; j < columnWidth[i]; j++)
                {
                    Console.Write("═");
                }
            }

            Console.WriteLine("╬");

            //Draw titles
            for (int i = 0; i < array.GetLength(0); i++)
            {

                //draw data
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write("║");
                    Console.Write(array[i, j]);
                    for (int k = 0; k < columnWidth[j] - array[i, j].Length; k++)
                    {
                        Console.Write(" ");
                    }
                }
                //Console.WriteLine("║");
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    // Console.Write("+");
                    for (int k = 0; k < columnWidth[j]; k++)
                    {
                        //Console.Write("═");
                    }
                }
                //Console.WriteLine("+");
            }

        }
    }
}