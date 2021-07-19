$(document).ready(function () {


    $("#CodigoEstado").change(function () {

        var value = $("#CodigoEstado option:selected").val();

        if (value !== "" || value !== undefined) {
            ListaCidade(value);
        }

    });

})



function ListaCidade(value) {
    var url = "/escolaview/ListaCidade/";
    var data = { sigla: value };

    $("#CodigoCidade").empty();


    $.ajax({
        url: url,
        type: "GET",
        dataType: "json",
        data: data

    }).done(funtion(data) {

        if(data.Resultado.length > 0){

        var dadosGrid = data.Resultado;

        $("#CodigoCidade").append('option value="">--Selecione</option>');

            $.each(dadosGrid, function (indice, item) {
                var opt = "";

                opt = '<option value="' + item["Codigo"] + '">' + item["Cidade"] + '</option>';

            });

        }
    })
}
