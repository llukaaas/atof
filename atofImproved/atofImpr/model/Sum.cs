using LINQtoCSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atofImpr.model
{
    [Serializable]
    public class Sum
    {
        [CsvColumn(Name = "Mesec", OutputFormat = "dd/mm/yyyy dd.mm.yyyy dd.mm.yyyy.")]
        public string Mesec { get; set; }

        [CsvColumn(Name = "Godina")]
        public int Godina { get; set; }

        [CsvColumn(Name = "UkupnoMerenja")]
        public string BrojMerenja { get; set; }

        [CsvColumn(Name = "Suma")]
        public string ZbirPoMesecu { get; set; }
    }
}
