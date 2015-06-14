using eShelvesAPI.Helpers;
using eShelvesAPI.Models;
using eShelvesAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eShelvesAPI.Controllers
{
    public class PreporukeController : ApiController
    {
        [HttpGet]
        public List<KnjigaVM> getPreporuceneKnjige(int korisnikId)
        {
            Preporuka p = new Preporuka();
            return p.GetPreporuceneKnjige(korisnikId);
        }
    }
}
/*
 public double ComputeCoeff(double[] values1, double[] values2)
{
    if(values1.Length != values2.Length)
        throw new ArgumentException("values must be the same length");

    var avg1 = values1.Average();
    var avg2 = values2.Average();

    var sum1 = values1.Zip(values2, (x1, y1) => (x1 - avg1) * (y1 - avg2)).Sum();

    var sumSqr1 = values1.Sum(x => Math.Pow((x - avg1), 2.0));
    var sumSqr2 = values2.Sum(y => Math.Pow((y - avg2), 2.0));

    var result = sum1 / Math.Sqrt(sumSqr1 * sumSqr2);

    return result;
}
Usage:

var values1 = new List<double> { 3, 2, 4, 5 ,6 };
var values2 = new List<double> { 9, 7, 12 ,15, 17 };

var result = ComputeCoeff(values1.ToArray(), values2.ToArray());
// 0.997054485501581

Debug.Assert(result.ToString("F6") == "0.997054");
 */