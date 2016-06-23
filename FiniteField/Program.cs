using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaloisField
{
    class Field
    {
        public const int order = 256;
        //Irreducible polynomial used : x^8 + x^4 + x^3 + x^2 + 1 (0x11D) with generator = 0x2
        public const int polynomial = 0x11D;
        public const byte generator = 0x2;
        //TEST
        //public const int polynomial = 0x11B;         
        //public const byte generator = 0x3;
        public static byte[] Exp;
        public static byte[] Log;

        private byte value;

        static Field()
        //generates Exp & Log table
        {
            Exp = new byte[order];
            Log = new byte[order];

            byte val = 0x01;
            for(int i=0; i<order; i++)
            {
                Exp[i] = val;
                if (i < order - 1)
                {
                    Log[val] = (byte)i;
                }
                val = multiply(generator,val);
            }
        }

        public Field()
        {
            value = 0; 
        }

        public Field(byte _value)
        {
            value = _value;
        }

        private static byte multiply(byte a, byte b) 
        //used only in Exp & Log table generation
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
                    aa ^= (polynomial & 0xFF);
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
            for(int i=0; i<Field.order; i++)
            {
                //Console.WriteLine(i.ToString("x") + " : "+Field.Log[i].ToString("x"));
                Console.WriteLine(i.ToString("x") + " : " + Field.Exp[i].ToString("x"));
                //Console.WriteLine(i.ToString("x") + " : " + Field.Exp[i].ToString("x")+", "+ Field.Log[i].ToString("x"));
            }
            Console.ReadLine();
        }
    }
}
