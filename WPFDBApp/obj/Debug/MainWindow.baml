   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5   	   C ?WPFDBApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:WPFDBApp.Services.AvalonEditBehaviors%WPFDBApp.Services.AvalonEditBehaviors 5   	   A%clr-namespace:WPFDBApp.ValueConverterWPFDBApp.ValueConverter 5   	   G(clr-namespace:WPFDBApp.View.UserControlsWPFDBApp.View.UserControls 5	   	      WPFDBApp.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   >d2http://schemas.microsoft.com/expression/blend/2008    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5   	   OavalonBehaviors3clr-namespace:WPFDBApp.Services.AvalonEditBehaviors     5   	   >valConverter%clr-namespace:WPFDBApp.ValueConverter     5   	   @UserControl(clr-namespace:WPFDBApp.View.UserControls     5	   	   -   5          � Title$  Data Base Viewer��5   	   $	��525��6"   $	��850��62    � Icon$ ./Images/dbs.png��6A    � 	Resources 5         iconNew  iconSaveProjectAs  iconOpen  iconSave  
iconSaveAs  iconConnected  iconDisconnected  	iconClose  iconUndo 	 iconRedo 
 iconCut  iconCopy  	iconPaste  
iconDelete  iconFind  iconFont �� 
TargetType �� Property �� Value*  $System.Windows.Controls.DataGridCell  styleForDGCell  styleForDGCellLeft)  #System.Windows.Controls.DataGridRow  styleForDGRow��%�  &	       &	 N    &	 �    &	 �    &	 '   &	 o   &	 �   &	     &	 R   &		 �   &	
 �   &	 0   &	 w   &	 �   &	    &	 O   'm� �    &	 �    &	 P    &	 �    �� 5   
   $��Images/NewDocumentHS.png��61   $��16��6S   $��16��6^   �� 5   
   $��Images/Save_project.png��6;   $��16��6\   $��16��6g   �� 5   
   $��Images/OpenHS.png��62   $��16��6M   $��16��6X   �� 5   
   $��Images/Save.png��62   $��16��6K   $��16��6V   �� 5   
   $��Images/Save_as.png��64   $��16��6P   $��16��6[   �� 5   
   $��Images/connected-24.png��67   $��16��6X   $��16��6c   �� 5   
   $ ��Images/disconnected-24.png��6:   $��16��6^   $��16��6i   �� 5   
   $��Images/Close.png��6"   $��16��6<   $��16��6G   �� 5   
   $��Images/Edit_UndoHS.png��62   $��16��6R   $��16��6]   �� 5   
   $��Images/Edit_RedoHS.png��62   $��16��6R   $��16��6]   �� 5   
   $��Images/CutHS.png��61   $��16��6K   $��16��6V   �� 5   
   $��Images/CopyHS.png��62   $��16��6M   $��16��6X   �� 5   
   $��Images/PasteHS.png��63   $��16��6O   $��16��6Z   �� 5   
   $��Images/DeleteHS.png��64   $��16��6Q   $��16��6\   �� 5   
   $��Images/FindHS.png��62   $��16��6M   $��16��6X   �� 5   
   $��Images/FontHS.png��62   $��16��6M   $��16��6X   �� 5!   
   " m�6   .�5"      ��  � ��$ 5q�6'   5#      �� 5&   
   "  6   .�5'      ��  � ��$ 1,0,0,0q�60   �� 5(       � ��
 �   �6,   5)      �� 5*   
   "  6   .�5+      ��  � ��$ 0q�60   �� 5,       � ��
 �   �6,   5-      �� 5.   
   "  6   .�5/      ��  � ��$ 0,0,0,1q�60   �� 50       � ��
 �   �6,   51      53      .��54      ]� ��. .+�56   
   o� $	��Top=�6   .@�57      m� $��_File��6   .@�58      m� $��_Open Project...��6    � ToolTip$ Open existing progect��65    m� InputGestureText$ Ctrl+O��59   ;    m� Icon# [ 5:      	 m� Command	 59      �� *OpenCommand+5:      m� 5;      $��Save Project As...��6   $ Save project as...��67   # [ 5=      	 5<      �� *SaveProjectAsCommand+
 m� CommandParameter
 5>      `� 5?      3  -WPFDBApp.ValueConverter.MultiBindingConverter   Instance `� 	Converter# Z 6'   .1�5@      ��  �� ElementName$ 
TreeViewUC��6&    �� Path$ TreeRootNode�6?   �� 5A      $ 
TreeViewUC��6&   $ TreeViewRootItem�6?   5B      5D      m� 5E      $��Save SQL Script _As...��6   $ Save sql script as...��6;   # [ 5I      	 5F      �� *SaveAsCommand+ m� CommandTarget 5G      �� $ MyAvalonEdit��$ 
ScriptText�
 5H      �� $ 
TreeViewUC��$ 	SQLScript�5I      �� 5J      m� 5K      $��Connect��6   $ Connect to the server��6,   # [ 5M      	 5L      �� *ShowConnectWindowCommand+5M      m� 5N      $��
Disconnect��6   $  Disconnect from the server��6/   # [ 5P      	 5O      �� *DisconnectServerCommand+5P      �� 5Q      m� 5R      $��E_xit��6   # [ 5S      	 5R   *   �� *ExitCommand+5S      5T      m� 5U      $��_Edit��6   .@�5V      m� $��_Undo��6   $ Ctrl+Z��5W   Y   # [ 5X      	 5V   *   �� *UndoCommand+ 5W      �� $ MyAvalonEdit��$ 
ScriptText�5X      m� 5Y      $��_Redo��6   $ Ctrl+Y��5Z   Y   # [	 5[      	 5Y   *   �� *RedoCommand+ 5Z      �� $ MyAvalonEdit��$ 
ScriptText�5[      �� 5\      6   m� 5]      $
��Cu_t��6   $ Ctrl+X��6H   # [
 5^      	 5]   )   �� *
CutCommand+ 5^   5   �� $ MyAvalonEdit��$ 
ScriptText�m� 5_      $��_Copy��6   $ Ctrl+C��6J   # [ 5`      	 5_   *   �� *CopyCommand+ 5`   6   �� $ MyAvalonEdit��$ 
ScriptText�m� 5a      $��_Paste��6   $ Ctrl+V��6L   # [ 5b      	 5a   +   �� *PasteCommand+ 5b   7   �� $ MyAvalonEdit��$ 
ScriptText�5c      m� 5d      $��_Help��6   .@�5e      m� $��_About��6   	 6+   �� *HelpCommand+5f      m� 5g      $��_Exit��6   	 6&   �� *ExitCommand+5h      `� 5i   
   $	��Top=�6   .�5j      c�  5k       �� Triggers �� Binding �� RelativeSource �� AncestorType �� AncestorLevel �� Value��%�   '��       �� 5l      " ��6    5m      �� 5n       6*   ��  �� " ��$ 1��$ 	IsEnabled�	 False6�   .a�5o   "   ��  �@J�Opacity$
 0.30Q�6<   5p      5r      5s      .@�5t      �� $��23��6   $ Connect to the server��6$   #��[ 6D   ��5u      �� *ShowConnectWindowCommand+�� 5v      $��23��6   $  Disconnect from the server��6$   #��[ 6I   ��5w      �� *DisconnectServerCommand+�� 5x      �� 5y      $��23��6   $ Open existing progect��6$   #��[ 6D   ��5z      �� *OpenCommand+�� 5{      $��23��6   $ Save project as...��6$   #��[ 6A   ��5|      �� *SaveProjectAsCommand+��5}      `� 5~      # Z 6'   .1�5      �� $ 
TreeViewUC��6&   $ TreeRootNode�6?   �� 5�      $ 
TreeViewUC��6&   $ TreeViewRootItem�6?   5�      5�      �� 5�      $��23��6   $ Save sql script as...��6$   #��[ 6D   ��5�      �� *SaveAsCommand+��67   �� $ MyAvalonEdit��$ 
ScriptText���5�      �� $ 
TreeViewUC��$ 	SQLScript��� 5�      �� 5�      $��23��6   #��[ 6$    +� IsFocusScope . ��6H   �� *UndoCommand+��5�      �� $ MyAvalonEdit��$ 
ScriptText�6]   �� 5�      $��23��6   #��[	 6$    . ��6H   �� *RedoCommand+��5�      �� $ MyAvalonEdit��$ 
ScriptText�6]   �� 5�      �� 5�      $��23��6   #��[
 6$    . ��6G   �� *
CutCommand+��5�      �� $ MyAvalonEdit��$ 
ScriptText�6]   �� 5�      $��23��6   #��[ 6$    . ��6H   �� *CopyCommand+��5�      �� $ MyAvalonEdit��$ 
ScriptText�6]   �� 5�      $��23��6   #��[ 6$    . ��6I   �� *PasteCommand+��5�      �� $ MyAvalonEdit��$ 
ScriptText�6]   5�      5�      �� 5�   
   $��Bottom=�6   
�������$
��Bold"�6b   $��22��6u   $��0 3 0 0q�6�   ��6,   �� *StatusColor+.@�5�      �� .��5�      �� ��6   �� *Status+5�      5�      � 5�   
    � ColumnDefinitions 5�      �� 5�      $
��0.4* �6#   $	��300��60   �� 5�      $��5 �6#   �� 5�      $��* �6#   $	��200��6-   �� 5�      $��5 �6#   �� 5�      $��* �6#   $	��150��6-   $	��400��6<   .+�5�      4  .WPFDBApp.View.UserControls.TreeViewUserControl  -   5          Name$ 
TreeViewUC��5�   .   $��0��6B      TreeSelectedItem 5�      �� *SelectedItem_+ �� UpdateSourceTrigger$ PropertyChanged=�   	SQLScript 5�      �� *	SqlScript+ �� Mode$ TwoWay=�$ PropertyChanged=�   TreeViewRootItem 5�      �� *Items+$ PropertyChanged=�   TreeRootNode 5�      �� *
RootNode+$ PropertyChanged=��� 5�      $��1��6   $��7��6+   $��Stretch=�65   
����İ� J� Opacity$	 0.2Q�6p   ]� 5�      -   5          ]�Name$  SqlScriptContainer��5�   =   ��. $��2��6-   .+�5�      ] YICSharpCode.AvalonEdit, Version=5.0.3.0, Culture=neutral, PublicKeyToken=9cc39be672370310'  !ICSharpCode.AvalonEdit.TextEditor  ?
avalonEdit.http://icsharpcode.net/sharpdevelop/avalonedit  5�      -   5        $��MyAvalonEdit��5�      $��Consolas)�6)   $
��10pt(�6?   !   ShowLineNumbers! . "   LineNumbersForeground" �	#FF2B91AF��5�      �� $ SqlScriptContainer��$ ActualWidth�#   Text.# 5�   N   .   (System.Windows.Interactivity.Interaction$   	Behaviors$ 5�      ?  9WPFDBApp.Services.AvalonEditBehaviors.AvalonEditBehaviour  5�      %   
ScriptText% 6>   �� $ 
TreeViewUC��$ 	SQLScript�$ TwoWay=�$ PropertyChanged=�.# 5�   /   5�      5�      �� 5�      $��3��6   $��7��6+   $��Stretch=�65   
����İ�$	 0.2Q�6p   ]� 5�      ��. $��4��6-   .+�5�      �� $�� Properties��6   $	��Top=�6/   &   System.Windows.Controls.DataGrid  5�      $��Stretch=�6   $��Stretch=�69   &   AutoGenerateColumns& .  '   HeadersVisibility$' Column=�5�   1   $��0q�6L   (   RowStyle#( [ 5�      )   	CellStyle#) [ 5�      ��5�      �� $ 
TreeViewUC��$. (TreeSelectedItem.Element.Data.Attributes�4	  .WPFDBApp.ValueConverter.DataGridItemsConverter* 	  Instance+ �� 	Converter#+ Z* $ PropertyChanged=�,   Columns, 5�      0
  *System.Windows.Controls.DataGridTextColumn
  5�      ,  &System.Windows.Controls.DataGridColumn-   Header$- 
 parameter��6-   .   Width5  /System.Windows.Controls.DataGridLengthConverter$
. 0.7* 6Y   /   
IsReadOnly/ . 0   	CellStyle#0 [ 6x   1  +System.Windows.Controls.DataGridBoundColumn1   Binding1 6A   �� *Key+6x   
  5�      $-  value��6-   $. 1* 6W   / . 1 6=   �� *Value+6b   5�      5�      5�      5�      5�      