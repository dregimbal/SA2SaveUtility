using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SA2SaveUtility {
    public static class ReadSave {
        public static SaveType FromSaveType = SaveType.GAMECUBE;
        public static SaveType ToSaveType = SaveType.PC;
        public static bool IsChaoSave = false;

        public static void ReadInt() { 
        
        }
    }

    public enum SaveType { 
        PC,
        GAMECUBE,
        RTE,
        SA,
        XBOX,
        PLAYSTATION
    }
    
}
