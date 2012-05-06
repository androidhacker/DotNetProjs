using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLAP;

namespace WaveToText
{
    class Program
    {
        [Verb]
        static void DoWaveToText(string lang, string waveFile)
        {
        }

        static void Main(string[] args)
        {
            Parser.Run<Program>(args);
        }
    }
}
