
// npm package: datatables.net-bs5
// github link: https://github.com/DataTables/Dist-DataTables-Bootstrap5

$(function () {
    'use strict';

    $(function () {
        $('#dataTableExample').DataTable({
            "aLengthMenu": [
                [10, 30, 50, -1],
                [10, 30, 50, "All"]
            ],
            "iDisplayLength": 10,
            "language": {
                search: ""
            },
            language: {
                url: '//cdn.datatables.net/plug-ins/2.0.3/i18n/es-ES.json',
            },
            buttons: [
                'copy', 'excel', 'pdf'
            ]
        }
        );
        $('#dataTableExample').each(function () {
            var datatable = $(this);
            //datatable.buttons().container().appendeTo('#layout')
            // SEARCH - Add the placeholder for Search and Turn this into in-line form control

            datatable.buttons.container().appendTo($('.col-sm-6:eq(0)', table.table().container()));
            var search_input = datatable.closest('.dataTables_wrapper').find('div[id$=_filter] input');
            search_input.attr('placeholder', 'Search');
            search_input.removeClass('form-control-sm');
            // LENGTH - Inline-Form control
            var length_sel = datatable.closest('.dataTables_wrapper').find('div[id$=_length] select');
            length_sel.removeClass('form-control-sm');
        });
    });

});