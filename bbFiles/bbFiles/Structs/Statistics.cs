using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace bbFiles.Structs
{
    class Statistics
    {
        public readonly Dictionary<string, int> BloodTypes = new Dictionary<string, int>
        {
            {"aWithMarker", 0 },
            {"aWithOutMarker", 0 },
            {"abWithMarker", 0 },
            {"abWithOutMarker", 0 },
            {"bWithMarker", 0 },
            {"bWithOutMarker", 0 },
            {"oWithMarker", 0 },
            {"oWithOutMarker", 0 }
        };


        public Statistics()
        {
            this.BloodTypes["aWithMarker"] = GetBloodAmount(bbFiles.BloodTypes.A, true) / 5000;
            this.BloodTypes["aWithOutMarker"] = GetBloodAmount(bbFiles.BloodTypes.A, false) / 5000;
            this.BloodTypes["abWithMarker"] = GetBloodAmount(bbFiles.BloodTypes.AB, true) / 5000;
            this.BloodTypes["abWithOutMarker"] = GetBloodAmount(bbFiles.BloodTypes.AB, false) / 5000;
            this.BloodTypes["bWithMarker"] = GetBloodAmount(bbFiles.BloodTypes.B, true) / 5000;
            this.BloodTypes["bWithOutMarker"] = GetBloodAmount(bbFiles.BloodTypes.B, false) / 5000;
            this.BloodTypes["oWithMarker"] = GetBloodAmount(bbFiles.BloodTypes.O, true) / 5000;
            this.BloodTypes["oWithOutMarker"] = GetBloodAmount(bbFiles.BloodTypes.O, false) / 5000;
        }

        private int GetBloodAmount(BloodTypes bloodType, bool rhMarker)
        {
            var stat = (from c in (new databaseDataContext()).Stats
                        where c.BloodType.StartsWith((bloodType).ToString() + (rhMarker == true ? "+" : "-"))
                        select c).Single();

            return (int)stat.TotalAmount;
            
        }
    }
}