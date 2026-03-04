using RetailShopManagement.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopManagement.Domain.Constants
{
    public static class UnitsConst
    {
        public static string None => "None";

        // WEIGHT
        public static string Gram => "Gram (g)";
        public static string Kilogram => "Kilogram (Kg)";
        
        // VOLUME
        public static string Milliliter => "Milliliter (mL)";
        public static string Liter => "Liter (L)";
        public static string CubicMeter => "Cubic Meter (m³)";
        public static string Gallon => "Gallon";
        
        // LENGTH
        public static string Millimeter => "Millimeter (mm)";
        public static string Centimeter => "Centimeter (cm)";
        public static string Meter => "Meter (m)";
        public static string Kilometer => "Kilometer (km)";
        public static string Inch => "Inch (in)";
        public static string Foot => "Foot (ft)";
        public static string Yard => "Yard (yd)";

        // AREA
        public static string SquareMeter => "Square Meter (m²)";
        public static string SquareFoot => "Square Foot (ft²)";
        
        // COUNT / PIECE
        public static string Piece => "Piece (Pc)";
        public static string Unit => "Unit";
        public static string Pair => "Pair";
        public static string Dozen => "Dozen (12)";

        // PACKAGING
        public static string Packet => "Packet";
        public static string Box => "Box";
        public static string Carton => "Carton";
        public static string Bottle => "Bottle";
        public static string Can => "Can";
        public static string Roll => "Roll";
        public static string Bundle => "Bundle";
        public static string Sack => "Sack";
        public static string Bag => "Bag";
        public static string Strip => "Strip";
 
        // DROPDOWN LIST
        public static IList<DropDownField> UnitsFields =>
            new List<DropDownField>
            {
            new() { Value = None, Text = None },

            // Weight
            new() { Value = Gram, Text = Gram },
            new() { Value = Kilogram, Text = Kilogram },

            // Volume
            new() { Value = Milliliter, Text = Milliliter },
            new() { Value = Liter, Text = Liter },
            new() { Value = CubicMeter, Text = CubicMeter },
            new() { Value = Gallon, Text = Gallon },

            // Length
            new() { Value = Millimeter, Text = Millimeter },
            new() { Value = Centimeter, Text = Centimeter },
            new() { Value = Meter, Text = Meter },
            new() { Value = Kilometer, Text = Kilometer },
            new() { Value = Inch, Text = Inch },
            new() { Value = Foot, Text = Foot },
            new() { Value = Yard, Text = Yard },

            // Area
            new() { Value = SquareMeter, Text = SquareMeter },
            new() { Value = SquareFoot, Text = SquareFoot },

            // Count
            new() { Value = Piece, Text = Piece },
            new() { Value = Unit, Text = Unit },
            new() { Value = Pair, Text = Pair },
            new() { Value = Dozen, Text = Dozen },

            // Packaging
            new() { Value = Packet, Text = Packet },
            new() { Value = Box, Text = Box },
            new() { Value = Carton, Text = Carton },
            new() { Value = Bottle, Text = Bottle },
            new() { Value = Can, Text = Can },
            new() { Value = Roll, Text = Roll },
            new() { Value = Bundle, Text = Bundle },
            new() { Value = Sack, Text = Sack },
            new() { Value = Bag, Text = Bag },
            new() { Value = Strip, Text = Strip },

            };
    }
}
