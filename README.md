# Два вида приложений .NET
# exe
# dll
    
# Три вида запускаемых приложений(exe)
    Консольное приложение
    Веб приложение
## Десктопное приложение
*TEXT*

# ACID
## Aтомарность  
Атомарность гарантирует, что никакая транзакция не будет зафиксирована в системе частично. Будут либо выполнены все её подоперации, либо не выполнено ни одной. Поскольку на практике невозможно одновременно и атомарно выполнить всю последовательность операций внутри транзакции, вводится понятие «отката» (rollback): если транзакцию не удаётся полностью завершить, результаты всех её до сих пор произведённых действий будут отменены и система вернётся во «внешне исходное» состояние — со стороны будет казаться, что транзакции и не было (естественно, счётчики, индексы и другие внутренние структуры могут измениться, но, если СУБД запрограммирована без ошибок, это не повлияет на внешнее её поведение).

## Согласованность
Транзакция, достигающая своего нормального завершения (англ. end of transaction, EOT) и тем самым фиксирующая свои результаты, сохраняет согласованность базы данных. Другими словами, каждая успешная транзакция по определению фиксирует только допустимые результаты. Это условие является необходимым для поддержки четвёртого свойства.

## Изолированность 
Во время выполнения транзакции параллельные транзакции не должны оказывать влияния на её результат. Изолированность — требование дорогое, поэтому в реальных базах данных существуют режимы, не полностью изолирующие транзакцию (уровни изолированности, допускающие фантомное чтение и ниже).

## Устойчивость +
Независимо от проблем на нижних уровнях (к примеру, обесточивание системы или сбои в оборудовании) изменения, сделанные успешно завершённой транзакцией, должны остаться сохранёнными после возвращения системы в работу. Другими словами, если пользователь получил подтверждение от системы, что транзакция выполнена, он может быть уверен, что сделанные им изменения не будут отменены из-за какого-либо сбоя.

# Различия FirstOrDeafult и SingleOrDefault
## SingleOrDefault: 
Выбирает единственный элемент коллекции. Если коллекция пуста, возвращает значение по умолчанию. Если в коллекции больше одного элемента, генерирует исключение.
## FirstOrDeafult:
Выбирает первый элемент коллекции или возвращает значение по умолчанию.

# Чистый SQL:Structured Query Language — «язык структурированных запросов»
# Реляционная база данных
Использование реляционных баз данных было предложено доктором Коддом из компании IBM в 1970 году.

# Жизненный цикл зависимостей .AddScoped(); .AddTransient(); .AddSingleton();

AddScoped: для каждого запроса создается свой объект сервиса. То есть если в течение одного запроса есть несколько обращений к одному сервису, то при всех этих обращениях будет использоваться один и тот же объект сервиса.

AddTransient: при каждом обращении к сервису создается новый объект сервиса. В течение одного запроса может быть несколько обращений к сервису, соответственно при каждом обращении будет создаваться новый объект. Подобная модель жизненного цикла наиболее подходит для легковесных сервисов, которые не хранят данных о состоянии.

AddSingleton: объект сервиса создается при первом обращении к нему, все последующие запросы используют один и тот же ранее созданный объект сервиса.

# Cтатус коды ответов сервера
100-информационные
200-успешные
300-переадресация
400-ошибка на стороне клиента
500-ошибка на стороне сервера

# System.InvalidOperationException: Unable to resolve service for type 'some service' while attempting to activate 'some controller'.
Ошибка произошла при внедрении зависимости(Dependency Injection).
Нужно смотреть на конструктор класса и на program.cs pipeline
В модулях,которые используют зависимости нужно внедрять абстракции(интерфейсы),а не конкретные раелизации.

# Тестирование приложений
## Существует множество различных видов автоматических тестов приложений.

    1.Компонентный уровень
    Чаще всего называют юнит тестированием. Реже называют модульным тестированием. На этом уровне тестируют атомарные части кода. Это могут быть классы, функции или методы классов.

    Пример: твоя компания разрабатывает приложение "Калькулятор", которое умеет складывать и вычитать. Каждая операция это одна функция. Проверка каждой функции, которая не зависит от других, является юнит тестированием.

    Юнит тесты находят ошибки на фундаментальных уровнях, их легче разрабатывать и поддерживать. Важное преимущество модульных тестов в том, что они быстрые и при изменении кода позволяют быстро провести регресс (убедиться, что новый код не сломал старые части кода).

    Тест на компонентном уровне:

    Всегда автоматизируют.

    Модульных тестов всегда больше, чем тестов с других уровней.

    Юнит тесты выполняются быстрее всех и требуют меньше ресурсов.

    Практически всегда компонентные тесты не зависят от других модулей (на то они и юнит тесты) и UI системы.

    В 99% разработкой модульных тестов занимается разработчик, при нахождении ошибки на этом уровне не создается баг-репортов. Разработчик находит баг, правит, запускает и проверяет (абстрактно говоря это разработка через тестирование) и так по новой, пока тест не будет пройден успешно.

    На модульном уровне разработчик (или автотестер) использует метод белого ящика. Он знает что принимает и отдает минимальная единица кода, и как она работает.
.
    
     2. Интеграционный уровень
    Проверят взаимосвязь компоненты, которую проверяли на модульном уровне, с другой или другими компонентами, а также интеграцию компоненты с системой (проверка работы с ОС, сервисами и службами, базами данных, железом и т.д.). Часто в английских статьях называют service test или API test.

    В случае с интеграционными тестами редко когда требуется наличие UI, чтобы его проверить. Компоненты ПО или системы взаимодействуют с тестируемым модулем с помощью интерфейсов. Тут начинается участие тестирования. Это проверки API, работы сервисов (проверка логов на сервере, записи в БД) и т.п
.   


     3.Функциональные (E2E, UI) тесты — тесты, когда запускается реальное окружение (браузер, тестовые стенды API) и тест прогоняется на странице в браузере, кликая по кнопкам, заполняя формы и работая с внешними сервисами или API.
        Системный уровень проверят взаимодействие тестируемого ПО с системой по функциональным и нефункциональным требованиям.

    Важно тестировать на максимально приближенном окружении, которое будет у конечного пользователя.

    Тест-кейсы на этом уровне подготавливаются:

    По требованиям.

    По возможным способам использования ПО.

    На системном уровне выявляются такие дефекты, как неверное использование ресурсов системы, непредусмотренные комбинации данных пользовательского уровня, несовместимость с окружением, непредусмотренные сценарии использования, отсутствующая или неверная функциональность, неудобство использования и т.д.

    На этом уровне используют черный ящик.