   M S B A M L   `   `   `  ����D  @MyCapital, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MyCapital	MyCapital  5   	       MyCapital.EnterIncome   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:MyCapital     5   	   X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  5        � Title$  EnterIncome��5	   	   $	��550��6   $	��500��6+    � WindowStartupLocation$ CenterScreen=�67    � WindowStyle$
 None=�6\    � AllowsTransparency$
 True��6o   
������  � 	Resources 5       �� Source�� 5   
   $&  /Style/EnterTheExpenseStyle.xaml?�6   %    .��5      � .+�5   
   ��    ColorElement#��[  6    �� CornerRadius$ 20��6<   $��3q�6N   ���#80ffad6b   �� 5   
     ColorOne#��[ 6   $ 20��68   $��200 60 200 60q�6J   � 5   
    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��Auto �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $��50 �6    �� 5      $��* �6    .+�5       �� 	 �� Orientation$	 
Horizontal=�6   $��Right=�62   $��2��6N   .+�5$      �� -   5        
 ��Name$
 BtnMinimize��5$   R     ButtonExpenses#��[ 6   $��0 5 5 0q�6A   .��5%      !  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$ WindowMinimize=�6#     ExpensesButtonIcon#��[ 69   5&      �� 5)      -   5        $
 BtnMaximize��5)      $��0 5 5 0q�6.   #��[ 6?   .��5*        $ WindowMaximize=�6#   #��[ 69   5+      �� 5/      -   5        $
 BtnClose��5/      $��0 5 5 0q�6+   #��[ 6<   .��50        $ WindowClose=�6#   #��[ 66   51      52      �� 55      $��1��6   $��1��6&   $��0 30 0 0q�66   .+�58      �� $!��Введите данные��6     FY#��[ 61   $��15(�6R   $��0q�6`   $��-1.5 10 0 5q�6l   $��Center=�6�     cs_ButtonAndText#��[ 59      (  "System.Windows.Controls.DatePicker  5<      -   5          Name$ 
DatePicker��5=   D     DatePickerStyle#��[ 5<      
 � Tag$ Дата��6F     DataPickerCalendarStyle   CalendarStyle# [ 5=      5>      �� 5A      -   5         ��Name$ CBScore��5A   R     ComboBoxStyle1#���  6   $ На счет��6D   5D      �� 5G      -   5        $ 
CBCategory��5G   T   #���  6   $ Категория��6D   5I      �� 5N      -   5        $��SummTextBox��5N   j    	 tbox_PeopleIconPlaceholder#��[	 6   ��6N   �� *
SummIncome+6j   �� 5Q      -   5         ��Name$ CommentTextBox��5Q   {    
 tbox_Comment#��[
 6   $ Комментарий��6@    �� AcceptsReturn$
 True��6R    �� TextWrapping$
 Wrap=�6g   6{   �� 5T      -	   5        $
 ButtonAddUpdate��5T   Z   $!��Добавить доход��6     bt_ButtonStyle#��[ 62   6s   5W      5Y      5[      5\      