   M S B A M L   `   `   `  ���� W  SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089X TFontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f   ButtonExpenses  �� 
TargetType �� Property �� Value  cs_ButtonAndText �� 
TargetType �� CornerRadius �� Triggers P� Property P� Value  ExpensesButtonIcon!   FontAwesome.Sharp.IconImage�  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.6.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]  ComboBoxToggleButton � ColumnDefinitions	 ��Name  cs_BorderBrush
 J� SnapsToDevicePixels F�Name �� Stretch �� Triggers �� 
TargetName  ComboBoxTextBox  ComboBoxStyle1 d�Name �� 	ClickMode d� 	IsChecked �� Mode �� RelativeSource �� Mode J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate ��Name  cs_Watermark
 � Tag ��Name �Name �� IsDropDownOpen � AllowsTransparency �Name �� MaxDropDownHeight  ColorElement 	 ColorOne  � OverridesDefaultStyle 
 cs_TextColor! K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation"   DirectionalNavigation# ]� 
Conditions$ �� Property% �� Value& P� 
SourceName  tbox_PeopleIconPlaceholder' ��Name  tbox_Comment  bt_ButtonStyle  cs_RBBackground  tbox_TextPlaceholder  bt_CalendarStyle  ib_CalendarAct  DataPickerCalendarStyle&   System.Windows.Controls.Calendar( ��Name5  /System.Windows.Controls.Primitives.CalendarItem)  Name*   CalendarItemStyle  DatePickerStyle(  "System.Windows.Controls.DatePicker:  4System.Windows.Controls.Primitives.DatePickerTextBox+  Name, ��Name- � 	StaysOpen. � HorizontalOffset/ � PlacementTarget0 �� ElementName�� H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation       :x,http://schemas.microsoft.com/winfx/2006/xaml      Ffa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  %�  &	        7  &	 �    7  &	 �    7  7  7  7  7  &	 �    &	 #    7  7  7  7  7  7 	 7 
 &	 [	    7  7  7  7  7  7  &	 �    7  7  7  7  7  7  &	 U    7  7  &	 �    7  7  7  7  7  &	 �    7  7  7  &	 B    7  7  &	 .    7  7  7  7  7  7  7  �� "  ��.���  � ��
 ���� ��  � ��8   ��  � ��$ Black"���  � ��$ Cricket)���  � ��$ 5 0 0 0q���  � ��$ 25����  � ��$ 25����  �@�FocusVisualStyle Z� ��  � ��$ 0q���  � �� �� " ��.R��� #��z��$ 15��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True��. ���  � �� �#00ffa9��  � �� �#ffffff�� "    .���  � ��$ 23����  � ��$ 23����  �@ 
Foreground8   �� " d�.R��  �� �� $��30 �.+��� $	 Border��$��2��$ 5��8��  $��2q�$

 True���� $��1��._�F� $ Arrow��F����        ���( ��( јaQ     јaQ P����̰( �H �    P����        8�� $
 Fill=�$��7��$��10��$��Center=�$��Center=� P�  �@J�IsMouseOver$
 True��. ��� 8  $ Border�� � ���� 8  $ Arrow�� � ��P�  �@J�IsKeyboardFocused$
 True��. ��� 8  $ Border�� � ���� $ Border�� � ��$ 0.5 0.5 0.5 3q��� " ��.R��� $	 PART_ContentHost��$|�False��#��z���� "  ��.���  � ��$ 35����  � ��$	 250����  �@�Cursor$
 Hand����  � ��$ Roboto)���  � ��$ 11(���  � ��8   ��  � ��$ Center=���  � ��$ 0 10 0 0q���  � �� �� " ��.R�� .+�d� $ ToggleButton��8�� $��2��$|�False��$ Press=� �� *IsDropDownOpen+$ TwoWay=� �� $ TemplatedParent=��� $��ContentSite��$ False��#��z #��z #��z��$��	11 0 25 0q�$��Center=�$
��Left=�$
{�True���� $ 	Watermark��$��Center=�$
��Left=�8�� $y�	Collapsed=�$��8 0 0 0q�#��z $��Roboto)�$��11(��� $ PART_EditableTextBox��8�� $
��Left=�$��Center=�$
|�True��$y�Hidden=�� $ Popup��$��Bottom=�#��z $
 True��$|�False��$��Slide=�.��� $ DropDown��$

 True��#��z #��z .+��� $	 DropDownBorder��$��1q�8�� 8�� �� $��4 6 4 6q�$

 True��$
  True��8�� $��Roboto)�$��11(��� $
! True��$" 	Contained=� ]� # �� $ �@��Text$%  ��./��� $ 	Watermark�� � y�$ Visible=�P�  �@��HasItems$ False��. ��� $ DropDownBorder�� � ��$ 95��P�  �@��
IsGrouping$
 True��. ���  � ��$ False��P� $& Popup�� �@�AllowsTransparency$
 True��. ��� $ DropDownBorder�� �@��CornerRadius$ 4���� $ DropDownBorder�� � ��$ 0 2 0 0q��� "  ��.���  � ��$ 11(���  � ��$ Roboto)���  � ��$ 35����  � ��$	 250����  � ��8   ��  � ��8  ��  � ��$ 0 5 5 4q���  � ��$ 0 10 0 0q���  � ��$ 1����  �@�Cursor$
 Hand����  � ��$ Center=���  � ��$ Center=���  � �� �� " ��.R��� $	 	TxtBorder��8�� $��2q�$ 5��$

 True��._�� #��z�� �� $��0 ��� $��* �.+��� $' PART_ContentHost��$|�False��$��1��$��5 0 0 0q�$��Hidden=�$��Hidden=��� $ 	Watermark��$��Center=�$
��Left=�8�� $y�	Collapsed=�$��5 0 20 0q�#��z��$��1�� P�  �@J�IsMouseOver$
 True��. ��� 8  $ 	TxtBorder�� � ��P�  �@J�IsKeyboardFocused$
 True��. ��� 8  $ 	TxtBorder�� � ���� $ 	TxtBorder�� � ��$ 0.5 0.5 0.5 3q�]� # �� $ � ��$%  ��./��� $ 	Watermark�� � y�$ Visible=��� "  ��.���  � ��$ 11(���  � ��$ Roboto)���  � ��$ 90����  � ��$	 250����  � ��8   ��  � ��8  ��  � ��$ 0 5 5 4q���  � ��$ 0 10 0 0q���  � ��$ 1����  �@�Cursor$
 Hand����  � ��$ Center=���  � ��$	 Top=���  � �� �� " ��.R��� $	 	TxtBorder��8�� $��2q�$ 5��$

 True��._�� #��z�� �� $��0 ��� $��* �.+��� $' PART_ContentHost��$|�False��$��1��$��5 0 0 0q�$��Hidden=�$��Hidden=��� $ 	Watermark��$	��Top=�$
��Left=�8�� $y�	Collapsed=�$��5 0 20 0q�#��z $��1�� P�  �@J�IsMouseOver$
 True��. ��� 8  $ 	TxtBorder�� � ��P�  �@J�IsKeyboardFocused$
 True��. ��� 8  $ 	TxtBorder�� � ���� $ 	TxtBorder�� � ��$ 0.5 0.5 0.5 3q�]� # �� $ � ��$%  ��./��� $ 	Watermark�� � y�$ Visible=��� "  ��.���  � ��$ 30����  � ��$	 100����  � ��8   ��  � ��$ 	33 13 0 0q���  �@�Cursor$
 Hand����  � ��$ Roboto)���  � ��$ 11(���  �@J�SnapsToDevicePixels$
 True����  � �� �� " ��.R�� $
 grid��.+��� $	 border��$ 10��8�� ._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True��. ��� $ border�� � �� �#00ffa9P�  � ��$
 True��. ��� $ border�� � �� �#00ffa9�� $ border�� � ��$ 2q���  � �� �#00ffa9�� "  ��.���  � ��$ 11(���  � ��$ Roboto)���  � ��$ 35����  � ��$	 250����  � ��8   ��  � ��
 �������  � ��$ 	5 4.5 5 4q���  � ��$ 1����  �@�Cursor$
 Hand����  � ��$ Center=���  � ��$ Center=���  � �� �� " ��.R��� $	 	TxtBorder��8�� $��2q�$ 5��$

 True��._�� #��z��.+��� $' PART_ContentHost��$|�False��$��1��$��0q�$��Hidden=�$��Hidden=��� $ 	Watermark��$��Center=�$
��Left=�8�� $y�	Collapsed=�$��3 0 20 0q�#��z $��1�� P�  �@J�IsMouseOver$
 True��. ��� 8  $ 	TxtBorder�� � ��P�  �@J�IsKeyboardFocused$
 True��. ��� 8  $ 	TxtBorder�� � ���� $ 	TxtBorder�� � ��$ 0.5 0.5 0.5 3q�]� # �� $ � ��$%  ��./��� $ 	Watermark�� � y�$ Visible=��� "  ��.���  � ��$ 25����  � ��$ 25����  � ��$ Right=���  � ��$ Center=���  �@J�SnapsToDevicePixels$
 True����  �@�Cursor$
 Hand����  � �� �� " ��.R��� $	 border��$��0q�8��   P�  �@J�IsMouseOver$
 True��. ��� $ border��8   � ��P�  � ��$
 True��. ��� $ border��8   � ���� "   .���  � ��8   ��  � �� �#70ff66��  � ��8  ��  � ��$ 2q���  � �� �� "  .R��� $( 	PART_Root��$��Center=�.+�  $) PART_CalendarItem��#��z��#��z��#��z��#��z* $��0 5 0 0q��� "   .���  � ��$ 0 10 0 0q���  � ��$ Center=���  � �� �� "  .R�� .+��� $	 Border��$��2��$��35��$	��250��$��2q�8��  $ 5��
������ ._��  �� $��* ��� $��35 �.+�  $+ PART_TextBox��8�� $��0q�$��0��$��Roboto)�$��11(�8�� $��Center=�$��13 4 0 0q��� $, PART_Button��8�� $��1��� $ 
PART_Popup��$
 True��$��Bottom=�$- False��$. 17��/ �� $0 PART_TextBox���� $ 	Watermark��$��Center=�$
��Left=�8�� $y�	Collapsed=�$��	11 0 20 0q�#��z $��Roboto)�$��11(� P�  �@J�IsMouseOver$
 True��. ��� 8  $ Border�� � ��P�  �@J�IsKeyboardFocused$
 True��. ��� 8  $ Border�� � ���� $ Border�� � ��$ 0.5 0.5 0.5 3q�]� # �� $ �@ Text$%  ��./��� $ 	Watermark�� � y�$ Visible=�