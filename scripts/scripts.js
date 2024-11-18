/**
 * Função de pesquisa na tabela de cadastros
 */
function pesquisar() {
    let termo = window.document.querySelector("input#ipesquisa").value.toUpperCase()
    let tabela = window.document.querySelector("tbody")
    let encontrou

    for(let i = 0; i < tabela.rows.length; i++){

        tabela.rows[i].style.display = ""
        encontrou = false

        for(let j = 0; j < tabela.rows[i].cells.length; j++){
            let conteudoCelula = tabela.rows[i].cells[j].textContent.toUpperCase()

            if(j == 0) {
                let nomeCompleto = conteudoCelula.split(" ")
                for(let k = 0; k < nomeCompleto.length; k++){
                    if(nomeCompleto[k].startsWith(termo)){
                        encontrou = true
                        break
                    }
                }
            } else {
                if(conteudoCelula.startsWith(termo)){
                    encontrou = true
                    break
                }
            }
            
        }
        if(!encontrou)
            tabela.rows[i].style.display = "none"
    }
}

/**
 * Buscar a lista de usuários e a criar se ela não existir
 */
function carregarUsers(){
    let users = JSON.parse(localStorage.getItem("Usuarios"))    //Busca a lista de usuários do sistema
    if(users == null)                                           //Se não existir lista de usuários, cria ela
        localStorage.setItem("Usuarios", "[]")
}

/**
 * Buscar a lista de cadastros e a criar se ela não existir, depois preenche a tabela com os dados dos colaboradores
 */
function carregarCadastros(){
    let cadastros = JSON.parse(localStorage.getItem("Cadastros"))   //Busca a lista de cadastros no sistema
    if(cadastros == null)                                           //Se não existir lista de cadastros, cria ela
        localStorage.setItem("Cadastros", "[]")

    let tabela = window.document.querySelector("tbody")             //Encontra a tabela na página
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
let validaRepeticaoEmail = (users, femail) => {
    for(let i = 0; i < users.length; i++){
        if(users[i].email == femail)
            return true
    }
    return false
}

/**
 * Percorre todo o array de usuários e verifica se a senha e email fornecidos pertencem ao mesmo usuário
 */
let validaLogin = (users, femail, fsenha) => {
    for(let i = 0; i < users.length; i++){
        if((users[i].email == femail) && (users[i].senha == fsenha))
            return true
    }
    return false
}

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
    if(femail == ""){                                                       //Lida com o campo de email vazio
        window.alert("Você deve fornecer um email para o cadastro")
        return false
    }else if(validaRepeticaoEmail(cadastros, femail)){                      //Verifica se o email é repetido
        window.alert("Uma conta com esse email já existe")
        return false
    }
    if(!((/^[\w\.]+@([\w-]+\.)+[\w-]{2,4}$/).test(femail))){                //Verifica se o email inserido é valido
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

/**
 * Função de login com validação para email e senha com validação para entradas vazias e existentes/corretas
 */
function login(){
    debugger
    let formData = new FormData(document.forms.formLogin)
    let users = JSON.parse(localStorage.getItem("Usuarios"))

    let femail = formData.get("email")                              
    if(femail == ""){                                               //Verifica se o campo de email está vazio
        window.alert("Você deve fornecer um email para o login!")
        return false
    } else if(!validaRepeticaoEmail(users, femail)){                //Verifica se o email informado existe no armazenamento
        window.alert("Email não encontrado no sistema!")
        return false
    }

    let fsenha = formData.get("senha")
    if(fsenha == ""){                                               //Verifica se o campo de senha está vazio
        window.alert("Você deve fornecer uma senha para o login!")
        return false
    }else if(!validaLogin(users, femail, fsenha)){                  //Valida se o email e senha informado pertencem ao mesmo usuário
        window.alert("Senha incorreta, tente novamente")
        return false
    }

    setTimeout(() => window.location.replace("./home.html"))
}