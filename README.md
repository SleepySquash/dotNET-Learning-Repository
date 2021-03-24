# dotNET-Learning-Repository

## Описание

Тема: мониторинг состояния пациентов (с болезнью Паркинсона).

Есть таблица пользователей, которые через мобильное приложение будут подключаться по комбинации номер + пароль к своей учётной записи, от имени которой они будут оставлять в таблицу состояний краткие отметки ("+" - хорошее самочувствие, "~" - нейтральное самочувствие и "-" - отрицательное самочувствие).
Врач должен иметь доступ к таблице пользователей, к их паролям, а также иметь возможность добавлять новых и редактировать или удалять уже имеющихся пациентов.
Кроме того, врач может добавлять состояния или редактировать их у пациентов - например, если человек пришёл на очный приём.

Тема выбрана такой потому, что моя ВКР подразумевает примерно такой сценарий использования. Правда, в ВКР будет использована для отправки состояний электронная почта, что немного странно, поэтому данный проект очень кстати - может, смогу переубедить научного руководителя.

## Замечания

Авторизация пока не используется, но её можно будет ввести в BLL слой.
(!!!) Кроме того, совершенно никакой безопасности в следствие отсутствие авторизации нет - всё-таки учебный проект, а не готовый.
Проект представляет собой два подпроекта: WebAPI (сам проект) и WebApplication (интерфейс). Общаются они по сети с помощью JSON пакетов.

## Установка

В качестве базы данных используется MySQL. В DefaultConnection указаны имя пользователя, пароль и имя схемы (базы данных).
Для применения миграций в папке DataAccess выполнить в командной строке (имея dotNET API и Entity Framework в нём установленными): "dotnet-ef database update" - в VS, вроде бы, можно без командной строки обойтись, но конкретно данная работа писалась в Rider, а там dotnet-ef только через терминал/cmd доступен.

## Использование

Сначала запустить WebAPI (бэкэнд), там подключён Swagger, который и откроется при запуске:
![Screenshot 1](https://github.com/SleepySquash/dotNET-Learning-Repository/blob/master/Screenshot%202021-03-24%20at%2020.08.37.jpg)

Затем запустить WebApplication (фротнэнд). На странице Users отображается список пользователей:
![Screenshot 2](https://github.com/SleepySquash/dotNET-Learning-Repository/blob/master/Screenshot%202021-03-24%20at%2020.10.11.jpg)

Можно удалить, а также добавить или отредактировать пользователя:
![Screenshot 3](https://github.com/SleepySquash/dotNET-Learning-Repository/blob/master/Screenshot%202021-03-24%20at%2020.11.35.jpg)

По нажатию кнопки ">>>" отображается отчёт состояния выбранного пользователя:
![Screenshot 4](https://github.com/SleepySquash/dotNET-Learning-Repository/blob/master/Screenshot%202021-03-24%20at%2020.10.38.jpg)

Аналогично пользователям, состояния тоже можно добавлять, удалять и редактировать.
