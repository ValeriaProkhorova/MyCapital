   M S B A M L   `   `   `  ���� D  @MyCapital, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MyCapital	MyCapital  U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf A%clr-namespace:MyCapital.User_ControlsMyCapital.User_Controls      MyCapital.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:MyCapital     Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  .uc%clr-namespace:MyCapital.User_Controls    � Title$   ��$	��740��$
��1360�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � AllowsTransparency$
 True��   ColorOne#��[  .���  � ColumnDefinitions �� $	��250 ��� $��* �.+���  �� CornerRadius$ 	0 10 0 10��  ColorElement#��[ $��0q�._��� .+���  �� Orientation$ 
Horizontal=�$��Center=�$��0 35 0 0q�.+��� $��40��$��40��  cs_ButtonAndText#��[ #��[ $��4q�._��� $��К��#��[ $��22(�$��Black"� �� TextAlignment$ Center=�$��Center=�$��Cricket)��� $��10 0 0 0q�$��Center=�.+��� $��Капитал��$��15(�$��0 0 0 5q�#��[ $��Black"�  FY#��[ �� $-��'Планирование бюджета��$��11(�$��0 0 0 5q�#��[ #��[ �� $	��0.8��$��20,30q����#00aa71�� -     
menuButton#��[ .���� $ 
Horizontal=�.+�!  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$ ShoppingCart=�  menuButtonIcon#��[ �� $��Расходы��  menuButonText#��[ �� -   #��[ .���� $ 
Horizontal=�.+�  $ Coins=�#��[ �� $��Доходы��#��[ �� -   #��[ .���� $ 
Horizontal=�.+�  $ CircleDollarToSlot=�#��[ �� $��Капитал��#��[ �� -   #��[ .���� $ 
Horizontal=�.+�  $ Calendar=�#��[ �� $��Планирование��#��[ �� -   #��[ .���� $ 
Horizontal=�.+�  $ 	Hourglass=�#��[ �� $��Накопления��#��[ �� -   #��[ .���� $ 
Horizontal=�.+�  $
 List=�#��[ �� $��Категории��#��[ �� -   #��[ .���� $ 
Horizontal=�.+�  $ ChartColumn=�#��[ �� $)��#Диаграмма Расходов��#��[ �� -   #��[ .���� $ 
Horizontal=�.+�  $ 	ChartLine=�#��[ �� $'��!Диаграмма Доходов��#��[ �� -	   #��[ .���� $ 
Horizontal=�.+�  $ PowerOff=�#��[ �� $��Закрыть��#��[ �� -
   $��1��$ 	0 10 10 0��#��[  ._�� 	 � RowDefinitions	 �� $
��auto ��� $	��200 ��� $��* �.+��� $ 
Horizontal=�$��Bottom=�$��Right=�$��	0 0 30 20q�.+��� -   $��День��  topmenuButton#��[ �� -   $��Неделя��#��[ �� -   $��
Месяц��#��[ �� -   $��Год��#��[ �� $3��-Общие сведения о бюджете��$��26(����#005538$
��Left=�$��
50 35 0 20q�$��Black"�#��[ � $��1��$��50 20 15 40q� �� $��* ��� $��* ��� $��* �.+�&    MyCapital.User_Controls.InfoCard  -   
  Name$
 InfoCardIncome��$��0��   Title$ Доходы��   Icon$ Ruble=�   Background1$ #827bff��   Background2$ #d9b5ff��   EllipseBackground1$ #e4bbff��   EllipseBackground2$ #e4bbff��  -   $
 InfoCardExpenses��$��1��$ Расходы��$ ShoppingCart=�$ #fd8a87��$ #f3ab92��$ #fdb5a8��$ #fdb89b��  -   $
 InfoCardScore��$��2��$ Капитал��$ Coins=�$ #fc84cd��$ #fc8fae��$ #fc8fae��$ #ffabe6��� $��2�� �� $��* ��� $��* �.+��� $ 20�� �� Padding$ 0q�#��[ $��
50 0 10 40q�._�� 	 �� $
��auto ��� $��* �.+��� $ 
Horizontal=�$��0 0 0 26q�.+��� $)��#Диаграмма Расходов��  	titleText#��[ #  LiveCharts.Wpf.CartesianChart  -     Name$ cartesianChart��$��1��$��0 0 0 25q�#��[ $	��540��&   LiveCharts.Wpf.Charts.Base.Chart   DataTooltip #  LiveCharts.Wpf.DefaultTooltip  ���#24336a���#d5e2ff   AxisX   LiveCharts.Wpf.Axis     MinValue$ 0Q�   MaxValue$ 7Q�   
ShowLabels$
 True��   Labels �� *Labels+   	Separator   LiveCharts.Wpf.Separator     StrokeThickness$ 0Q�   AxisY   $ 0Q�$ 10000Q�$
 True��   $ 0Q�   Series 	  LiveCharts.Wpf.LineSeries	  
  LiveCharts.Wpf.Series 
  Stroke �#ada4fd 
  StrokeThickness$ 2Q� 	  PointGeometrySize$ 0Q�  
  Values  �� *SeriesCollection+�� $��1��$ 20��$
 0 25q�#��[ $��	15 0 47 0q�._�]� .+�� $��5, 0, 5, 15q� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* �	 �� $��2* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+�� $��0��$��0��$��6��$��5q�	 �� $��* ��� $��2* �.+��� ���#a5a5a5$��0��$��2��$��3q�$ 25���� -   ! ��Name$! tbOut��$��40(�
�������$��
20 0 20 20q�$��1��$ Right=����� " �� Path$$" (local:Calculator.InputBuffer)��� $��20(����#A5A5A5$��
27 10 27 0q�$��0��$ Right=����� $%" (local:Calculator.OutputBuffer)��� $��0��$��1�� 	 
greyButton#��[	 $	��⟵���� $��0��$��2�� 
 darkGreyButton#��[
 $��7���� $��0��$��3��#��[
 $��4���� $��0��$��4��#��[
 $��1���� $��0��$��5��$��2��#��[
 $��0���� $��1��$��1��#��[	 $��CE���� $��1��$��2��#��[
 $��8���� $��1��$��3��#��[
 $��5���� $��1��$��4��#��[
 $��2���� $��2��$��1��#��[	 $��C���� $��2��$��2��#��[
 $��9���� $��2��$��3��#��[
 $��6���� $��2��$��4��#��[
 $��3���� $��2��$��5��#��[
 $��.���� $��4��$��2��$��2��$��=��# �� 	IsDefault$
# True��  orangeButtonSigns#��[ �� $��3��$��1��$��/��#��[ �� $��3��$��2��$��*��#��[ �� $��4��$��1��$��-��#��[ �� $��6��$��1��$��+��#��[ �� -   $��3��$��3��$��3��$��+ Доход��  orangeButton#��[ 
$ � Tag$ �� $$" (local:Calculator.InputBuffer)��� -   $��3��$��4��$��3��$��+ Расход��#��[ $ �� $$" (local:Calculator.InputBuffer)��� -   $��3��$��5��$��3��$��Накопление��#��[ $ �� $$" (local:Calculator.InputBuffer)�