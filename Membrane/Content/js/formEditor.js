var formEditor =
{
    initialiseTextEditor: function() {
        $('.texteditor').wymeditor({
            stylesheet: '/Content/css/styles.css',
            basePath: '/Content/wymeditor/',
            skinPath: '/Content/wymeditor/skins/default/',
            postInit: function(wym) {

                //postInit is executed after WYMeditor initialization
                //'wym' is the current WYMeditor instance

                //we generally activate plugins after WYMeditor initialization

                //activate 'hovertools' plugin
                //which gives advanced feedback to the user:
                wym.hovertools();
            }

        });
    },

    initialistDatePicker: function() {

        $('.datePicker').DatePicker({
            format: 'd/m/Y',
            date: $('.datePicker').val(),
            current: $('.datePicker').val(),
            starts: 1,
            position: 'r',
            onBeforeShow: function() {
                if ($('.datePicker').val() != '')
                    $('.datePicker').DatePickerSetDate($('.datePicker').val(), true);
                else
                    $('.datePicker').DatePickerSetDate(new Date(), true);

            },
            onChange: function(formated, dates) {
                $('.datePicker').val(formated);
                $('.datePicker').DatePickerHide();
            }

        });
    }
}

$(document).ready(function() {
    formEditor.initialiseTextEditor();
    formEditor.initialistDatePicker();
});