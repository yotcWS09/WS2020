using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIN_LIB
{
    internal class Regions
    {
        public string CodeFrom;
        public string CodeTo;
        public string Country;
        public Regions(string codeFrom, string codeTo, string country) {
            CodeFrom = codeFrom;
            CodeTo = codeTo;
            Country = country;
        }
        public Boolean CodeInRegion(string code) {
            return String.Compare(code, CodeFrom)>=0 & String.Compare(code, CodeTo)<=0;
        }
    }

    public class vin_info
    {

        private List<Regions> WMI = new List<Regions>()
        {
            new Regions("AA", "AH", "ЮАР"),
            new Regions("JA", "JT", "Япония")
        };

        public Boolean CheckVIN (string vin){
            var AlphaList =     "ABCDEFGHJKLMNPRSTUVWXYZ";
            string DigitsList = "12345678123457923456789";
            var WeightString = new List<int>() { 8, 7, 6, 5, 4, 3, 2, 10, 0, 9, 8, 7, 6, 5, 4, 3, 2 };

            try
            {
                if (vin.Length != 17)
                    throw new Exception("Длина VIN должна быть 17 знаков");

                var Vin = vin.ToUpper();

                var AvailableChers = "0123456789ABCDEFGHJKLMNPRSTUVWXYZ";
                foreach (char Char in Vin)
                {
                    if (AvailableChers.IndexOf(Char) == -1)
                        throw new Exception( String.Format("Недопустимый символ в VIN-номере {0}", Char) );
                }

                var found = false;
                foreach (Regions Region in WMI)
                {
                    if (Region.CodeInRegion(Vin.Substring(0, 2)))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                    throw new Exception(String.Format("Регион VIN-номера не найден {0}", Vin.Substring(0, 2)));

                if ("0123456789X".IndexOf(Vin.Substring(8, 1)) == -1)
                    throw new Exception(String.Format("Контрольная сумма VIN-номера ({0}) вне диапазона 0-9,X", Vin.Substring(8, 1)));

                var LastForDigits = Vin.Substring(Vin.Length - 4);
                foreach (char Char in LastForDigits)
                {
                    if (!char.IsDigit(Char))
                        throw new Exception("Последние 4 символа VIN-номера должны быть цифрами");
                }

                int CHK = 0;

                for (int i = 0; i < 17; i++)
                {
                    if (i != 8)
                    {
                        var index = AlphaList.IndexOf(Vin[i]);
                        if (index >= 0)
                        {
                            //CHK += int.Parse(DigitsList[index];
                            CHK += int.Parse(DigitsList[index].ToString()) * WeightString[i];
                        }
                        else
                        {
                            CHK += int.Parse(Vin[i].ToString()) * WeightString[i];
                        }
                    }
                }

                CHK -= (int)((Math.Ceiling((Decimal)(CHK / 11))) * 11);
                if (CHK == 10)
                {
                    return Vin[8] == 'x';
                }

                if(Vin[8].ToString() != CHK.ToString())
                    throw new Exception("Не совпала контрольная сумма VIN-номера");

                return true;
            } catch (Exception e) {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return false;
            }
        }


        public string GetVINCountry(string vin)
        {
            foreach (Regions Region in WMI)
            {
                if (Region.CodeInRegion( vin.ToUpper().Substring(0, 2) ))
                {
                    return Region.Country;
                }
            }
            return "";
        }
    }


}
