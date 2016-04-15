using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DXWebReport
{
    public partial class FileDialogControl : System.Web.UI.UserControl
    {        
        private string _ClientInstanceName;
        public string ClientInstanceName
        {
            get { return String.IsNullOrEmpty(_ClientInstanceName) ? ClientID : _ClientInstanceName; }
            set { _ClientInstanceName = value; }
        }
        
        public FileDialogControl()
        {
            _ClientInstanceName = null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            popup.ClientSideEvents.Init = String.Format("function(s, e) {{ {0}.initializeControl(); }}", ClientInstanceName);
            fileManager.ClientSideEvents.SelectionChanged = String.Format("function(s, e) {{ {0}.fileManager_SelectionChanged(s, e); }}", ClientInstanceName);
            buttonCancel.ClientSideEvents.Click = String.Format("function(s, e) {{ {0}.buttonCancel_Click(); }}", ClientInstanceName);
            buttonOk.ClientSideEvents.Click = String.Format("function(s, e) {{ {0}.buttonOk_Click(); }}", ClientInstanceName);
            validationCallback.ClientSideEvents.CallbackComplete = String.Format("function(s, e) {{ {0}.validationCallback_CallbackComplete(s, e); }}", ClientInstanceName);
        }

        protected void validationCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            try
            {
                string[] parameters = e.Parameter.Split('|');
                string dialogMode = parameters[0];
                string fileName = Server.MapPath(parameters[1]);
                switch (dialogMode)
                {
                    case "OPEN":
                        using (var file = File.OpenRead(fileName))
                        {
                        }
                        break;
                    case "SAVE":
                        if (!File.Exists(fileName))
                        {
                            using (var file = File.Create(fileName))
                            {
                            }
                            File.Delete(fileName);
                        }
                        break;
                }
                e.Result = String.Empty;
            }
            catch (Exception ex)
            {
                string rootPath = Server.MapPath(fileManager.Settings.RootFolder);
                e.Result = ex.Message.Replace(rootPath, fileManager.Settings.RootFolder);
            }
        }
    }
}