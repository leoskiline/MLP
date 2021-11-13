using System;

namespace MLP
{
    class Program
    {
		int[,] matriz = new int[4, 3];

		int[,] AND()
		{
			matriz[0,0] = 0;
			matriz[0,1] = 0;
			matriz[0,2] = 0;

			matriz[1,0] = 0;
			matriz[1,1] = 1;
			matriz[1,2] = 0;

			matriz[2,0] = 1;
			matriz[2,1] = 0;
			matriz[2,2] = 0;

			matriz[3,0] = 1;
			matriz[3,1] = 1;
			matriz[3,2] = 1;

			return matriz;
		}

		int[,] OR()
		{
			matriz[0,0] = 0;
			matriz[0,1] = 0;
			matriz[0,2] = 0;

			matriz[1,0] = 0;
			matriz[1,1] = 1;
			matriz[1,2] = 1;

			matriz[2,0] = 1;
			matriz[2,1] = 0;
			matriz[2,2] = 1;

			matriz[3,0] = 1;
			matriz[3,1] = 1;
			matriz[3,2] = 1;

			return matriz;
		}

		int[,] XOR()
		{
			matriz[0,0] = 0;
			matriz[0,1] = 0;
			matriz[0,2] = 0;

			matriz[1,0] = 0;
			matriz[1,1] = 1;
			matriz[1,2] = 1;

			matriz[2,0] = 1;
			matriz[2,1] = 0;
			matriz[2,2] = 1;

			matriz[3,0] = 1;
			matriz[3,1] = 1;
			matriz[3,2] = 0;

			return matriz;
		}

		int[,] XNOR()
		{
			matriz[0,0] = 0;
			matriz[0,1] = 0;
			matriz[0,2] = 1;

			matriz[1,0] = 0;
			matriz[1,1] = 1;
			matriz[1,2] = 0;

			matriz[2,0] = 1;
			matriz[2,1] = 0;
			matriz[2,2] = 0;

			matriz[3,0] = 1;
			matriz[3,1] = 1;
			matriz[3,2] = 1;

			return matriz;
		}


		static void Main(string[] args)
        {
            Console.WriteLine("MULTI LAYER PERCEPTRON");
			Console.WriteLine("1 - AND");
			Console.WriteLine("2 - OR");
			Console.WriteLine("3 - XOR");
			Console.WriteLine("4 - XNOR");
			string entradas = Console.ReadLine();
        }
    }
}
