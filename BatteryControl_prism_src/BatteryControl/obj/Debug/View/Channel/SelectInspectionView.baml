   M S B A M L   `   `   `  ����I  EBatteryControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:BatteryControl.View.ChannelBatteryControl.View.Channel  5      D @SharedLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullH2clr-namespace:SharedLib.Defines;assembly=SharedLibSharedLib.Defines 5      6    0BatteryControl.View.Channel.SelectInspectionView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:BatteryControl.View.Channel     5      Edefine2clr-namespace:SharedLib.Defines;assembly=SharedLib     5      .��5      �   � RowDefinitions  5      �� 5      $��23 �6   �� 5      $��18 �6   �� 5      $��1 �6   �� 5      $��12 �6   �� 5      $
��Auto �6   �� 5      �� 5      $��40 �6   .+�5      ��    PopupTitleTextStyle#��[  6   $��진단��6;   �� 5      $��2��6   ���#8686866   �� 5      $��4��6    �� Orientation$ 
Horizontal=�6   $��Right=�65   .+�5      �� #��[  6   $��12(�6<   $��Normal"�6J   $��배터리 정보 : ��6^   �� 5      #��[  6   $��12(�6<   $��Normal"�6J   ��6^   ��  �� Path$! SelectedBatteryInfo.Summary�5      � 5      $��5��6	    � ColumnDefinitions 5       �� 5!      �� 5"      $
��Auto �6   �� 5#      $��8 �6   �� 5$      $
��Auto �6   �� 5%      $��8 �6   �� 5&      $
��Auto �6   �� 5'      .+�5+      �� $��1��6   $	��174��6   $	��179��6'   $��0q�64    � Cursor$
 Hand��6@    J� InputBindings 5,      d� 5-       d� MouseAction$ 	LeftClicke�6    �� Command 6,   �� $  ChangeProgressStateCommand� �� CommandParameter 5.      *  $SharedLib.Defines.ProgressStateTypes 5/   	   SimpleInspection=�65   51      .��53      �� -   5        	 ��Name$
	 IMG1��53      $	��174��6   $	��179��6&   $D��>/BatteryControl;component/Resources/btn_diagnosis01_normal.png��63   54      �� 57      $��3��6   $	��174��6   $	��179��6(   $
 Hand��6i   ��65   �� $  ChangeProgressStateCommand���58      �� 59      
 �� 
TargetType"
 ��6   .�5:      ��  �� Property � �� �� Value$ 0q�6"   �� 5;       � ��$ Stretch=�65   �� 5<       � ��$ Stretch=�63   �� 5=       �@�Cursor$
 Hand��6!   �� 5>       � �� 5?   	   �� 5@   
    �� 
TargetType" ��6   .R�5A      �� $
	 IMG2��6   $	��174��6   $	��179��6+   $D��>/BatteryControl;component/Resources/btn_diagnosis02_normal.png��68    �� Triggers 5B      P� 5C       P� Property � �� P� Value . . �5D      ��  �� 
TargetName$
 IMG2��6    � ��$D >/BatteryControl;component/Resources/btn_diagnosis02_active.png��68   5E      5G      5I   	   5J      ��5L       5M      NormalInspection=�63   5O      �� 5R      $��5��6   $	��174��6   $	��179��6(   $
 Hand��6i   ��65   �� $  ChangeProgressStateCommand���5S      �� 5T      "
 ��6   .�5U      ��  � ��$ 0q�6"   �� 5V       � ��$ Stretch=�65   �� 5W       � ��$ Stretch=�63   �� 5X       �@�Cursor$
 Hand��6!   �� 5Y       � �� 5Z   	   �� 5[   
   " ��6   .R�5\      �� $
	 IMG3��6   $	��174��6   $	��179��6+   $D��>/BatteryControl;component/Resources/btn_diagnosis03_normal.png��68    5]      P� 5^       � �� . . �5_      �� $
 IMG3��6    � ��$D >/BatteryControl;component/Resources/btn_diagnosis03_active.png��68   5`      5b      5d   	   5e      ��5g       5h      CloseInspection=�62   5j      5k      �� 5m      $��6��6
     PopupClickableLabelStyle#��[ 6   $	��140��6I   ���#888888$��이전��6j    5n      d� 5o      $ 	LeftClicke�6    6+   �� $  ChangeProgressStateCommand� 5p       5q      ConnectBattery=�62   5s      5u      5v      5x      