Выкладываю свое первое [тестовое задание](https://github.com/RostislavBerezhnoy/WebService-v1/blob/master/Test.pdf) по WebAPI которое я сделал за 5 дней не имея никаких по сути для этого знаний. 
Задание скорее всего не самое идеальное но позже я планирую создать новый репозиторий где я переделаю его набравшись больше опыта и знаний. А пока я выкладываю его как есть без изменения.


# WebService

## В проекте подключен Swagger, который доступен по ссылке https://localhost:8050/swagger/index.html
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

### **1. Добавление новой точки измерения с указанием счетчика, трансформатора тока и трансформатора напряжения.**

> **POST https://localhost:8050/api/ElectricityMeteringPoint**

**Body:**

```json
{
    "ElectricityMeters": {
        "No": "111",
        "Type": "A1",
        "VerificationDate": "2018-02-01"
    },
    "VoltageTransformers": {
        "No": "222",
        "Type": "B1",
        "VerificationDate": "2018-02-01",
        "TransformationRatio": 152.4
    },
    "ElectricalTransformers": {
        "No": "333",
        "Type": "C1",
        "VerificationDate": "2018-02-01",
        "TransformationRatio": 152.4
    },
    "name": "Точка измерения 5",
    "consumptionObjectID": 1
}
```

**Ответ сервера:**

```json
{
    "consumptionObjectID": 1,
    "consumptionObjectName": "ПС 110/10 Весна",
    "voltageTransformers": {
        "electricityMeteringPointID": 4,
        "transformationRatio": 152.4,
        "id": 4,
        "no": "222",
        "type": "B1",
        "electricityMeteringPointName": "Точка измерения 5",
        "verificationDate": "2018-02-01T00:00:00"
    },
    "electricalTransformers": {
        "electricityMeteringPointID": 4,
        "transformationRatio": 152.4,
        "id": 4,
        "no": "333",
        "type": "C1",
        "electricityMeteringPointName": "Точка измерения 5",
        "verificationDate": "2018-02-01T00:00:00"
    },
    "electricityMeters": {
        "electricityMeteringPointID": 4,
        "id": 4,
        "no": "111",
        "type": "A1",
        "electricityMeteringPointName": "Точка измерения 5",
        "verificationDate": "2018-02-01T00:00:00"
    },
    "id": 4,
    "name": "Точка измерения 5"
}
```

По завершению запроса также, помимо *ElectricityMeteringPoint*, будут отдельно созданы и записаны три новых сущности *(voltageTransformer, electricalTransformer, electricityMeter)*

### **2. Выбрать все расчетные приборы учета в 2018 году.**

> **GET https://localhost:8050/api/MeteringDevice?year=2018**

Ответ сервера:
```json
[
    {
        "id": 1,
        "no": "445566000",
        "startDate": "2018-12-01T00:00:00"
    },
    {
        "id": 2,
        "no": "778899000",
        "startDate": "2018-08-15T00:00:00"
    },
    {
        "id": 3,
        "no": "112233000",
        "startDate": "2019-03-18T00:00:00"
    }
]
```

### **3. По указанному объекту потребления выбрать все счетчики с закончившимся сроком поверке.**

> **GET https://localhost:8050/api/ElectricityMeter?ConsumptionObjectID=1&WithExpiredDate=true**

Ответ сервера:
```json
[
    {
        "electricityMeteringPointID": 1,
        "id": 1,
        "no": "112233000",
        "type": "A1",
        "electricityMeteringPointName": "Точка измерения 1",
        "verificationDate": "2020-01-13T00:00:00"
    },
    {
        "electricityMeteringPointID": 2,
        "id": 2,
        "no": "445566000",
        "type": "B1",
        "electricityMeteringPointName": "Точка измерения 2",
        "verificationDate": "2021-02-14T00:00:00"
    }
]
```

### **4. По указанному объекту потребления выбрать все трансформаторы напряжения с закончившимся сроком поверке.**

> **GET https://localhost:8050/api/VoltageTransformer?ConsumptionObjectID=1&WithExpiredDate=true**

Ответ сервера:
```json
[
    {
        "electricityMeteringPointID": 1,
        "transformationRatio": 2.4,
        "id": 1,
        "no": "445566000",
        "type": "H3",
        "electricityMeteringPointName": "Точка измерения 1",
        "verificationDate": "2020-07-19T00:00:00"
    },
    {
        "electricityMeteringPointID": 2,
        "transformationRatio": 2.5,
        "id": 2,
        "no": "112233000",
        "type": "J3",
        "electricityMeteringPointName": "Точка измерения 2",
        "verificationDate": "2021-08-20T00:00:00"
    }
]
```

### **5. По указанному объекту потребления выбрать все трансформаторы тока с закончившимся сроком поверке.** 

> **GET https://localhost:8050/api/ElectricalTransformer?ConsumptionObjectID=1&WithExpiredDate=true**

Ответ сервера:
```json
[
    {
        "electricityMeteringPointID": 1,
        "transformationRatio": 1.1,
        "id": 1,
        "no": "778899000",
        "type": "D2",
        "electricityMeteringPointName": "Точка измерения 1",
        "verificationDate": "2020-04-16T00:00:00"
    },
    {
        "electricityMeteringPointID": 2,
        "transformationRatio": 1.2,
        "id": 2,
        "no": "445566000",
        "type": "F2",
        "electricityMeteringPointName": "Точка измерения 2",
        "verificationDate": "2021-05-17T00:00:00"
    }
]
```