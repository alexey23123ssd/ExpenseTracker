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
