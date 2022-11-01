using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LINQtoCSV;

namespace atofImpr.model
{
    [Serializable]
    public class Result
    {
        [CsvColumn(Name = "Datum", OutputFormat = "dd/mm/yyyy dd.mm.yyyy dd.mm.yyyy.")]
        public DateTime Date { get; set; }

        [CsvColumn(Name = "Rezultat")]
        public string Res { get; set; }

        [CsvColumn(Name = "Komentar")]
        public string Comm { get; set; }

        public string getMonth(DateTime dateTime)
        {
            switch (dateTime.Month)
            {
                case 1:
                    return "Januar";
                case 2:
                    return "Februar";
                case 3:
                    return "Mart";
                case 4:
                    return "April";
                case 5:
                    return "Maj";
                case 6:
                    return "Jun";
                case 7:
                    return "Jul";
                case 8:
                    return "Avgust";
                case 9:
                    return "Septembar";
                case 10:
                    return "Oktobar";
                case 11:
                    return "Novembar";
                case 12:
                    return "Decembar";
                default:
                    return "";
            }
        }
        public bool checkRes(string res)
        {
            string strRegex = @"^[0-9]+[.]{0,1}[0-9]*[eE]{1}[+-]{0,1}[1-9]{1}[0-9]*$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(res))
                return true;
            else
                return false;
        }

        public bool isInt(string res)
        {
            string strRegex = @"^[0-9]+$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(res))
                return true;
            else
                return false;
        }

        public bool isDouble(string res)
        {
            string strRegex = @"^(([1-9]+[0-9]*)|([0-9]{1}))[.]{1}[0-9]+$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(res))
                return true;
            else
                return false;
        }
    }
}
