using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCapital
{
  public class Calculator
    {
        //перечесление операция слочение, вычитание, умножение, деление, ничего
        public enum CalculatorOperationType { Addition, Substraction, Dividing, Multiplying, None };
        static Calculator()
        {
            InputBuffer = "0";
            OutputBuffer = string.Empty;
            InputRestriction = -1;

            isFirstNumValueSet = false;
            isBlocked = false;
            doesTheInputContainResOrPrevNum = false;
            doesTheInputContainConstantValue = false;
            isUnaryOperation = false;
            doesTheLastOutputContainsParentheses = false;
            isRadians = false;

            firstNumValue = 0;
            secondNumValue = 0;
            unaryBuffer = string.Empty;
            lastComputation = string.Empty;

            nextOperationType = CalculatorOperationType.None;
            prevOperationType = CalculatorOperationType.None;

            decimalSeparator = Convert.ToChar(System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        }

        //ПОЛЯ
        //входные данные для отображения на экране
        private static string _inputBuffer;
        //предыдущее число
        private static string _outputBuffer;
        //одинарный ограничитель
        private static string unaryBuffer;
        //последнее вычесление
        private static string lastComputation;
        //ограничение ввода
        private static int _inputRestriction;


        //установлено ли первое числовое значение
        private static bool isFirstNumValueSet;
        //Содержит ли этот параметр значение или предыдущий номер
        private static bool doesTheInputContainResOrPrevNum;
        //Содержит ла параметр постоянное значение
        private static bool doesTheInputContainConstantValue;
        //заблокирован?
        private static bool isBlocked;
        //Это обычная операция
        private static bool isUnaryOperation;
        //Содержит ли Последний Вывод круглые скобки
        private static bool doesTheLastOutputContainsParentheses;
        //это радианы
        private static bool isRadians;


        //Первое числовое значение
        private static double firstNumValue;
        //Второе числовое значение
        private static double secondNumValue;


        //изменение входного буфера
        public static event EventHandler InputBufferChanged;
        //изменние выходного буфера
        public static event EventHandler OutputBufferChanged;
        //Вычисление завершено
        public static event EventHandler ComputationEnded;


        //Следующий тип операции
        private static CalculatorOperationType nextOperationType;
        //Тип предварительной операции
        private static CalculatorOperationType prevOperationType;


        //???
        private const double PI = Math.PI;
        //Разделитель десятичных дробей
        private static char decimalSeparator;

        //СВОЙСТВА
        //вывод на экран
        public static string InputBuffer
        {
            get
            {
                return _inputBuffer;
            }



            private set
            {
                //Содержит ли этот параметр значение или предыдущий номер или содержит ли постоянное значение ?
                if (doesTheInputContainResOrPrevNum || doesTheInputContainConstantValue)
                {
                    //если переменная не пуста
                    if (_inputBuffer != string.Empty)

                        //bool Remove(K key): удаляет по ключу элемент из словаря
                        //Другая версия этого метода позволяет получить удленный элемент в выходной параметр: bool Remove(K key, out V value)
                        //че он делает 
                        value = value.Remove(0, _inputBuffer.Length);

                    if (value != string.Empty && value[0] == decimalSeparator)
                        //замена на 0
                        value = value.Insert(0, "0");

                    doesTheInputContainResOrPrevNum = false;
                    doesTheInputContainConstantValue = false;
                }

                if (value.Length > 1 && value[0] == '0')
                {
                    if (value[1] != decimalSeparator)
                        //Substring(int startIndex). Возвращает подстроку, которая начинается с startIndex и продолжается до конца строки
                        value = value.Substring(1);
                }

                _inputBuffer = value;

                OnInputBufferChanged(EventArgs.Empty);
            }
        }
        //история 
        public static string OutputBuffer
        {
            get
            {
                if (_outputBuffer.Length >= 40) return "..." + _outputBuffer.Substring(_outputBuffer.Length - 40, 40);
                return _outputBuffer;
            }
            private set
            {
                _outputBuffer = value;
                OnOutputBufferChanged(EventArgs.Empty);
            }
        }
        //Ограничение ввода
        public static int InputRestriction
        {
            get
            {
                return _inputRestriction;
            }
            set
            {
                if (value < -1) value = Math.Abs(value);
                _inputRestriction = value;
            }
        }


        //ввод цифры
        public static void EnterNumber(int num)
        {
            if (isInputAllowed())
            {
                if (isUnaryOperation)
                {
                    EraseUnaryOperation();
                    InputBuffer = num.ToString();
                }
                else InputBuffer += num.ToString();
            }
        }

        //ввод точки

        public static void EnterDot()
        {
            if (isInputAllowed() && (!InputBuffer.Contains(decimalSeparator.ToString()) || doesTheInputContainResOrPrevNum || doesTheInputContainConstantValue))
            {
                if (isUnaryOperation)
                {
                    EraseUnaryOperation();
                    InputBuffer = "0" + decimalSeparator.ToString();
                }
                else InputBuffer += decimalSeparator;
            }
        }
        //Последнее стирание
        public static void EraseLast()
        {
            if (!doesTheInputContainResOrPrevNum && !isBlocked && !doesTheInputContainConstantValue && !isUnaryOperation)
            {
                InputBuffer = InputBuffer.Remove(InputBuffer.Length - 1);
                if (InputBuffer.Length == 0 || (InputBuffer.Length == 1 && InputBuffer[0] == '-')) InputBuffer = "0";
            }
        }

        //Очистить Запись
        public static void ClearEntry()
        {
            doesTheInputContainConstantValue = false;
            doesTheInputContainResOrPrevNum = false;
            if (isUnaryOperation) EraseUnaryOperation();
            InputBuffer = "0";
            if (isBlocked)
            {
                OutputBuffer = string.Empty;
                unaryBuffer = string.Empty;
                isBlocked = false;
            }

        }

        //Очистить
        public static void Clear()
        {

            doesTheInputContainResOrPrevNum = false;
            ClearOutputBuffer();

            isBlocked = false;
            InputBuffer = "0";


        }


        //ИзменитьРежимИзмерения
        //УДАЛИТЬ
        public static void ChangeMeasureMode()
        {
            isRadians = !isRadians;
        }


        //РАВНО =
        public static void Equal()
        {
            if (!isBlocked)
            {
                if (doesTheInputContainResOrPrevNum || (nextOperationType == CalculatorOperationType.None && isUnaryOperation))
                {

                    doesTheInputContainResOrPrevNum = true;

                    if (OutputBuffer != string.Empty)
                    {
                        if (isUnaryOperation)
                        {
                            lastComputation = _outputBuffer + " = " + InputBuffer;
                            ComputationEnded?.Invoke(null, new EventArgs());

                        }
                        else if (prevOperationType != CalculatorOperationType.None || _outputBuffer[_outputBuffer.Length - 4] == ')')
                        {
                            lastComputation = _outputBuffer.Remove(_outputBuffer.Length - 2, 2) + "= " + InputBuffer;
                            ComputationEnded?.Invoke(null, new EventArgs());
                        }


                    }
                    ClearOutputBuffer();

                }
                else if ((prevOperationType != CalculatorOperationType.None || (prevOperationType == CalculatorOperationType.None && nextOperationType != CalculatorOperationType.None)))
                {
                    if (Double.TryParse(InputBuffer, out secondNumValue))
                    {
                        string secondNum = InputBuffer;
                        ExecuteOperationImpl(nextOperationType);
                        if (!isUnaryOperation) lastComputation = _outputBuffer + secondNum + " = " + InputBuffer;
                        else lastComputation = _outputBuffer + " = " + InputBuffer;

                        ComputationEnded?.Invoke(null, new EventArgs());
                        ClearOutputBuffer();

                    }
                }
            }
        }



        //ДобавитьСледующуюОперацию
        private static void AddNextOperation(in CalculatorOperationType operation, char sign)
        {
            double num;
            if (Double.TryParse(InputBuffer, out num))
            {
                if (!isFirstNumValueSet)
                {
                    firstNumValue = num;
                    isFirstNumValueSet = true;
                }
                else secondNumValue = num;

                string outputValueStr;

                if (nextOperationType != CalculatorOperationType.None && (!doesTheInputContainResOrPrevNum || doesTheInputContainConstantValue))
                {
                    ExecuteOperationImpl(nextOperationType);
                    if (isBlocked) return;

                    if (!isUnaryOperation)
                    {
                        outputValueStr = secondNumValue.ToString();
                    }
                    else
                    {
                        outputValueStr = string.Empty;
                        isUnaryOperation = false;
                        unaryBuffer = string.Empty;
                    }

                    if ((operation == CalculatorOperationType.Multiplying || operation == CalculatorOperationType.Dividing)
                        && (prevOperationType == CalculatorOperationType.Addition || prevOperationType == CalculatorOperationType.Substraction)
                        && !doesTheLastOutputContainsParentheses)
                    {
                        OutputBuffer = "(" + _outputBuffer + outputValueStr + ")" + ' ' + sign + ' ';
                        doesTheLastOutputContainsParentheses = true;
                    }
                    else OutputBuffer = _outputBuffer + outputValueStr + ' ' + sign + ' ';

                }
                else if (!doesTheInputContainResOrPrevNum || doesTheInputContainConstantValue)
                {
                    if (!isUnaryOperation) OutputBuffer = _outputBuffer + firstNumValue.ToString() + ' ' + sign + ' ';
                    else
                    {
                        OutputBuffer = _outputBuffer + " " + sign + " ";
                        isUnaryOperation = false;
                        unaryBuffer = string.Empty;
                    }

                    if (InputBuffer[InputBuffer.Length - 1] == decimalSeparator || (InputBuffer.Length > 1 && InputBuffer[InputBuffer.Length - 2] == decimalSeparator && InputBuffer[InputBuffer.Length - 1] == '0')) InputBuffer = firstNumValue.ToString();
                }
                else
                {
                    if (prevOperationType != CalculatorOperationType.None
                        && (operation == CalculatorOperationType.Substraction || operation == CalculatorOperationType.Addition)
                        && doesTheLastOutputContainsParentheses)
                    {
                        OutputBuffer = (_outputBuffer.Remove(_outputBuffer.Length - 4, 4) + ' ' + sign + ' ').Remove(0, 1);
                        doesTheLastOutputContainsParentheses = false;
                    }
                    else if (prevOperationType != CalculatorOperationType.None
                        && (operation == CalculatorOperationType.Multiplying || operation == CalculatorOperationType.Dividing)
                        && !doesTheLastOutputContainsParentheses)
                    {
                        OutputBuffer = "(" + _outputBuffer.Remove(_outputBuffer.Length - 3, 3) + ")" + ' ' + sign + ' ';
                        doesTheLastOutputContainsParentheses = true;
                    }
                    else
                    {
                        if (OutputBuffer == string.Empty && doesTheInputContainResOrPrevNum) OutputBuffer = firstNumValue.ToString() + " " + sign + " ";
                        else OutputBuffer = _outputBuffer.Remove(_outputBuffer.Length - 2, 2) + sign + ' ';
                    }
                }

                nextOperationType = operation;
                doesTheInputContainResOrPrevNum = true;
                doesTheInputContainConstantValue = false;


            }
        }


        //ВыполнитьОперациюВыполнить
        private static void ExecuteOperationImpl(CalculatorOperationType op)
        {
            switch (op)
            {
                case CalculatorOperationType.Addition:
                    firstNumValue += secondNumValue;
                    break;
                case CalculatorOperationType.Substraction:
                    firstNumValue -= secondNumValue;
                    break;
                case CalculatorOperationType.Dividing:
                    if (secondNumValue == 0)
                    {
                        InvokeError();
                        return;
                    }
                    else
                    {
                        firstNumValue /= secondNumValue;
                    }
                    break;
                case CalculatorOperationType.Multiplying:
                    firstNumValue *= secondNumValue;
                    break;
            }
            prevOperationType = nextOperationType;
            nextOperationType = CalculatorOperationType.None;
            unaryBuffer = string.Empty;
            doesTheInputContainConstantValue = false;
            InputBuffer = firstNumValue.ToString();
            doesTheInputContainResOrPrevNum = true;
            doesTheLastOutputContainsParentheses = false;

        }


        //ВыполнитьОперацию
        public static void ExecuteOperation(CalculatorOperationType op)
        {
            if (op != CalculatorOperationType.None && !isBlocked)
            {
                switch (op)
                {
                    case CalculatorOperationType.Addition:
                        AddNextOperation(CalculatorOperationType.Addition, '+');
                        break;
                    case CalculatorOperationType.Substraction:
                        AddNextOperation(CalculatorOperationType.Substraction, '-');
                        break;
                    case CalculatorOperationType.Dividing:
                        AddNextOperation(CalculatorOperationType.Dividing, '/');
                        break;
                    case CalculatorOperationType.Multiplying:
                        AddNextOperation(CalculatorOperationType.Multiplying, '*');
                        break;
                }
            }
        }

        //получитьПоследнееВычисление
        public static string getTheLastComputation()
        {
            return lastComputation;
        }

        //Разрешен ли ввод?

        private static bool isInputAllowed()
        {
            return (!isBlocked && (doesTheInputContainResOrPrevNum || (InputRestriction == -1 || InputBuffer.Length + 1 <= InputRestriction) || isUnaryOperation));
        }

        //Унарная операция стирания
        private static void EraseUnaryOperation()
        {
            if (OutputBuffer != string.Empty && nextOperationType != CalculatorOperationType.None)
            {

                if (unaryBuffer != string.Empty)
                {
                    OutputBuffer = _outputBuffer.Substring(0, _outputBuffer.Length - (unaryBuffer.Length + 1)) + ' ';
                }
            }
            else OutputBuffer = string.Empty;
            isUnaryOperation = false;
            unaryBuffer = string.Empty;
        }
        //Ошибка вызова
        private static void InvokeError()
        {
            prevOperationType = CalculatorOperationType.None;
            nextOperationType = CalculatorOperationType.None;
            doesTheInputContainResOrPrevNum = false;
            isFirstNumValueSet = false;
            isBlocked = true;
            InputBuffer = "Error";
        }


        //Очистить буфер вывода
        private static void ClearOutputBuffer()
        {
            isFirstNumValueSet = false;
            doesTheInputContainConstantValue = false;
            isUnaryOperation = false;
            OutputBuffer = string.Empty;
            unaryBuffer = string.Empty;
            doesTheLastOutputContainsParentheses = false;
            nextOperationType = CalculatorOperationType.None;
            prevOperationType = CalculatorOperationType.None;
        }
        //EventArgs - это класс, дающий возможность передать какую-нибудь дополнительную информацию обработчику
        // InputBufferChanged - изменение входного буфера
        //Invoke в контексте EventHandler в C# — это функция, которая позволяет вызвать событие.
        protected static void OnInputBufferChanged(EventArgs e) => InputBufferChanged?.Invoke(null, e);
        protected static void OnOutputBufferChanged(EventArgs e) => OutputBufferChanged?.Invoke(null, e);
    }
}
