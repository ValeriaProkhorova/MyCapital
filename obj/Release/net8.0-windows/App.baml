   M S B A M L   `   `   `  ���� D  @MyCapital, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MyCapital	MyCapital  U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf     MyCapital.App   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     )localclr-namespace:MyCapital     X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf    �� 	Resources     ColorOne �� 
StartPoint �� EndPoint � Color � Offset  ColorElement  cs_ButtonAndText �� Color  cs_BorderBrush  cs_Watermark  cs_TextColor  cs_RBBackground  
cs_Trigger  FY 	 ib_CalendarAct �� ImageSource 
 
menuButton �� 
TargetType �� Property	 �� Value
 �� 
TargetType �� Triggers P� Property P� Value  menuButtonIcon!  FontAwesome.Sharp.IconImage�  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]] �� Path �� RelativeSource �� AncestorType  menuButonText �� Mode  topmenuButton �� CornerRadius  	titleText  BackgroundFill  LiveCharts.Wpf.LineSeries  LiveCharts.Wpf.Series  LiveCharts.Wpf.Axis �� Triggers �� 
TargetName  orangeButtonSigns  orangeButton �� BasedOn  darkGreyButton  
greyButton  
blueButton  ScrollBarLineButtonStyle � Points " ScrollBarLineButtonBottomStyle  ScrollBarPageButtonStyle  ScrollBarThumbStyle  VerticalScrollBar � RowDefinitions ]� DecreaseRepeatButton ]� Thumb ]� IncreaseRepeatButton��%�  &	        &	 X     &	 �     &	 �     &	 �     &	 �     &	     &	     &	 k    &		 �    &	
 �    7  7  7  &	 �    &	     7  &	 �    7  7  &	 B    7  7  &	 �    '  2    7  '  �    7  '�� 4    &	 E
    &	 �    &	 �    7��7  &	     7��&	 \    7��&	 �    &	 3    &	 �    &	 -    &	 �    7  7  7  7  7  '��     7  �� $	 0,0&�$	 0,5&�.��� $ #c6f4d1��$	 0.0Q�� $ 	#FFF5ECEB��$	 0.5Q��� $ 0.5,0&�$ 0.5,1&�.��� $ #a9efbb��$	 0.4Q�� $ #70e48d��$	 1.2Q��� $ #005538���� $ 	#FF97CEDA���� $ #bdb2b5���� $ 	#FF333333���� $ #3eff92���� $ 0,0.5&�$ 1,0.5&�.��� $ #00ffa9��$ 0Q�� $ #33ffba��$ 3Q�*�/Font/#Spectral)��� $ Icons/calen.png���� " ��.���  � ��
	 ���� ��  � ��8	   ��  � ��$	 0 0 0 5q���  � ��$	 45����  �@�FocusVisualStyle	 Z� ��  �@�Tag$	 #005538����  � ��	 �� "
 ��.R��� #��z��$��4 0 0 0q�
������ ._��� $��Stretch=�$��Center=� P�  �@J�IsMouseOver$
 True��. ���  � ��	 �#ffffff��  �@�Tag$	 #ffffff����  � ��8	  ��  � ��	 �� "
 ��.R��� #��z��$��4 0 0 0q�8�� ._��� $��Stretch=�$��Center=��� "  .���  � ��$	 23����  � ��$	 23����  �@ 
Foreground	 �� $	 Tag� �� *FindAncestor+" ���� " ��.���  � ��$	 13(���  � ��8	   ��  � ��$	 Center=���  � ��	 �� $ 
Foreground� �� $ FindAncestor=�" ���� " ��.���  � ��
	 ���� ��  � ��8	   ��  � ��$	 Black"���  � ��8	  ��  � ��$	 5 0 0 0q���  � ��$	 30����  � ��$	 80����  �@�FocusVisualStyle	 Z� ��  � ��$	 0q���  � ��	 �� "
 ��.R��� #��z��$ 15��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True��. ���  � ��	 �#00ffa9��  � ��	 �#ffffff�� " ��.���  � ��8	   ��  � ��$	 18(���  � ��$	 Black"���  � ��8	  ��  � ��$	 10 0 0 0q���  � ��$	 Center=��� $ 0.5,0&�$ 0.5,1&�.��� $ #b397e2��$	 0.4Q�� $ #6a6ae4��$	 1.2Q��� "  .���  �@ Stroke	 �#6a6ae4��  �@ Fill8	   ��  �@ StrokeThickness$	 0Q���  �@ PointGeometrySize$	 0Q��� "  .���  �@ FontSize$	 12Q���  �@ 
FontFamily$	 Calibri)���  �@ 
Foreground8	   �� " ��.���  � ��$	 50����  � ��$	 50����  � ��$	 25(���  � ��$
	 Bold"���  � ��
	 �������  � ��$	 
3, 3, 3, 3q���  �@�Cursor$
	 Hand����  � ��$	 False����  � |�$	 False����  � ��	 �� "
 ��.R�� .+��� $��backgroundElement��$��0q�$ 25��#��z���� $��Center=�$��Center=�#��z��#��z��#��z��#��z�� P�  �@J�IsMouseOver$
 True��. ��� $ backgroundElement��	 ��  �� *TemplatedParent+$ BorderBrush� � ��-   �� " ��.���  � ��	 �#FE9E09��  � ��	 �#FBC78E��  � ��$	 50����  � ��$	 50����  � ��$	 25(���  � ��$
	 Bold"���  � ��
	 �������  � ��$	 
3, 3, 3, 3q���  �@�Cursor$
	 Hand����  � ��$	 False����  � |�$	 False��-   ��  � ��	 �� "
 ��.R�� .+��� $��backgroundElement��$��0q�$ 25��#��z���� $��Center=�$��Center=�#��z��#��z��#��z��#��z�� P�  �@J�IsMouseOver$
 True��. ��� $ backgroundElement��	 ��  �� *TemplatedParent+$ BorderBrush� � ���� " ��.���  � ��	 �#FE9E09��  � ��	 �#FBC78E��  � ��$	 50����  � ��$	 50����  � ��$	 25(���  � ��$
	 Bold"���  � ��
	 �������  � ��$	 
3, 3, 3, 3q���  �@�Cursor$
	 Hand����  � ��$	 False����  � |�$	 False����  � ��	 �� "
 ��.R�� .+��� $��backgroundElement��$��0q�$ 25��#��z���� $��Center=�$��Center=�#��z��#��z��#��z��#��z�� P�  �@J�IsMouseOver$
 True��. ��� $ backgroundElement��	 ��  �� *TemplatedParent+$ BorderBrush� � ���� 8   " ��.���  � ��8	  ��  � ��	 �#737373�� 8   " ��.���  � ��	 �#a5a5a5��  � ��	 �#D9D9D7�� 8   " ��.���  � ��	 �#4285F4��  � ��	 �#95b6ed�� " ��.���  � |�$	 False����  � ��	 �� "
 ��.R��� $��18��.+�� 
����ح�' �   ��- ��� ��� ��� @T� ��- �� " ��.���  � |�$	 False����  � ��	 �� "
 ��.R��� $��18��.+�� 
����ح�' �   ��- ��- @T� ��� ��� ��- �� " ��.���  � ��$	 False����  � |�$	 False����  � ��	 �� "
 ��.R��� 
������ �� " n�.���  � ��$	 False����  � |�$	 False����  � ��$	 1,0,1,0q���  � ��	 �� "
 n�.R��� 
����ح�$��2q��� "
 ��.R��  �� $��18���� $��* ��� $��18��.+��� $��0��$��18��8��  $��ScrollBar.LineUpCommand��]� $��
PART_Track��$��1��$
��True�� �� $��ScrollBar.PageUpCommand��8��  n� 8��  �� $��ScrollBar.PageDownCommand��8�� �� $��3��$��18��8�� $��ScrollBar.LineDownCommand���� " ��.���  � ��8	   