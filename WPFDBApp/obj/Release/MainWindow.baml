   M S B A M L   `   `   `  ���� c  _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  C ?WPFDBApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:WPFDBApp.Services.AvalonEditBehaviors%WPFDBApp.Services.AvalonEditBehaviors A%clr-namespace:WPFDBApp.ValueConverterWPFDBApp.ValueConverter G(clr-namespace:WPFDBApp.View.UserControlsWPFDBApp.View.UserControls    WPFDBApp.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    >d2http://schemas.microsoft.com/expression/blend/2008    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     OavalonBehaviors3clr-namespace:WPFDBApp.Services.AvalonEditBehaviors     >valConverter%clr-namespace:WPFDBApp.ValueConverter     @UserControl(clr-namespace:WPFDBApp.View.UserControls     -     � Title$  Data Base Viewer��$	��525��$	��850�� � Icon$ ./Images/dbs.png�� � 	Resources    iconNew  iconSaveProjectAs  iconOpen  iconSave  
iconSaveAs  iconConnected  iconDisconnected  	iconClose  iconUndo 	 iconRedo 
 iconCut  iconCopy  	iconPaste  
iconDelete  iconFind  iconFont �� 
TargetType �� Property �� Value*  $System.Windows.Controls.DataGridCell  styleForDGCell  styleForDGCellLeft)  #System.Windows.Controls.DataGridRow  styleForDGRow��%�  &	       &	 6    &	 k    &	 �    &	 �    &	 �    &	 ,   &	 d    &	 �   &		 �   &	
 �   &	 (   &	 W   &	 �   &	 �   &	 �   'm�     &	 8    &	 x    &	 �    �� $��Images/NewDocumentHS.png��$��16��$��16���� $��Images/Save_project.png��$��16��$��16���� $��Images/OpenHS.png��$��16��$��16���� $��Images/Save.png��$��16��$��16���� $��Images/Save_as.png��$��16��$��16���� $��Images/connected-24.png��$��16��$��16���� $ ��Images/disconnected-24.png��$��16��$��16���� $��Images/Close.png��$��16��$��16���� $��Images/Edit_UndoHS.png��$��16��$��16���� $��Images/Edit_RedoHS.png��$��16��$��16���� $��Images/CutHS.png��$��16��$��16���� $��Images/CopyHS.png��$��16��$��16���� $��Images/PasteHS.png��$��16��$��16���� $��Images/DeleteHS.png��$��16��$��16���� $��Images/FindHS.png��$��16��$��16���� $��Images/FontHS.png��$��16��$��16���� " m�.���  � ��$ 5q��� "  .���  � ��$ 1,0,0,0q���  � ��
 �   ��� "  .���  � ��$ 0q���  � ��
 �   ��� "  .���  � ��$ 0,0,0,1q���  � ��
 �   �.��]� ��. .+�o� $	��Top=�.@�m� $��_File��.@�m� $��_Open Project...�� � ToolTip$ Open existing progect�� m� InputGestureText$ Ctrl+O�� m� Icon# [ 	 m� Command	 �� *OpenCommand+m� $��Save Project As...��$ Save project as...��# [ 	 �� *SaveProjectAsCommand+
 m� CommandParameter
 `� 3  -WPFDBApp.ValueConverter.MultiBindingConverter   Instance `� 	Converter# Z .1���  �� ElementName$ 
TreeViewUC�� �� Path$ TreeRootNode��� $ 
TreeViewUC��$ TreeViewRootItem�m� $��Save SQL Script _As...��$ Save sql script as...��# [ 	 �� *SaveAsCommand+ m� CommandTarget �� $ MyAvalonEdit��$ 
ScriptText�
 �� $ MyAvalonEdit��$ 
ScriptText��� m� $��Connect��$ Connect to the server��# [ 	 �� *ShowConnectWindowCommand+m� $��
Disconnect��$  Disconnect from the server��# [ 	 �� *DisconnectServerCommand+�� m� $��E_xit��# [ 	 �� *ExitCommand+m� $��_Edit��.@�m� $��_Undo��$ Ctrl+Z��# [ 	 �� *UndoCommand+ �� $ MyAvalonEdit��$ 
ScriptText�m� $��_Redo��$ Ctrl+Y��# [	 	 �� *RedoCommand+ �� $ MyAvalonEdit��$ 
ScriptText��� m� $
��Cu_t��$ Ctrl+X��# [
 	 �� *
CutCommand+ �� $ MyAvalonEdit��$ 
ScriptText�m� $��_Copy��$ Ctrl+C��# [ 	 �� *CopyCommand+ �� $ MyAvalonEdit��$ 
ScriptText�m� $��_Paste��$ Ctrl+V��# [ 	 �� *PasteCommand+ �� $ MyAvalonEdit��$ 
ScriptText�m� $��_Help��.@�m� $��_About��	 �� *HelpCommand+m� $��_Exit��	 �� *ExitCommand+`� $	��Top=�.�c�   �� Triggers �� Binding �� RelativeSource �� AncestorType �� AncestorLevel �� Value��%y   '��       �� " �� ��  ��  �� " ��$ 1��$ 	IsEnabled�	 False.a���  �@J�Opacity$
 0.30Q�.@��� $��23��$ Connect to the server��#��[ ���� *ShowConnectWindowCommand+�� $��23��$  Disconnect from the server��#��[ ���� *DisconnectServerCommand+�� �� $��23��$ Open existing progect��#��[ ���� *OpenCommand+�� $��23��$ Save project as...��#��[ ���� *SaveProjectAsCommand+��`� # Z .1��� $ 
TreeViewUC��$ TreeRootNode��� $ 
TreeViewUC��$ TreeViewRootItem��� $��23��$ Save sql script as...��#��[ ���� *SaveAsCommand+���� $ MyAvalonEdit��$ 
ScriptText����� $ 
TreeViewUC��$ 	SQLScript��� �� $��23��#��[  +� IsFocusScope . ���� *UndoCommand+���� $ MyAvalonEdit��$ 
ScriptText��� $��23��#��[	  . ���� *RedoCommand+���� $ MyAvalonEdit��$ 
ScriptText��� �� $��23��#��[
  . ���� *
CutCommand+���� $ MyAvalonEdit��$ 
ScriptText��� $��23��#��[  . ���� *CopyCommand+���� $ MyAvalonEdit��$ 
ScriptText��� $��23��#��[  . ���� *PasteCommand+���� $ MyAvalonEdit��$ 
ScriptText��� $��Bottom=�
�������$
��Bold"�$��22��$��0 3 0 0q����� *StatusColor+.@��� .���� ���� *Status+�  � ColumnDefinitions �� $
��0.4* �$	��300���� $��5 ��� $��* �$	��200���� $��5 ��� $��* �$	��150��$	��400��.+�4  .WPFDBApp.View.UserControls.TreeViewUserControl  -     Name$ 
TreeViewUC��$��0��   TreeSelectedItem �� *SelectedItem_+ �� UpdateSourceTrigger$ PropertyChanged=�   	SQLScript �� *	SqlScript+ �� Mode$ TwoWay=�$ PropertyChanged=�   TreeViewRootItem �� *Items+$ PropertyChanged=�   TreeRootNode �� *
RootNode+$ PropertyChanged=��� $��1��$��7��$��Stretch=�
����İ� J� Opacity$	 0.2Q�]� -     ]�Name$  SqlScriptContainer����. $��2��.+�] YICSharpCode.AvalonEdit, Version=5.0.3.0, Culture=neutral, PublicKeyToken=9cc39be672370310'  !ICSharpCode.AvalonEdit.TextEditor  ?
avalonEdit.http://icsharpcode.net/sharpdevelop/avalonedit  -   $��MyAvalonEdit��$��Consolas)�$
��10pt(�!   ShowLineNumbers! . "   LineNumbersForeground" �	#FF2B91AF���� $ SqlScriptContainer��$ ActualWidth�#   Text.# .   (System.Windows.Interactivity.Interaction$   	Behaviors$ ?  9WPFDBApp.Services.AvalonEditBehaviors.AvalonEditBehaviour  %   
ScriptText% �� $ 
TreeViewUC��$ 	SQLScript�$ TwoWay=�$ PropertyChanged=�.# �� $��3��$��7��$��Stretch=�
����İ�$	 0.2Q�]� ��. $��4��.+��� $�� Properties��$	��Top=�&   System.Windows.Controls.DataGrid  $��Stretch=�$��Stretch=�&   AutoGenerateColumns& .  '   HeadersVisibility$' Column=�$��0q�(   RowStyle#( [ )   	CellStyle#) [ ���� $ 
TreeViewUC��$. (TreeSelectedItem.Element.Data.Attributes�4	  .WPFDBApp.ValueConverter.DataGridItemsConverter* 	  Instance+ �� 	Converter#+ Z* $ PropertyChanged=�,   Columns, 0
  *System.Windows.Controls.DataGridTextColumn
  ,  &System.Windows.Controls.DataGridColumn-   Header$- 
 parameter��.   Width5  /System.Windows.Controls.DataGridLengthConverter$. * /   
IsReadOnly/ . 0   	CellStyle#0 [ 1  +System.Windows.Controls.DataGridBoundColumn1   Binding1 �� *Key+
  $-  value��$. * / . 1 �� *Value+