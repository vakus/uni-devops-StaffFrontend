using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StaffFrontend
{
    public class Utils
    {
        /**
         * This function creates UriBuilder based on values provided in config variable.
         * It will take the value of "domain" as base for the URI, then it will set the port based on "port" 
         * in configuration section.
         * 
         * Then it will create path which will be ran through processString, with values.
         * After that, this function will check if get parameters are set in the configuration
         * if they are they will be added to the UriBuilder, with parameters being also processed by
         * processString function
         */
        public static UriBuilder createUriBuilder(IConfigurationSection config, Dictionary<string, object> values)
        {
            UriBuilder builder = new UriBuilder(config.GetValue<string>("domain"));

            builder.Port = config.GetValue<int>("port");
            builder.Path = processString(config.GetValue<string>("path"), values);

            IConfigurationSection parameters = config.GetSection("get");

            if (parameters.Exists())
            {
                //Parse all get parameters

                var Query = HttpUtility.ParseQueryString(builder.Query);

                foreach (KeyValuePair<string, string> entry in parameters.AsEnumerable())
                {
                    Query[entry.Key] = processString(entry.Value, values);
                }

                builder.Query = Query.ToString();
            }

            return builder;
        }

        /**
         * This function takes a string, and a dictionary.
         * The function will loop over every pair in dictionary, and replace any findings of "{key}" with its equivalent value.
         * 
         * e.g. If we have string "test/{replace}" and list which has key "replace" and value 42, the result will be "test/42"
         */
        public static string processString(string str, Dictionary<string, object> values)
        {
            string processed = str;
            foreach(KeyValuePair<string, object> entry in values)
            {
                processed = processed.Replace(("{" + entry.Key + "}"), entry.Value.ToString());
            }
            return processed;
        }
    }
}
