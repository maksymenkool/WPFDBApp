   M S B A M L   `   `   `  ����  WPFDBApp!    WPFDBApp.View.ConnectWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   >d2http://schemas.microsoft.com/expression/blend/2008    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          �Name$
  This��5   	    � Title$ Connect to Server��5   	   $	��320��5	   	   $	��480��6    � 
ResizeMode$ NoResize=�6"   $��Arial)�5
   	   $��12(�6    � Icon$ ../Images/cts.jpg��5   	    � WindowStartupLocation$ CenterScreen=�6"   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��	0 10 10 0q�6    ��5      �� 5      $��Sql Connection��6   $��14(�62   $��DemiBold"�6@   .��5      � $
��0 10q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $��1* �6'    5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   .+�5      �� $��Server name: ��6   $��Center=�61   $��5q�6L   $��0��6W   $��0��6g   � 5       $��1��6   $��0��6'    5!      �� 5"      $��1* �6+   �� 5#      $
��Auto �6+   .+�5%      �� $��5q�6    �� 
IsEditable . ��6*   �� *	Servers+ �� SelectedItem 5&      �� *SelectedServer+	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=� �� Text 5'      �� *EditedServerName+$
 	LostFocus=�5(      �� 5*      $��Authentication:��6   $��0��64   $��2��6D   $��2��6Q   $��3q�6e   .��5+      �� .+�5,      	� -   5         	�Name$ SqlAuthTypeWindows��5,   R   $��Windows Authentication��6&   $��5q�6G    d� 	IsChecked$
 TrueY�6n   	� 5-      -   5        $ SqlAuthTypeSql��5-   N   $��Sql Authentication��6&   $��5q�6C    6f   �� *IsAuth+$	 TwoWay=�� 5.      {�6   ��  �� ElementName$ SqlAuthTypeSql�� �� Path$ 	IsChecked� 5/      �� 50   "   $
��Auto �63   �� 51   "   $��1* �63    53      �� 54   "   $
��Auto �60   �� 55   "   $
��Auto �60   .+�58      �� $��
Username: ��6(   $��0��6:   $��0��6J   $��3q�6W   $��Center=�6b   �� 59      $��1��6&   $��0��66   $��3q�6C   ��6N   �� *
UserName+$
 PropertyChanged=��� 5:      $��
Password: ��6(   $��0��6:   $��1��6J   $��3q�6W   $��Center=�6b   G� 5;      -   5         G�Name$ passwordBox��5;   *   $��1��6?   $��1��6O   $��3q�6\   5=      5>      5?      5A      5B      � 5D   
   $��1��6   $
��0 15q�6    5E      �� 5F      $
��251* �6#   �� 5G      $
��223* �6#   .+�5I      �� -   5         ��Name$ 	escButton��5I      $��Cancel��6(   $��0��6[   $
��Left=�6k   $��5,10,0,0q�6�   $	��Top=�6�   $��80��6�    �� IsCancel . $��20��6�   ��69   �� *CancelCommand+6�   �� 5J      -   5        $ ConnectButton��5J      $��Connect��6,   $��1��6�   $��Right=�6�   $��	0,10,15,0q�6�   $	��Top=�6�    �� 	IsDefault . $��80��6�   $��20��6	  ��6>   �� *ServerConnectCommand+��6g   �� $
 This��6	  5K      5L      5M      