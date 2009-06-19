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
    $('.datePicker').datepicker({ dateFormat: 'dd/mm/yy'});
    }
}

$(document).ready(function() {
    formEditor.initialiseTextEditor();
    formEditor.initialistDatePicker();
});