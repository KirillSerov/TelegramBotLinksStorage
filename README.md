# TelegramBotLinksStorage
## Chat
- ### class TelegramChat
  #### Реализует интерфейс IChat. Принимает в конструктор token телеграм бота. Метод Start() запускает прослушку.
## Commands
### Эти классы реализуют интерфейс ICommand. Метод Execute() реализует некоторую логику и возвращает результат в виде строкового значения.
- ### class StartCommand
  - Создается при получении команды /start. Возвращает приветственное сообщение.
- ### class HelpCommand
  - Создается при получении команды /help. Возвращает список команд.
- ### class StoreLinkCommand
  - Создается при получении команды /store_link. Запускает процесс сохранения ссылки.
- ### class GetLinksCommand
  - Создается при получении команды /get_links. Запускает процесс отображения ссылок.
## class CommandFactory
  ### Класс для создания комманд. Метод GetCommand() возвращает объект в зависимости от полученной команды.
## class CommandRepository
  ### Класс для хранения незавершенных команд.
## Handler
- ### class CommandHandler
    #### Объект данного класса обрабатывает обновления от Telegram.
## Storage
- ### class MemoryStorage
    #### Объект хранилища ссылок.
