# 🧾 Controle de Estoque — Projeto em C# (Console)

## 📖 Sobre o Projeto
Este projeto foi desenvolvido como parte da disciplina de **Programação (2º Bimestre)** e tem como objetivo criar um **sistema de controle de estoque em C#**, executado no console, com foco em boas práticas de desenvolvimento e persistência de dados em arquivos CSV.

O sistema permite **cadastrar produtos, registrar entradas e saídas de estoque**, e **gerar relatórios de controle**, garantindo a integridade das informações e a validação das regras de negócio.

---

## 🎯 Objetivos
- Implementar **CRUD completo** de produtos e movimentações.  
- Controlar **entradas e saídas de estoque** com validações.  
- Utilizar **arquivos CSV** para persistência de dados.  
- Aplicar **boas práticas** como escrita atômica e tratamento de erros.  
- Gerar **relatórios automáticos** no console (ex: produtos abaixo do mínimo, extrato por produto).

---

## 🧱 Estrutura do Projeto

```
EstoqueConsole/
│
├── data/
│   ├── produtos.csv
│   └── movimentos.csv
│
├── src/
│   ├── Program.cs
│   ├── Modelo/
│   │   ├── Produto.cs
│   │   └── Movimento.cs
│   └── Servico/
│       ├── InventarioServico.cs
│       └── CsvArmazenamento.cs
│
└── README.md
```

---

## 📦 Formato dos Arquivos

**produtos.csv**
```
id;nome;categoria;estoqueMinimo;saldo
```

**movimentos.csv**
```
id;produtoId;tipo;quantidade;data;observacao
```
> tipo: `ENTRADA` ou `SAIDA`

---

## 💻 Menu Principal (CLI)

```
1. Listar produtos
2. Cadastrar produto
3. Editar produto
4. Excluir produto
5. Dar ENTRADA em estoque
6. Dar SAÍDA de estoque
7. Relatório: Estoque abaixo do mínimo
8. Relatório: Extrato de movimentos por produto
9. Salvar (CSV)
0. Sair
```

---

## ⚙️ Principais Componentes

### 🔹 Produto
`Id`, `Nome`, `Categoria`, `EstoqueMinimo`, `Saldo`

Validações:
- Nome obrigatório  
- Estoque mínimo ≥ 0  
- Impedir remoção com saldo negativo  

---

### 🔹 Movimento
`Id`, `ProdutoId`, `Tipo`, `Quantidade`, `Data`, `Observacao`

Regras:
- `Entrada()` → aumenta saldo  
- `Saida()` → reduz saldo (bloquear se saldo < quantidade)

---

### 🔹 InventarioServico
- Armazena listas em memória  
- Implementa CRUD de produtos  
- Valida e aplica movimentações  
- Gera relatórios no console  

---

### 🔹 CsvArmazenamento
- Lê e grava arquivos `.csv`  
- Implementa **escrita atômica**:  
  grava em `.tmp`, substitui o original apenas após sucesso  

---

## 📊 Relatórios

- **Produtos abaixo do mínimo:** lista produtos com saldo menor que o estoque mínimo.  
- **Extrato por produto:** mostra todas as movimentações (entradas e saídas) em ordem cronológica.

---

## 🗓️ Cronograma de Desenvolvimento

| Semana | Sprint | Tarefas Principais |
|---------|---------|--------------------|
| 1 | Fundamentos | Estrutura do projeto, CRUD inicial |
| 2 | Persistência e Validações | Concluir CRUD, leitura/escrita CSV |
| 3 | Regras de Estoque | Entradas e saídas com validações |
| 4 | Boas Práticas | Tratamento de erros e revisão |
| 5 | Relatórios | Estoque mínimo e extrato por produto |
| 6 | Backup e Refatoração | Escrita atômica e ajustes finais |
| 7 | UX e Consolidação | Melhorar usabilidade e testes |
| 8 | Apresentação Final | Demonstração e entrega do projeto |

---

## 👩‍💻 Autores

- Ariane da Silva Archanjo
- Lucas Vinicius Barros Dias
- Pedro Henrique Kafka Zaratino
- Caio Melo Canhetti
- Rafael Martins Schreurs Sales
- Matheus Sizanoski Figueiredo

**Orientação:** Prof. Marlos Alex de Oliveira Marques
Disciplina: Programação – 2º Bimestre
Turma: 2ESCN
**Centro Universitário Autônomo do Brasil**
