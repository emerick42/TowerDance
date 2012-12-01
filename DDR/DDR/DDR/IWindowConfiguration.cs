using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDR
{
    interface IWindowConfiguration
    {
        int width
        {
            set;
            get;
        }
        int height
        {
            set;
            get;
        }

        void applyChanges();
    }
}
