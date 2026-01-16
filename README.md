# 💻 StepikCourseDownloader

💻 📝 Программа для скачивания текстовых уроков со Stepik в формате `PDF`.

![StepikCourseDownloader](https://github.com/snikitin-de/StepikCourseDownloader/assets/25394427/c438dfb2-bdc9-4d76-908e-496b0c562f34)

## 📄 Описание

В программу вводится идентификатор курса со Stepik. Идентификатор курса находится в `URL` после слова "course":

`https://stepik.org/course/120679/syllabus`

Здесь идентификатор курса — *120679*.

По данному идентификатору скачиваются все текстовые уроки указанного курса в формате `HTML`.

Помимо идентификатора курса необходимо создать приложение в Stepik и получить `clientId` и `clientSecret` для возможности работы со Stepik API.

Приложение создаётся по адресу https://stepik.org/oauth2/applications/ со следующими параметрами:

<img width="497" height="647" alt="image" src="https://github.com/user-attachments/assets/1a2e1a33-dc12-4491-8f91-e4e5c22168a1" />

После получения авторизационных данных, необходимо рядом с исполняемым файлом программы (.exe) расположить файл `config.yaml` со следующим содержимым:

```yaml
ClientId: XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
ClientSecret: XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
```

Вместо 'X' необходимо ввести полученные `clientId` и `clientSecret` от Stepik.

Программа создает папку по названию курса рядом с исполняемым файлом. В указанной папке создаются подпапки, в названии которых указывается названии секции курса.

Пример структуры папок скачанного курса:

![Пример структуры скачанного курса](https://github.com/snikitin-de/StepikCourseDownloader/assets/25394427/493f50ea-a549-4431-a5bc-9e63c13be5b5)

## 💻 Работа программы

Пример работы программы:

![Пример работы программы](https://github.com/snikitin-de/StepikCourseDownloader/assets/25394427/fab592c8-ea9c-4926-b0d6-454d5ad2d83d)

Пример полученной PDF страницы из курса [Тестирование ПО: Postman для тестирования API](https://stepik.org/lesson/746806/step/3?unit=748617):

![Пример полученной PDF страницы](https://github.com/snikitin-de/StepikCourseDownloader/assets/25394427/52883beb-e6d3-4013-8702-164b843ab290)

## 🔧 Техническая часть

* Проект реализован в виде консольного приложения.
* Для работы с `JSON` используется библиотека *Newtonsoft.Json*.
* Для чтения конфига используется библиотека *YamlDotNet*.
* Для конвертации `HTML` в `PDF` используется библиотека *OpenHtmlToPdf*.
* Для объединения нескольких `PDF` файлов в один используется библиотека *PDFsharp*.

### 🧩 Архитектура

Структура каталога решения:

![Структура каталога решения](https://github.com/snikitin-de/StepikCourseDownloader/assets/25394427/f0cedba1-55fd-4042-b7f1-6e84b4fe78c3)

