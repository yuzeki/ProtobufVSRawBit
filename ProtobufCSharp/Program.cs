using ProtoBuf;
using System;
using System.IO;

namespace ProtobufCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var writeClass = new TestClass();
            var random = new Random();
            byte[] bytes;

            while (true)
            {
                using (var memoryStream = new MemoryStream())
                {
                    writeClass.int_0_15 = random.Next(0, 15);
                    //writeClass.int_0_127 = random.Next(0, 127);
                    //writeClass.float_0_10 = NextFloat(0, 10);
                    //writeClass.float_0_500 = NextFloat(0, 500);

                    Serializer.Serialize(memoryStream, writeClass);
                    bytes = memoryStream.ToArray();
                    Console.WriteLine($"byte count:{bytes.Length}");
                    Console.WriteLine("---------write---------");
                    Console.WriteLine($"write.int_0_15      :{writeClass.int_0_15}");
                    Console.WriteLine($"write.int_0_127     :{writeClass.int_0_127}");
                    Console.WriteLine($"write.float_0_10    :{writeClass.float_0_10:F9}");
                    Console.WriteLine($"write.float_0_500   :{writeClass.float_0_500:F9}");
                    Console.WriteLine(BitConverter.ToString(bytes));
                }
                using (var memoryStream = new MemoryStream(bytes))
                {
                    var readClass = Serializer.Deserialize<TestClass>(memoryStream);
                    Console.WriteLine("---------read---------");
                    Console.WriteLine($"read.int_0_15       :{readClass.int_0_15}");
                    Console.WriteLine($"read.int_0_127      :{readClass.int_0_127}");
                    Console.WriteLine($"read.float_0_10     :{readClass.float_0_10:F9}");
                    Console.WriteLine($"read.float_0_500    :{readClass.float_0_500:F9}");
                }
                Console.WriteLine("按ESC退出,按其他键继续...\n");
                var key = Console.ReadKey();
                if(key.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
        static float NextFloat(float min, float max)
        {
            System.Random random = new System.Random();
            double val = (random.NextDouble() * (max - min) + min);
            return (float)val;
        }
    }


    [ProtoContract]
    class TestClass
    {
        //[ProtoMember(1)]
        //public Vector3 vector_0_500;
        [ProtoMember(2)]
        public int int_0_15;
        [ProtoMember(3)]
        public int int_0_127;
        [ProtoMember(4)]
        public float float_0_10;
        [ProtoMember(5)]
        public float float_0_500;
    }
}
