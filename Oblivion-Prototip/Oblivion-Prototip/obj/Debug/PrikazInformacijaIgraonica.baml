   M S B A M L   `   `   `  ����L  HOblivion-Prototip, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Oblivion_PrototipOblivion_Prototip  5   	   2    ,Oblivion_Prototip.PrikazInformacijaIgraonica   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Oblivion_Prototip     5   	     � Title$   PrikazInformacijaIgraonica��5   	   $	��450��6,   $	��400��69    � WindowStartupLocation$ CenterScreen=�6E    � 
ResizeMode$ NoResize=�6j    � WindowStyle$
 None=�6�   
���iii�$��Zen Tokyo Zoo)�5	   	   $
��Bold"�6$   $��15(�66   .��5
      �� ���#353935$��2q�6$    �� CornerRadius$ 2��68   $��10q�6I   ._�5   
   �  � RowDefinitions 5      �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6     � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   �� 5      $
��0.2* �6#   .+�5      �� $��2��6   ��5      �� 5       �� EndPoint$ 0.5,1&�6*    �� 
StartPoint$ 0.5,0&�6;   .��5      � 	 � Color$	 White��6'   
 � Offset$
 0Q�65   � 5       $	 	#FF673AB7��6'   $
 0.627Q�69   5!      .+�5#      �� -   5        $��lblIzlaz��5#      $��X��6(   $��Right=�64   $	��Top=�6P   $��26(�6j   6y   5$      �� 5%      -   5        $��	lblNaslov��5%      $��Podaci o igraonici��6%   $��2��6B   $��Center=�6V   $��Center=�6z   $��27(�6�   $
��Bold"�6�   ��5&      �� 5'      $ 0.5,1&�6*   $ 0.5,0&�6;   .��5(      � $	 White��6'   $
 0Q�65   � 5)      $	 	#FF673AB7��6'   $
 0.627Q�69   5*      5,      �� 5-      
�������$��1��6+   $��Right=�68   $��WrapWithOverflow=�6T   $��Naziv igraonice:��5.      $��0,0,15,0q�6,   $��Center=�5/      �� 50      -   5        $��tbNaziv��50      
�������$��1��6:   $��1��6G   $
��Left=�6W   $��WrapWithOverflow=�6r   $��Naziv��51      $��15,0,0,0q�6!   $��Center=�52      $��2��6/   �� 53      
�������$��2��6+   $��Right=�68   $��WrapWithOverflow=�6T   $��Vlasnik:��54      $��0,0,15,0q�6$   $��Center=�55      �� 56      -   5        $��	tbVlasnik��56      
�������$��2��6<   $��1��6I   $
��Left=�6Y   $��WrapWithOverflow=�6t   $��Vlasnik��57      $��15,0,0,0q�6#   $��Center=�58      $��2��6/   �� 59      
�������$��3��6+   $��Right=�68   $��WrapWithOverflow=�6T   $��Adresa:��5:      $��0,0,15,0q�6#   $��Center=�5;      �� 5<      -   5        $��tbAdresa��5<      
�������$��3��6;   $��1��6H   $
��Left=�6X   $��WrapWithOverflow=�6s   $��Adresa��5=      $��15,0,0,0q�6"   $��Center=�5>      $��2��6/   �� 5?      
�������$��4��6+   $��Right=�68   $��WrapWithOverflow=�6T   $��Broj telefona:��5@      $��0,0,15,0q�6*   $��Center=�5A      �� 5B      -   5        $��	tbBrojTel��5B      
�������$��4��6<   $��1��6I   $
��Left=�6Y   $��WrapWithOverflow=�6t   $��Broj telefona��5C      $��15,0,0,0q�6)   $��Center=�5D      $��2��6/   �� 5E      
�������$��5��6+   $��Right=�68   $��WrapWithOverflow=�6T   $��Broj radnika:��5F      $��0,0,15,0q�6)   $��Center=�5G      �� 5H      -   5        $��tbBrojRadnika��5H      
�������$��5��6@   $��1��6M   $
��Left=�6]   $��WrapWithOverflow=�6x   $��Broj radnika��5I      $��15,0,0,0q�6(   $��Center=�5J      $��2��6/   �� 5K      
�������$��6��6+   $��Right=�68   $��WrapWithOverflow=�6T   $��Broj računara:��5L      $��0,0,15,0q�6*   $��Center=�5M      �� 5N      -   5        $��tbBrojRačunara��5N      
�������$��6��6A   $��1��6N   $
��Left=�6^   $��WrapWithOverflow=�6y   $��Broj računara��5O      $��15,0,0,0q�6)   $��Center=�5P      $��2��6/   �� 5Q      
�������$��7��6+   $��Right=�68   $��WrapWithOverflow=�6T   $ ��Broj raspoloživih igrica:��5R      $��0,0,15,0q�65   $��Center=�5S      �� 5T      -	   5        $��tbBrojIgrica��5T      
�������$��7��6?   $��1��6L   $
��Left=�6\   $��WrapWithOverflow=�6w   $��Broj igrica��5U      $��15,0,0,0q�6'   $��Center=�5V      $��2��6/   5W      5X      5Y      