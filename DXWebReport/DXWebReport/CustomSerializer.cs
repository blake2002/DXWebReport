using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Data;

namespace DXWebReport
{
    public class MyDataViewSerializer : DevExpress.XtraReports.Native.IDataSerializer
    {
        public const string Name = "MyDataViewSerializer";

        public bool CanSerialize(object data, object extensionProvider)
        {
            return (data is DataView);
        }
        public string Serialize(object data, object extensionProvider)
        {
            DataView v = data as DataView;
            if (v != null)
            {
                DataTable tbl = v.ToTable();
                StringBuilder sb = new StringBuilder();
                XmlWriter writer = XmlWriter.Create(sb);
                tbl.WriteXml(writer, XmlWriteMode.WriteSchema);
                return sb.ToString();
            }
            return string.Empty;
        }
        public bool CanDeserialize(string value, string typeName, object extensionProvider)
        {
            return typeName == "System.Data.DataView"; 
        }
        public object Deserialize(string value, string typeName, object extensionProvider)
        {
            DataTable tbl = new DataTable();
            using (XmlReader reader = XmlReader.Create(new StringReader(value)))
            {
                tbl.ReadXml(reader);
            }
            return new DataView(tbl);  
        }
    }
}
