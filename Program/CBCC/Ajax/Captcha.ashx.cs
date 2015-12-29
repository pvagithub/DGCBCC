using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CBCC.Ajax
{
    /// <summary>
    /// Summary description for Captcha
    /// </summary>
    public class Captcha : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            var key = "key-" + (!string.IsNullOrEmpty(context.Request["t"]) ? context.Request["t"] : "") + (!string.IsNullOrEmpty(context.Request["rand"]) ? context.Request["rand"] : "");

            if (context.Session["captcha_key"] != null) {
                if (context.Session["captcha_key"].ToString() == key) return;
            }
            context.Session["captcha_key"] = key;
            
            WebControlCaptcha.CaptchaImage ci = new WebControlCaptcha.CaptchaImage();

            #region design Captcha

            ci.TextChars = "1234567890";
            ci.TextLength = 5;
            ci.LineNoise = WebControlCaptcha.CaptchaImage.LineNoiseLevel.None;
            ci.LineNoiseColor = System.Drawing.Color.Beige;
            ci.BackgroundNoise = WebControlCaptcha.CaptchaImage.BackgroundNoiseLevel.Medium;
            ci.BackgroundNoiseColor = System.Drawing.Color.Black;
            ci.Font = "Tahoma";
            #endregion
            string _randomText = ci.RandomText;
            System.Drawing.Bitmap bmp = ci.RenderImage();
            if (!string.IsNullOrEmpty(context.Request["t"]))
            {
                
                switch (context.Request["t"].ToUpper())
                {
                    case "JOIN"://Affiliate program
                        context.Session.Add("CAPTCHAJoin", _randomText);
                        break;
                    case "TRYNOW"://Try demo
                        context.Session.Add("TRYNOW", _randomText);
                        break;
                    default:
                        context.Session.Add(context.Request["t"].ToUpper(), _randomText);
                        break;
                }
            }
            else
            {
                context.Session.Add("CAPTCHA", _randomText);
            }
            bmp.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            bmp.Dispose();
            context.Response.ContentType = "image/jpeg";
            context.Response.StatusCode = 200;
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}