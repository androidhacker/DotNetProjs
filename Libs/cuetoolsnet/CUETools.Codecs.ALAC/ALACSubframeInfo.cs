﻿namespace CUETools.Codecs.ALAC
{
    unsafe class ALACSubframeInfo
    {
        public ALACSubframe best;
        public int* samples;
        public uint done_fixed;
        public LpcContext[] lpc_ctx;

        public ALACSubframeInfo()
        {
            best = new ALACSubframe();
            lpc_ctx = new LpcContext[Alac.MAX_LPC_WINDOWS];
            for (int i = 0; i < Alac.MAX_LPC_WINDOWS; i++)
                lpc_ctx[i] = new LpcContext();
        }

        public void Init(int* s, int* r)
        {
            samples = s;
            best.residual = r;
            best.size = AudioSamples.UINT32_MAX;
            best.order = 0;
            for (int iWindow = 0; iWindow < Alac.MAX_LPC_WINDOWS; iWindow++)
                lpc_ctx[iWindow].Reset();
            done_fixed = 0;
        }
    }
}
