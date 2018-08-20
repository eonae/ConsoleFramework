namespace ConsoleAppLib
{
    public class CommandParameters
    {
        private string[] arr;
        public bool IsEmpty
        {
            get // Не уверен, нужда ли здесь эта проверка. Вопрос в том создаётся ли массив при инициализации без параметров???
            {
                if (arr != null)
                    return (arr.Length == 0);
                else
                    return true;
            }
        }

        public CommandParameters(params string[] parameters)
        {
            arr = parameters;
        }
    }
}
