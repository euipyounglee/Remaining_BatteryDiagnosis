   M S B A M L   `   `   `  ����I  EBatteryControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:BatteryControlBatteryControl  5	      9!clr-namespace:BatteryControl.ViewBatteryControl.View  5
          BatteryControl.MainWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      S OPrism.Wpf, Version=8.0.0.1909, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59$prismhttp://prismlibrary.com/  5      (localclr-namespace:BatteryControl  5	      ,view!clr-namespace:BatteryControl.View  5
      -   5          � WindowState$  	Maximized=�5      ���#ededed � WindowStyle$
 None=�5       � UseLayoutRounding .  J� SnapsToDevicePixels .  � Title$) #배터리 팩 성능진단시스템��5      $
��1024��6   $
��1280��6)   $
��1280��66   $	��768��6F   '  !System.Windows.Shell.WindowChrome   WindowChrome 5        5         CaptionHeight$ 0Q�6      ResizeBorderThickness$ 5q�6#    � DataContext 5      "   BatteryControl.MainViewModel  5      *  $Microsoft.Xaml.Behaviors.Interaction	   Triggers	 5      +  %Microsoft.Xaml.Behaviors.EventTrigger  5      
   	EventName$
 Loaded��6   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5      -  'Prism.Interactivity.InvokeCommandAction     Command 6   ��  �� Path$ LoadedCommand�5        5      $
 Unloaded��6   . 5         6   �� $ UnloadedCommand�5       .��5#      �  � RowDefinitions 5$      �� 5%      $
��Auto �6   �� 5&      .+�5)      �� -   5        ���#ededed._�5*      � .+�5+      �� $��1280px��6   $
��94px��6   $
��None=�6)    �� StretchDirection$ DownOnly=�68    .  . $��Resources/bg_header.png��6�   � 5,      $	��500��6   ���#ededed$��	380,0,0,0q�6,   .+�5-      �� $��28(�64   
��� ���$
��Bold"�6Z   $��0,26,0,0q�6l   $
��Left=�6~   $	��Top=�6�   ��6   �� $ DisplayTitle�6�   5.      �� 50      $��Right=�6   $	��Top=�6)   ���#ededed$��30��6V   $��30��6a   $��	0,10,10,0q�6m    �� Padding$ 5q�6�    � Cursor$
 Hand��6�    J� InputBindings 51      d� 52       d� MouseAction$ 	LeftClicke�6    �� Command 6-   �� $ ProgramExitCommand� �� CommandParameter 6Y   ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" �._�54      �  J� RenderTransformOrigin$ 0.5 0.5&�6   z�55      �� 56   	    �� Angle$ 45Q�6   .+�58      �� $��3��6   ���#8888886   �� 59      $��3��6   ���#8888886   5:      5;      5<      5=      �� 5?      $��1��6   ��6    �� ��5@      �� 5A       �� 
TargetType" ��6   .�5B      ��  �� Property � �� �� Value 5C      �� 5D   	   .R�5E   
   #   BatteryControl.View.LoginView  5F   
   5H       �� Triggers 5I      �� 5J       �� Binding 6   �� $ CurrentWorkspaceType�  �� Value  Main6B   .a�5K   	   ��  � �� 5L   
   �� 5M      .R�5N      '   !BatteryControl.View.WorkspaceView  5O      5Q   
   5R   	   5T      5V      5W      5Y      