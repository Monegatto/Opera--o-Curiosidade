/**
 * Buscar a lista de usuários e a criar se ela não existir
 */
function carregarUsers(){
    let users = JSON.parse(localStorage.getItem("Usuarios"))
    if(users == null)
        localStorage.setItem("Usuarios", "[]")
}

/**
 * Buscar a lista de cadastros e a criar se ela não existir, depois preenche a tabela com os dados dos colaboradores
 */
function carregarCadastros(){
    let cadastros = JSON.parse(localStorage.getItem("Cadastros"))
    if(cadastros == null)
        localStorage.setItem("Cadastros", "[]")

    let tabela = window.document.querySelector("tbody")     //Encontra a tabela na página
    for(let i = 0; i < cadastros.length; i++){
        let user = cadastros[i]

        let nome = document.createElement("td")
        nome.append(user.nome)

        let email = document.createElement("td")
        email.append(user.email)

        let ativo = document.createElement("td")
        
        if(!user.ativo){
            ativo.className += " " + "inativo"
            ativo.append("Inativo")
        }else
            ativo.append("Ativo")

        let linha = document.createElement("tr")
        linha.append(nome, email, ativo)

        tabela.append(linha)
    }
}

/**
 * Percorre todo o array e compara o nome de cada entrada com o nome fornecido
 */
let validaRepeticaoNome = (cadastros, fnome) => {
    for(let i = 0; i < cadastros.length; i++){
        if(cadastros[i].nome == fnome)
            return true
    }
    return false
}

/**
 * Percorre todo o array e compara o email de cada entrada com o email fornecido
 */
let validaRepeticaoEmail = (cadastros, femail) => {
    for(let i = 0; i < cadastros.length; i++){
        if(cadastros[i].email == femail)
            return true
    }
    return false
}

//adaptar a função para funcionar com usuários ao invés de colaboradores
/**
 * Função que cadastra novos colaboradores no sistema
 */
function cadastrar(){
    let formData = new FormData(document.forms.formCadastro)
    let cadastros = JSON.parse(localStorage.getItem("Cadastros"))

    let fnome = formData.get("nome")
    if(fnome == ""){                                                        //Lida com campo de nome vazio
        window.alert("Você precisa fornecer um nome para o cadastro!")
        return false
    } else if(validaRepeticaoNome(cadastros, fnome)){                       //Verifica se o nome é repetido
        window.alert("Esse nome já está registrado no sistema!")
        return false
    }                                                                     
    let femail = formData.get("email")
    if(femail == ""){
        window.alert("Você deve fornecer um email para o cadastro")
        return false
    }                                                   //Se o email não for vazio, a validação ocorre
    else if(validaRepeticaoEmail(cadastros, femail)){                    //Verifica se o email é repetido
        window.alert("Uma conta com esse email já existe")
        return false
    }
    if(!((/^[\w\.]+@([\w-]+\.)+[\w-]{2,4}$/).test(femail))){   //Verifica se o email inserido é valido
        window.alert("Email inválido, tente cadastrar outro")
        return false
    }

    let fidade = formData.get("idade")
    let fendereco = formData.get("endereco")
    let finfo = formData.get("info")
    let finter = formData.get("inter")
    let fsenti = formData.get("senti")
    let fvalor = formData.get("valor")

    let iativo = window.document.querySelector("input#iativo").checked

    let fcebola = {inter: finter, senti: fsenti, valor: fvalor}
    let novoCadastro = {nome: fnome, idade: fidade, email: femail, endereco: fendereco, info: finfo, ativo: iativo, cebola: fcebola}

    cadastros.push(novoCadastro)

    localStorage.setItem("Cadastros", JSON.stringify(cadastros))

    setTimeout(() => window.location.replace("./home.html"))
}