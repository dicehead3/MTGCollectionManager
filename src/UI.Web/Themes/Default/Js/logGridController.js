(function ($, window) {

    "use strict";

    var defaults = {
        selectors: {
            LogGrid: '#logGrid'
        },
        urls: {
        },
        translations: {
        }
    };


    var logGridController = function (options) {

        var self = this;
        self.options = $.extend(defaults, options);
        self.data = { length: 0 }; // Empty dataset which is provided by reference to the grid, by updating this dataset the grid's data is also updated.

        var gridColumns = this.getGridColumns();
        var gridOptions = this.getGridOptions();

        // Create new grid with empty dataset
        var grid = new Slick.Grid(self.options.selectors.LogGrid, self.data, gridColumns, gridOptions);

        // Subscibe to viewport changes (when grid is scrolled by user)
        grid.onViewportChanged.subscribe(function (e, args) {

            // Update dataset
            var vp = grid.getViewport();
            self.updateData(vp.top, vp.bottom);
            
            // Refresh rows in viewport
            for (var i = vp.top; i <= vp.bottom; i++) {
                grid.invalidateRow(i);
            }
            
            // Update total rows count
            grid.updateRowCount();
            
            // Refresh grid
            grid.render();
        });

        // Trigger viewport change to load data for the first page
        grid.onViewportChanged.notify();
    };

    logGridController.prototype.getGridColumns = function() {
        return [
            { id: "title", name: "Title", field: "title", cssClass: 'logGridItem' },
            { id: "duration", name: "Duration", field: "duration", cssClass: 'logGridItem' },
            { id: "%", name: "% Complete", field: "percentComplete", cssClass: 'logGridItem' },
            { id: "start", name: "Start", field: "start", cssClass: 'logGridItem' },
            { id: "finish", name: "Finish", field: "finish", cssClass: 'logGridItem' },
            { id: "effort-driven", name: "Effort Driven", field: "effortDriven", cssClass: 'logGridItemLast' }
        ];
    };

    logGridController.prototype.getGridOptions = function() {
        return {
            enableCellNavigation: true,
            enableColumnReorder: false,
            rowHeight: 44,
            forceFitColumns: true
        };
    };

    logGridController.prototype.updateData = function(from, to) {

        // Determine from, to
        
        // Create request
        
        // Do request (loading indicator)
        
        // On success
        
        // On error

        for (var i = from; i <= to; i++) {
            this.data[i] = {
                title: "Task " + i,
                duration: "5 days",
                percentComplete: Math.round(Math.random() * 100),
                start: "01/01/2009",
                finish: "01/05/2009",
                effortDriven: (i % 5 == 0)
            };
        }

        this.data.length = 500;
    };

    window.LogGridController = logGridController;

})($, window);