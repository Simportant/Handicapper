using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handicapper
{
    interface iBuffer
    {

       
        double Minimum { get;  }
        double Maximum { get;  }
        int Category { get;  }
        int BufferValue { get;  }
        double Increase { get; }
        double Reduction { get;  }

    }
}
