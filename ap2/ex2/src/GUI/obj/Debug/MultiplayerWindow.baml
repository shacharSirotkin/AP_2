   M S B A M L   `   `   `  ����>  :GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GUIGUI  5   	   +clr-namespace:GUI.ControlsGUI.Controls  5   	       GUI.MultiplayerWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:GUI     5   	   /controlsclr-namespace:GUI.Controls     5   	   -   5          � Title$  MultiPlayerMenu��5	   	   $	��401��6!   $	��304��6.   .��5      � $��0,0,0,0q�6    � RowDefinitions 5   
   �� 5      $	��64* �6   �� 5      $
��205* �6   .+�5   
   �� $��Multi player��6   $��100,10,92,0q�6(   $��16(�6=   $	��Top=�6K   $
��Bold"�6c   $��	Underline{�6u   �� 5   
   $��Join to game��6   $��100,39,61,0q�6(   $��16(�6=   $	��Top=�6K   �� 5   
   $��List of Games:��6   $
��Auto��6+   $
��Auto��69   $��16(�6F   $��19,79,165,260q�6T   $��2��6k   �� 5   
   -   5         ��Name$ comboBox��5      $y�Visible=�6%   $
��Auto��6c   $
��Auto��6q   
�������$��127,76,34,267q�6�   $��2��6�   ��6�   �� *Games+ �� SelectedItem 6�   �� *SelectedMaze+6�   �� 5   
   -   5        $��
JoinButton��5      $��100,22,92,229q�6#   $
��Join��6:   $��Stretch=�6I   $��Stretch=�6e   $
��Auto��6�   $
��Auto��6�   $��1��6�   6�   �� 5   
   $��Start a new game��6   $��
88,60,73,0q�6,   $��16(�6@   $	��Top=�6N   $��1��6f   )   #GUI.Controls.MazeDetailsUserControl  5   
   -   5          Name$ MazeDetails��5   *   )   #GUI.Controls.MazeDetailsUserControl   Loaded$# MazeDetailsUserControl_Loaded��6�   $��
10,22,24,8q�6?   $��1��6S      Rows 6`   �� *
MazeRows+   Cols 6z   �� *
MazeCols+   MazeNameProp 6�   �� *
MazeName+6�   �� 5   
   
���   �$��1q�6%   $
��Left=�69   $��84��6T   $��	10,65,0,0q�6`   $	��Top=�6s   $	��270��6�   $��2��6�   �� 5   
   
���   �$��1q�6%   $
��Left=�69   $	��188��6T   $��	10,84,0,0q�6a   $��1��6t   $	��Top=�6�   $	��270��6�   �� 5   
   -   5        $��StartButton��5      $��16(�6   $��95,217,97,23q�6�   $��Stretch=�6�   $��Stretch=�6�   $
��Auto��6�   $
��Auto��6�   $��1��6�   ��6%   �� *MazeButtonContent+	 �� UpdateSourceTrigger$	 PropertyChanged=�
 �� Mode$
 OneWay=�6  5      5      