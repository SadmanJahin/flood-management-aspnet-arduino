using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FloodManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            String filePath = @"C:\Users\13343\Downloads\Data\Stat\DATA.CSV";
            String[] lines = System.IO.File.ReadAllLines(filePath);
            String[] lastline  = lines.Last().Split(',');
            ViewBag.waterLevel = Convert.ToInt32(lastline[0]);
            ViewBag.windspeed  = Convert.ToInt32(lastline[1]);
            ViewBag.temp       = Convert.ToDouble(lastline[2]);
            ViewBag.humidity = Convert.ToDouble(lastline[3]);

            return View();
        }

        public ActionResult About()
        {
            var data = System.IO.File.ReadAllLines(@"C:\Users\13343\Downloads\Data\DATA.CSV");
            List<Double> distance = new List<Double>();
            List<Double> humidity = new List<Double>();
            List<Double> windspeed = new List<Double>();
            List<Double> temperature = new List<Double>();
            List<Double> time = new List<Double>();
            bool firstLine = false;
            int tomorrow = 36;
            foreach (string line in data)
            {
                if(firstLine)
                {
                var delimitedLine = line.Split(',');
                distance.Add(Convert.ToDouble(delimitedLine[0]));
                windspeed.Add(Convert.ToDouble(delimitedLine[1]));
                temperature.Add(Convert.ToDouble(delimitedLine[2]));
                humidity.Add(Convert.ToDouble(delimitedLine[3]));
                time.Add(Convert.ToDouble(delimitedLine[4]));
                }
                firstLine = true;
            }
            double[] xValues = new double[distance.Count] ;
            double[] yValues = new double[distance.Count];
            
            for (int i=0;i < distance.Count;i++)
            {
                xValues[i] = time[i];
                yValues[i] = distance[i];
            }
            ViewBag.predictedWaterlevel=LinearRegression(xValues, yValues,tomorrow);

            for (int i = 0; i < distance.Count; i++)
            {
                xValues[i] = time[i];
                yValues[i] = humidity[i];
            }
            ViewBag.predictedHumidity = LinearRegression(xValues, yValues,tomorrow);
            for (int i = 0; i < distance.Count; i++)
            {
                xValues[i] = time[i];
                yValues[i] = windspeed[i];
            }
            ViewBag.predictedWindSpeed = LinearRegression(xValues, yValues,tomorrow);

            for (int i = 0; i < distance.Count; i++)
            {
                xValues[i] = time[i];
                yValues[i] = temperature[i];
            }
            ViewBag.predictedTemperature = LinearRegression(xValues, yValues,tomorrow);

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public static double LinearRegression(
              double[] xVals,
              double[] yVals,
              int val
              )
        {
            if (xVals.Length != yVals.Length)
            {
                throw new Exception("Input values should be with the same length.");
            }

            double sumOfX = 0;
            double sumOfY = 0;
            double sumOfXSq = 0;
            double sumOfYSq = 0;
            double sumCodeviates = 0;

            for (var i = 0; i < xVals.Length; i++)
            {
                var x = xVals[i];
                var y = yVals[i];
                sumCodeviates += x * y;
                sumOfX += x;
                sumOfY += y;
                sumOfXSq += x * x;
                sumOfYSq += y * y;
            }

            var count = xVals.Length;
            var ssX = sumOfXSq - ((sumOfX * sumOfX) / count);
            var ssY = sumOfYSq - ((sumOfY * sumOfY) / count);

            var rNumerator = (count * sumCodeviates) - (sumOfX * sumOfY);
            var rDenom = (count * sumOfXSq - (sumOfX * sumOfX)) * (count * sumOfYSq - (sumOfY * sumOfY));
            var sCo = sumCodeviates - ((sumOfX * sumOfY) / count);

            var meanX = sumOfX / count;
            var meanY = sumOfY / count;
            var dblR = rNumerator / Math.Sqrt(rDenom);

           var rSquared = dblR * dblR;
          var  yIntercept = meanY - ((sCo / ssX) * meanX);
          var  slope = sCo / ssX;

            return yIntercept+val*slope;
        }
    }

}
