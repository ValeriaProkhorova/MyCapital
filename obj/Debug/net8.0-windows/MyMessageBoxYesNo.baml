   M S B A M L   `   `   `  ����D  @MyCapital, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MyCapital	MyCapital  5   	   !    MyCapital.MyMessageBoxYesNo   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:MyCapital     5   	   $	��200��5      $	��300��6     � WindowStyle$
  None=�5	       � 
ResizeMode$ NoResize=�6   
������  � WindowStartupLocation$ CenterScreen=�6H    � 	Resources 5
       �� Source�� 5   
   $# /Style/MyMessageBoxStyle.xaml?�6   %    .��5      � .+�5      ��    ColorElement#��[  6    �� CornerRadius$ 1��68   $��2q�6I   ���#80ffad._�5   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6    �� 5      $
��Auto �6    .+�5      �� -   5         ��Name$ 	TitleText��5      $ ��Подтверждение��6+   $��18(�5      $
��Bold"�6$   $
��Left=�5      $��
77,10,0,10q�61     cs_ButtonAndText#��[ 5      $��0��6E     FY#��[ 5      $��1��69   �� 5      -   5        $ MessageText��5      $
��Wrap=�5      $��16,20,20,20q�6*   $��1��6?   #��[ 5      #��[ 5       $��1��69   �� 5"       �� Orientation$ 
Horizontal=�6   $
��Left=�62   $��
33,10,0,10q�5#      $��2��6+   $��1��68   .+�5$      �� -   5        $
��Да��5$      	 �� 	IsDefault$
	 True��6&   $��5q�5%        GreenButtonStyle#��[ 6"   �� 5&      -   5        $��Нет��5&      
 �� IsCancel$

 True��6'   $��5q�5'        RedButtonStyle#��[ 6"   5(      5*      5+      5,      5-      