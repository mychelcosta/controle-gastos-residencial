# Implantação do back-end

A API **ApiFinanceira** possui uma documentação em OpenAPI com Scalar que pode ser acessada no caminho `/scalar` onde a API estiver escutando. Exemplo: http://localhost:5139/scalar

## Processo de Implantação

### Sem Docker

* Instale o SDK do dotnet versão 10.0
* Abra o projeto em `src/back-end`, é onde a API está localizada
* Para criação do banco de dados, execute o comando:
```bash
dotnet ef database update
```
* Caso ocorra um erro por falta da ferramenta `dotnet-ef`, execute o comando:
```bash
dotnet tool install --global dotnet-ef
```
* E, por fim, para construir e iniciar a aplicação, execute:
```bash
dotnet run
```
> A saida do terminal irá apresentar a URL onde a API está escutando: http://localhost:5139
