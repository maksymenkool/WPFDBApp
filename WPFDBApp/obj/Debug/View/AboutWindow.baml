   M S B A M L   `   `   `  ����C  ?WPFDBApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:WPFDBApp.ViewWPFDBApp.View  5   	   9!clr-namespace:WPFDBApp.PropertiesWPFDBApp.Properties  5   	       WPFDBApp.View.AboutWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:WPFDBApp.View     5   	   /p!clr-namespace:WPFDBApp.Properties     5   	     � 
ResizeMode$  NoResize=�5       � Title$ About...��5	   	   $	��610��6   $	��524��6'    � WindowStartupLocation$ CenterScreen=�63   .��5
      � .+�5   
   ]� $��5q�6   .+�5      � $��Bottom=�6    � ColumnDefinitions 5      �� 5      �� 5      .+�5      �� -   5        $��DoNotShowCheckBox��5      $��0��64   $
��Left=�6D   $��
20 15 0 15q�6_    d� 	IsChecked$ FalseY�5      {�5      �� "  WPFDBApp.Properties.Settings   Default �� Source# Z  �� Path$ AboutCheckBoxIsEnabled�.��5   *   Do not show again6=   �� 5      $��1��6   $��Right=�6)   $��Ok��6E   $��60��6R   $��20��6]   $��
0 15 20 15q�6i    �� IsCancel . 6}   5      �� 5      $	��Top=�6   $��Center=�6+   $��Center=�6M   $��0q�5      $��52��6*   $��16(�66   $
��Bold"�6D   .��6V   Hello, Friend!5      �� 5      $��1q�6   
�������$��Center=�6<   $��Center=�6Y   ._�5      �� $	��458��6   $	��436��6+   .��5      �� $	��Top=�6    $��5q�65   	 �� Padding$	 5q�6@   $��14(�6L   <:This application allows you to retrieve database metadata.�� 5   T   75Automatically creates a script based on the metadata.�� 5   O   �� 6[   �� 5       $
��Bold"�6   .�60   To get started,6A    you need to �� 6S   $
��Bold"�6W   .�6i   	connect6r    to the server�� 6�   �� 5"      
 �� Orientation$
 
Horizontal=�6%   $	��420��6>   $	��255��6J   .+�5#      �� $��0 7 15 5q�6!   $��1q�63   
����  �$��Center=�6Z   $��Center=�6w   ._�5$      �� $��../Images/Connect1.png��6$   $	��170��6D   5%      �� 5&      $��15 7 0 5q�6!   $��1q�63   
����  �$��Center=�6Z   $��Center=�6w   ._�5'      �� $��../Images/Connect2.png��6$   $	��210��6D   5(      5)      �� 6'   or �� 5+      $
��Bold"�6!   .�63   open an existing project6M   �� 6R   �� 5-      $
 
Horizontal=�6%   $	��420��6>   $	��255��6J   .+�5.      �� $��0 7 15 5q�6!   $��1q�63   
����  �$��Center=�6Z   $��Center=�6w   ._�5/      �� $ ��../Images/OpenProject1.png��6$   $	��170��6H   50      �� 51      $��15 7 0 5q�6!   $��1q�63   
����  �$��Center=�6Z   $��Center=�6w   ._�52      �� $ ��../Images/OpenProject2.png��6$   $	��210��6H   53      54      �� 6'   To �� 55      $
��Bold"�6!   .�63   
disconnect6?    from server you need:�� 6Z   �� 57      $
 
Horizontal=�6%   $	��420��6>   $	��255��6J   .+�58      �� $��0 7 15 5q�6!   $��1q�63   
����  �$��Center=�6Z   $��Center=�6w   ._�59      �� $��../Images/Disconnect1.png��6$   $	��170��6G   5:      �� 5;      $��15 7 0 5q�6!   $��1q�63   
����  �$��Center=�6Z   $��Center=�6w   ._�5<      �� $��../Images/Disconnect2.png��6$   $	��210��6G   5=      5>      �� 6'   To �� 5@      $
��Bold"�6!   .�63   save project6A   
 you need:�� 6P   �� 5A      $
 
Horizontal=�6%   $	��420��6>   $	��255��6J   .+�5B      �� $��0 7 15 5q�6!   $��1q�63   
����  �$��Center=�6Z   $��Center=�6w   ._�5C      �� $ ��../Images/SaveProject1.png��6$   $	��170��6H   5D      �� 5E      $��15 7 0 5q�6!   $��1q�63   
����  �$��Center=�6Z   $��Center=�6w   ._�5F      �� $ ��../Images/SaveProject2.png��6$   $	��210��6H   5G      5H      �� 6'   To �� 5J      $
��Bold"�6!   .�63   create sql script6F    you need open context menu�� 6f   )'on the item and select "Create SQL...":�� 5K   A   �� 5L      $
 
Horizontal=�6%   $	��410��6>   $	��165��6J   .+�5M      �� $��0 7 15 5q�6!   $��1q�63   
����  �$��Center=�6Z   $��Center=�6w   ._�5N      �� $!��../Images/ScriptCreate1.png��6$   $	��180��6I   5O      5P      �� 6'   To �� 5R      $
��Bold"�6!   .�63   save sql script6D   
 you need:�� 6S   �� 5S      $
 
Horizontal=�6%   $	��420��6>   $	��255��6J   .+�5T      �� $��0 7 15 5q�6!   $��1q�63   
����  �$��Center=�6Z   $��Center=�6w   ._�5U      �� $��../Images/SaveScript1.png��6$   $	��170��6G   5V      �� 5W      $��15 7 0 5q�6!   $��1q�63   
����  �$��Center=�6Z   $��Center=�6w   ._�5X      �� $��../Images/SaveScript2.png��6$   $	��210��6G   5Y      5Z      �� 6'   5[      5\      5]      5^      5_      5`      