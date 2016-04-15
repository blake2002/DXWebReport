using DevExpress.DataAccess.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.DataAccess.EntityFramework;
using DevExpress.XtraReports.Native;
using DXWebReport.Models;

namespace DXWebReport
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ASPxReportDesigner1.DataSources.Add("Genres", DSGenres.Select(new DataSourceSelectArguments()));
                ASPxReportDesigner1.DataSources.Add("Items", new ItemList());

                DevExpress.DataAccess.Sql.SqlDataSource sql = GenerateSqlDataSource();
                ASPxReportDesigner1.DataSources.Add("MS-SQLDatasource", sql);

                DevExpress.DataAccess.ObjectBinding.ObjectDataSource objds = GenerateObjectDataSource();
                ASPxReportDesigner1.DataSources.Add("MyObjectDatasource", objds);

                EFDataSource efds = new EFDataSource(new EFConnectionParameters(typeof(ChinookModel)));
                ASPxReportDesigner1.DataSources.Add("MyEFDatasource", efds);

                var rpt = new DevExpress.XtraReports.UI.XtraReport();
                rpt.Extensions[SerializationService.Guid] = MyDataViewSerializer.Name;
                ASPxReportDesigner1.OpenReport(rpt);
            }
        }
        private DevExpress.DataAccess.Sql.SqlDataSource GenerateSqlDataSource()
        {
            DevExpress.DataAccess.Sql.SqlDataSource result = new DevExpress.DataAccess.Sql.SqlDataSource("ChinookConnection");
            // Create an SQL query.
            result.Queries.Add(new CustomSqlQuery("MyGenreQuery", "SELECT * FROM Genre;"));
            result.RebuildResultSchema();
            return result;
        }

        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource GenerateObjectDataSource()
        {
            DevExpress.DataAccess.ObjectBinding.ObjectDataSource result = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource();
            result.Name = "ObjSource";
            result.DataSourceType = typeof(ItemList);
            result.Constructor = new DevExpress.DataAccess.ObjectBinding.ObjectConstructorInfo();
            return result;
        }
    }

    public class ItemList : List<Item>
    {
        public ItemList()
        {
            for (int i = 0; i < 10; i++)
            {
                Add(new Item() { Name = String.Format("Item{0}", i) });
            }
        }
    }
    public class Item
    {
        public string Name { get; set; }
    }

}