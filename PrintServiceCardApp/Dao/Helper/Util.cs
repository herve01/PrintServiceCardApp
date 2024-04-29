using PrintServiceCardApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintServiceCardApp.Dao.Helper
{
    public class Util
    {
        public static SexeType ToSexeType(string value)
        {

            switch (value.Trim())
            {
                case "Femme":
                case "Féminin":
                    return SexeType.Femme;

                case "Homme":
                case "Masculin":
                    return SexeType.Homme;

                default:
                    return SexeType.Homme;
            }
        }

    }
}
