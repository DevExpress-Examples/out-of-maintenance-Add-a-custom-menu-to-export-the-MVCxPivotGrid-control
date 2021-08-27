<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/180379079/18.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T830425)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# Add a custom menu to export the MVCxPivotGrid control
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/180379079/)**
<!-- run online end -->
 
This example demonstrates how to create a custom menu that allows exporting MVCxPivotGrid to different formats and how to process the menu click event on the client and server sides.
 
The MVCxPivotGrid control does not provide a build in a toolbar. Thus, it is necessary to create a toolbar manually by using the <a href="https://docs.devexpress.com/AspNet/9003/asp.net-mvc-extensions/site-navigation-and-layout/menu/overview-menu">Menu</a> extension.
 

```cs
@Html.DevExpress().Menu(settings => {
    settings.Name = "menu";
    settings.Width = Unit.Parse("100%");
    settings.ShowPopOutImages = DefaultBoolean.True;
    settings.RootItemSubMenuOffset.LastItemX = 8;

    settings.SettingsAdaptivity.Enabled = true;
    settings.SettingsAdaptivity.EnableAutoHideRootItems = false;
    settings.SettingsAdaptivity.EnableCollapseRootItemsToIcons = true;
    settings.SettingsAdaptivity.CollapseRootItemsToIconsAtWindowInnerWidth = 1200;
    settings.Styles.Item.CssClass = "dxm-menuItem";
    settings.Styles.SubMenu.CssClass = "subMenu";
    settings.Items.Add(item => {
        item.Name = "XlsWysiwyg";
        item.Text = "Export to XLS(WYSIWYG)";
        item.Image.IconID = DevExpress.Web.ASPxThemes.IconID.ExportExporttoxls32x32;
    });
    settings.Items.Add(item => {
        item.Name = "XlsDataAware";
        item.Text = "Export to XLS(Data Aware)";
        item.Image.IconID = DevExpress.Web.ASPxThemes.IconID.ExportExporttoxls32x32gray;
    });
    ...
    settings.ClientSideEvents.ItemClick = "onMenuItemClick";
}).GetHtml()

```

 
It is necessary to include the pivot grid extension into the form to properly apply control state during export
```cs
@using (Html.BeginForm("ExportTo", "Home")) {
    @Html.Hidden("exportCommand")
    @Html.Action("PivotGridPartial")
}

```

On the client side, the following script is used to write information about the clicked item to the hidden exportCommand control and to submit the form.
```js
function onMenuItemClick(s, e) {
	var $exportFormat = $('#exportCommand');
	$exportFormat.val(e.item.name);
	$('form').submit();
	window.setTimeout(function () {
		$exportFormat.val("");
	}, 0);
}
```
 
On the server side, the ExportTo action is used to export the pivot grid to different formats. The value of the exportCommand control is passed there as a parameter.


```cs
public ActionResult ExportTo(string exportCommand) {
    var settings = CreatePivotGridSettings();
    var data = NwindModel.GetInvoices();

    switch (exportCommand) {
        case "XlsWysiwyg":
            return PivotGridExtension.ExportToXls(settings,data, 
                new DevExpress.Web.ASPxPivotGrid.PivotXlsExportOptions() { ExportType = DevExpress.Export.ExportType.WYSIWYG } );
        case "XlsDataAware":
            return PivotGridExtension.ExportToXls(settings, data);
            ...
```
 
This example is built using MVCxPivotGrid control version 18.2.3 and MVC5 however, it is possible to use the same approach in the earlier control versions.
