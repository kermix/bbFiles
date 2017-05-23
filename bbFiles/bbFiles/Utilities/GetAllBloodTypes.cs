using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bbFiles
{
    partial class Utilities
    {
        public static Dictionary<BloodTypes, string> GetAllBloodTypes()
        {
            Dictionary<BloodTypes, string> BloodTypesDictionary = new Dictionary<BloodTypes, string>();
            BloodTypesDictionary.Add(BloodTypes.O, "0");
            BloodTypesDictionary.Add(BloodTypes.A, "A");
            BloodTypesDictionary.Add(BloodTypes.B, "B");
            BloodTypesDictionary.Add(BloodTypes.AB, "AB");
            return BloodTypesDictionary;
        }
    }
}
