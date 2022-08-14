# Post Thumb Generator

<div align="center">

![](docs/cover.png)

</div>

### Este é um serviço bem simples e util para gerar imagens de capa para postagens (criado para o blog [tuliocalil.com.br](https://tuliocalil.com.br)) escrito em C# com [Dotnet core](https://dotnet.microsoft.com/en-us/download).

### Você pode [testar o serviço acessando essa url aqui](https://thumb-generator.herou.com).

## Como usar

A utlização é super simples, basta fazer uma chamada `GET` para o serviço na rota `/generatecover` passando os parametros requeridos e você terá como retorno uma imagem:

```bash
curl --request GET \
  --url 'https://localhost:7282/generatecover?title=Como%20eu%20criei%20um%20servi%C3%A7o%20que%20gera%20thumbnails%20para%20meu%20blog%20usando%20C%23&tags=gitlab&tags=java&tags=github&tags=github&tags=csharp&author=Tulio%20Calil&date=11%20de%20agosto'
```

### Parametros disponiveis

| Nome   | Descrição                                                      | Requerido |
| ------ | -------------------------------------------------------------- | --------- |
| title  | Titulo da postagem                                             | Sim       |
| tags   | Array com nome das tecnologias envolvidas para gerar os ícones | Não       |
| date   | Data da postagem                                               | Não       |
| author | Nome do autor da postagem                                      | Não       |

### Tags de tecnologia

As tags são um array de string e são utilizadas para adicionar icones na thumb.
Você pode consultar os icones suportados atualmente visitando a pasta ´[src/DotNetCover/Assets/Images/Techs](src/DotNetCover/Assets/Images/Techs)´.

## Rodando local

Instale o [Dotnet core](https://dotnet.microsoft.com/en-us/download).

Clone o projeto:

```bash
git clone
```

Rode o projeto:

```bash
dotnet watch
```

<div align="center">

### Made with 💙 in Bahia, Brasil.

</div>
