﻿namespace CUETools.Processor
{
    public class CUEToolsFormat
    {
        public CUEToolsFormat(
            string _extension,
            CUEToolsTagger _tagger,
            bool _allowLossless,
            bool _allowLossy,
            bool _allowLossyWAV,
            bool _allowEmbed,
            bool _builtin,
            CUEToolsUDC _encoderLossless,
            CUEToolsUDC _encoderLossy,
            string _decoder)
        {
            extension = _extension;
            tagger = _tagger;
            allowLossless = _allowLossless;
            allowLossy = _allowLossy;
            allowLossyWAV = _allowLossyWAV;
            allowEmbed = _allowEmbed;
            builtin = _builtin;
            encoderLossless = _encoderLossless;
            encoderLossy = _encoderLossy;
            decoder = _decoder;
        }
        public string DotExtension
        {
            get
            {
                return "." + extension;
            }
        }
        public override string ToString()
        {
            return extension;
        }
        public string extension;
        public CUEToolsUDC encoderLossless;
        public CUEToolsUDC encoderLossy;
        public string decoder;
        public CUEToolsTagger tagger;
        public bool allowLossless, allowLossy, allowLossyWAV, allowEmbed, builtin;
    }
}
