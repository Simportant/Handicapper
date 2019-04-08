using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handicapper
{
    public class BufferCategory : iBuffer
    {
        public BufferCategory(int ID, double min, double max, int buffer, double increase, double reduction)
        {
            Category = ID;
            Minimum = min;
            Maximum = max;
            BufferValue = buffer;
            Increase = increase;
            Reduction = reduction;
        }

        public BufferCategory()
        {
            Category = 0;
            Minimum = 0;
            Maximum = 0;
            BufferValue = 0;
            Increase = 0;
            Reduction = 0;
        }


        public double Minimum { get; }
        public double Maximum { get; }
        public int Category { get; }
        public int BufferValue { get; }

        public double Increase { get; }
        public double Reduction { get; }
    }
}
