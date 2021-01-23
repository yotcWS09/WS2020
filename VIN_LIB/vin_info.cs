using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIN_LIB
{
    public class vin_info
    {

        private List<string> WMI = new List<string>()
        {
            "AA","AB","AC","AD","AE","AF","AG","AH"
        };

        public Boolean CheckVIN (string vin){
            var AlphaList = "ABCDEFGHJKLMNPRSTUVWXYZ";
            string DigitsList = "12345678123457923456789";
            var WeightString = new List<int>() { 8, 7, 6, 5, 4, 3, 2, 10, 0, 9, 8, 7, 6, 5, 4, 3, 2 };

            if( vin.Length!=17)
                return false;

            var Vin = vin.ToUpper();
            
            var AvailableChers = "0123456789ABCDEFGHJKLMNPRSTUVWXYZ";
            foreach(char Char in Vin)
            {
                if (AvailableChers.IndexOf(Char) == -1)
                    return false;
            }

            if (WMI.IndexOf(Vin.Substring(0, 2)) == -1)
                return false;

            if ("0123456789X".IndexOf(Vin.Substring(8, 1)) == -1)
                return false;
            var LastForDigits = Vin.Substring(Vin.Length - 4);
            foreach(char Char in LastForDigits)
            {
                if (!char.IsDigit(Char))
                    return false;
            }

            int CHK = 0;

            for (int i=0; i<17; i++)
            {
                if (i != 8)
                {
                    
             

                    var index = AlphaList.IndexOf(Vin[i]);
                    if (index >= 0)
                    {
                        //CHK += int.Parse(DigitsList[index];
                        CHK += int.Parse (DigitsList[index].ToString())*WeightString[i];
                    }
                    else
                    {
                        CHK += int.Parse(Vin[i].ToString()) * WeightString[i];
                    }
                }
            }
            CHK -= (int)((Math.Ceiling((Decimal)(CHK / 11))) *11);
            if (CHK == 10)
            {
                return Vin[8] == 'x';
            }

            return Vin[8].ToString() == CHK.ToString();
        }
    }

}
