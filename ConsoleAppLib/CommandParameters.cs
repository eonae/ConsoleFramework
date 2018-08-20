namespace ConsoleAppLib
{
    public class CommandParameters
    {
        public string[] Array { get; private set; }
        public bool IsEmpty
        {
            get // Не уверен, нужда ли здесь эта проверка. Вопрос в том создаётся ли массив при инициализации без параметров???
            {
                if (Array != null)
                    return (Array.Length == 0);
                else
                    return true;
            }
        }

        public CommandParameters(params string[] parameters) //Надо будет потом сделать так, чтобы передавать можно было объекты!
        {
            Array = parameters;
        }
    }
}

//Добавить индексатор!
