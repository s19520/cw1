using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace cw1
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            if (args.Length == 0) throw new ArgumentNullException("Arguments list is empty. Please provide URL as the first argument.");
            bool result = Uri.TryCreate(args[0], UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttps || uriResult.Scheme == Uri.UriSchemeHttp);
            if (!result) throw new ArgumentException("The first argument is not the URL.");
            
            using HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(uriResult);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                Regex mailRegex = new Regex("[a-z]+@[a-z.]+");
                var matches = mailRegex.Matches(content);
                HashSet<string> hashSet = new HashSet<string>();
                foreach (object m in matches) hashSet.Add(m.ToString());
                foreach (object n in hashSet) Console.WriteLine(n);
                if (hashSet.Count==0) Console.WriteLine("Nie znaleziono adresów email"); //dodatkowe-wymagania
            }
            else throw new Exception("Błąd w czasie pobierania strony"); //dodatkowe-wymagania


        }
    }
}
