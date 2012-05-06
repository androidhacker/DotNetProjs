using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CLAP;
using SpeechRecognizer;

namespace WaveToText
{
    class Program
    {
        [Verb(IsDefault = true)]
        static void DoWaveToText(string lang, string waveFile)
        {
            using (StreamWriter sw = new StreamWriter("log.txt", false, Encoding.Unicode))
            {
                using (FileStream fs = new FileStream(waveFile, FileMode.Open, FileAccess.Read))
                {
                    string output = SoundRecognition.WavStreamToGoogle(lang, fs);
                    Console.WriteLine("reco done!");
                    sw.WriteLine("Reco result: {0}", output);
                }
            }
        }

        static void Main(string[] args)
        {
            Parser.RunConsole<Program>(args);
        }
    }
}
