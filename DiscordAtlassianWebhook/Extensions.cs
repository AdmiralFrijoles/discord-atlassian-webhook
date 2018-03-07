using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using DiscordAtlassianWebhook.Config.Models;

namespace DiscordAtlassianWebhook
{
    internal static class Extensions
    {
        public static void CreateResponse(this HttpListenerResponse response, HttpStatusCode statusCode, string message,
            string contentType = "text/plain")
        {
            using (var memStream = new MemoryStream())
            {
                var contentBytes = Encoding.UTF8.GetBytes(message);
                memStream.Write(contentBytes, 0, contentBytes.Length);

                memStream.Seek(0, SeekOrigin.Begin);
                response.ContentEncoding = Encoding.UTF8;
                response.CreateResponse(statusCode, memStream, contentType);
            }
        }

        public static void CreateResponse(this HttpListenerResponse response, HttpStatusCode statusCode,
            Stream contentStream, string contentType)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            try
            {
                var assemblyName = Assembly.GetEntryAssembly().GetName();
                string serverName = assemblyName.Name;
                string serverVersion = assemblyName.Version.ToString();

                response.StatusCode = (int) statusCode;
                response.AddHeader("Date", DateTime.UtcNow.ToString("R"));
                response.Headers.Remove("Server");
                response.AddHeader("Server", $"{serverName}/{serverVersion}");
                response.ContentType = contentType;
                response.ContentLength64 = contentStream.Length;

                var buffer = new byte[1024 * 16];
                int nbytes;
                while ((nbytes = contentStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    response.OutputStream.Write(buffer, 0, nbytes);
                }

                response.OutputStream.Flush();
            }
            catch
            {
                response.StatusCode = (int) HttpStatusCode.InternalServerError;
            }
            finally
            {
                response.OutputStream.Close();
            }
        }
        
        public static string ToCamelCase(this string source)
        {
            return string.IsNullOrWhiteSpace(source)
                ? string.Empty
                : $"{source[0].ToString().ToLowerInvariant()}{source.Substring(1)}";
        }

        public static string ToPascalCase(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }

            // Replace all non-letter and non-digits with an underscore and lowercase the rest.
            string sample = string.Join("", str?.Select(c => char.IsLetterOrDigit(c) ? c.ToString().ToLower() : "_").ToArray());

            // Split the resulting string by underscore
            // Select first character, uppercase it and concatenate with the rest of the string
            var arr = sample?
                .Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => $"{s.Substring(0, 1).ToUpper()}{s.Substring(1)}");

            // Join the resulting collection
            sample = string.Join("", arr);

            return sample;
        }

        public static int ToInteger(this Color color)
        {
            return BitConverter.ToInt32(new byte[] {color.B, color.G, color.R, 0}, 0);
        }

        public static IEnumerable<Discord.Models.Webhook> GetWebhooks(this DiscordConfig config, params string[] webhookNames)
        {
            if (webhookNames != null && webhookNames.Length > 0)
            {
                return config.Webhooks?.Where(i => webhookNames.Contains(i.Name, StringComparer.InvariantCultureIgnoreCase));
            }
            else
            {
                return config.Webhooks;
            }
        }

        public static string Truncate(this string source, int maxLength)
        {
            return source.Length > maxLength ? source.Substring(0, maxLength) : source;
        }

        public static string FirstLine(this string source)
        {
            return source.Lines().FirstOrDefault();
        }

        public static IEnumerable<string> Lines(this string source)
        {
            var lines = source.Split(new[] {"\r\n", "\n"}, StringSplitOptions.None);
            foreach (string line in lines)
            {
                yield return line;
            }
        }
    }
}
