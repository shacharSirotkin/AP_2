   M S B A M L   `   `   `  ���� >  :GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GUIGUI  +clr-namespace:GUI.ControlsGUI.Controls      GUI.MultiplayerWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     #localclr-namespace:GUI     /controlsclr-namespace:GUI.Controls     -     � Title$  MultiPlayerMenu��$	��401��$	��304��.��� $��0,0,0,0q� � RowDefinitions �� $	��64* ��� $
��205* �.+��� $��Multi player��$��100,10,92,0q�$��16(�$	��Top=�$
��Bold"�$��	Underline{��� $��Join to game��$��100,39,61,0q�$��16(�$	��Top=��� $��List of Games:��$
��Auto��$
��Auto��$��16(�$��19,79,165,260q�$��2���� -    ��Name$ comboBox��$y�Visible=�$
��Auto��$
��Auto��
�������$��127,76,34,267q�$��2������ *Games+ �� SelectedItem �� *SelectedMaze+�� -   $��
JoinButton��$��100,22,92,229q�$
��Join��$��Stretch=�$��Stretch=�$
��Auto��$
��Auto��$��1���� $��Start a new game��$��
88,60,73,0q�$��16(�$	��Top=�$��1��)   #GUI.Controls.MazeDetailsUserControl  -     Name$ MazeDetails��)   #GUI.Controls.MazeDetailsUserControl   Loaded$# MazeDetailsUserControl_Loaded��$��
10,22,24,8q�$��1��   Rows �� *
MazeRows+   Cols �� *
MazeCols+   MazeNameProp �� *
MazeName+�� 
���   �$��1q�$
��Left=�$��84��$��	10,65,0,0q�$	��Top=�$	��270��$��2���� 
���   �$��1q�$
��Left=�$	��188��$��	10,84,0,0q�$��1��$	��Top=�$	��270���� -   $��StartButton��$��16(�$��95,217,97,23q�$��Stretch=�$��Stretch=�$
��Auto��$
��Auto��$��1������ *MazeButtonContent+	 �� UpdateSourceTrigger$	 PropertyChanged=�
 �� Mode$
 OneWay=�