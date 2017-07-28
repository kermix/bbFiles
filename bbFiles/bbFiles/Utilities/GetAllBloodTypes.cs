using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bbFiles
{
    partial class Utilities
    {
        public static Dictionary<BloodType, string> GetAllBloodTypes()
        {
            Dictionary<BloodType, string> BloodTypesDictionary = new Dictionary<BloodType, string>();
            BloodTypesDictionary.Add(BloodType.O, "0");
            BloodTypesDictionary.Add(BloodType.A, "A");
            BloodTypesDictionary.Add(BloodType.B, "B");
            BloodTypesDictionary.Add(BloodType.AB, "AB");
            return BloodTypesDictionary;
        }
    }
}
