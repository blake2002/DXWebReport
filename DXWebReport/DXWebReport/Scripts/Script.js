function GetDesignerUrl() {
    return reportDesigner.GetDesignerModel().navigateByReports.currentTab().url();
}

function SetDesignerUrl(url) {
    reportDesigner.GetDesignerModel().navigateByReports.currentTab().url(url);
}

function reportDesigner_Open() {
    fileDialog.ShowOpenFileDialog(OpenFileDialogResult);
}

function reportDesigner_Save() {
    var fileName = GetDesignerUrl();
    if (fileName) {
        DevExpress.Designer.Report.ReportStorageWeb.setData(reportDesigner.GetDesignerModel().model().serialize(), fileName)
            .done(function (r) {
                alert("Report " + fileName + " was saved.");
            })
            .fail(function (xhr, textStatus, errorThrown) {
                debugger;
                alert("An exception occured while saving the report: " + f.statusText);
            });
    }
    else {
        reportDesigner_SaveAs();
    }
}

function reportDesigner_SaveAs() {
    fileDialog.ShowSaveFileDialog(SaveFileDialogResult);
}

function OpenFileDialogResult(fileName) {
    DevExpress.Designer.Report.ReportStorageWeb.getData(fileName)
        .done(function (result) {
            var model = new DevExpress.Designer.Report.ReportViewModel(JSON.parse(result.reportLayout));
            model.dataSourceRefs = result.dataSourceRefInfo;
            reportDesigner.GetDesignerModel().model(model);
            SetDesignerUrl(fileName);
        })
        .fail(function (f) {
            alert("An exception occured while opening the report: " + f.statusText);
        });
}

function SaveFileDialogResult(fileName) {
    DevExpress.Designer.Report.ReportStorageWeb.setNewData(reportDesigner.GetDesignerModel().model().serialize(), fileName)
        .done(function (result) {
            SetDesignerUrl(result);
            alert("Report " + result + " was saved.");
        })
        .fail(function (f) {
            alert("An exception occured while saving the report: " + f.statusText);
        });
}

function reportDesigner_CustomizeMenuActions(s, e) {
    var defaultOpenAction = e.Actions.filter(function (x) { return x.text === 'Open' && x.imageClassName !== 'reportDesignerIconOpen' })[0];
    var defaultSaveAction = e.Actions.filter(function (x) { return x.text === 'Save' && x.imageClassName !== 'reportDesignerIconSave' })[0];
    var defaultSaveAsAction = e.Actions.filter(function (x) { return x.text === 'Save As' && x.imageClassName !== 'reportDesignerIconSaveAs' })[0];

    if (defaultOpenAction)
        defaultOpenAction.clickAction = reportDesigner_Open;

    if (defaultSaveAction) {
        defaultSaveClickAction = defaultSaveAction.clickAction;
        defaultSaveAction.clickAction = reportDesigner_Save;
    }

    if (defaultSaveAsAction)
        defaultSaveAsAction.clickAction = reportDesigner_SaveAs;
}
