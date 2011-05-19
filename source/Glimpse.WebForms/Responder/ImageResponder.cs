﻿using System.Reflection;
using System.Web;
using Glimpse.WebForms.Configuration;

namespace Glimpse.WebForms.Responder
{
    public abstract class ImageResponder : GlimpseResponder
    {
        public abstract string ContentType { get; } 

        public override void Respond(HttpApplication application, GlimpseConfiguration config)
        {
            var response = application.Response;
            var assembly = Assembly.GetExecutingAssembly();

            using (var resourceStream = assembly.GetManifestResourceStream("Glimpse.WebForms." + ResourceName))
            {
                if (resourceStream != null)
                {
                    var byteArray = new byte[resourceStream.Length];
                    resourceStream.Read(byteArray, 0, byteArray.Length);
                    response.OutputStream.Write(byteArray, 0, byteArray.Length);
                }
            }
            response.AddHeader("Content-Type", ContentType);
            application.CompleteRequest();

            return;
        }
    }
}