﻿using CUETools.Processor.Settings;

namespace CUETools.Processor
{
    public class CUEToolsProfile
    {
        public CUEToolsProfile(string name)
        {
            _config = new CUEConfig();
            _name = name;
            switch (name)
            {
                case "verify":
                    _action = CUEAction.Verify;
                    _script = "only if found";
                    break;
                case "convert":
                    _action = CUEAction.Encode;
                    break;
                case "fix":
                    _action = CUEAction.Encode;
                    _script = "fix offset";
                    break;
            }
        }

        public void Load(SettingsReader sr)
        {
            _config.Load(sr);

            _useFreeDb = sr.LoadBoolean("FreedbLookup") ?? _useFreeDb;
            _useMusicBrainz = sr.LoadBoolean("MusicBrainzLookup") ?? _useMusicBrainz;
            _useAccurateRip = sr.LoadBoolean("AccurateRipLookup") ?? _useAccurateRip;
            _useCUEToolsDB = sr.LoadBoolean("CUEToolsDBLookup") ?? _useCUEToolsDB;
            _useLocalDB = sr.LoadBoolean("LocalDBLookup") ?? _useLocalDB;
            _skipRecent = sr.LoadBoolean("SkipRecent") ?? _skipRecent;
            _outputAudioType = (AudioEncoderType?)sr.LoadInt32("OutputAudioType", null, null) ?? _outputAudioType;
            _outputAudioFormat = sr.Load("OutputAudioFmt") ?? _outputAudioFormat;
            _action = (CUEAction?)sr.LoadInt32("AccurateRipMode", (int)CUEAction.Encode, (int)CUEAction.CorrectFilenames) ?? _action;
            _CUEStyle = (CUEStyle?)sr.LoadInt32("CUEStyle", null, null) ?? _CUEStyle;
            _writeOffset = sr.LoadInt32("WriteOffset", null, null) ?? 0;
            _outputTemplate = sr.Load("OutputPathTemplate") ?? _outputTemplate;
            _script = sr.Load("Script") ?? _script;
        }

        public void Save(SettingsWriter sw)
        {
            _config.Save(sw);

            sw.Save("FreedbLookup", _useFreeDb);
            sw.Save("MusicBrainzLookup", _useMusicBrainz);
            sw.Save("AccurateRipLookup", _useAccurateRip);
            sw.Save("LocalDBLookup", _useLocalDB);
            sw.Save("SkipRecent", _skipRecent);
            sw.Save("CUEToolsDBLookup", _useCUEToolsDB);
            sw.Save("OutputAudioType", (int)_outputAudioType);
            sw.Save("OutputAudioFmt", _outputAudioFormat);
            sw.Save("AccurateRipMode", (int)_action);
            sw.Save("CUEStyle", (int)_CUEStyle);
            sw.Save("WriteOffset", (int)_writeOffset);
            sw.Save("OutputPathTemplate", _outputTemplate);
            sw.Save("Script", _script);
        }

        public CUEConfig _config;
        public AudioEncoderType _outputAudioType = AudioEncoderType.Lossless;
        public string _outputAudioFormat = "flac", _outputTemplate = null, _script = "default";
        public CUEAction _action = CUEAction.Encode;
        public CUEStyle _CUEStyle = CUEStyle.SingleFileWithCUE;
        public int _writeOffset = 0;
        public bool _useFreeDb = true, _useMusicBrainz = true, _useAccurateRip = true, _useCUEToolsDB = true, _useLocalDB = true, _skipRecent = false;

        public string _name;
    }
}
