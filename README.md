# 💻 StepikCourseDownloader

💻 Программа для скачивания текстовых уроков в формате `HTML` со Stepik.

![StepikCourseDownloader](https://github.com/snikitin-de/StepikCourseDownloader/assets/25394427/7de2f352-e731-43ff-a918-121e12ec683f)

## 📄 Описание

В программу вводится идентификатор курса со Stepik. Идентификатор курса находится в `URL` после слова "course":

`https://stepik.org/course/87924/info`

Здесь идентификатор курса — *87924*.

По данному идентификатору скачиваются все текстовые уроки указанного курса в формате `HTML`.

Помимо идентификатора курса необходимо создать приложение в Stepik и получить `clientId` и `clientSecret` для возможности работы со Stepik API.

После получения авторизационных данных, необходимо рядом с исполняемым файлом программы расположить файл `config.yaml` со следующим содержимым:

```yaml
ClientId: XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
ClientSecret: XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
```

Вместо 'X' необходимо ввести полученные `clientId` и `clientSecret` от Stepik.

Программа создает папку по названию курса рядом с исполняемым файлом. В указанной папке создаются подпапки, в названии которых указывается названии секции курса.

Пример структуры папок скачанного курса:

![Пример структуры скачанного курса](https://github.com/snikitin-de/StepikCourseDownloader/assets/25394427/fc62e3fe-3e8a-4c70-97ef-fff5aeae6afd)

## 💻 Работа программы

Пример работы программы:

![Пример работы программы](https://github.com/snikitin-de/StepikCourseDownloader/assets/25394427/fab592c8-ea9c-4926-b0d6-454d5ad2d83d)

## 🔧 Техническая часть

* Проект реализован в виде консольного приложения.
* Для чтения конфига используется библиотека *YamlDotNet*.

### 🧩 Архитектура

Структура каталога решения:

![Структура каталога решения](https://github.com/snikitin-de/StepikCourseDownloader/assets/25394427/5c988ad3-728e-4e60-933b-909ec9495407)
