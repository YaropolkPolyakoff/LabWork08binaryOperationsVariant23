using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BitwiseOperationsApp
{
    /// <summary>
    /// Главный класс Windows Forms приложения для выполнения побитовых операций
    /// </summary>
    public class MainForm : Form
    {
        // Элементы управления
        private TextBox txtChar1;
        private TextBox txtChar2;
        private Button btnExecute;
        private RichTextBox txtOutput;
        private Label lblChar1;
        private Label lblChar2;
        private Label lblOutput;
        private Label lblTitle;

        public MainForm()
        {
            InitializeComponents();
        }

        /// <summary>
        /// Инициализация элементов управления формы
        /// </summary>
        private void InitializeComponents()
        {
            this.Text = "Побитовые операции - Сложение по модулю 2";
            this.Size = new Size(800, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Заголовок
            lblTitle = new Label
            {
                Text = "Приложение для выполнения побитовых операций над двумя символами",
                Location = new Point(20, 20),
                Size = new Size(740, 40),
                Font = new Font("Arial", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Метка для первого символа
            lblChar1 = new Label
            {
                Text = "Первый символ:",
                Location = new Point(20, 80),
                Size = new Size(150, 25),
                Font = new Font("Arial", 10)
            };

            // Поле ввода первого символа
            txtChar1 = new TextBox
            {
                Location = new Point(180, 80),
                Size = new Size(100, 25),
                Font = new Font("Arial", 10),
                MaxLength = 1
            };

            // Метка для второго символа
            lblChar2 = new Label
            {
                Text = "Второй символ:",
                Location = new Point(300, 80),
                Size = new Size(150, 25),
                Font = new Font("Arial", 10)
            };

            // Поле ввода второго символа
            txtChar2 = new TextBox
            {
                Location = new Point(460, 80),
                Size = new Size(100, 25),
                Font = new Font("Arial", 10),
                MaxLength = 1
            };

            // Кнопка выполнения операции
            btnExecute = new Button
            {
                Text = "Выполнить операцию",
                Location = new Point(580, 75),
                Size = new Size(180, 35),
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.LightBlue
            };
            btnExecute.Click += BtnExecute_Click;

            // Метка для вывода результатов
            lblOutput = new Label
            {
                Text = "Результаты выполнения:",
                Location = new Point(20, 130),
                Size = new Size(250, 25),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            // Поле вывода результатов
            txtOutput = new RichTextBox
            {
                Location = new Point(20, 160),
                Size = new Size(740, 480),
                Font = new Font("Courier New", 9),
                ReadOnly = true,
                BackColor = Color.White,
                WordWrap = false
            };

            // Добавление элементов на форму
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblChar1);
            this.Controls.Add(txtChar1);
            this.Controls.Add(lblChar2);
            this.Controls.Add(txtChar2);
            this.Controls.Add(btnExecute);
            this.Controls.Add(lblOutput);
            this.Controls.Add(txtOutput);
        }

        /// <summary>
        /// Обработчик нажатия кнопки выполнения операции
        /// </summary>
        private void BtnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                // Проверка ввода
                if (string.IsNullOrEmpty(txtChar1.Text) || string.IsNullOrEmpty(txtChar2.Text))
                {
                    MessageBox.Show("Пожалуйста, введите оба символа!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                char char1 = txtChar1.Text[0];
                char char2 = txtChar2.Text[0];

                // Выполнение побитовой операции с трассировкой
                string result = PerformBitwiseOperation(char1, char2);
                
                txtOutput.Clear();
                txtOutput.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Основной метод выполнения побитовой операции с трассировкой
        /// </summary>
        /// <param name="char1">Первый входной символ</param>
        /// <param name="char2">Второй входной символ</param>
        /// <returns>Строка с подробной трассировкой выполнения операции</returns>
        private string PerformBitwiseOperation(char char1, char char2)
        {
            StringBuilder trace = new StringBuilder();
            
            trace.AppendLine("===========================================================");
            trace.AppendLine("         ТРАССИРОВКА ПОБИТОВЫХ ОПЕРАЦИЙ");
            trace.AppendLine("===========================================================\n");

            // Получаем коды символов
            ushort code1 = (ushort)char1;
            ushort code2 = (ushort)char2;

            trace.AppendLine("ШАГ 1: ИСХОДНЫЕ ДАННЫЕ");
            trace.AppendLine("-----------------------------------------------------------");
            trace.AppendLine($"Первый символ:  '{char1}'");
            trace.AppendLine($"Код символа:    {code1}");
            trace.AppendLine($"Двоичное представление: {ConvertToBinaryString(code1, 16)}");
            trace.AppendLine($"  Левый байт:   {ConvertToBinaryString(GetLeftByte(code1), 8)}  (значение: {GetLeftByte(code1)})");
            trace.AppendLine($"  Правый байт:  {ConvertToBinaryString(GetRightByte(code1), 8)}  (значение: {GetRightByte(code1)})");
            trace.AppendLine();
            trace.AppendLine($"Второй символ:  '{char2}'");
            trace.AppendLine($"Код символа:    {code2}");
            trace.AppendLine($"Двоичное представление: {ConvertToBinaryString(code2, 16)}");
            trace.AppendLine($"  Левый байт:   {ConvertToBinaryString(GetLeftByte(code2), 8)}  (значение: {GetLeftByte(code2)})");
            trace.AppendLine($"  Правый байт:  {ConvertToBinaryString(GetRightByte(code2), 8)}  (значение: {GetRightByte(code2)})");
            trace.AppendLine();

            // Сложение по модулю 2 (XOR)
            trace.AppendLine("ШАГ 2: СЛОЖЕНИЕ ПО МОДУЛЮ 2 (XOR)");
            trace.AppendLine("-----------------------------------------------------------");
            trace.AppendLine($"Операция: {code1} XOR {code2}");
            trace.AppendLine($"  {ConvertToBinaryString(code1, 16)}  (первый символ)");
            trace.AppendLine($"^ {ConvertToBinaryString(code2, 16)}  (второй символ)");
            trace.AppendLine("  --------------------------------");
            
            ushort xorResult = (ushort)(code1 ^ code2);
            trace.AppendLine($"  {ConvertToBinaryString(xorResult, 16)}  (результат XOR)");
            trace.AppendLine($"Результат XOR: {xorResult}");
            trace.AppendLine();

            // Проверка условия
            trace.AppendLine("ШАГ 3: ПРОВЕРКА УСЛОВИЯ");
            trace.AppendLine("-----------------------------------------------------------");
            trace.AppendLine($"Результат XOR = {xorResult}");
            trace.AppendLine($"Проверка: {xorResult} < 255 ?");
            
            ushort newCode1 = code1;
            ushort newCode2 = code2;
            
            // Проверка с использованием побитовых операций
            // Число меньше 255, если старшие биты (биты 8-15) все равны 0
            bool isLessThan255 = IsLessThan255(xorResult);
            
            trace.AppendLine($"Ответ: {(isLessThan255 ? "ДА" : "НЕТ")}");
            trace.AppendLine();

            if (isLessThan255)
            {
                // Заменить левый байт второго символа на 4
                trace.AppendLine("ШАГ 4: ВЫПОЛНЕНИЕ ОПЕРАЦИИ (результат < 255)");
                trace.AppendLine("-----------------------------------------------------------");
                trace.AppendLine("Действие: Заменить левый байт второго символа на значение 4");
                trace.AppendLine();
                trace.AppendLine($"Исходный второй символ: {ConvertToBinaryString(code2, 16)}");
                trace.AppendLine($"  Левый байт:  {ConvertToBinaryString(GetLeftByte(code2), 8)}  (значение: {GetLeftByte(code2)})");
                trace.AppendLine($"  Правый байт: {ConvertToBinaryString(GetRightByte(code2), 8)}  (значение: {GetRightByte(code2)})");
                trace.AppendLine();
                
                newCode2 = SetLeftByte(code2, 4);
                
                trace.AppendLine("Применение операции:");
                trace.AppendLine($"  1. Маска для правого байта:  {ConvertToBinaryString(0x00FF, 16)} (0x00FF)");
                trace.AppendLine($"  2. Извлечение правого байта: {ConvertToBinaryString(code2, 16)} & {ConvertToBinaryString(0x00FF, 16)} = {ConvertToBinaryString(code2 & 0x00FF, 16)}");
                trace.AppendLine($"  3. Значение 4 в левом байте: {ConvertToBinaryString(4 << 8, 16)} (4 << 8)");
                trace.AppendLine($"  4. Объединение (OR):         {ConvertToBinaryString(code2 & 0x00FF, 16)} | {ConvertToBinaryString(4 << 8, 16)} = {ConvertToBinaryString(newCode2, 16)}");
                trace.AppendLine();
                trace.AppendLine($"Новый второй символ: {ConvertToBinaryString(newCode2, 16)}");
                trace.AppendLine($"  Левый байт:  {ConvertToBinaryString(GetLeftByte(newCode2), 8)}  (значение: {GetLeftByte(newCode2)})");
                trace.AppendLine($"  Правый байт: {ConvertToBinaryString(GetRightByte(newCode2), 8)}  (значение: {GetRightByte(newCode2)})");
            }
            else
            {
                // Обнулить правый байт первого символа
                trace.AppendLine("ШАГ 4: ВЫПОЛНЕНИЕ ОПЕРАЦИИ (результат >= 255)");
                trace.AppendLine("-----------------------------------------------------------");
                trace.AppendLine("Действие: Обнулить правый байт первого символа");
                trace.AppendLine();
                trace.AppendLine($"Исходный первый символ: {ConvertToBinaryString(code1, 16)}");
                trace.AppendLine($"  Левый байт:  {ConvertToBinaryString(GetLeftByte(code1), 8)}  (значение: {GetLeftByte(code1)})");
                trace.AppendLine($"  Правый байт: {ConvertToBinaryString(GetRightByte(code1), 8)}  (значение: {GetRightByte(code1)})");
                trace.AppendLine();
                
                newCode1 = ZeroRightByte(code1);
                
                trace.AppendLine("Применение операции:");
                trace.AppendLine($"  1. Маска для левого байта:   {ConvertToBinaryString(0xFF00, 16)} (0xFF00)");
                trace.AppendLine($"  2. Применение маски (AND):   {ConvertToBinaryString(code1, 16)} & {ConvertToBinaryString(0xFF00, 16)} = {ConvertToBinaryString(newCode1, 16)}");
                trace.AppendLine();
                trace.AppendLine($"Новый первый символ: {ConvertToBinaryString(newCode1, 16)}");
                trace.AppendLine($"  Левый байт:  {ConvertToBinaryString(GetLeftByte(newCode1), 8)}  (значение: {GetLeftByte(newCode1)})");
                trace.AppendLine($"  Правый байт: {ConvertToBinaryString(GetRightByte(newCode1), 8)}  (значение: {GetRightByte(newCode1)})");
            }

            trace.AppendLine();
            trace.AppendLine("===========================================================");
            trace.AppendLine("         ИТОГОВЫЕ РЕЗУЛЬТАТЫ");
            trace.AppendLine("===========================================================");
            trace.AppendLine($"Первый символ:");
            trace.AppendLine($"  Исходный:  '{char1}' (код: {code1}, двоичное: {ConvertToBinaryString(code1, 16)})");
            trace.AppendLine($"  Конечный:  '{(char)newCode1}' (код: {newCode1}, двоичное: {ConvertToBinaryString(newCode1, 16)})");
            trace.AppendLine();
            trace.AppendLine($"Второй символ:");
            trace.AppendLine($"  Исходный:  '{char2}' (код: {code2}, двоичное: {ConvertToBinaryString(code2, 16)})");
            trace.AppendLine($"  Конечный:  '{(char)newCode2}' (код: {newCode2}, двоичное: {ConvertToBinaryString(newCode2, 16)})");
            trace.AppendLine("===========================================================");

            return trace.ToString();
        }

        /// <summary>
        /// Получить левый байт (старшие 8 бит) числа используя побитовые операции
        /// </summary>
        /// <param name="value">Входное 16-битное значение</param>
        /// <returns>Левый байт (биты 8-15)</returns>
        private byte GetLeftByte(ushort value)
        {
            // Сдвиг вправо на 8 бит для получения старших 8 бит
            return (byte)(value >> 8);
        }

        /// <summary>
        /// Получить правый байт (младшие 8 бит) числа используя побитовые операции
        /// </summary>
        /// <param name="value">Входное 16-битное значение</param>
        /// <returns>Правый байт (биты 0-7)</returns>
        private byte GetRightByte(ushort value)
        {
            // Применение маски 0xFF (0000000011111111) для получения младших 8 бит
            return (byte)(value & 0xFF);
        }

        /// <summary>
        /// Установить левый байт числа используя побитовые операции
        /// </summary>
        /// <param name="value">Исходное 16-битное значение</param>
        /// <param name="leftByte">Новое значение для левого байта</param>
        /// <returns>Новое значение с замененным левым байтом</returns>
        private ushort SetLeftByte(ushort value, byte leftByte)
        {
            // 1. Сохраняем правый байт: value & 0x00FF
            // 2. Сдвигаем новый левый байт влево: leftByte << 8
            // 3. Объединяем через OR
            return (ushort)((value & 0x00FF) | (leftByte << 8));
        }

        /// <summary>
        /// Обнулить правый байт числа используя побитовые операции
        /// </summary>
        /// <param name="value">Исходное 16-битное значение</param>
        /// <returns>Значение с обнуленным правым байтом</returns>
        private ushort ZeroRightByte(ushort value)
        {
            // Применяем маску 0xFF00 (1111111100000000) для обнуления правого байта
            return (ushort)(value & 0xFF00);
        }

        /// <summary>
        /// Проверить, меньше ли значение 255 используя побитовые операции
        /// </summary>
        /// <param name="value">Проверяемое значение</param>
        /// <returns>true если значение меньше 255, иначе false</returns>
        private bool IsLessThan255(ushort value)
        {
            // Число меньше 255 если все биты старше 7-го равны 0
            // Применяем маску 0xFF00 (биты 8-15)
            // Если результат 0, значит все старшие биты равны 0
            return (value & 0xFF00) == 0;
        }

        /// <summary>
        /// Преобразовать число в двоичную строку используя побитовые операции
        /// </summary>
        /// <param name="value">Преобразуемое значение</param>
        /// <param name="bits">Количество бит для отображения</param>
        /// <returns>Строка с двоичным представлением числа</returns>
        private string ConvertToBinaryString(int value, int bits)
        {
            StringBuilder binary = new StringBuilder();
            
            // Проходим по битам от старшего к младшему
            for (int i = bits - 1; i >= 0; i--)
            {
                // Создаем маску для текущего бита
                int mask = 1 << i;
                // Проверяем, установлен ли бит
                int bit = (value & mask) != 0 ? 1 : 0;
                binary.Append(bit);
                
                // Добавляем пробел после каждых 4 бит для читаемости
                if (i > 0 && (i & 3) == 0)
                {
                    binary.Append(" ");
                }
            }
            
            return binary.ToString();
        }
    }

    /// <summary>
    /// Главный класс программы
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Точка входа в приложение
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
