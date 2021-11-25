using System;

namespace MLP
{
    class Program
    {
		int[,] matriz = new int[4, 3];
		int epocas = 20;
		int[,] pesosEntrada = new int[2,2];
		int[,] pesosSaida = new int[2, 2];
		int[,] netsOculta = new int[1,2];
		int[,] netsSaida = new int[1, 2];



		// Camada Oculta multiplicar por entrada 1 e entrada 2.
		// ErroS1 = DesejeadoS1 - iS1
		// ErrroS2 = DesejadoS2 - iS2
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

		double fNet(double valor)
        {
			return valor/2;
        }

		double getI(double net)
        {
			return net / 2;
        }

		double getNet(double entrada1,double peso1,double entrada2,double peso2)
        {
			return (entrada1*peso1)+(entrada2*peso2);
        }

		double getErroNeuronio(double desejado,double i,double fnet)
        {
			return (desejado - i) * fnet;
        }

		double getErroRede(double desejado,int i)
        {
			return 0.5 * Math.Pow(desejado - i,2);
        }

		double getNewPeso(double peso,double taxaAprendizado,double erro, double i)
        {
			return peso + (taxaAprendizado * erro * i);
        }

		static void Main(string[] args)
        {
            Console.WriteLine("MULTI LAYER PERCEPTRON");
			Console.WriteLine("1 - AND");
			Console.WriteLine("2 - OR");
			Console.WriteLine("3 - XOR");
			Console.WriteLine("4 - XNOR");
			string entradas = Console.ReadLine();
            switch (entradas)
            {
				case "1":

					break;
				case "2":

					break;
				case "3":

					break;
				case "4":

					break;
            }
        }
    }
}
