   M S B A M L   `   `   `  ���� D  @MyCapital, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MyCapital	MyCapital      MyCapital.EnterIncome   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:MyCapital     X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp    � Title$  EnterIncome��$	��550��$	��500�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � AllowsTransparency$
 True��
������  � 	Resources  �� Source�� $&  /Style/EnterTheExpenseStyle.xaml?�%    .��� .+���    ColorElement#��[   �� CornerRadius$ 20��$��3q����#80ffad��   ColorOne#��[ $ 20��$��200 60 200 60q��  � ColumnDefinitions �� $��* ��� $
��Auto ��� $��* � � RowDefinitions �� $��50 ��� $��* �.+��� 	 �� Orientation$	 
Horizontal=�$��Right=�$��2��.+��� -   
 ��Name$
 BtnMinimize��  ButtonExpenses#��[ $��0 5 5 0q�.��!  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$ WindowMinimize=�  ExpensesButtonIcon#��[ �� -   $
 BtnMaximize��$��0 5 5 0q�#��[ .��  $ WindowMaximize=�#��[ �� -   $
 BtnClose��$��0 5 5 0q�#��[ .��  $ WindowClose=�#��[ �� $��1��$��1��$��0 30 0 0q�.+��� $!��Введите данные��  FY#��[ $��15(�$��0q�$��-1.5 10 0 5q�$��Center=�  cs_ButtonAndText#��[ (  "System.Windows.Controls.DatePicker  -     Name$ 
DatePicker��  DatePickerStyle#��[ 
 � Tag$ Дата��  DataPickerCalendarStyle   CalendarStyle# [ �� -    ��Name$ CBScore��  ComboBoxStyle1#���  $ На счет���� -   $ 
CBCategory��#���  $ Категория���� -   $��SummTextBox�� 	 tbox_PeopleIconPlaceholder#��[	 ���� *
SummIncome+�� -    ��Name$ CommentTextBox�� 
 tbox_Comment#��[
 $ Комментарий�� �� AcceptsReturn$
 True�� �� TextWrapping$
 Wrap=��� -	   $
 ButtonAddUpdate��$!��Добавить доход��  bt_ButtonStyle#��[ 