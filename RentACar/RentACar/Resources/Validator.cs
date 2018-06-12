using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Resources
{
    class Validator
    {
        public static bool StringToDecimal(string param)
        {
            decimal test;
            return (decimal.TryParse(param, out test));
        }

        public static bool StringToInteger(string param)
        {
            decimal test;
            return (decimal.TryParse(param, out test));
        }

        public static bool ValidarCedula(string cedula)
        {
            if (cedula == "00000000000") { return false; }

            int vnTotal = 0;
            string vcCedula = cedula.Replace("-", "");
            int pLongCed = vcCedula.Trim().Length;
            int[] digitoMult = new int[11] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 };

            if (pLongCed < 11 || pLongCed > 11)
                return false;

            for (int vDig = 1; vDig <= pLongCed; vDig++)
            {
                int vCalculo = Int32.Parse(vcCedula.Substring(vDig - 1, 1)) * digitoMult[vDig - 1];
                if (vCalculo < 10)
                    vnTotal += vCalculo;
                else
                    vnTotal += Int32.Parse(vCalculo.ToString().Substring(0, 1)) + Int32.Parse(vCalculo.ToString().Substring(1, 1));
            }

            if (vnTotal % 10 == 0)
                return true;
            else
                return false;
        }
    }
}
