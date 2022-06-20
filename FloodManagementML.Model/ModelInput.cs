// This file was auto-generated by ML.NET Model Builder. 

using Microsoft.ML.Data;

namespace FloodManagementML.Model
{
    public class ModelInput
    {
        [ColumnName("Distance"), LoadColumn(0)]
        public float Distance { get; set; }


        [ColumnName("Wind Speed"), LoadColumn(1)]
        public float Wind_Speed { get; set; }


        [ColumnName("Temperature"), LoadColumn(2)]
        public float Temperature { get; set; }


        [ColumnName("Humidity"), LoadColumn(3)]
        public float Humidity { get; set; }


        [ColumnName("Time"), LoadColumn(4)]
        public float Time { get; set; }


    }
}
