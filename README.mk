# ?? Controle de Estoque � Projeto em C# (Console)

## ?? Sobre o Projeto
Este projeto foi desenvolvido como parte da disciplina de **Programa��o (2� Bimestre)** e tem como objetivo criar um **sistema de controle de estoque em C#**, executado no console, com foco em boas pr�ticas de desenvolvimento e persist�ncia de dados em arquivos CSV.

O sistema permite **cadastrar produtos, registrar entradas e sa�das de estoque**, e **gerar relat�rios de controle**, garantindo a integridade das informa��es e a valida��o das regras de neg�cio.

---

## ?? Objetivos
- Implementar **CRUD completo** de produtos e movimenta��es.  
- Controlar **entradas e sa�das de estoque** com valida��es.  
- Utilizar **arquivos CSV** para persist�ncia de dados.  
- Aplicar **boas pr�ticas** como escrita at�mica e tratamento de erros.  
- Gerar **relat�rios autom�ticos** no console (ex: produtos abaixo do m�nimo, extrato por produto).

---

## ?? Estrutura do Projeto

```
EstoqueConsole/
?
??? data/
?   ??? produtos.csv
?   ??? movimentos.csv
?
??? src/
?   ??? Program.cs
?   ??? Modelo/
?   ?   ??? Produto.cs
?   ?   ??? Movimento.cs
?   ??? Servico/
?       ??? InventarioServico.cs
?       ??? CsvArmazenamento.cs
?
??? README.md
```

---

## ?? Formato dos Arquivos

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

## ?? Menu Principal (CLI)

```
1. Listar produtos
2. Cadastrar produto
3. Editar produto
4. Excluir produto
5. Dar ENTRADA em estoque
6. Dar SA�DA de estoque
7. Relat�rio: Estoque abaixo do m�nimo
8. Relat�rio: Extrato de movimentos por produto
9. Salvar (CSV)
0. Sair
```

---

## ?? Principais Componentes

### ?? Produto
`Id`, `Nome`, `Categoria`, `EstoqueMinimo`, `Saldo`

Valida��es:
- Nome obrigat�rio  
- Estoque m�nimo ? 0  
- Impedir remo��o com saldo negativo  

---

### ?? Movimento
`Id`, `ProdutoId`, `Tipo`, `Quantidade`, `Data`, `Observacao`

Regras:
- `Entrada()` ? aumenta saldo  
- `Saida()` ? reduz saldo (bloquear se saldo < quantidade)

---

### ?? InventarioServico
- Armazena listas em mem�ria  
- Implementa CRUD de produtos  
- Valida e aplica movimenta��es  
- Gera relat�rios no console  

---

### ?? CsvArmazenamento
- L� e grava arquivos `.csv`  
- Implementa **escrita at�mica**:  
  grava em `.tmp`, substitui o original apenas ap�s sucesso  

---

## ?? Relat�rios

- **Produtos abaixo do m�nimo:** lista produtos com saldo menor que o estoque m�nimo.  
- **Extrato por produto:** mostra todas as movimenta��es (entradas e sa�das) em ordem cronol�gica.

---

## ??? Cronograma de Desenvolvimento

| Semana | Sprint | Tarefas Principais |
|---------|---------|--------------------|
| 1 | Fundamentos | Estrutura do projeto, CRUD inicial |
| 2 | Persist�ncia e Valida��es | Concluir CRUD, leitura/escrita CSV |
| 3 | Regras de Estoque | Entradas e sa�das com valida��es |
| 4 | Boas Pr�ticas | Tratamento de erros e revis�o |
| 5 | Relat�rios | Estoque m�nimo e extrato por produto |
| 6 | Backup e Refatora��o | Escrita at�mica e ajustes finais |
| 7 | UX e Consolida��o | Melhorar usabilidade e testes |
| 8 | Apresenta��o Final | Demonstra��o e entrega do projeto |

---

## ????? Autores

- Ariane da Silva Archanjo
- Lucas Vinicius Barros Dias
- Pedro Henrique Kafka Zaratino
- Caio Melo Canhetti
- Rafael Martins Schreurs Sales
- Matheus Sizanoski Figueiredo

**Orienta��o:** Prof. Marlos Alex de Oliveira Marques
Disciplina: Programa��o � 2� Bimestre
Turma: 2ESCN
**Centro Universit�rio Aut�nomo do Brasil**