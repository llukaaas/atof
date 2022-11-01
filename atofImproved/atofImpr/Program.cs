using atofImpr.model;
using LINQtoCSV;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace atofImpr
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Result> result = ReadCsvFile();
            WriteCsvFile(result);
            Console.ReadKey();
        }

        private static List<Result> ReadCsvFile()
        {
            var csvFileDesc = new CsvFileDescription
            {
                FirstLineHasColumnNames = true,
                SeparatorChar = ',',
                UseFieldIndexForReadingData = false
            };

            var csvContext = new CsvContext();
            var results = csvContext.Read<Result>("input.csv", csvFileDesc);

            List<Result> res = new List<Result>();

            foreach(Result rs in results)
            {
                res.Add(rs);
            }

            return res;
        }

        private static void WriteCsvFile(List<Result> result)
        {
            List<Sum> sums = new List<Sum>();

            foreach (Result r in result)
            {
                Console.WriteLine(r.checkRes(r.Res));
                if (r.isInt(r.Res))
                {
                    Sum suma = new Sum();
                    suma.Mesec = r.getMonth(r.Date);
                    suma.Godina = r.Date.Year;
                    suma.BrojMerenja = result.Where(w => w.Date.Month == r.Date.Month).Count();
                    suma.ZbirPoMesecu += Double.Parse(r.Res) * 1.0;
                    Console.WriteLine(suma.Mesec + " | " + suma.BrojMerenja + " | " + suma.ZbirPoMesecu);
                    //AddNewMonth(sums, suma);
                    sums.Add(suma);
                }
                else if (r.isDouble(r.Res))
                {
                    Sum suma = new Sum();
                    suma.Mesec = r.getMonth(r.Date);
                    suma.Godina = r.Date.Year;
                    suma.BrojMerenja = result.Where(w => w.Date.Month == r.Date.Month).Count();
                    suma.ZbirPoMesecu += Double.Parse(conNum(r.Res));
                    Console.WriteLine(suma.Mesec + " | " + suma.BrojMerenja + " | " + suma.ZbirPoMesecu);
                    //AddNewMonth(sums,suma);
                    sums.Add(suma);
                }
                else if (r.checkRes(r.Res))
                {
                    Sum suma = new Sum();
                    suma.Mesec = r.getMonth(r.Date);
                    suma.Godina = r.Date.Year;
                    suma.BrojMerenja = result.Where(w => w.Date.Month == r.Date.Month).Count();

                    string[] separatingStrings = { "e", "E" };
                    string[] numbers = r.Res.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
                    string num = numbers.ElementAt(0).ToString();
                    num = conNum(num);
                    double firstNumb = Double.Parse(conNum(numbers.ElementAt(0).ToString()));
                    double sumPerMonth = Resolve(firstNumb, numbers.ElementAt(1));
                    suma.ZbirPoMesecu = sumPerMonth;
                    
                    Console.WriteLine(suma.Mesec + " | " + suma.BrojMerenja + " | " + suma.ZbirPoMesecu);
                    //AddNewMonth(sums, suma);
                    sums.Add(suma);
                } 
            }

            var csvFileDesc = new CsvFileDescription
            {
                FirstLineHasColumnNames = true,
                SeparatorChar = ',',
                UseFieldIndexForReadingData = false
            };

            var csvContext = new CsvContext();
            csvContext.Write(sums, "output.csv", csvFileDesc);

            
        }

       /* private static void AddNewMonth(List<Sum> sums, Sum suma)
        {
            int ind = 0;
            for(int i = 0; i < sums.Count(); i++)
            {
                if(sums.ElementAt(i).Mesec == suma.Mesec)
                {
                    ind = 1;
                    sums.ElementAt(i).ZbirPoMesecu += suma.ZbirPoMesecu;
                    return;
                }
                if(ind == 1)
                {
                    sums.Add(suma);
                    return;
                }
            }
        }*/

        private static string conNum(string num)
        {
            string s = "";
            for(int i = 0; i < num.Length; i++)
            {
                if (num.ElementAt(i).ToString().Equals("."))
                {
                    s += ",";
                }
                else
                {
                    s += num.ElementAt(i).ToString();
                }
            }
            return s;
        }

        private static double Resolve(double firstNumb, string s)
        {
            if(s.ElementAt(0).ToString().Equals("-"))
            {
                string[] separatingStrings = { "-" };
                string[] numbers = s.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
                for(int i = 0; i < int.Parse(numbers.ElementAt(0)); i++)
                {
                    firstNumb /= 10;
                }
            }
            else if(s.ElementAt(0).ToString().Equals("+"))
            {
                string[] separatingStrings = { "+" };
                string[] numbers = s.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < int.Parse(numbers.ElementAt(0)); i++)
                {
                    firstNumb *= 10;
                }
            }
            else
            {
                for (int i = 0; i < int.Parse(s); i++)
                {
                    firstNumb *= 10;
                }
            }

            return firstNumb;
        }
    }
}
