   M S B A M L   `   `   `  ���� D  @MyCapital, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MyCapital	MyCapital      MyCapital.Categories   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:MyCapital     X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp    � Title$  
Categories��$	��450��$	��520�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � AllowsTransparency$
 True��
������  � 	Resources  �� Source�� $! /Style/CategoriesStyle.xaml?�%    .����    ColorElement#��[   �� CornerRadius$ 20��$��3q����#80ffad._�� -    �Name$ 
Pagination��$	��430��#��[   � RowDefinitions �� $��50 ��� $
��Auto �.+��� 	 �� Orientation$	 
Horizontal=�$��Right=�$��0��.+��� -   
 ��Name$
 AddCategories��  ButtonExpenses#��[ $��0 0 5 0q�.��!  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$ 
PlusSquare=�  ExpensesAddButtonIcon#��[ �� -   $
 BtnMinimize��#��[ $��0 0 5 0q�.��  $ WindowMinimize=�  ExpensesButtonIcon#��[ �� -   $
 BtnMaximize��$��0 0 5 0q�#��[ .��  $ WindowMaximize=�#��[ �� -   $
 BtnClose��$��0 0 5 0q�#��[ .��  $ WindowClose=�#��[ � $��0��$
��Left=� � ColumnDefinitions �� �� �� .+��� -     ButtonCategories#��[ $��Расходы��  cs_ButtonAndText#��[   FY#��[ $��30(�$��Center=�$��0��$	��120���� -   #��[ $��Доходы��#��[ #��[ $��30(�$��Center=�$��1��$	��120���� -   #��[ $��Накопления��#��[ #��[ $��30(�$��Center=�$��2��$	��165��� $��1��.+��� $	��380��.���� -	    ��Name$ ListViewCategories��#��[  $��0q�  �� 
TargetType �� Property �� Value  ColorOne��%�   '��       '�� *     7  �� " ��.���  � y�$ 	Collapsed=��� " ��.���  � ��8   ��  � �� �#E0E0E0��  � ��$ 0q���  � ��$ 50����  � ��$	 500�� �� View �� .P���  �� CellTemplate �� .R��� .+���  ��Name$ MyLabel��$
��Bold"�#��[ $��18(�#��[ $��
100 0 10 0q����� *Title+��  �� .R��� .+��� $
 BtnMaximize��-
   $��0 0 5 0q�#��[ 
 � Tag �� *Id+.��  $	 Pen=�#��[ ��  �� .R��� .+��� $
 BtnMaximize��-   $��0 0 5 0q�#��[  �� *Id+.��  $ Trash=�#��[ 