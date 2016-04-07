using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetwiseTask
{
    class Program
    {

        static void Main(string[] args)
        {
            String fileName = args[0];
            Decimal currCash = Decimal.Zero;
            Decimal sumDecimal = Decimal.Zero;




            try
            {
                var fileStream = new FileStream(@fileName, FileMode.Open, FileAccess.ReadWrite);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string line = null;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        String[] parsedAmount = Regex.Split(line, "[@amount:]");
                        String[] parsedWithoutPLN = Regex.Split(parsedAmount[parsedAmount.Length - 1], "[PLN]");

                        if (parsedWithoutPLN[0] == String.Empty) continue;
                        currCash = Decimal.Parse(parsedWithoutPLN[0]);
                        sumDecimal = Decimal.Add(sumDecimal, currCash);
                    }

                    streamReader.Close();
                }
                fileStream.Close();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            var fileStream2 = new FileStream(@fileName, FileMode.Append, FileAccess.Write);
            using (var streamWriter = new StreamWriter(fileStream2, Encoding.UTF8))
            {
                streamWriter.WriteLine("\n\nSum: " + sumDecimal + "PLN");
            }

            using (var db = new OrderModel())
            {
                order Order = new order();
                Order.name = "DB";
                Order.surname = "DB";
                Order.amount = sumDecimal;

                db.orders.Add(Order);
                db.SaveChanges();
            }
            Console.WriteLine("Sum: " + sumDecimal + "PLN");
            Console.WriteLine("Press any key...");
            Console.ReadLine();
        }
    }
}
