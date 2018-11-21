using DevExpress.Web.Mvc;
using DevExpress.XtraPivotGrid;
using MPG_ExportMenu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPG_ExportMenu.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult PivotGridPartial() {
            ViewBag.PivotSettings = CreatePivotGridSettings();
            return PartialView("_PivotGridPartial", NwindModel.GetInvoices());
        }

        public ActionResult ExportTo(string exportCommand) {
            var settings = CreatePivotGridSettings();
            var data = NwindModel.GetInvoices();

            switch (exportCommand) {
                case "XlsWysiwyg":
                    return PivotGridExtension.ExportToXls(settings,data, 
                        new DevExpress.Web.ASPxPivotGrid.PivotXlsExportOptions() { ExportType = DevExpress.Export.ExportType.WYSIWYG } );
                case "XlsDataAware":
                    return PivotGridExtension.ExportToXls(settings, data);
                case "XlsxWysiwyg":
                    return PivotGridExtension.ExportToXlsx(settings, data,
                        new DevExpress.Web.ASPxPivotGrid.PivotXlsxExportOptions() { ExportType = DevExpress.Export.ExportType.WYSIWYG });
                case "XlsxDataAware":
                    return PivotGridExtension.ExportToXlsx(settings, data);
                case "Csv":
                    return PivotGridExtension.ExportToCsv(settings, data);
                case "Pdf":
                    return PivotGridExtension.ExportToPdf(settings, data);
                case "Rtf":
                    return PivotGridExtension.ExportToRtf(settings, data);

            }
            return new EmptyResult();            
        }

        private static PivotGridSettings CreatePivotGridSettings() {
            PivotGridSettings settings = new PivotGridSettings();
            settings.Name = "pivotGrid";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "PivotGridPartial" };
            settings.OptionsView.HorizontalScrollBarMode =  DevExpress.Web.ScrollBarMode.Auto;
            settings.OptionsData.AutoExpandGroups = DevExpress.Utils.DefaultBoolean.False;
            settings.Width = new System.Web.UI.WebControls.Unit(100, System.Web.UI.WebControls.UnitType.Percentage);
            settings.Fields.Add("Country", PivotArea.ColumnArea);
            settings.Fields.Add("City", PivotArea.ColumnArea);
            settings.Fields.Add(field => {
                field.Area = PivotArea.DataArea;
                field.FieldName = "ExtendedPrice";
            });
            settings.Fields.Add("ProductName", PivotArea.RowArea);

            return settings;
        }

    }
}