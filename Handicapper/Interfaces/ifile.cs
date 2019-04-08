using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Handicapper
{
    
    interface ifile
    {
      
        StreamReader DataFile { get; }
        void WriteEntry(string Entry);
    }
}
