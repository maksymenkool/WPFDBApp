   M S B A M L   `   `   `  ����C  ?WPFDBApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:WPFDBApp.ValueConverterWPFDBApp.ValueConverter  5      O,clr-namespace:WPFDBApp.Services.TreeServicesWPFDBApp.Services.TreeServices  5      4    .WPFDBApp.View.UserControls.TreeViewUserControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      >d2http://schemas.microsoft.com/expression/blend/2008    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      9valConv%clr-namespace:WPFDBApp.ValueConverter     5      @service,clr-namespace:WPFDBApp.Services.TreeServices     5      .��5
      ]� ��. .+�5   
   �� $	��Top=�6   ��5      �� 5        �� 
StartPoint$	  0,0&�6&    �� EndPoint$	 0,1&�67   .��5      �  � Color$ 	#33000000��6#    � Offset$ 0Q�65   � 5      $ 	#11000000��6#   $ 1Q�65   5      ._�5      �� $��Center=�6   $��4q�66    �� Orientation$ 
Horizontal=�6A   .+�5      �� $��Search for:��6   $��13(�6/   �� 5      -   5         ��Name$ searchTextBox��5      $	��6,0q�61   $	��150��5   V   ��6   �� *
SearchItem+ �� UpdateSourceTrigger$ PropertyChanged=�6V   �� 5      -   5        $��_Find��5      $	��8,0q�6%    �� 	IsDefault . ��5      �� *SearchCommand+��6;   ��  �� ElementName$ searchTextBox��	 �� Path$	 DataContext�5   D   5      5      R� 5   
   $��0q�5      $
��5 10q�6'   ��5      �� *Items+��5      �� 5      
 �� 
TargetType"
 Q�6   .�5      ��  �� Property �@Q�
IsExpanded �� Value 63   �� *
IsExpanded+ �� Mode$ TwoWay=��� 5        �@Q�
IsSelected 63   �� *
IsSelected+$ TwoWay=��� 5!       � ��$ Normal"�63   �� 5"       � ��$ 12(�61   �� 5$      9   3WPFDBApp.Services.TreeServices.TreeViewItemBehavior% �@ IsBroughtIntoViewWhenSelected . 6c    �� Triggers 5&      P� 5'       P� Property �@Q�
IsSelected P� Value . . �5(      ��  � ��$
 Bold"�6;   �� 5)       � ��$ 14(�69   �� 5*       �@+�FocusedElement 6L   ��  �� RelativeSource �� *Self+5+      5-      ��5/      �� 50       �� ItemsSource 6+   �� *
Children+.R�51      �� $ 
Horizontal=�6!   .+�52      �� $��16��6    $��16��6+   $��3q�67   ��53       �� *Type+4   .WPFDBApp.ValueConverter.TreeViewImageConverter   Instance �� 	Converter# Z �� 55       ��Name$ txtName��6$   $��Center=�65   ��6P   �� *Name+ � ContextMenu 56      �� 57   "   .@�58   &   m� $��Create SQL . . .��6/    m� Command 6I   �� *CreateSQLCommand+�� 59   &   m� 5:   &   $��Refresh��6/    6@   �� *RefreshCommand+5;   #   5=      �� 5>      $��Center=�6$   $�� (expanding...)��6?   y�5?      �� $	 IsBusy�$ OneWay=�$ PropertyChanged=�:   4WPFDBApp.ValueConverter.BooleanToVisibilityConverter   	IsVisible# Z 5@       �� Triggers 5A      P� 5B       �@J�IsMouseOver . . �5C      ��  �� 
TargetName$ txtName��6%    � ��$
 Bold"�6P   �� 5D      $ txtName��6%    � ��$ 14(�6N   5E      5G      5I      5J      5K      