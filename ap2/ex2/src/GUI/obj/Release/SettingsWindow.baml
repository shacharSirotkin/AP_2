   M S B A M L   `   `   `  ���� >  :GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GUIGUI      GUI.SettingsWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     #localclr-namespace:GUI     -     � Title$  Settings��$	��300��$	��308��.��� $��10q� � ColumnDefinitions �� $
��Auto ��� $��* � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �$��37���� $
��Auto �.+��� $
��Left=�$��	10,10,0,0q�$
��Wrap=�$	��Top=�$��16��$��90���� $
��Bold"�$�Communication���� $
��Left=�$��	10,35,0,0q�$
��Wrap=�$��IP��$	��Top=�$��0��$��0��$��16��$��10���� -   $��IP��$��	10,32,0,0q� �� TextWrapping$
 Wrap=�$��TextBox��$	��Top=�$��0��$��1��$��22���� $
��Left=�$��	10,66,0,0q�$
��Wrap=�$
��Port��$	��Top=�$��16��$��22���� -   $
��Port��$��	10,66,0,0q�$
 Wrap=�$��TextBox��$	��Top=�$��1��$��22���� $
��Left=�$��	10,96,0,0q�$
��Wrap=�$	��Top=�$��16��$	��123���� $
��Bold"�$�Default maze settings���� $
��Left=�$��
10,126,0,0q�$
��Wrap=�$	��Top=�$��16��$��73���� $�Rows ���� $�number���� -   $��DefaultRows��$
 Wrap=�$��TextBox��$	��Top=�$��
10,123,0,0q�$��1��$��22���� $
��Left=�$��
10,157,0,0q�$
��Wrap=�$	��Top=�$��16��$��91���� $�Columns ���� $�number���� -   $��DefaultColumns��$��
10,154,0,0q�$
 Wrap=�$��TextBox��$	��Top=�$��1��$��22���� $
��Left=�$��
10,191,0,0q�$
��Wrap=�$��Default search algorithm��$	��Top=�$��16��$	��130���� -    ��Name$ DefaultAlgorithm��$	��Top=�$��10,188,29,0q� �� SelectedIndex$ 0��$��1��$��Stretch=�$��22��.@��� $	��BFS��$
��Left=��� $	��DFS��$
��Left=��� -   $��OK��$��Center=�$��6��$��0��$��Center=�$��70��$��22��$
��30,8q��� -   $��Cancel��$��Center=�$��6��$��1��$��Center=�$��70��$��22��$
��30,8q�