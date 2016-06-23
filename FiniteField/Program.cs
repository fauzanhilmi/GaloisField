using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaloisField
{
    class Field
    {
        //Irreducible polynomial used : x^8 + x^4 + x^3 + x^2 + 1 (0x11D)
        public const byte polynomial = 0x1D; //0x11D & 0xFF
        public static byte[] Exp;
        public static byte[] Log;

        private byte number;

        static Field()
        {

        }

        public Field()
        {
            number = 0; 
        }

        public Field(byte _number)
        {
            number = _number;
        }

        private byte multiply(byte a, byte b) 
        //used in exp & log table generation
        //implemented with Russian Peasant Multiplication algorithm
        {
            byte result = 0;
            byte aa = a;
            byte bb = b;
            while (bb != 0)
            {
                if ((bb & 1) != 0)
                {
                    result ^= aa;
                }
                byte highest_bit = (byte)(aa & 0x80);
                aa <<= 1;
                if (highest_bit != 0)
                {
                    aa ^= polynomial;
                }
                bb >>= 1;
            }
            return result;
        }

    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Field f = new Field();
            //Console.WriteLine(f.multiply(255,239));
            Console.ReadLine();
        }
    }
}
