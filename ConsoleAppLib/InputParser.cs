namespace ConsoleAppLib
{
    public class InputParser
    {
        private CommandsDispatcher dispatcher;

        public (Command Command, CommandParameters Parameters) TryParse(string input)
        {
            // Распознаёт команду
            var decomposed = Decompose(input);
            var command = dispatcher.GetCommandByName(decomposed.Name);
            if (command == null)
                return (dispatcher.GetCommandByName("RepeatInput"), new CommandParameters());
            else
                return (command, decomposed.parameters);
        }
        private (string Name, CommandParameters parameters) Decompose(string input)
        {
            var arr = input.Split(' ');
            var name = arr[0];
            if (arr.Length == 1)
                return (name, new CommandParameters());
            else
            {
                var parameters = new string[arr.Length - 1];
                for (int i = 0; i < parameters.Length; i++)
                    parameters[i] = arr[i - 1];
                return (name, new CommandParameters(parameters));
            }
        }

        public InputParser(CommandsDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }
    }

    /// 1. Переделать внутренние команды (внутренние - это те, которые нельзя ввести с консоли - RepeatInput)
    /// 2. К командам прикрутить функцию валидации
    /// 3. Поставить валидацию в инпут парсер
    /// 4. Команду RepeatInput сделать с параметром "неверная команда", "неверные параметры".
    /// 5. По фану - прикрутить английский и русский языки и сделать команду switch language
    /// 6. Сделать вывод списка доступных команд..
    /// 7. Возможно, строковые константы вынести в файлы.

}
