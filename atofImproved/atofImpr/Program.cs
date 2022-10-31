using atofImpr.model;
using LINQtoCSV;
using System;
using System.Collections.Generic;
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
            foreach(var r in result)
            {
                Console.WriteLine(r.checkRes(r.Res));
            }
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

            foreach (var r in result)
            {
                if (r.checkRes(r.Res) == true)
                {
                    Console.WriteLine("NAUCNI");
                    sums.Add(new Sum { Mesec = r.getMonth(r.Date), Godina = r.Date.Year, BrojMerenja = result.Where(w => w.Date.Month == r.Date.Month).Count().ToString(), ZbirPoMesecu = "2" });
                }
                else if (int.TryParse(r.Res, out int a)) 
                {
                    Console.WriteLine("NORMALNI 1");
                    sums.Add(new Sum { Mesec = r.getMonth(r.Date), Godina = r.Date.Year, BrojMerenja = result.Where(w => w.Date.Month == r.Date.Month).Count().ToString(), ZbirPoMesecu = result.Where(w => w.Date.Month == r.Date.Month).Sum(item => int.Parse(item.Res)).ToString() });
                }
                else if (double.TryParse(r.Res, out double b))
                {
                    Thread.Sleep(3000);
                    Console.WriteLine("NORMALNI 2");
                    sums.Add(new Sum { Mesec = r.getMonth(r.Date), Godina = r.Date.Year, BrojMerenja = result.Where(w => w.Date.Month == r.Date.Month).Count().ToString(), ZbirPoMesecu = result.Where(w => w.Date.Month == r.Date.Month).Sum(item => Decimal.Parse(item.Res)).ToString() });
                }
                else
                {
                    string createText = "Hello and Welcome" + Environment.NewLine;
                    File.WriteAllText("output.err", createText);
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
    }
}
