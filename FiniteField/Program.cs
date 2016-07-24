using System;

namespace GaloisField
{
    class Field
    {
        public const int order = 256;
        //irreducible polynomial used : x^8 + x^4 + x^3 + x^2 + 1 (0x11D)
        public const int polynomial = 0x11D;
        //generator to be used in Exp & Log table generation
        public const byte generator = 0x2;
        public static byte[] Exp;
        public static byte[] Log;

        private byte value;

        public Field()
        {
            value = 0;
        }

        public Field(byte _value)
        {
            value = _value;
        }

        //generates Exp & Log table for fast multiplication operator
        static Field()
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

        //getters and setters
        public byte getValue()
        {
            return value;
        }

        public void setValue(byte _value)
        {
            value = _value;
        }

        //operators
        public static Field operator+ (Field Fa, Field Fb)
        {
            byte bres = (byte)(Fa.value ^ Fb.value);
            return new Field(bres);
        }

        public static Field operator- (Field Fa, Field Fb)
        {
            byte bres = (byte)(Fa.value ^ Fb.value);
            return new Field(bres);
        }

        public static Field operator* (Field Fa, Field Fb)
        {
            Field FRes = new Field(0);
            if (Fa.value !=0 && Fb.value != 0)
            {
                byte bres = (byte)((Log[Fa.value] + Log[Fb.value]) % (order-1));
                bres = Exp[bres];
                FRes.value = bres;
            }
            return FRes;
        }

        public static Field operator/ (Field Fa, Field Fb)
        {
            if (Fb.value == 0)
            {
                throw new System.ArgumentException("Divisor cannot be 0","Fb");
            }

            Field Fres = new Field(0);
            if (Fa.value != 0)
            {
                byte bres = (byte)(((order-1) + Log[Fa.value] - Log[Fb.value]) % (order-1));
                bres = Exp[bres];
                Fres.value = bres;
            }
            return Fres;
        }

        public static bool operator== (Field Fa, Field Fb)
        {
            return (Fa.value == Fb.value);
        }

        public static bool operator !=(Field Fa, Field Fb)
        {
            return !(Fa.value == Fb.value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Field F = obj as Field;
            if((System.Object)F == null)
            {
                return false;
            }
            return (value == F.value);
        }

        public override int GetHashCode()
        {
            return value;
        }

        //multiplication method which is only used in Exp & Log table generation
        //implemented with Russian Peasant Multiplication algorithm
        private static byte multiply(byte a, byte b) 
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
            //usage example
            Field f1 = new Field(3);
            Field f2 = new Field(2);
            Field f3 = f1 - f2;
            Console.WriteLine(f3);
            Console.ReadLine();
        }
    }
}
