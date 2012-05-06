using System;
using System.IO;
using System.Linq;
using System.Net;
using CUETools.Codecs;
using CUETools.Codecs.FLAKE;

//thanks to  Lukasz Kwiecinski, Istrib for his article and source code http://www.codeproject.com/KB/audio-video/Mp3SoundCapture.aspx
//thanks to Alexander Yakhnev for the article http://habrahabr.ru/blogs/google/117234/  and source code http://startup-news.ru/data/habrGoogleSpeech.zip


namespace SpeechRecognizer
{
    public static class SoundRecognition
    {
        internal static string GoogleRequestString =
            "https://www.google.com/speech-api/v1/recognize?xjerr=1&client=chromium&lang=";

        /// <summary>
        /// Send request to google speech recognition service and return recognized string
        /// </summary>
        /// <param name="lang">language</param>
        /// <param name="flacName">path to .flac file</param>
        /// <param name="sampleRate">Rate</param>
        /// <returns>recognized string</returns>
        public static string GoogleRequest(string lang, string flacName, int sampleRate)
        {
            var bytes = File.ReadAllBytes(flacName);
            return GoogleRequest(lang, bytes, sampleRate);
        }

        /// <summary>
        /// Send request to google speech recognition service and return recognized string
        /// </summary>
        /// <param name="lang">language</param>
        /// <param name="bytes">byte array wich is a sound in .flac</param>
        /// <param name="sampleRate">Rate</param>
        /// <returns>recognized string</returns>
        public static string GoogleRequest(string lang, byte[] bytes, int sampleRate)
        {
            Stream stream = null;
            StreamReader sr = null;
            WebResponse response = null;
            JSon.RecognizedItem result;
            try
            {
                // TODO: validate lang here
                WebRequest request = WebRequest.Create(GoogleRequestString + lang);
                request.Method = "POST";
                request.ContentType = "audio/x-flac; rate=" + sampleRate;
                request.ContentLength = bytes.Length;

                stream = request.GetRequestStream();

                stream.Write(bytes, 0, bytes.Length);
                stream.Close();

                response = request.GetResponse();

                stream = response.GetResponseStream();
                if (stream == null)
                {
                    throw new Exception("Can't get a response from server. Response stream is null.");
                }
                sr = new StreamReader(stream);

                //Get response in JSON format
                string respFromServer = sr.ReadToEnd();

                var parsedResult = JSon.Parse(respFromServer);
                result =
                    parsedResult.hypotheses.Where(d => d.confidence == parsedResult.hypotheses.Max(p => p.confidence)).FirstOrDefault();
            }
            finally
            {
                if (stream != null)
                    stream.Close();

                if (sr != null)
                    sr.Close();

                if (response != null)
                    response.Close();
            }

            return result == null ? "" : result.utterance;
        }

        /// <summary>
        /// Send request to google speech recognition service and return recognized string
        /// </summary>
        /// <param name="lang">language</param>
        /// <param name="flacStream">stream of sound in flac format</param>
        /// <param name="sampleRate">Rate</param>
        /// <returns>recognized string</returns>
        public static string GoogleRequest(string lang, MemoryStream flacStream, int sampleRate)
        {
            flacStream.Position = 0;
            var bytes = new byte[flacStream.Length];
            flacStream.Read(bytes, 0, (int)flacStream.Length);
            return GoogleRequest(lang, bytes, sampleRate);
        }

        /// <summary>
        /// Convert .wav file to .flac file with the same name
        /// </summary>
        /// <param name="WavName">path to .wav file</param>
        /// <returns>Sample Rate of converted .flac</returns>
        public static int Wav2Flac(string WavName)
        {
            int sampleRate;
            var flacName = Path.ChangeExtension(WavName, "flac");

            FlakeWriter audioDest = null;
            IAudioSource audioSource = null;
            try
            {
                audioSource = new WAVReader(WavName, null);

                AudioBuffer buff = new AudioBuffer(audioSource, 0x10000);

                audioDest = new FlakeWriter(flacName, audioSource.PCM);

                sampleRate = audioSource.PCM.SampleRate;

                while (audioSource.Read(buff, -1) != 0)
                {
                    audioDest.Write(buff);
                }
            }
            finally
            {
                if (audioDest != null) audioDest.Close();
                if (audioSource != null) audioSource.Close();
            }
            return sampleRate;
        }

        /// <summary>
        /// Convert stream of wav to flac format and send it to google speech recognition service.
        /// </summary>
        /// <param name="lang">language</param>
        /// <param name="stream">wav stream</param>
        /// <returns>recognized result</returns>
        public static string WavStreamToGoogle(string lang, Stream stream)
        {
            FlakeWriter audioDest = null;
            IAudioSource audioSource = null;
            string answer;
            try
            {
                var outStream = new MemoryStream();

                stream.Position = 0;

                audioSource = new WAVReader("", stream);

                var buff = new AudioBuffer(audioSource, 0x10000);

                audioDest = new FlakeWriter("", outStream, audioSource.PCM);

                var sampleRate = audioSource.PCM.SampleRate;
                
                while (audioSource.Read(buff, -1) != 0)
                {
                    audioDest.Write(buff);
                }
                
                answer = GoogleRequest(lang, outStream, sampleRate);

            }
            finally
            {
                if (audioDest != null) audioDest.Close();
                if (audioSource != null) audioSource.Close();
            }
            return answer;
        }
    }
}
