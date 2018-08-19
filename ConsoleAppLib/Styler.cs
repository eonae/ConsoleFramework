using System;
using System.Text;

namespace ConsoleAppLib
{
    public enum LineStyle { Normal, Strong }
    public enum Alignment { Left, Center, Right }

    public sealed class Styler
    {
        private const string _defaultGreetings = "Welcome to console application!";
        private const string _defaultFarewell = "Thank you for using our application!";

        private struct Line
        {
            public readonly string Text;
            public readonly (int Left, int Right) InnerSpaces;

            public Line(string text, (int, int) innerSpaces)
            {
                Text = text;
                InnerSpaces = innerSpaces;
            }

            public string GetString(char verticalLineChar)
            {
                string margin = new string(' ', Appearence.Margins.Left);

                var sb = new StringBuilder();
                sb.Append(margin);
                sb.Append(verticalLineChar);
                sb.Append(' ', InnerSpaces.Left);
                sb.Append(Text);
                sb.Append(' ', InnerSpaces.Right);
                sb.Append(verticalLineChar);

                return sb.ToString();
            }
        }

        public static AppearenceConfig Appearence { get; } = new AppearenceConfig();
        public static string GreetingsMessage { get; set; } = _defaultGreetings;
        public static string FarewellMessage { get; set; } = _defaultFarewell;
        
        public static void DisplayGreetings()
        {
            Console.WriteLine(BoxMessage(GreetingsMessage, Alignment.Left, LineStyle.Normal));
        }
        public static void DisplayFarewell()
        {
            Console.WriteLine(BoxMessage(FarewellMessage, Alignment.Left, LineStyle.Normal));
        }
        public static void DisplayInputSymbols()
        {
            Console.Write($"{Appearence.InputSymbols} ");
        }

        private static string BoxMessage(string message, Alignment alignment, LineStyle linesStyle)
        {
            // Неплохо бы провести рефакторинг..

            (char Horizontal, char Vertical) lineChars = (Appearence.HorizontalLineStyles.Normal, Appearence.VerticalLineStyles.Normal);
            switch (linesStyle)
            {
                case LineStyle.Normal:
                    break;
                case LineStyle.Strong:
                    lineChars.Horizontal = Appearence.HorizontalLineStyles.Strong;
                    lineChars.Vertical = Appearence.VerticalLineStyles.Strong;
                    break;
            }
            
            var decomposed = Decompose(message);
            var spaces = GetSpaces(decomposed.LinesArr, decomposed.MaxLength, alignment);
            string horizontalLine =
                new string(' ', Appearence.Margins.Left) +
                new string(lineChars.Horizontal, decomposed.MaxLength + Appearence.Margins.Left + Appearence.InnerMargin.Horizontal*2+1);
            var sb = new StringBuilder();
            sb.AppendLine(horizontalLine);
            sb.Append(GetVerticalMargin(lineChars.Vertical, decomposed.MaxLength + Appearence.InnerMargin.Horizontal * 2, Appearence.InnerMargin.Vertical));
            for (int i = 0; i < decomposed.LinesArr.Length; i++)
                sb.AppendLine(new Line(decomposed.LinesArr[i], spaces[i]).GetString(lineChars.Vertical));
            sb.Append(GetVerticalMargin(lineChars.Vertical, decomposed.MaxLength + Appearence.InnerMargin.Horizontal * 2, Appearence.InnerMargin.Vertical));
            sb.AppendLine(horizontalLine);

            return sb.ToString();
        }

        private static string GetVerticalMargin(char verticalLineChar, int width, int margin)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < margin; i++)
            {
                sb.Append(' ', Appearence.Margins.Left);
                sb.Append(verticalLineChar);
                sb.Append(' ', width);
                sb.Append(verticalLineChar);
                sb.AppendLine();
            }
            return sb.ToString();
        }
        private static (string[] LinesArr, int MaxLength) Decompose(string message)
        {
            // Разбивает строку на подстроки (если есть символы перевода строки) и вычисляет максимальную длину подстроки.

            string[] lines = message.Split(new string[] { "\n", "\r", "\r\n" }, StringSplitOptions.None);
            int maxLength = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length > maxLength)
                    maxLength = lines[i].Length;
            }
            return (lines, maxLength);
        }
        private static (int Left, int Right)[] GetSpaces(string[] lines, int maxLength, Alignment align)
        {
            // Вычислить количество пробелов слева и справа (у каждой строки)

            (int l, int r)[] result = new(int l, int r)[lines.Length];

            int width = maxLength + Appearence.InnerMargin.Horizontal*2;
            for (int i = 0; i < lines.Length; i++)
            {
                int left = 0, right = 0;
                int totalSpaces = width - lines[i].Length;
                switch (align)
                {
                    case Alignment.Left:
                        left = Appearence.InnerMargin.Horizontal; break;
                    case Alignment.Right:
                        left = totalSpaces - Appearence.InnerMargin.Horizontal; break;
                    case Alignment.Center:
                        left = totalSpaces / 2; break;
                }
                right = totalSpaces - left;
                result[i] = (left, right);
            }
            return result;
        }


    }
}
