public void PrintPositiveNumbers(int[] numbers)
{
    foreach (var number in numbers)
    {
        if (number > 0)
        {
            Console.WriteLine(number);
        }
    }
}
