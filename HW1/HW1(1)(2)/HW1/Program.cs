class Program
{
    //1）编写helloworld程序
    static void HelloWorld()
    {
        Console.WriteLine("Hello, World!");
    }

    //2）素数因子枚举
    static string GetPrimeFactors(int number)
    {
        if (number <= 1)
        {
            return string.Empty; 
        }
        string primeFactors = "";
        bool isFirstFactor = true;
        while (number % 2 == 0)
        {
            if (!isFirstFactor)
            {
                primeFactors += ", ";
            }
            else
            {
                isFirstFactor = false;
            }
            primeFactors += "2";
            number /= 2;
        }
        for (int factor = 3; factor * factor <= number; factor += 2)
        {
            while (number % factor == 0)
            {
                if (!isFirstFactor)
                {
                    primeFactors += ", ";
                }
                else
                {
                    isFirstFactor = false;
                }
                primeFactors += factor.ToString();
                number /= factor;
            }
        }
        if (number > 2)
        {
            if (!isFirstFactor)
            {
                primeFactors += ", ";
            }
            primeFactors += number.ToString();
        }

        return primeFactors;
    }

    static void Main(string[] args)
    {
        //1）题
        //HelloWorld();

        //2）题
        Console.Write("请输入一个数字: ");
        int number = Convert.ToInt32(Console.ReadLine());
        string primeFactors = GetPrimeFactors(number);
        Console.WriteLine($"数字 {number} 的所有素数因子为: {primeFactors}");
    }


}