# EDI.API.Tests

# Travis CI status:
https://travis-ci.org/vtyurinmn/EDI.API.Tests.svg?branch=master

## Небольшой disclaimer

До выполнения задания на С# ничего не писал. С языками со статической типизацией долгого времени отношений не имел. Да и компьютер с ОС Windows пришлось брать под честное слово, ибо Xamarin Studio оказалась злом. :)
Писал вечерами, ибо основная работа не отпускала от слова "совсем".

## Что использовал?

Для выполнения задания использовал:

* NUnit.3.5.0 - из-за удобного синтаксиса проверок, вменяемой работы с Xamarin Studio и удобного Gui Runner.
* Newtonsoft.Json.9.0.1 - выбрана как самая популярная библиотека для работы с JSON-объектами.
* RestSharp.105.2.3 - искал что-то с удобным синаксисом, наподобие гема rest-client в Ruby.
* Postman - для создания прототипов запросов.

Писал изначально в Xamarin Studio, потом нашел компьютер с Windows и Visual Studio. Жить стало лучше.

## Мысли по заданию

**Что сделал?**

Сделал три простых теста: 

* Проверка аутентификации - возвращаемая строка заканчивается символами "==".
* Отправка сообщения - ответный JSON содержит непустой MessageID.
* Повторная отправка сообщения - проверка, что получен валидный параметр MessageUndeliveryReasons.

Решение из двух файлов - APIHelper.cs (описания методов) и ApiTest.cs (описание самих тестов)

**Что не сделал и почему?**

Не успел разобраться с maven. 
Думаю, что могу за вечер или два прикрутить и его.

Не совсем понял - можно ли автоматизировать проверки с Интерфейсом поставщика.
Не успел добавить автоматические инкриментирование для NN01 в файле ORDERS на шаге [TearDown] при помощи File.WriteAllLines\ReadAllLines.

## Обратная связь

* Через почту - i.a.kadochnikov@yandex.ru
* Через Issues. :)
