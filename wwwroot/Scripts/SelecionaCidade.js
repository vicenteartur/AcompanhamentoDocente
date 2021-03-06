$(document).ready(function () {
    $("#CodigoEstado").on("change", function () {
        $list = $("#CodigoCidade");
        $id = $("#CodigoEstado").val();
        console.log(this.id);
        $.ajax({
            url: "/escolaview/listacidade/" + $id,
            type: "POST",
            traditional: true,
            success: function (result) {
                $list.empty();
                console.log(result);

                $list.append('<option value=""> --Selecione uma Cidade </option>');

                $.each(result.resultado, function (i, item) {
                    $list.append('<option value="' + item.value + '"> ' + item.text + ' </option>');
                    console.log(item);
                });
                // Destroi o select
                $('select').material_select('destroy');

                // Insere as opções via Ajax

                // Inicializa o select com a nova propriedade
                $('select').material_select();
                console.log(result);
            },
            error: function () {
                alert("Something went wrong call the police");
            }
        });
    });
});