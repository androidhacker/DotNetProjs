﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace NAudio.CoreAudioApi.Interfaces
{
    [Guid("F294ACFC-3146-4483-A7BF-ADDCA7C260E2"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IAudioRenderClient
    {
        IntPtr GetBuffer(int numFramesRequested);
        void ReleaseBuffer(int numFramesWritten, AudioClientBufferFlags bufferFlags);
    }


}
