using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            ReadHeader obj = new ReadHeader();
            string[] header = obj.ReadWavHeader(@"C:\Users\Sheikh Mobeen Ahmad\Downloads\WavFile-3\WavFile.wav");
            foreach (string headerLine in header)
            {
                Console.WriteLine(headerLine);
            }

            AddingNewDummyFile.WriteDummyWavFile(@"C:\Users\Sheikh Mobeen Ahmad\Downloads\WavFile-3\DummyFile.wav", 94, 3, 4);



            using (var fileStream = new FileStream(@"C:\Users\Sheikh Mobeen Ahmad\Downloads\WavFile-3\WavFile.wav", FileMode.Open))
            {
                using (var binaryReader = new BinaryReader(fileStream))
                {
                    // Read chunk ID
                    byte[] chunkIdBytes = binaryReader.ReadBytes(4);
                    string chunkId = System.Text.Encoding.ASCII.GetString(chunkIdBytes);
                    Console.WriteLine($"Chunk ID: {chunkId}");

                    // Read chunk size
                    int chunkSize = binaryReader.ReadInt32();
                    Console.WriteLine($"Chunk Size: {chunkSize}");

                    // Read other header fields as needed...
                }
            }




            Console.ReadKey();
        }


    }
}
