$(document).ready(function () {
    $("#CodigoAno").on("change", function () {
        $list = $("#CodigoCC");
        $id = $("#CodigoAno").val();
        console.log(this.id);
        $.ajax({
            url: "/AtribCColEscView/ListaCC/" + $id,
            type: "POST",
            traditional: true,
            success: function (result) {
                $list.empty();
                console.log(result);

                $list.append('<option value=""> --Selecione um Componente </option>');

                $.each(result.resultado, function (i, item) {
                    $list.append('<option value="' + item.value + '"> ' + item.text + ' </option>');

                });
                console.log(result);
            },
            error: function () {
                alert("Something went wrong call the police");
            }
        });
    });
});