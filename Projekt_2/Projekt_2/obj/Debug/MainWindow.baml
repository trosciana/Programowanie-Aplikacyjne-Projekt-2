   M S B A M L   `   `   `  ����D  @Projekt_2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Projekt_2	Projekt_2  5   	       Projekt_2.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Projekt_2     5   	     � Title$  
MainWindow��5   	   $	��444��6   $	��280��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��310* �6   �� 5      $
��183* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $
��dane��5   >   $��	0,111,0,0q�6+      AutoGenerateColumns .  $��2��6f   ��6   ��    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ danie��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 64   �� *danie+  5      $ kalorie��6%    66   �� *	kalorie+  5      $ ilosc��6%    64   �� *ilosc+4  .System.Windows.Controls.DataGridTemplateColumn  5         CellTemplate 5      �� 5      .R�5      �� -   5         � DataContext 5   8   �� *danie+.��6V   +6Y   5      5        5       5      �� 5      .R�5      �� -   5         5   :   �� *danie+.��6X   -6[   5      5         5!      $
 Suma��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 110 6S    63   �� *SumaKalorii+6S   5#      �� 5$   
   -   5        $��guzik��5$   .   $��DODAJ NOWY ELEMENT��6   $
��Left=�6;   $��	73,10,0,0q�6V   $	��Top=�6i   $��2��6�   �� 5%   
   $
��Left=�6   $��	73,55,0,0q�6/   $
��Wrap=�6B   $��Całościowa Suma Kalorii��6V   $	��Top=�6u   $	��134��6�   $��2��6�   	 �� TextAlignment$	 Center=�6�   �� 5&   
   -   5        $��SumaSuma��5&   �   $��2��6   $
��Left=�6&   $��	80,76,0,0q�6A   
 �� TextWrapping$

 Wrap=�6T   $��TextBox��6h   $	��Top=�6w   $	��120��6�    �� TextAlignment$ Center=�6�    �� 
IsReadOnly . 6�   5'      5(      