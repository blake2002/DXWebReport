using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace DXWebReport
{
    public class FilesystemReportStorageWebExtension : DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension
    {
        private readonly string _ReportStoragePath;
        private readonly HttpContext _Context;

        public FilesystemReportStorageWebExtension(HttpContext context)
        {
            _Context = context;
            _ReportStoragePath = context.Server.MapPath(@"~/App_Data/Reports/");
        }

        public HttpContext Context { get { return _Context; } }

        protected string GetPath(string url)
        {
            if (Path.IsPathRooted(url))
            {
                return url;
            }

            return Context.Server.MapPath(url);
        }

        public override bool CanSetData(string url)
        {
            string filePath = GetPath(url);
            return File.Exists(filePath);
        }

        public override byte[] GetData(string url)
        {
            string filePath = GetPath(url);
            return File.ReadAllBytes(filePath);

        }

        public override Dictionary<string, string> GetUrls()
        {
            Dictionary<string, string> urls = new Dictionary<string, string>();

            string[] reportFiles = Directory.GetFiles(_ReportStoragePath, "*.repx", SearchOption.AllDirectories);

            foreach (string filePath in reportFiles)
            {
                urls.Add(filePath, filePath.Replace(_ReportStoragePath, String.Empty));
            }

            return urls;
        }

        public override bool IsValidUrl(string url)
        {
            string filePath = GetPath(url);
            if (File.Exists(filePath))
                return true;

            try
            {
                File.Create(filePath).Close();
                File.Delete(filePath);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public override void SetData(XtraReport report, string url)
        {
            string filePath = GetPath(url);
            report.SaveLayout(filePath, true);
        }

        public override string SetNewData(XtraReport report, string defaultUrl)
        {
            string filePath = GetPath(defaultUrl);
            report.SaveLayout(filePath, true);
            return filePath;
        }

    }
}