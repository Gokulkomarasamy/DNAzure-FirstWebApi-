using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pensioner_Details.Repository
{
    public class PensionerRepository : IPensionerdetail
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(PensionerRepository));
        //private IConfiguration configuration;


        public PensionerDetail PensionerDetailByAadhar(string aadhar)
        {
            List<PensionerDetail> pensionDetails = GetDetailsCsv();
            _log4net.Info("Pensioner details invoked by Aadhar Number!");
            return pensionDetails.FirstOrDefault(s => s.AadharNumber == aadhar);
        }

        public List<PensionerDetail> GetDetailsCsv()
        {
            _log4net.Info("Data is read from CSV file");  // Logging Implemented
            List<PensionerDetail> pensionerdetail = new List<PensionerDetail>();
            try
            {
                //string csvConn = configuration.GetValue<string>("MySettings:CsvConnection");  // Initializing the csvConn  for the File path
                //string csvConn = "details.csv";
                using (StreamReader sr = new StreamReader("details.csv"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');
                        pensionerdetail.Add(new PensionerDetail() { Name = values[0], Dateofbirth = DateTime.ParseExact(values[1], "MM/dd/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture),Pan = values[2], AadharNumber = values[3], SalaryEarned = Convert.ToInt32(values[4]), Allowances = Convert.ToInt32(values[5]), PensionType = (PensionTypeValue)Enum.Parse(typeof(PensionTypeValue), values[6]), BankName = values[7], AccountNumber = values[8], BankType = (BankType)Enum.Parse(typeof(BankType), values[9]) });
                    }

                }
            }
            catch (NullReferenceException e)
            {
                _log4net.Error("Values cannot be fetched from the Csv file" + e);
                return null;
            }
            catch (Exception e)
            {
                _log4net.Error("Values cannot be fetched from the Csv file" + e);
                return null;
            }
            return pensionerdetail.ToList();
        }

        public string InsertIntoCsv(PensionerDetail pensionerDetail)
        {
            
            
            using(FileStream fs = new FileStream("details.csv",FileMode.Append,FileAccess.Write))
            using(StreamWriter writer = new StreamWriter(fs))
            {
                string line = pensionerDetail.Name.ToString() +
                    "," + pensionerDetail.Dateofbirth.ToString() +
                    "," + pensionerDetail.Pan.ToString() + "," + pensionerDetail.AadharNumber.ToString() + "," + pensionerDetail.SalaryEarned.ToString() +
                    "," + pensionerDetail.Allowances + "," + (int)pensionerDetail.PensionType +
                    "," + pensionerDetail.BankName.ToString() + "," + pensionerDetail.AccountNumber.ToString() +
                    "," + (int)pensionerDetail.BankType;
                
                writer.Write(line);

                writer.Flush();
            }
            var p = PensionerDetailByAadhar(pensionerDetail.AadharNumber);
            if (p != null)
                return "Success";
            else
                return "Unsuccessfull";

        }
        public string Update(string aadhar, PensionerDetail pensionerDetail)
        {
            List<PensionerDetail> list = new List<PensionerDetail>();
            list = GetDetailsCsv();
            //PensionerDetail pensionerToUpdate = PensionerDetailByAadhar(aadhar);
            list.RemoveAll(s => s.AadharNumber == aadhar);
           
            list.Add(pensionerDetail);
            using (StreamWriter writer = new StreamWriter("details.csv"))
            {
                foreach (var p in list)
                {
                    string line = p.Name.ToString() +
                                    "," + p.Dateofbirth.ToString() +
                                    "," + p.Pan.ToString() + "," + p.AadharNumber.ToString() + "," + p.SalaryEarned.ToString() +
                                    "," + p.Allowances + "," + (int)p.PensionType +
                                    "," + p.BankName.ToString() + "," + p.AccountNumber.ToString() +
                                    "," + (int)p.BankType + "\n";

                    writer.Write(line);
                }

                writer.Flush();
            }

            return "Updated";


        }
        public string Delete(string aadhar)
        {
            List<PensionerDetail> list = new List<PensionerDetail>();
            //PensionerDetail pensionerTodelete = PensionerDetailByAadhar(aadhar);
            list = GetDetailsCsv();
            list.RemoveAll(s=>s.AadharNumber==aadhar);
           
                using (StreamWriter writer = new StreamWriter("details.csv"))
                {
                foreach (var p in list)
                {
                    string line = p.Name.ToString() +
                                    "," + p.Dateofbirth.ToString() +
                                    "," + p.Pan.ToString() + "," + p.AadharNumber.ToString() + "," + p.SalaryEarned.ToString() +
                                    "," + p.Allowances + "," + (int)p.PensionType +
                                    "," + p.BankName.ToString() + "," + p.AccountNumber.ToString() +
                                    "," + (int)p.BankType + "\n";

                    writer.Write(line); 
                }

                writer.Flush();
                }
            
            
            return "Record Deleted";
        }

    }
}