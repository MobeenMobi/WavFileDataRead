using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
   public static class AddingNewDummyFile
    {
        public static void WriteDummyWavFile(string filePath, int sampleRate, int durationInSeconds, int channels)
        {
            // Create WAV file writer with specified settings
            var writer = new WaveFileWriter(filePath, new WaveFormat(sampleRate, 16, channels));

            // Generate dummy audio data (sine wave)
            var amplitude = 0.25 * short.MaxValue;
            var frequency = 440.0;
            var sampleCount = sampleRate * durationInSeconds;
            var sampleValues = new double[channels];
            var sampleBuffer = new byte[2 * channels];
            for (int i = 0; i < sampleCount; i++)
            {
                var t = (double)i / sampleRate;
                var value = amplitude * Math.Sin(2 * Math.PI * frequency * t);

                // Write sample values to buffer
                for (int c = 0; c < channels; c++)
                {
                    sampleValues[c] = value;
                }
                Buffer.BlockCopy(sampleValues, 0, sampleBuffer, 0, sampleBuffer.Length);

                // Write sample buffer to WAV file
                writer.Write(sampleBuffer, 0, sampleBuffer.Length);
            }

            // Close WAV file writer
            writer.Close();
        }




    }
}
