//Faz a chamada dos JavaScripts referenciados nas paginas. Cuidado ao alterar, 
//pois mudará a forma e funcionamento dos ítens referenciados pelo Materialize

//levanta visão parcial de adicionar estado


// faz o build das caixa select 
document.addEventListener('DOMContentLoaded', function () {
    var elems = document.querySelectorAll('select');
    var options = {};
    var instances = M.FormSelect.init(elems, options);

});

// faz o build do Modal

    var elem = document.querySelector('.modal');
    var options = { };
    var instance = new M.Modal(elem, options);


//inicia a paginal parcial inserir estado dentro de uma modal
    $(document).ready(function () {

        $.ajaxSetup({ cache: false });
            $("#createbtn").click(function () {


        $("#teste").load("https://localhost:44313/Estado/Create/", function () {
            $('#modal1').modal("show");
        });
            });
        });

//atualiza select cidade em funcao do estado

$('#CodigoEstado').load(function () {
    var CodigoEstado = $(this).val();

    $.get('/Escola/Create/ListaEstado/', { }).done(function (data) {
        var drop = $('#CodigoEstado');
        drop.html("");
        $.each(data, function (i, item) {
            drop.append('<option val="' + item + '">' + item + '</option>');
        });
    });
});

$('#CodigoEstado').change(function () {
    var CodigoEstado = $(this).val();

    $.get('/Escola/Create/ListaCidade/', { Codigo: Codigo }).done(function (data) {
        var drop = $('#CodigoCidade');
        drop.html("");
        $.each(data, function (i, item) {
            drop.append('<option val="' + item + '">' + item + '</option>');
        });
    });
});