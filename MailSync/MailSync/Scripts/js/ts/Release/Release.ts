import { EmitType, Internationalization } from '@syncfusion/ej2-base';
import { Ajax } from '@syncfusion/ej2-base';
import { enableRipple } from '@syncfusion/ej2-base';
import { Button } from '@syncfusion/ej2-buttons';
import { Calendar, ChangedEventArgs, RenderDayCellEventArgs } from '@syncfusion/ej2-calendars';
import { DataManager, Query, UrlAdaptor } from '@syncfusion/ej2-data';
import { ExcelExport, Filter, FilterType, Freeze, Grid, Group, Page, QueryCellInfoEventArgs, Resize, RowDataBoundEventArgs, RowDD, Selection, Sort, SortEventArgs, Toolbar } from '@syncfusion/ej2-grids';
import { ClickEventArgs } from '@syncfusion/ej2-navigations';
import { Dialog } from '@syncfusion/ej2-popups';
enableRipple(true);

Grid.Inject(Sort, Page, Freeze, Filter, Toolbar, RowDD, Selection, ExcelExport, Group, Resize);

var listId = $("#listId").val();

let data: DataManager = new DataManager({
    adaptor: new UrlAdaptor(),
    crossDomain: true,
    requestType: 'GET',
    url: '/lists/details/' + listId,
});

window.Subscribe = function (status) {
    if (status ) {
        return '<span class="commentTypeDesc unsubscribed">No</span>';
    } else {

        return '<span class="commentTypeDesc subscribed">Yes</span>';
    }
};

window.Action = function (data) {   

    return '<a href="javascript:void(0)" onclick="deleteconfirmationdialog(' + data.ListID + ',' + data.CustomerId + ', \'' + data.Name +'\')">Delete </a>';
};

window.refresh = function () {
    grid.refresh();
};

let grid: Grid = new Grid(
    {
        allowExcelExport: true,
        allowPdfExport: true,
        allowPaging: true,
        allowSorting: true,
        showColumnChooser: true,
        frozenColumns: 1,
        columns: [
            { field: 'EmailId', headerText: 'Email Address', width: 200 },
            { field: 'Name', headerText: 'Name ', width: 200 },
            { field: 'DOB', headerText: 'DOB', width: 200, format: 'yMd', },
            { field: 'CustomerId',headerText: 'Is Subscribed', template: '${Subscribe(data.IsUnSubscribed)}', width: 150 },
            { field: 'DateAdded', headerText: 'Date Added', format: 'yMd', width: 200, visible: true },
            { field: 'LastUpdated', headerText: 'Last changed', format: 'yMd', width: 200, visible: true },
            { field: 'Name', headerText: 'Action', template: '${Action(data)}', width: 100, visible: true },
        ],
        dataSource: data,
        pageSettings: { pageSize: 10, pageSizes: true },
        toolbar: ['excelexport', 'search', 'columnchooser'],
    });
grid.appendTo('#releaselist');

grid.toolbarClick = (args: ClickEventArgs) => {
    if (args.item.id === 'releaselist_excelexport') {
        grid.excelExport();
    }
    if (args.item.id === 'releaselist_csvexport') {
        grid.csvExport();
    }
};
