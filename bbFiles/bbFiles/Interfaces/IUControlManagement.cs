using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bbFiles
{
    interface IUControlManagement
    {
        User user { get; set; }
        bool editEnded { get; set; }
    }
}
