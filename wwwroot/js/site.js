//Faz a chamada dos JavaScripts referenciados nas paginas. Cuidado ao alterar, 
//pois mudará a forma e funcionamento dos ítens referenciados pelo Materialize

//levanta visão parcial de adicionar estado


// faz o build das caixa select 
    //document.addEventListener('DOMContentLoaded', function () {
    //    var elems = document.querySelectorAll('select');
    //    var options = {};
    //    var instance = M.FormSelect.init(elems, options);

//});



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

function validar_registro() {
    
    var Email = document.getElementById("Email");
    var Password = document.getElementById("Password");
    var ConfirmPassword = document.getElementById("ConfirmPassword");
    var formulario = document.getElementsByName("registro");

    
    if (Email.value == "") {
        alert("Informe o E-mail Corporativo");
        Email.focus();
        return false;
    }
    if (Password.value == "") {
        alert("É Obrigatória uma senha com pelo menos 8 caracteres.");
        Password.focus();
        return false;
    } else {
        var re = /^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,}$/;
        if (re.test(Password.value) != true) {
            alert("A senha deve conter 8 caracteres, letras maiúsculas, minúsculas, números e 1 caracter especial");
        }
    }
    if (ConfirmPassword.value == "") {
        alert("Confirme a senha que digitou anteriormente.");
        ConfirmPassword.focus();
        return false;
    }

    if (ConfirmPassword.value != Password.value) {
        alert("As senhas não são iguais");
        ConfirmPassword.focus();
        return false;
    }

    formulario.submit();
    
}

function validar_login() {
    
    var Email = document.getElementById("Email");
    var Password = document.getElementById("Password");
    var formulario = document.getElementsByName("login");

    
    if (Email.value == "") {
        alert("Informe o E-mail Corporativo");
        Email.focus();
        return false;
    }
    if (Password.value == "") {
        alert("Insira a Senha");
        Password.focus();
        return false;
    }

    formulario.submit();

}

function validar_escola() {

    var Escola = document.getElementById("Escola");
    var Rua = document.getElementById("Rua");
    var Bairro = document.getElementById("Bairro");
    var CodigoEstado = document.getElementById("CodigoEstado");
    var CodigoCidade = document.getElementById("CodigoCidade");
    var INEP = document.getElementById("INEP");
    var formulario = document.getElementsByName("login");


    if (Escola.value == "") {
        alert("Informe a Escola");
        Escola.focus();
        return false;
    }
    if (Rua.value == "") {
        alert("Insira a Rua");
        Rua.focus();
        return false;
    }
    if (Bairro.value == "") {
        alert("Insira a Bairro");
        Bairro.focus();
        return false;
    }
    if (CodigoEstado.value == "") {
        alert("Selecione o Estado");
        CodigoEstado.focus();
        return false;
    }
    if (CodigoCidade.value == "") {
        alert("Selecione a Cidade");
        CodigoCidade.focus();
        return false;
    }
    if (INEP.value == "") {
        alert("Insira o Código INEP");
        INEP.focus();
        return false;
    }

    formulario.submit();

}

/*function validar() {
    // pegando o valor do nome pelos names
    var nome = document.getElementById("nome");
    var sobrenome = document.getElementById("sobrenome");
    var email = document.getElementById("email");
    var senha = document.getElementById("senha");
    var telefone = document.getElementById("telefone");
    var cep = document.getElementById("cep");
    var sexo = document.getElementById("sexo");
    var newsletter = document.getElementById("newsletter").checked;

    // verificar se o nome está vazio
    if (nome.value == "") {
        alert("Nome não informado");

        // Deixa o input com o focus
        nome.focus();
        // retorna a função e não olha as outras linhas
        return;
    }
    if (sobrenome.value == "") {
        alert("Sobrenome não informado");
        sobrenome.focus();
        return;
    }
    if (email.value == "") {
        alert("E-mail não informado");
        email.focus();
        return;
    }
    if (senha.value == "") {
        alert("Senha não informada");
        senha.focus();
        return;
    }
    if (telefone.value == "") {
        alert("Telefone não informado");
        telefone.focus();
        return;
    }
    if (cep.value == "") {
        alert("CEP não informado");
        cep.focus();
        return;
    }
    if (sexo.value == "") {
        alert("CEP não informado");
        sexo.focus();
        return;
    }
    alert("Formulário enviado!");
    // envia o formulário
    //formulario.submit();
}
*/  
