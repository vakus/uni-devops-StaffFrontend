using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
         * processString function.
         */
        public static UriBuilder CreateUriBuilder(IConfigurationSection config, Dictionary<string, object> values)
        {
            UriBuilder builder = new UriBuilder(config.GetValue<string>("domain"))
            {
                Port = config.GetValue<int>("port"),
                Path = ProcessString(config.GetValue<string>("path"), values)
            };

            builder.Query = CreateGetParameters(config, values, builder);

            return builder;
        }

        /**
         * This function creates get parameters for UriBuilder, and returns the query as a string.
         * After processing parameters with values, if the result would be an empty value, it will be skipped instead.
         */
        public static string CreateGetParameters(IConfigurationSection config, Dictionary<string, object> values, UriBuilder builder)
        {
            IConfigurationSection parameters = config.GetSection("get");
            var Query = HttpUtility.ParseQueryString(builder.Query);

            if (parameters.Exists())
            {
                //Parse all get parameters

                Dictionary<string, string> entries = new Dictionary<string, string>();
                parameters.Bind(entries);

                foreach (KeyValuePair<string, string> entry in entries)
                {
                    //we want only to add non-empty/non-null parameters
                    string processedValue = ProcessString(entry.Value, values);
                    if (!String.IsNullOrEmpty(processedValue))
                    {
                        Query[entry.Key] = processedValue;
                    }
                }
            }

            return Query.ToString();
        }

        /**
         * This function creates list of key value pairs that represent post parameters based on config file as well as data from values.
         * This function works similar to CreateGetParameters.
         * 
         * After processing parameters with values, if the result would be an empty value, it will be skipped instead.
         */
        public static List<KeyValuePair<string, string>> CreatePostParameters(IConfigurationSection config, Dictionary<string, object> values)
        {
            var resp = new List<KeyValuePair<string, string>>();
            if (config.Exists())
            {
                var entries = new Dictionary<string, string>();
                config.Bind(entries);

                foreach(KeyValuePair<string, string> entry in entries)
                {
                    string processedValue = ProcessString(entry.Value, values);
                    if (!String.IsNullOrEmpty(processedValue))
                    {
                        resp.Add(new KeyValuePair<string, string>(entry.Key, processedValue));
                    }
                }
            }
            return resp;
        }

        /**
         * This function takes a string, and a dictionary.
         * The function will loop over every pair in dictionary, and replace any findings of "{key}" with its equivalent value.
         * If the string is equal to "{key}" but the value for the key is null, this function will return null
         * 
         * few examples:
         * 
         * If we have string "test/{replace}" and list which has key "replace" and value 42, the result will be "test/42"
         * If we have string "{replace}" and list which has key "replace" and null for value, the result will be null
         * If we have string "{replace}" and list which has key "replace" and value 42, the result will be "42"
         * If we have string "test/replace" and list which has key "replace" and value 42, the result will be "test/replace"
         */
        public static string ProcessString(string str, Dictionary<string, object> values)
        {
            string processed = str;
            foreach(KeyValuePair<string, object> entry in values)
            {
                if (entry.Value == null)
                {
                    if (processed.Equals("{" + entry.Key + "}"))
                    {
                        //the value was nullable and contains no value, and the string is equal to the key
                        return null;
                    }
                }
                else
                {
                    processed = processed.Replace(("{" + entry.Key + "}"), entry.Value.ToString());
                }
            }
            return processed;
        }

        /**
         * This function returns response from get/post request based on information in config as well as from values.
         * If config has "post" section, the call will automatically be switched to "post". All Get parameters will be sent along Post request
         * If config doesnt have "post" section, the call will automatically be switched to "get". Only Get parameters will be sent with Get request
         */
        public static async Task<HttpResponseMessage> Request(HttpClient client, IConfigurationSection config, Dictionary<string, object> values)
        {
            string url = Utils.CreateUriBuilder(config, values).ToString();

            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");

            if (config.GetSection("post").Exists())
            {
                //this should be post request
                return await client.PostAsync(url, new FormUrlEncodedContent(CreatePostParameters(config.GetSection("post"), values)));

            }
            else
            {
                return await client.GetAsync(url);
            }

        }
    }
}
