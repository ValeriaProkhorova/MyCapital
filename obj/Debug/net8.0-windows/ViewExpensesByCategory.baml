   M S B A M L   `   `   `  ����D  @MyCapital, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MyCapital	MyCapital  5   	   &     MyCapital.ViewExpensesByCategory   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:MyCapital     5   	   X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  5   	     � Title$  ViewExpensesByCategory��5	   	   $	��450��6(   $	��500��65    � WindowStartupLocation$ CenterScreen=�6A    � WindowStyle$
 None=�6f    � AllowsTransparency$
 True��6y   
������  � 	Resources 5       �� Source�� 5   
   $- '/Style/ViewExpensesByCategoryStyle.xaml?�6   %    .��5      �� -   5           ColorElement#��[  5       �� CornerRadius$ 20��68   $��3q�6J   ���#80ffad._�5   
   � -   5         �Name$ 
Pagination��5      $	��430��6   #��[  62    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      .+�5   
   �� $!��Общие сведения��6     cs_ButtonAndText#��[ 6)     FY#��[ 6X   $��30(�6y   $��Center=�5   	   $��0��6&   �� 5      $��0��6   	 �� Orientation$	 
Horizontal=�6&   $��Right=�6?   .+�5      �� -   5          ButtonExpenses#��[ 5      .��5       !  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]
   Icon$
 Calendar=�6     ExpensesButtonIcon#��[ 6/   5!      �� 5#      -   5        #��[ 5#      .��5$        $
 Download=�6#   #��[ 63   5%      �� 5&      -   5        #��[ 5&      .��5'        $
 WindowClose=�6#   #��[ 66   5(      5)      � 5.      $��1��6   .+�5/      �� $	��380��6    .��50      �� -   5         ��Name$ ListViewExpensesCategory��50   J   #��[  6   $��0q�6l    52       �� 
TargetType �� Property �� Value��%Y   '��       �� 53      " ��6$   .�54   "   ��  � y�$ 	Collapsed=�6?   55      56       �� View 57      �� 59      .P�5;   "   ��  �� CellTemplate 5<   &   �� 5=   *   .R�5>   .   �� .+�5?   2   �� -   5          ButtonListView#��[ 5?   Z   ��6:   �� *
Categories+
 � Tag 5@   :   �� *
Categories+6U   5A   /   5B   +   5D   #   �� 5F   "    5G   &   �� 5H   *   .R�5I   .   �� .+�5J   2   �� $
��Bold"�6R   #��[ 6d   $��18(�5K      #��[ 6!   $��
100 0 10 0q�6B   ��5J   9   �� *Summ+5K   B   5L   /   5M   +   5O   #   5Q      5S      5T      5U      5W      5X      5Y      