//Faz a chamada dos JavaScripts referenciados nas paginas. Cuidado ao alterar, 
//pois mudará a forma e funcionamento dos ítens referenciados pelo Materialize

//levanta visão parcial de adicionar estado


// faz o build das caixa select 
document.addEventListener('DOMContentLoaded', function () {
    var elems = document.querySelectorAll('select');
    var options = {};
    var instance = M.FormSelect.init(elems, options);

});

//sidenav build
//document.addEventListener('DOMContentLoaded', function () {
//    var elems = document.querySelectorAll('.sidenav');
//    var options = {};
//    var instance = new M.Sidenav.init(elems, options);
//});

//$(".button-collapse").sidenav();


// faz o build do Modal

    //var elem = document.querySelector('.modal');
    //var options = { };
    //var instance = new M.Modal(elem, options);


    
