# Breakeven - WebAPI de contracheques

---

## Objetivo
- Construir um sistema simples de gestão de funcionários que também faça a geração do contracheque.

## Requisitos
- [Docker](https://docs.docker.com/get-docker/)
- [docker-compose](https://docs.docker.com/compose/install/)

## Como executar localmente
Possuindo docker e docker-compose instalados, execute o comando abaixo na raiz do projeto:
```bash
docker compose up -d 
```
A aplicação estará disponível em http://localhost:8080 e o adminer para gestão do banco de dados em http://localhost:5500.
Os endpoints da API estão disponíveis com exemplos [aqui](./src/BreakEven.API/BreakEven.http).

## Resumo das Funcionalidades

Para alcançar o objetivo proposto, foram implementadas 4 funcionalidades principais:

- GET /api/employees - Lista todos os funcionários cadastrados no sistema.
   - Em caso de sucesso retorna:
     ```json
     [
       {
         "cpf": "XXXXXXXXXXX",
         "firstName": "string",
         "surName": "string",
         "sector": "string",
         "grossSalary": "double",
         "admissionDate": "YYYY-DD-MMTHH:mm:ss.SSS",
         "hasHealthInsurance": "bool",
         "hasDentalInsurance": "bool",
         "hasTransportationAllowance": "bool"
       }, ...
     ]
     ```

- GET /api/employees/{id} - Lista um funcionário específico.
  - Caso não exista um usuário com esse id, retorna 404 com a mensagem:
    
    ```"Employee not found"```  

  - Em caso de sucesso retorna:
    ```json
    {
      "cpf": "XXXXXXXXXXX",
      "firstName": "string",
      "surName": "string",
      "sector": "string",
      "grossSalary": "double",
      "admissionDate": "YYYY-DD-MMTHH:mm:ss.SSS",
      "hasHealthInsurance": "bool",
      "hasDentalInsurance": "bool",
      "hasTransportationAllowance": "bool"
    }
    ```
  
- POST /api/employees - Cadastra um novo funcionário com seus dados.
  - Caso já exista um usuário com esse cpf, retorna 400 com a mensagem:

    ```"Employee already exists"```
  - Em caso de sucesso retorna:
    ```json
    { "id":  "XXXXXXXXXXX" }
    ```

- GET /api/employees/{id}/paycheck - Gera o contracheque de um funcionário específico.
  - Caso não exista um usuário com esse id, retorna 404 com a mensagem:

    ```"Employee not found"```

  - Em caso de sucesso retorna:
    ```json
    {
      "month": "MM/YYYY",
      "adjustments": [
        {
          "type": "Discount | Payment",
          "amount": "R$ XXX.XX",
          "description": "string",
          "percentage": "X.XX%"
        }, ...
      ],
      "grossSalary": "R$ XX,XXX.XX",
      "totalDiscounts": "R$ -XX,XXX.XX",
      "netSalary": "R$ XX,XXX.XX"
    }
    ```

Os descontos que o funcionário recebe são:

- **INSS** (Obrigatório - em cima do salário bruto) :

| **Faixa Salarial**       | **Alíquota** | 
|--------------------------|--------------|
| até 1.045,00             | 7.5%         |
| de 1.045,01 até 2.089,60 | 9%           |
| de 2.089,61 até 3.134,40 | 12%          |
| de 3.134,41 até 6.101,06 | 14%          | 
| acima de 6.101,06        | 17%          | 


- **IRRF** (Obrigatório - em cima do salário bruto) (Obrigatório):

| **Faixa salarial**       | **Alíquota** | **Valor máximo do desconto**  |
|--------------------------|--------------|-------------------------------|
| até 1.903,89             | -            | -                             |
| de 1.903,90 até 2.826,65 | 7.5%         | R$ 142,80                     |            
| de 2.826,66 até 3.751,05 | 15%          | R$ 354,80                     | 
| de 3.751,06 até 4.664,68 | 22.5%        | R$ 636,13                     | 
| acima de 4.664,68        | 27.5%        | R$ 869,36                     | 

- **FGTS**: 8% (Obrigatório - em cima do salário bruto)
- **Plano de saúde**: R$ 10 (Opcional - em cima do salário bruto)
- **Plano dental**: R$ 5 (Opcional - em cima do salário bruto)
- **Vale transporte**: 6% (Opcional - em cima do salário bruto. Caso o funcionário ganhe menos que R$ 1500, não há desconto)