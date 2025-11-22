# 🏪 Sistema de Controle de Estoque

<div align="center">

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Console](https://img.shields.io/badge/Console-Application-blue?style=for-the-badge)

**Sistema completo de gerenciamento de estoque desenvolvido em C# com persistência em CSV**

[Funcionalidades](#-funcionalidades) • [Instalação](#-instalação) • [Como Usar](#-como-usar) • [Documentação](#-documentação-técnica) • [Equipe](#-equipe-de-desenvolvimento)

</div>

---

## 📋 Sobre o Projeto

Sistema de **Controle de Estoque** desenvolvido como projeto acadêmico para a disciplina de Programação (2º Bimestre) do curso de Engenharia de Software. A aplicação console oferece gerenciamento completo de produtos com validações robustas, histórico de movimentações e relatórios gerenciais.

### 🎯 Objetivos Principais

- ✅ Implementar **CRUD completo** de produtos com validações de negócio
- ✅ Controlar **entradas e saídas** de estoque com rastreamento
- ✅ Garantir **persistência segura** de dados em arquivos CSV
- ✅ Aplicar **boas práticas** de desenvolvimento (escrita atômica, tratamento de erros)
- ✅ Fornecer **relatórios gerenciais** em tempo real

---

## ⚡ Funcionalidades

### 📦 Gerenciamento de Produtos
- **Cadastro** de novos produtos com validação de campos obrigatórios
- **Edição** de informações de produtos existentes
- **Exclusão** de produtos (apenas com saldo zerado)
- **Listagem** completa com status de estoque

### 📊 Controle de Movimentações
- **Entrada de estoque** com registro de quantidade e observações
- **Saída de estoque** com validação de saldo disponível
- **Histórico completo** de todas as movimentações por produto
- **Alertas automáticos** quando o estoque fica abaixo do mínimo

### 📈 Relatórios Gerenciais
- **Produtos abaixo do mínimo** - Identifica itens que precisam de reposição
- **Extrato por produto** - Histórico cronológico de entradas e saídas
- **Totalizadores** - Estatísticas de movimentações por produto

### 💾 Persistência de Dados
- **Salvamento automático** após cada operação
- **Escrita atômica** (`.tmp` + `replace`) para evitar corrupção de dados
- **Formato CSV** com suporte a caracteres especiais e UTF-8
- **Backup automático** antes de sobrescrever arquivos

---

## 🏗️ Arquitetura do Sistema

```
EstoqueConsole/
│
├── 📂 data/                           # Arquivos de persistência
│   ├── produtos.csv                   # Base de dados de produtos
│   └── movimentos.csv                 # Histórico de movimentações
│
├── 📂 src/
│   ├── 📄 Program.cs                  # Ponto de entrada e interface
│   │
│   ├── 📂 Modelo/                     # Camada de dados
│   │   ├── Produto.cs                 # Entidade Produto
│   │   └── Movimento.cs               # Entidade Movimento
│   │
│   └── 📂 Servico/                    # Camada de negócio
│       ├── InventarioServico.cs       # Lógica de negócio
│       └── CsvArmazenamento.cs        # Persistência em CSV
│
└── 📄 README.md                       # Este arquivo
```

### 🔹 Camada de Modelo

**`Produto.cs`**
```csharp
- Id: int                    // Identificador único
- Nome: string               // Nome do produto (obrigatório)
- Categoria: string          // Categoria para organização
- EstoqueMinimo: int         // Quantidade mínima em estoque (>= 0)
- Saldo: int                 // Quantidade atual disponível (>= 0)
```

**`Movimento.cs`**
```csharp
- Id: int                    // Identificador único
- ProdutoId: int             // Referência ao produto
- Tipo: string               // "ENTRADA" ou "SAIDA"
- Quantidade: int            // Quantidade movimentada (> 0)
- Data: DateTime             // Data/hora do registro
- Observacao: string         // Descrição opcional
```

### 🔹 Camada de Serviço

**`InventarioServico.cs`**
- Gerencia listas em memória de produtos e movimentos
- Implementa todas as operações CRUD
- Aplica regras de negócio e validações
- Gera relatórios formatados

**`CsvArmazenamento.cs`**
- Leitura e escrita de arquivos CSV
- Implementa escrita atômica para integridade
- Parse robusto com suporte a campos entre aspas
- Tratamento de caracteres especiais

---

## 🚀 Instalação

### Pré-requisitos

- [.NET SDK 6.0+](https://dotnet.microsoft.com/download) instalado
- Editor de código (Visual Studio, VS Code, Rider)
- Terminal/Console

### Passos para Instalação

1. **Clone o repositório**
```bash
git clone https://github.com/seu-usuario/controle-de-estoque.git
cd controle-de-estoque
```

2. **Restaure as dependências**
```bash
dotnet restore
```

3. **Compile o projeto**
```bash
dotnet build
```

4. **Execute a aplicação**
```bash
dotnet run
```

### Executando o Arquivo Compilado

Após a compilação, o executável estará em:
```
bin/Debug/net6.0/controle-de-estoque-ub.exe
```

---

## 💻 Como Usar

### Menu Principal

Ao iniciar o sistema, você verá o seguinte menu:

```
╔═══════════════════════════════════════╗
║  SISTEMA DE CONTROLE DE ESTOQUE       ║
╚═══════════════════════════════════════╝

1 - Listar produtos
2 - Cadastrar produto
3 - Editar produto
4 - Excluir produto
5 - Dar ENTRADA em estoque
6 - Dar SAÍDA de estoque
7 - Relatório: Estoque abaixo do mínimo
8 - Relatório: Extrato por produto
9 - Salvar dados (CSV)
0 - Sair

Escolha uma opção:
```

### 📝 Exemplos de Uso

#### Cadastrar um Produto
```
Opção: 2

ID do novo produto: 1
Nome do produto: Mouse Gamer
Categoria: Periféricos
Estoque mínimo: 10
Saldo inicial: 50

[OK] Produto cadastrado com sucesso!
```

#### Registrar Entrada de Estoque
```
Opção: 5

ID do produto: 1
Produto: Mouse Gamer | Saldo atual: 50
Quantidade a adicionar: 30
Observação: Compra mensal

[OK] Novo saldo: 80
[OK] Entrada registrada com sucesso!
```

#### Registrar Saída de Estoque
```
Opção: 6

ID do produto: 1
Produto: Mouse Gamer | Saldo atual: 80
Quantidade a remover: 25
Observação: Venda atacado

[OK] Novo saldo: 55
[OK] Saída registrada com sucesso!
```

---

## 📚 Documentação Técnica

### Regras de Negócio

#### Produtos
- ✔️ **Nome obrigatório** - Não pode ser vazio ou apenas espaços
- ✔️ **Estoque mínimo >= 0** - Não aceita valores negativos
- ✔️ **Saldo >= 0** - Quantidade em estoque não pode ser negativa
- ✔️ **ID único** - Não pode haver produtos com IDs duplicados
- ✔️ **Exclusão condicional** - Só permite excluir produtos com saldo zerado

#### Movimentações
- ✔️ **Entrada sempre positiva** - Adiciona ao saldo do produto
- ✔️ **Saída validada** - Bloqueia se saldo insuficiente
- ✔️ **Histórico imutável** - Movimentos não podem ser editados/excluídos
- ✔️ **Rastreabilidade** - Cada movimento registra data/hora e observação

### Formato dos Arquivos CSV

**produtos.csv**
```csv
id;nome;categoria;estoqueMinimo;saldo
1;Mouse Gamer;Periféricos;10;55
2;Teclado Mecânico;Periféricos;5;30
3;Monitor 24";Monitores;3;8
```

**movimentos.csv**
```csv
id;produtoId;tipo;quantidade;data;observacao
1;1;ENTRADA;50;2025-01-15T10:30:00;Estoque inicial
2;1;ENTRADA;30;2025-01-20T14:15:00;Compra mensal
3;1;SAIDA;25;2025-01-22T16:45:00;Venda atacado
```

### Escrita Atômica

O sistema implementa escrita atômica para garantir integridade:

1. **Grava em arquivo temporário** (`.tmp`)
2. **Valida a escrita completa**
3. **Substitui o arquivo original** apenas após sucesso
4. **Remove o arquivo temporário**

Isso previne corrupção de dados em caso de:
- Falhas de disco
- Interrupção do programa
- Falta de energia
- Erros de escrita

---

## 🧪 Testes e Validações

### Casos de Teste Implementados

| Funcionalidade | Validação | Comportamento Esperado |
|---|---|---|
| Cadastrar produto | Nome vazio | ❌ Rejeita com mensagem de erro |
| Cadastrar produto | ID duplicado | ❌ Impede cadastro |
| Editar produto | ID inexistente | ❌ Informa produto não encontrado |
| Excluir produto | Saldo > 0 | ❌ Bloqueia exclusão |
| Saída de estoque | Quantidade > Saldo | ❌ Impede operação |
| Entrada de estoque | Quantidade <= 0 | ❌ Rejeita entrada |
| Salvar dados | Escrita com falha | 🔄 Mantém arquivo original intacto |

---

## 🎓 Cronograma de Desenvolvimento

| Semana | Sprint | Entregas |
|:---:|---|---|
| **1** | Fundamentos | Estrutura do projeto, CRUD inicial |
| **2** | Persistência | Leitura/escrita CSV, validações |
| **3** | Regras de Estoque | Entradas/saídas com validações |
| **4** | Boas Práticas | Tratamento de erros, refatoração |
| **5** | Relatórios | Estoque mínimo, extrato por produto |
| **6** | Backup | Escrita atômica, testes de integridade |
| **7** | UX | Melhorias na interface console |
| **8** | Apresentação | Demo final e entrega |

---

## 📊 Critérios de Avaliação

| Critério | Pontos | Descrição |
|---|:---:|---|
| **Corretude / Regras de Negócio** | 40 | CRUD completo + validações de estoque |
| **Persistência** | 20 | CSV + escrita atômica |
| **Qualidade de Código** | 10 | Organização, comentários, boas práticas |
| **Relatórios & UX** | 10 | Clareza dos relatórios e usabilidade |
| **Questionário Individual** | 20 | Avaliação individual sobre o projeto |
| **TOTAL** | **100** | |

---

## 👥 Equipe de Desenvolvimento

<table>
  <tr>
    <td align="center">
      <b>Ariane da Silva Archanjo</b><br>
      <sub>RA: 2025106857</sub>
    </td>
    <td align="center">
      <b>Caio Melo Canhetti</b><br>
      <sub>RA: 2025104636</sub>
    </td>
    <td align="center">
      <b>Lucas Vinicius Barros Dias</b><br>
      <sub>RA: 2025105450</sub>
    </td>
  </tr>
  <tr>
    <td align="center">
      <b>Matheus Sizanoski Figueiredo</b><br>
      <sub>RA: 2025105007</sub>
    </td>
    <td align="center">
      <b>Pedro Henrique Kafka Zaratino</b><br>
      <sub>RA: 2025105057</sub>
    </td>
    <td align="center">
      <b>Rafael Martins Schreurs Sales</b><br>
      <sub>RA: 2025105454</sub>
    </td>
  </tr>
</table>

### 🎓 Informações Acadêmicas

- **Curso:** Engenharia de Software
- **Disciplina:** Programação
- **Período:** 2º Bimestre
- **Turma:** 2ESCN
- **Professor:** Marlos Alex de Oliveira Marques
- **Instituição:** Centro Universitário Autônomo do Brasil (UniBrasil)

---

## 📝 Licença

Este projeto foi desenvolvido para fins acadêmicos como parte do curso de Engenharia de Software.

---

## 🤝 Contribuições

Este é um projeto acadêmico fechado. Para dúvidas ou sugestões, entre em contato com os membros da equipe.

---

## 📞 Suporte

Em caso de dúvidas sobre o projeto:
- Consulte a documentação técnica acima
- Verifique os comentários no código-fonte
- Entre em contato com o professor orientador

---

<div align="center">

**Desenvolvido com 💙 pela Equipe 2ESCN**

*Centro Universitário Autônomo do Brasil - 2025*

</div>