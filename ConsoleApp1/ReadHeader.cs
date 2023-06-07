using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace ConsoleApp1
{
    public class ReadHeader
    {
        public string[] ReadWavHeader(string filePath)
        {
            string[] headerInfo = new string[7];
           string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath) + ".csv");
            // Open the wave file
            using (WaveFileReader reader = new WaveFileReader(filePath))
            {
                // Add basic header information to the array
                //headerInfo[0] = "Chunk ID: " + reader.ChunkId;
                //headerInfo[1] = "Chunk Size: " + reader.ChunkSize;
                //headerInfo[2] = "Format: " + reader.WaveFormat.ToString();
                //headerInfo[3] = "Subchunk1 ID: " + reader.Subchunk1Id;
                //headerInfo[4] = "Subchunk1 Size: " + reader.Subchunk1Size;
                //headerInfo[5] = "Audio Format: " + reader.AudioFormat;
                headerInfo[0] = "Channels: " + reader.WaveFormat.Channels;
                headerInfo[1] = "Sample Rate: " + reader.WaveFormat.SampleRate;
                headerInfo[2] = "Byte Rate: " + reader.WaveFormat.AverageBytesPerSecond;
                headerInfo[3] = "Block Align: " + reader.WaveFormat.BlockAlign;
                headerInfo[4] = "Bits Per Sample: " + reader.WaveFormat.BitsPerSample;
                headerInfo[5] = "Encoding: " + reader.WaveFormat.Encoding;
                headerInfo[6] = "Format: " + reader.WaveFormat.ToString();

                double timePerSample = 1.0 / reader.WaveFormat.SampleRate;


                //Console.WriteLine("Audio Data:");
                //for (int sample = 0; sample < reader.SampleCount; sample++)
                //{
                //    var sampleValues = reader.ReadNextSampleFrame();
                //    for (int channel = 0; channel < reader.WaveFormat.Channels; channel++)
                //    {
                //        Console.Write($"{sampleValues[channel]}\t");
                //    }
                //    Console.Write($"{timePerSample * sample:F4}"); // Output time for sample
                //    Console.WriteLine();
                //}

                using (var writer = new StreamWriter(outputFilePath))
                {
                    writer.Write("Time,");
                    for (int channel = 0; channel < reader.WaveFormat.Channels; channel++)
                    {
                        writer.Write($"Channel {channel + 1},");
                    }
                    writer.WriteLine();

                    // Calculate time per sample
                    //double timePerSample = 1.0 / reader.WaveFormat.SampleRate;
                    for (int sample = 0; sample < reader.SampleCount; sample++)
                    {
                        var sampleValues = reader.ReadNextSampleFrame();
                        short[] shortArray = new short[sampleValues.Length];

                        for (int i = 0; i < sampleValues.Length; i++)
                        {
                            if(sampleValues[i] > 0)
                            {
                                shortArray[i] = (short)Math.Ceiling((sampleValues[i] * short.MaxValue));
                            }
                            else
                            {
                                shortArray[i] = (short)Math.Floor((sampleValues[i] * short.MaxValue));
                            }
                            
                        }
                        writer.Write($"{timePerSample * sample:F4},");
                        for (int channel = 0; channel < reader.WaveFormat.Channels; channel++)
                        {
                            writer.Write($"{shortArray[channel]},");
                        }

                        writer.WriteLine();
                    }
                }
            }



            return headerInfo;
        }


    }
}
