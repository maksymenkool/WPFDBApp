   M S B A M L   `   `   `  ����   WPFDBApp!    WPFDBApp.View.ConnectWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    >d2http://schemas.microsoft.com/expression/blend/2008    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     �Name$
  This�� � Title$ Connect to Server��$	��320��$	��480�� � 
ResizeMode$ NoResize=�$��Arial)�$��12(� � Icon$ ../Images/cts.jpg�� � WindowStartupLocation$ CenterScreen=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $
��Auto �.+��� $��0��$��	0 10 10 0q����� $��Sql Connection��$��14(�$��DemiBold"�.��� $
��0 10q� � ColumnDefinitions �� $
��Auto ��� $��1* � �� $
��Auto ��� $
��Auto �.+��� $��Server name: ��$��Center=�$��5q�$��0��$��0��� $��1��$��0�� �� $��1* ��� $
��Auto �.+��� $��5q� �� 
IsEditable . ���� *	Servers+ �� SelectedItem �� *SelectedServer+	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=� �� Text �� *EditedServerName+$
 	LostFocus=��� $��Authentication:��$��0��$��2��$��2��$��3q�.���� .+�	� -    	�Name$ SqlAuthTypeWindows��$��Windows Authentication��$��5q� d� 	IsChecked$
 TrueY�	� -   $ SqlAuthTypeSql��$��Sql Authentication��$��5q� �� *IsAuth+$	 TwoWay=�� {���  �� ElementName$ SqlAuthTypeSql�� �� Path$ 	IsChecked� �� $
��Auto ��� $��1* � �� $
��Auto ��� $
��Auto �.+��� $��
Username: ��$��0��$��0��$��3q�$��Center=��� $��1��$��0��$��3q����� *
UserName+$
 PropertyChanged=��� $��
Password: ��$��0��$��1��$��3q�$��Center=�G� -    G�Name$ passwordBox��$��1��$��1��$��3q�� $��1��$
��0 15q� �� $
��251* ��� $
��223* �.+��� -    ��Name$ 	escButton��$��Cancel��$��0��$
��Left=�$��5,10,0,0q�$	��Top=�$��80�� �� IsCancel . $��20������ *CancelCommand+�� -   $ ConnectButton��$��Connect��$��1��$��Right=�$��	0,10,15,0q�$	��Top=� �� 	IsDefault . $��80��$��20������ *ServerConnectCommand+���� $
 This��