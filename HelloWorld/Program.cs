using System;

internal class HelloWorld
{
  static void Main(string[] args)
  {
    int[] array = new int[10];
    Random random = new Random();

    for (int i = 0; i < array.Length; i++)
    {
      array[i] = random.Next(1, 100);
    }
    
    foreach (int number in array)
    {
      Console.WriteLine(number);
    }
  }
}