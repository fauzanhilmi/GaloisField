using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaloisField
{
    class FieldTable
    {
        const byte polynomial = 0x1D; //from 0x11D
        byte[] Exp;
        byte[] Log;

        public byte multiply (byte a, byte b) //using Russian Peasant Multiplication algorithm
        {
            byte result = 0;
            byte aa = a;
            byte bb = b;
            while(bb != 0)
            {
                if ((bb & 1) != 0)
                {
                    result ^= aa;
                }
                byte highest_bit = (byte) (aa & 0x80);
                aa <<= 1;
                if(highest_bit != 0)
                {
                    aa ^= polynomial;
                }
                bb >>= 1;
            }
            return result;
        }
    }

    class Field
    {
        private byte number;

        Field()
        {
            number = 0; 
        }

        Field(byte _number)
        {
            number = _number;
        }

    }
    
    class Program
    {
        static void Main(string[] args)
        {
            FieldTable ft = new FieldTable();
            Console.WriteLine(ft.multiply(255,239));
            Console.ReadLine();
        }
    }
}
