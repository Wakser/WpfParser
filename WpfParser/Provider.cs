using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WpfParser
{
    public class Provider
    {
        public static double price = 0;
        public static string Get_Name(string path)
        {
            var request = new GetRequest(path);
            request.Run();
            string response = request.Response;

            string meta_description = "<meta property=\"og:title\" content=\"";
            int meta_start = response.IndexOf(meta_description) + meta_description.Length;
            int meta_end = response.IndexOf("\"", meta_start);
            string product_name = response.Substring(meta_start, meta_end - meta_start);

            return product_name;
        }

        public static double Get_Price(string path)
        {
            var request = new GetRequest(path);
            request.Run();
            string response = request.Response;

            var json = JObject.Parse(response);
            double price = Convert.ToDouble(json["prices"]["current"]["amount"]);

            return price;
        }

        public static string Get_ProductPath(string path)
        {
            string main_path = "https://catalog.api.onliner.by/products/";
            int index = path.LastIndexOf('/');
            index++;
            for (; index < path.Length; index++)
            {
                main_path += path[index];
            }
            main_path += "/prices-history?period=2m";
            return main_path;
        }
    }
}
