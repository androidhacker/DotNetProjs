using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace SpeechRecognizer
{
    public class JSon
    {
        
        public class RecognizedItem
        {
        
            public string utterance;

        
            public float confidence;
        }

        
        public class RecognitionResult
        {
        
            public string status;

        
            public string id;

        
            public List<RecognizedItem> hypotheses;
        }

        public static RecognitionResult Parse(String toParse)
        {
            //Шапка
            Regex regexCommonInfo = new Regex(@"""status"":(?<status>\d),""id"":""(?<id>[\w-]+)""");
            RecognitionResult result = new RecognitionResult();
            var match = regexCommonInfo.Match(toParse);
            result.id = match.Groups["id"].Value;
            result.status = match.Groups["status"].Value;

            //Гипотезы
            Regex regexUtter = new Regex(@"""utterance"":""(?<utter>[а-яА-Я\s\w.,]+)"",""confidence"":(?<conf>[\d.]+)");

            float confidence;
            var matches = regexUtter.Matches(toParse);
            List<RecognizedItem> hypos = new List<RecognizedItem>();
            foreach (Match m in matches)
            {
                var g = m.Groups;
                confidence = float.Parse(g["conf"].Value.Replace(".",","));
                hypos.Add(new RecognizedItem { confidence = confidence, utterance = g["utter"].Value });
            }
            result.hypotheses = hypos;


            return result;
        }
    }
}
