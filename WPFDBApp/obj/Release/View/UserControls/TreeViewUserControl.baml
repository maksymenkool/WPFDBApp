   M S B A M L   `   `   `  ���� C  ?WPFDBApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:WPFDBApp.ValueConverterWPFDBApp.ValueConverter  4    .WPFDBApp.View.UserControls.TreeViewUserControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    >d2http://schemas.microsoft.com/expression/blend/2008    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     9valConv%clr-namespace:WPFDBApp.ValueConverter     .��]� ��. .+��� $	��Top=�����   �� 
StartPoint$	  0,0&� �� EndPoint$	 0,1&�.���  � Color$ 	#33000000�� � Offset$ 0Q�� $ 	#11000000��$ 1Q�._��� $��Center=�$��4q� �� Orientation$ 
Horizontal=�.+��� $��Search for:��$��13(��� -    ��Name$ searchTextBox��$	��6,0q�$	��150������ *
SearchItem+ �� UpdateSourceTrigger$ PropertyChanged=��� -   $��_Find��$	��8,0q� �� 	IsDefault . ���� *SearchCommand+R� $��0q�$
��5 10q����� *Items+����  �� 
TargetType" Q�.��� 	 �� Property	 �@Q�
IsExpanded
 �� Value
 �� *
IsExpanded+ �� Mode$ TwoWay=��� 	 �@Q�
IsSelected
 �� *
IsSelected+$ TwoWay=��� 	 � ��$
 Normal"��� 	 � ��$
 12(� �� Triggers P�  P� Property �@Q�
IsSelected P� Value . . ��� 	 � ��$

 Bold"��� 	 � ��$
 14(��� 	 �@+�FocusedElement
 ��  �� RelativeSource �� *Self+����  �� ItemsSource �� *
Children+.R��� $ 
Horizontal=�.+��� $��16��$��16��$��3q����� *Type+4   .WPFDBApp.ValueConverter.TreeViewImageConverter   Instance �� 	Converter# Z ��  ��Name$ txtName��$��Center=����� *Name+ � ContextMenu �� .@�m� $��Create SQL . . .�� m� Command �� *CreateSQLCommand+�� m� $��Refresh�� �� *RefreshCommand+�� $��Center=�$�� (expanding...)��y���  �� Path$ IsBusy�$ OneWay=�$ PropertyChanged=�:   4WPFDBApp.ValueConverter.BooleanToVisibilityConverter   	IsVisible# Z  �� Triggers P�  �@J�IsMouseOver . . ���  �� 
TargetName$ txtName��	 � ��$

 Bold"��� $ txtName��	 � ��$
 14(�