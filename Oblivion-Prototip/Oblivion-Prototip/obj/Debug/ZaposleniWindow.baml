   M S B A M L   `   `   `  ����L  HOblivion-Prototip, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Oblivion_PrototipOblivion_Prototip  5   	   E ADragablz, Version=0.0.3.223, Culture=neutral, PublicKeyToken=null5(clr-namespace:Dragablz;assembly=DragablzDragablz 5   	   d `DotNetProjects.Input.Toolkit, Version=6.0.90.0, Culture=neutral, PublicKeyToken=79778c5f2eed289bgKclr-namespace:System.Windows.Controls;assembly=DotNetProjects.Input.ToolkitSystem.Windows.Controls 5	   	   '    !Oblivion_Prototip.ZaposleniWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Oblivion_Prototip     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   7dragablz(clr-namespace:Dragablz;assembly=Dragablz  5   	   ZcontrolsKclr-namespace:System.Windows.Controls;assembly=DotNetProjects.Input.Toolkit  5	   	   -   5          � Title$  	Zaposleni��5   	   $	��700��6   $
��1250��6(   $	��700��65   $
��1250��6E    � WindowStyle$
 None=�6U    � WindowState$ 	Maximized=�6h      MaterialDesignBody#���   5   
   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	   ���	#FF01010C$��Zen Tokyo Zoo)�5   	   $
��Bold"�6$   $��15(�66   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $	��12* �6    � ColumnDefinitions 5   
   �� 5      $��70 �6   �� 5      $��* �6   �� 5      $��70 �6   �� 5      $��70 �6   .+�5!   
   �� -   5        $��btnIgraonica��5!      $��0��6;   $
��5,10q�6K   ��6%   Z� .��5"      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ GoogleController=�6&   $��22��6>   $��28��6J    � ToolTip 5#      _� 5$      ���	#FF251149.��5%      �� $��Podaci o igraonici��6   
������ 6:   5&      5(      �� 5)   
   -   5        $��btnPromjena��5)      $��2��6:   $
��5,10q�6J   ��6$   Z� .��5*        $ 
AccountCog=�6&    5+      _� 5,      ���	#FF251149.��5-      �� $��Izmjena ličnih podataka��6   
������ 6?   5.      50      �� 51   
   -   5        $��	btnOdjava��51      $��3��68   $
��5,10q�6H   ��6"   Z� .��52        $ Logout=�6&    53      _� 54      ���	#FF251149.��55      �� $��Odjava sa sistema��6   
������ 69   56      58        Dragablz.TabablzControl  59   
   $��1��6"   $��4��6/   	   InterTabController	 5:      !  Dragablz.InterTabController  5;      .@�5=      �� ��5>      �� 5?      
 �� Orientation$
 
Horizontal=�6!   .+�5@        $ DesktopClassic=�6.   
�������6D   �� 5A      $�� POČETNA STRANICA  ��6$   
�������6?   5B      .��5D      � -   5         �Name$ mreza��5D       5E      �� 5F      $
��424* �6+   �� 5G      $
��187* �6+   .+�5I      �� $��10q�6#   $��1��6/   $��0��6=   $
��Auto=�6M   .��5J      � -   5         �Name$ wrapRacunari��5J       5K      �� 5L      -   5        $��spPodaci��5L   !   
���iii�$��1��6G   $	��800��6W   5M      5N      �� 5O      ��5P      �� 5Q      $
 
Horizontal=�6!   .+�5R        $ GamepadVariantOutline=�62   
�������6O   �� 5S      $��	 IGRICE  ��6$   
�������65   5T      .��5V      �  5W      �� 5X      $��30 �6(   �� 5Y      $��* �6(    5[      �� 5\      $��4* �6+   �� 5]      $��* �6+   .+�5_      � $��Center=�6   $��1��67    5`      �� 5a      $��2* �6/   �� 5b      $��6* �6/   .+�5d      �� -   5        $��	btnSearch��5d   !   
���iii� �� 	IsDefault . .��5e        $ Magnify=�66   5f      -  'System.Windows.Controls.AutoCompleteBox  5g      -	   5        $��acSearchBox��5g   C   $��1��63   
���iii�
�������$��Center=�6�   6�   5h      �� 5i      $��10q�6#   $��1��6/   $��2��6=   $
��Auto=�6Q   .��5j      � -
   5        $ 
wrapIgrice��5j   $   5k      5l      5m      5n      5p      5q      