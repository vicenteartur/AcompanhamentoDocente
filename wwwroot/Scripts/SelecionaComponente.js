$(document).ready(function () {
    $("#CodigoModalidade").on("change", function () {
        $list = $("#CodigoAno");
        $id = $("#CodigoModalidade").val();
        console.log(this.id);
        $.ajax({
            url: "/AtribCColEscView/ListaAno/" + $id,
            type: "POST",
            traditional: true,
            success: function (result) {
                $list.empty();
                console.log(result);

                $list.append('<option value=""> --Selecione uma Turma </option>');

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